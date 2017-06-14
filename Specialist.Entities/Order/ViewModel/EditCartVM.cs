using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Context.ViewModel
{
    public class EditCartVM
    {
        public decimal? EditDetailID { get; set; }

        public decimal? EditExamID { get; set; }

        public string EditTrackTC { get; set; }

    	public int? UserTestId { get; set; }

    }
}