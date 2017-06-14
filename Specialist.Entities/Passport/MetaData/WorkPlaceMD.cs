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
            For(x => x.Name).Display("�������� �����������");
            For(x => x.Site).Display("����");
            For(x => x.FullName).Display("��� �������� �� ��������");
            For(x => x.Phone).Display("�������");
            For(x => x.Email).Display("E-mail");
        }
    }
}