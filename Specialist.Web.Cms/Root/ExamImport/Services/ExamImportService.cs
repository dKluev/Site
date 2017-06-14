using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using PrometricGrabber;
using PrometricImport;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Examination.Const;
using Specialist.Web.Cms.Root.ExamImport.Logic;
using Tuple = System.Tuple;

namespace Specialist.Services.Examination {
	public class ExamImportService {

		public const string PioneerConnectionString =
			"data source=pioneer.specialist.ru;initial catalog=SPECIALIST;" +
				"user id=SpecWebDataEditor;password=ghjdbltw;MultipleActiveResultSets=True";

		public static bool IsStarted {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get; 
			[MethodImpl(MethodImplOptions.Synchronized)]
			set;
		}
		public static bool IsComplete {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get; 
			[MethodImpl(MethodImplOptions.Synchronized)]
			set;
		}

		public static bool IsCheckPrice {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get; 
			[MethodImpl(MethodImplOptions.Synchronized)]
			set;
		}
		public static System.Tuple<string,int> Status {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get; 
			[MethodImpl(MethodImplOptions.Synchronized)]
			set;
		}

		public void Import() {
			if(!IsStarted) {
				IsStarted = true;
				WriteStatus("Выгрузка начата", null, 0, 0);
				ImportVue();
				ImportPrometric();
				IsComplete = true;
				WriteStatus("Выгрузка завершена", null, 0, 0);
			}
		}

		public static void WriteFile(string path, string contents) {
			var fullPath = HostingEnvironment.MapPath("~/temp/examimport/" + path);
			File.WriteAllText(fullPath, contents);
		}
		public static string[] GetFiles(string path) {
			var fullPath = HostingEnvironment.MapPath("~/temp/examimport/" + path);
			return Directory.GetFiles(fullPath);
		}

		public static void WriteStatus(string text, string examType, int step, int count) {
			if(count == 0) {
				count = 1;
				step = -1;
			}
			examType = examType == null ? "" : "[{0}] ".FormatWith(examType);
			Status = Tuple.Create(examType + text , (int)100.0 * (step + 1)/count);

		}

		public static List<ExamPrice> UpdateExamPricesFromProviderPrices(bool submit = false) {
			var result = new List<ExamPrice>();
			var context = new SpecialistDataContext(PioneerConnectionString);
			var currency = context.Currencies
				.Where(c => c.CurrencyType_TC == "USD").OrderByDescending(c => c.FiscalDate)
				.First().Rate;
			var exams = context.Exams.Where(c => c.ProviderPrice != null && c.ProviderPrice > 0).ToList();
			var examCount = exams.Count();
			var i = 0;
			foreach (var exam in exams) {
				if(submit)
					WriteStatus("Обновление цен", null,  i,  examCount);
				var price = exam.ProviderPrice.Value*ProviderConst.NdsCoefficient *currency;
				if(exam.ExamType == Providers.VueExamType) {
					price = price*ProviderConst.VueCoefficient;
				}
				var newPrice = price%10 == 0
					? price
					: ((int) (price*(decimal) 0.1))*10 + 10;
				result.Add(new ExamPrice{Exam = exam, 
					Old = (int)exam.ExamPrice.GetValueOrDefault(), New = (int)newPrice});
				exam.ExamPrice = newPrice;
			}
			if(submit)
				context.SubmitChanges();
			return result;
		}


		public static void ImportVue() {
			var vendors = VueSpider.GetVendors().ToList();
			for (int i = 0; i < vendors.Count; i++) {
				var vendor = vendors[i];
				var vendorId = ProviderConst.VueVendorsIDList.GetValueOrDefault(vendor.Key);
				if (vendorId == 0)
					continue;
				WriteStatus("Обработка вендоров", Providers.VueExamType, i, vendors.Count);
				ImportExams(vendor.Value.Where(e => e.Price > 0), vendorId, Providers.VueExamType, Providers.Vue);
			}
		}

		public static void ImportPrometric() {
			PrometricSpider.DownloadAllExams();
			var files = GetFiles(PrometricSpider.ExamFolder);
			var parser = new PageParser();
			var fileCount = files.Count();
			var i = 0;
			foreach (var file in files) {
				WriteStatus("Обработка вендоров", Providers.PrometricExamType,i++,fileCount);


				var exams = parser.ParseExams(File.ReadAllText(file));

				if (exams.IsEmpty())
					continue;
				var match = Regex.Match(file, @"client(.*?)program");
				var clientID = int.Parse(match.Groups[1].Value);
				if (!ProviderConst.PrometricVendorIDList.ContainsKey(clientID))
					continue;
				var vendorID = ProviderConst.PrometricVendorIDList[clientID];
				var examType = Providers.PrometricExamType;
				var providerId = Providers.Prometric;

				var providerExams = exams.Where(pe =>
					pe.Languages.Intersect(ProviderConst.GetPrometricLanguages()).Any());
				ImportExams(providerExams, vendorID, examType, providerId);
			}
		}

		public static void ImportExams(IEnumerable<ProviderExam> providerExams, 
			int vendorID, string examType, int providerId) {

			var context = new SpecialistDataContext(PioneerConnectionString);
			foreach (var providerExam in providerExams) {
				var exam = context.Exams
					.FirstOrDefault(e => e.Exam_TC == providerExam.Number);
				if (exam != null) {
					exam.LastChangeDate = DateTime.Now;
				}
				else {
					exam = new Exam {
						Exam_TC = providerExam.Number,
						ExamName = providerExam.Name,
						ExamType = examType,
						Vendor_ID = vendorID,
						LastChangeDate = DateTime.Now
					};
					context.Exams.InsertOnSubmit(exam);
				}

				if(!exam.ProviderPrice.HasValue || examType == exam.ExamType )
				/*	if (exam.ExamProviders.All(e => e.Provider_ID != providerId)) {
						exam.ExamProviders.Add(
							new ExamProvider {
								Provider_ID = providerId,
								LastChanger_TC = Employees.Specweb,
								InputDate = DateTime.Now,
								Employee_TC = Employees.Specweb,
								LastChangeDate = DateTime.Now
							});
					}*/
					/*|| (vendorID == ProviderConst.OracleId && examType == Providers.VueExamType)*/ {
					exam.ProviderPrice = providerExam.Price;
					exam.Available = true;
					exam.Languages = providerExam.Languages.Where(l =>
					ProviderConst.GetPrometricLanguages().Contains(l))
					.JoinWith(",");
				}

			}
			context.SubmitChanges();
		}
	}
}