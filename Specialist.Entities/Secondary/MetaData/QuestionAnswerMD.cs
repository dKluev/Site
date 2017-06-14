using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Entities.Secondary;

namespace Specialist.Entities.Profile.MetaData
{
    public class QuestionAnswerMD: BaseMetaData<QuestionAnswer>
    {
        public override void Init()
        {
            For(x => x.Question1)
				.Display("�������������� �� �������� � ������ ��������� ����� �����?");
        	For(x => x.Question2).Display("�����������").UIHint(Controls.TextArea);
        	For(x => x.Question3)
				.Display("��������� ��������, ��� �� �������������� �������� � ����� ������ ������ ����� ��� ���������?(0 � �� ������������, 10 � ����������� ������������)");
        	For(x => x.Question4)
				.Display("����, �� ������� �� �� ���������� ������, ������ �������� ��������� ����").UIHint(Controls.BigTextArea);
        	For(x => x.Question5)
				.Display("�������� ��� ����� �����! ��������, ���� ��� �� �������, ��� ����, ����� �� ����� ������ � ��� �������?").UIHint(Controls.BigTextArea);
        }
    }
}