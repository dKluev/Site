using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Message {
    public interface IMessageSectionService: IRepository<MessageSection> {
    	Dictionary<int,int> SectionMessageCounts();

    	Dictionary<int,DateTime> SectionLastMessageDates();
    }
}