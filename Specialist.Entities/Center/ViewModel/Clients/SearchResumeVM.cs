using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using System.Linq;
using System.Linq.Dynamic;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Web;

namespace Specialist.Entities.Center.ViewModel
{
    public class SearchResumeVM : IViewModel
    {
        public string YourPosition { get; set; }
        public string YourCity { get; set; }
        public string YourAgeSince { get; set; }
        public string YourAgeTill { get; set; }
        public string YourEducation { get; set; }
        public string YourExperience { get; set; }
        public string YourSex { get; set; }
        public string YourProfit { get; set; }
        public string YourCurrency { get; set; }
        public string YourMetro { get; set; }
        public string Result { get; set; }
        public bool s1 { get; set; }
        public bool s2 { get; set; }
        public bool s3 { get; set; }
        public bool s4 { get; set; }
        public bool s5 { get; set; }
        public bool s6 { get; set; }
        public bool s7 { get; set; }
        public bool s8 { get; set; }
        public bool s9 { get; set; }
        public bool s10 { get; set; }
        public bool s11 { get; set; }
        public bool s12 { get; set; }
        public bool s13 { get; set; }
        public bool s14 { get; set; }

        public SimplePage Page { get; set; }

        private bool _isStartSearch = false;

        public bool isStartSearch
        {
            get
            {
                return _isStartSearch;
            }
            set
            {
                _isStartSearch = value;
            }
        }


        public PagedList<Resume> Resume { get; set; }

        public SearchResumeVM()
        {
            Resume = new PagedList<Resume>();
        }

        public bool IsSearchParamsResume
        {
            get
            {
                return UrlName == SimplePages.Urls.SearchParamsResume;
            }
        }

        public bool IsSeachResultResume
        {
            get
            {
                return UrlName == SimplePages.Urls.SearchResultsResume;
            }
        }

        public List<SimplePage> Pages { get { return SimplePages.GetSearchResume(); } }

        public string UrlName { get; set; }        

        public string Title
        {
            get { return ""; }
        }
    }
}
