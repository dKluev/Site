using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Entities.Passport.ViewModel
{
    public class PhoneVM
    {
      /*  [DisplayName("Код")]
        [Required]
        [RegularExpression(RegExp.OnlyNumbers)]
        public string Code { get; set; }*/

        [DisplayName("Телефон")]
        [Example("(495)1234567<br/>Наши менеджеры с удовольствием проконсультируют Вас, расскажут о текущих акциях и запишут в удобную для вас группу. Мы не передаем номера третьим лицам")]
        public string Phone { get; set; }

        public int ContactType { get; set; }
    }
}