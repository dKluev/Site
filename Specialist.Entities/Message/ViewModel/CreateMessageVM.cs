using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Entities.Message.ViewModel {
    public class CreateMessageVM {

		public bool CannotAddMessageToClub { get; set; }

        public CreateMessageVM() {
            Description = string.Empty;
        }
        public string Image { get; set; }
        public const string LoadImage = "Загрузить";

        public string IsLoad { get; set; }

        [DisplayName("Содержание")]
        [UIHint(Controls.BigTextArea)]
        public string Description { get; set; }
    }
}