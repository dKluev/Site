using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Education.ViewModel
{
    public class GroupForumVM: IViewModel
    {
        public PagedList<UserMessage> UserMessages { get; set; }

        public Group Group { get; set; }

        public string Title
        {
            get { return "Обсуждение группы по курсу: " + Group.Course.WebName; }
        }

        [DisplayName("Новое сообщение")]
        [UIHint(Controls.TextArea)]
        public string NewMessage { get; set; }
    }
}