using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Message.ViewModel
{
    public class SectionListVM: IViewModel
    {
        public List<MessageSection> MessageSections { get; set; }

        public bool IsForum { get
        {
            return MessageSections.Any(x => x.Parent != null
                && x.Parent.SysName == Profile.Const.MessageSections.Forum);
        }}

    	public User User { get; set; }
        public string Title
        {
            get
            {
                return MessageSections.First().Parent.Name;
            }
        }
    }
}