using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Profile.ViewModel
{
    public class UserFileListVM: IViewModel
    {
        public PagedList<UserFile> Files { get; set; }

        public string Title
        {
            get { return "Файлы преподавателя"; }
        }
    }
}