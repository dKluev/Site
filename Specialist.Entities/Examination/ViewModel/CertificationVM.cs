using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Interface;

namespace Specialist.Entities.ViewModel
{
    public class CertificationVM: IViewModel
    {
        public Certification Certification { get; set; }

        public List<Certification> Children { get; set; }

        public List<TrackDiscount> Tracks { get; set; }

	    public List<MarketingAction> Actions { get; set; }

        public List<Group> Groups { get; set; }

	    public CertificationVM() {
		    Actions = new List<MarketingAction>();
	    }

	    public bool IsMicrosoft { get { return Certification.Vendor_ID == Vendor.Microsoft; } }

        public IEnumerable<IGrouping<string, CertificationExam>> ExamGroups() {
            return
                from ce in Certification.CertificationExams
                where ce.CertificationTable_ID == null &&
                    ce.CertificationVariant_ID == null
                group ce by ce.CertificationExamsSet
                into gr select gr;
        }

        public string Title {
            get { return "Сертификация " + Certification.Name; }
        }

	    public List<Employee> Trainers { get; set; }
    }
}