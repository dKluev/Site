using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context;

namespace Specialist.Web.Util {
	public class ExcelUtl {
		public static string Link(string x) {
			if (x.Contains("http://")) return "HYPERLINK(\"" + x + "\",\"" + x + "\")";
			return null;
		}
		public static byte[] CreateXls(List<List<string>> data) {
			using (var package = new ExcelPackage()) {
				var ws = package.Workbook.Worksheets.Add("Курсы");
				data.ForEach((x, i) => {
					x.ForEach((v, j) => {
						ws.Cells[i + 1,j + 1].Value = v;
						if (v != null && v.StartsWith("http://")) {
							ws.Cells[i + 1,j + 1].Hyperlink = new Uri(v);
						}
					});
					
				});
				ws.Cells[ws.Dimension.Address].AutoFitColumns();
				return package.GetAsByteArray();
			}
		} 
	}
}