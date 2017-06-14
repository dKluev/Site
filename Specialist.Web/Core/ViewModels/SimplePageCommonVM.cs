using System;
using System.Web;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Microsoft.Linq.Translations.Auto;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Common {
	public class SimplePageCommonVM<T>:IViewModel {
		private Tuple<string,string> _page;
        public T Data { get; set; }

		public SimplePageCommonVM(T data) {
			Data = data;
			var url = HttpContext.Current.Request.Url.AbsolutePath;
			var page = new SpecialistWebDataContext().SimplePages
				.Where(x => x.UrlName == url).Select(x => new {x.Title,x.Description})
				.FirstOrDefault();
			_page = page.GetOrDefault(x => Tuple.Create(x.Title, x.Description))
				?? new Tuple<string, string>("","");

		}

		public string Title { get { return _page.Item1; } }
		public string Text { get { return _page.Item2; } }
	}

    public class SimplePageCommonVM {
        public static SimplePageCommonVM<T> Create<T>(T data) {
            return new SimplePageCommonVM<T>(data);
        }
    }
}