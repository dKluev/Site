using System;
using System.Collections.Generic;
using System.ComponentModel;
using GSearch;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;

namespace Specialist.Web.Common.Page
{
    public class SearchVM: IViewModel
    {
        public const int PageSize = 8;
        [DisplayName(" ")]
        public string Text { get; set; }

	    public string Suggestion { get; set; }

//        public List<Result> Results { get; set; }
        public GWebResponseData ResponseData { get; set; }

        public int PageIndex { get; set; }

        public int TotalRecords { get
        {
            return Math.Min(ResponseData.Cursor.EstimatedResultCount, 8 * PageSize);
        } 
        }

        public SearchVM()
        {
//            Results = new List<Result>();
        }

        public string Title {
            get { return "Поиск"; }
        }
    }
}