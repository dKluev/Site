using System;
using System.Collections;
using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using SimpleUtils;
using Specialist.Entities.Examination.ViewModel;

namespace Specialist.Entities.ViewModel
{
    public class VendorVM: EntityCommonVM, IViewModel
    {
		public class Tab {

			public const string Testing = "testing";

			public const string Certifications = "certifications";
			public string Name { get; set; }

			public string UrlPart { get; set; }

			public Tab(string name, string urlPart) {
				Name = name;
				UrlPart = urlPart;
			}
		}

    	public VendorVM(IEntityCommonInfo entity) : base(entity) {}
        public string Title {
            get {
	            if (CurrentTab == 2)
		            return "Экзамены " + Vendor.Name;
	            if (CurrentTab == 1)
		            return "Сертификация " + Vendor.Name;
            	var titleParts = new List<string>();
				if(!Vendor.Description.IsEmpty())
					titleParts.Add("курсы");
				if(!Vendor.CertificationDescription.IsEmpty())
					titleParts.Add("сертификации");
				return new [] { titleParts.JoinWith(" и ")
					.ToTitleCase(), Vendor.Name}.JoinWith(" "); }
        }

    	public int CurrentTab { get; set; }

    	public ExamListVM Exams { get; set; }

        public Vendor Vendor {
            get { return Entity.As<Vendor>(); }
        }
    }
}