using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Common.Logic;
using Specialist.Entities.Context;

namespace Specialist.Entities.Common.ViewModel {
	public class SimplePageVM : EntityCommonVM, IViewModel {
		public class Control {
			public string PartialViewName { get; set; }

			public object Model { get; set; }

			public bool OnBottom { get; set; }

			public string Title { get; set; }

			public Control(string partialViewName, object model, string title = null) {
				PartialViewName = partialViewName;
				Model = model;
				Title = title;
			}

			public Control(string content) {
				Content = content;
			}

			public string Content { get; set; }
		}

		public TextWithInfoTags Description { get; set; }

		/*   public bool ShowMainPageRightColumn
        {
            get
            {
                
                var simplePage = Entity;
                if (simplePage.SysName == SimplePages.Center)
                    return true;
                var rootParent = simplePage.RootMainParent;
                return rootParent != null && rootParent.SysName == SimplePages.Center;
            }
        }*/

		public SimplePageVM(IEntityCommonInfo entity) : base(entity) {
			Description = new TextWithInfoTags(entity.Description);
			Controls = new List<Control>();
			RightColumnControls = new List<Control>();
		}

		public new SimplePage Entity {
			get { return base.Entity.As<SimplePage>(); }
			set { base.Entity = value; }
		}

		public bool ShowTabs {
			get { return Entity.MainParent.GetOrDefault(sp => sp.UseTabs); }
		}

		public List<SimplePage> Tabs {
			get { return Entity.MainParent.Children.OrderBy(x => x.WebSortOrder).ToList(); }
		}

		public List<Control> Controls { get; set; }

		public List<Control> RightColumnControls { get; set; }

		public string Title {
			get { return ShowTabs ? Entity.MainParent.Name : Entity.Name; }
		}
	}
}