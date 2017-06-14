using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using SimpleUtils.Extension;

namespace Specialist.Entities.Profile.ViewModel
{
    public class FileListVM: IViewModel
    {
        public List<UserFile> Files { get; set; }

        public List<UserFile> UserFiles { get; set; }

        public Course Course { get; set; }

        public Group Group { get; set; }

        [DisplayName("Выбрать файл")]
        [UIHint(Controls.Select)]
        public int SelectedFileID { get; set; }

        public string CourseTC { get
        {
            return Course.GetOrDefault(c => c.Course_TC);
        } }

        public decimal? GroupID { get
        {
            return Group.GetOrDefault(c => (decimal?)c.Group_ID);
        } }
        public string Title
        {
            get
            {
                var title = "Файлы";
                if (Course != null)
                    title += " курса " + Course.Name;
                if (Group != null)
                    title += " группы " + Group.Group_ID;
                return title;
            }
        }
    }
}