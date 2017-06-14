using System.ComponentModel;
using SimpleUtils.Common.Enum;
using SimpleUtils.Extension;

namespace Specialist.Entities.Context
{
    public enum OrderStep
    {
        [EnumDisplayName("���� �������")]
        Cart,
        [EnumDisplayName("����/�����������")]
        Register,
//        [EnumDisplayName("�������")]
//        Contract,
        [EnumDisplayName("����� ����� ������")]
        PaymentTypeChoice
    }
}