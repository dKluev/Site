using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Specialist.Entities.Context;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Message;
using Specialist.Entities.Message.ViewModel;
using Specialist.Entities.Profile.Const;
using Specialist.Web.Common.Extension;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Message;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Common.Logic
{
    public class ForumBreadCrumbs
    {
        HtmlHelper _helper = null;
        string Forum;

        public ForumBreadCrumbs(HtmlHelper helper)
        {
            _helper = helper;
            Forum = helper.Forum();
        }


        public List<string> GetBreadCrumbs(object model)
        {
            return new List<string>();
        }

        public List<string> GetBreadCrumbs(SectionListVM model)
        {
            var parent = model.MessageSections.First().Parent;
            if(parent.SysName == MessageSections.Forum)
                return new List<string>();

            return GetBreadCrumbs(parent, false);
        }

        private List<string> GetBreadCrumbs(MessageSection parent, bool withParent)
        {
            var breadcrumb = new List<string>();
            if(parent != null && !withParent)
                parent = parent.Parent;
            while(parent != null && parent.MessageSectionID > 0)
            {
                if(parent.SysName == MessageSections.Forum)
                {
                    parent = null;
                }
                else
                {
                    breadcrumb.Add(_helper.ActionLink<MessageController>(
                        c => c.Section(parent.MessageSectionID, 1), parent.Name).ToString());
                    parent = parent.Parent;
                }
            }

            breadcrumb.Add(Forum);
            breadcrumb.Reverse();
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(MessageVM model)
        {
            var message = model.Message;
            if(message.IsGroup) {
                return GetBreadCrumbsForGroup(message);
            }
            return GetBreadCrumbs(message.MessageSection, true);
        }

        private List<string> GetBreadCrumbsForGroup(UserMessage message) {
            var groupID = message.GroupID.Value;
            var breadCrumbPart = BreadCrumbs.GetBreadCrumbPart(_helper);
            var path = breadCrumbPart.GetPath(typeof(GroupVM));
            var breadcrumb = path.Select(bc => bc.Link).Reverse().ToList();
            breadcrumb.Add(_helper.ActionLink<GroupController>(c =>
                c.Details(groupID), "Страница группы").ToString());
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(EditMessageVM model)
        {
            return GetBreadCrumbs(model.MessageSection, true);
        }

        public List<string> GetBreadCrumbs(AddAnswerVM model) {
            var crumbs = model.Message.IsGroup 
                ? GetBreadCrumbsForGroup(model.Message)
                : GetBreadCrumbs(model.Message.MessageSection, true);
            crumbs.Add(_helper.ActionLink<MessageController>(
                c => c.Details(model.Message.UserMessageID, 1), 
                model.Message.Title).ToString());
            return crumbs;
        }

        public List<string> GetBreadCrumbs(SectionMessageListVM model)
        {
            return GetBreadCrumbs(model.Section, false);
        }

    }
}