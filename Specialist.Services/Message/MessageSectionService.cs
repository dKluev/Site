using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;
using Specialist.Services.Common.Extension;

namespace Specialist.Services.Message {
    public class MessageSectionService: Repository<MessageSection>, IMessageSectionService {

        public MessageSectionService() : base(new ContextProvider()) {}

        [Dependency]
        public IRepository<UserMessage> UserMessageService { get; set; }

        [Cached]
        public virtual Dictionary<int,int> SectionMessageCounts() {
        	return GetAll().IsActive().ToList().ToDictionary(x => x.MessageSectionID,
        		x => GetMessageCount(_.List(x)));
        }

        [Cached]
        public virtual Dictionary<int,DateTime> SectionLastMessageDates() {
        	return GetAll().IsActive().ToList().ToDictionary(x => x.MessageSectionID,
        		x => GetLastMessageDate(_.List(x)));
        }

		public int GetMessageCount(List<MessageSection> sections) {
			var sectionIds = sections.Select(x => x.MessageSectionID).ToList();
			var result = UserMessageService
				.GetAll(x => sectionIds.Contains(x.MessageSectionID.Value)).Count();
			if(sections.Any(x => x.Children.Where(c => c.IsActive).Any()))
				result += GetMessageCount(sections
					.SelectMany(x => x.Children.Where(c => c.IsActive)).ToList()); 
			return result;

		}

		public DateTime GetLastMessageDate(List<MessageSection> sections) {
			var sectionIds = sections.Select(x => x.MessageSectionID).ToList();
			var result = UserMessageService
				.GetAll(x => sectionIds.Contains(x.MessageSectionID.Value)).Max(
				x => (DateTime?)x.CreateDate);
			if(sections.Any(x => x.Children.Where(c => c.IsActive).Any()))
			{
				var result2 =  GetLastMessageDate(sections
					.SelectMany(x => x.Children.Where(c => c.IsActive)).ToList());
				if(result2 > result)
					result = result2;
			} 
			return result.GetValueOrDefault(DateTime.Now);
		}

    }
}