using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Message.ViewModel
{
    public class EditMessageVM: CreateMessageVM, IViewModel
    {
      
        public MessageSection MessageSection { get; set; }

        [DisplayName("���� ����������")]
        public string MessageTitle { get; set; }

    	public long? MessageId { get; set; }

        public string Title
        {
            get {
            	if(MessageId.HasValue)
            		return "��������������";
				return "����� ���������� � ������: " + MessageSection.Name;
            }
        }
    }
}