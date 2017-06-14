using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using SimpleUtils;
using Specialist.Entities.Message.ViewModel;

namespace Specialist.Entities.Message
{
    public class MessageVM : NewMessageVM, IViewModel
    {
        public UserMessage Message { get; set; }

        public PagedList<UserMessage> Answers { get; set; }

    	/*public Dictionary<decimal, string> RealStudents { get; set; }

    	public List<decimal> BestStudents { get; set; } */

        public List<UserMessage> AllMessages
        {
            get
            {
                if(Answers.PageIndex == 0 && !Message.IsGroup)
                    return new List<UserMessage> {Message}.AddFluent(Answers);
                return Answers;
            }
        }

        public string Title
        {
            get { return Message.Title; }
        }
    }
}