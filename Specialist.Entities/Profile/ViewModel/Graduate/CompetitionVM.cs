using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Profile.ViewModel {
    public class CompetitionVM: IViewModel {

        public Competition Competition { get; set; }

        public string Title {
            get { return "Конкурс: " + Competition.Name; }
        }

        public bool IsWinner { get; set; }

        public bool IsJoin { get; set; }

        [DisplayName("Заявка на участие")]
        [UIHint(Controls.BigTextArea)]
        public string Request { get; set; }

        [DisplayName("Файл")]
        [Example("Примечание: файл размером не более 1 mb")]
        [UIHint(Controls.File)]
        public string File { get; set; }

        public UploadFile UploadFile { get; set; }
    }
}