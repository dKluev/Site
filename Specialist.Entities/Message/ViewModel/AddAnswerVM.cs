using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Message.ViewModel
{
    public class AddAnswerVM: CreateMessageVM, IViewModel
    {
        public UserMessage Message { get; set; }

		

        public string Title
        {
            get { return "Ответ в обсуждении: " + Message.Title; }
        }
    }
}