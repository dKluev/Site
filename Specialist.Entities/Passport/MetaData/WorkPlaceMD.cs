using System;
using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.FluentAttributes.Core.Interfaces;

namespace Specialist.Entities.Passport.MetaData
{
    public class WorkPlaceMD: BaseMetaData<WorkPlace>
    {
        public WorkPlaceMD()
        {
            For(x => x.Name).Display("Название организации");
            For(x => x.Site).Display("Сайт");
            For(x => x.FullName).Display("ФИО контакта от компании");
            For(x => x.Phone).Display("Телефон");
            For(x => x.Email).Display("E-mail");
        }
    }
}