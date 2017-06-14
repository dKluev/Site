using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Message.ViewModel
{
    public class NewMessageVM
    {
        public User Receiver { get; set; }

        [DisplayName("Новое сообщение")]
        [UIHint(Controls.TextArea)]
        public string SendMessage { get; set; }
    }
}