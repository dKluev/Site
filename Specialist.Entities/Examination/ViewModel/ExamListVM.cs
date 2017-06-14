using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Examination.ViewModel
{
    public class ExamListVM: IViewModel
    {
        public Vendor Vendor { get; set; }

        public PagedList<Exam> Exams { get; set; }

        public bool HasPrice { get { return Exams.Any(e => e.ExamPrice.HasValue); } }

        public string Title
        {
            get { return "Ёкзамены " + Vendor.Name; }
        }
    }
}