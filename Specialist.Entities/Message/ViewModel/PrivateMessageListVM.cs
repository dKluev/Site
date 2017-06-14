using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Message.ViewModel
{
    public class PrivateMessageListVM: NewMessageVM, IViewModel
    {
        public MessageSection Section { get; set; }

        public PagedList<UserMessage> Messages { get; set; }

        public User Sender { get; set; }

        public string Title
        {
            get { return "История переписки"; }
        }
    }
}