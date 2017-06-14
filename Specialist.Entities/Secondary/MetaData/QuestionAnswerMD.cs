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
				.Display("Способствовало ли обучение в Центре улучшению Вашей жизни?");
        	For(x => x.Question2).Display("Комментарий").UIHint(Controls.TextArea);
        	For(x => x.Question3)
				.Display("Насколько вероятно, что Вы порекомендуете обучение в нашем Центре своему другу или знакомому?(0 – не порекомендую, 10 – обязательно порекомендую)");
        	For(x => x.Question4)
				.Display("Курс, на который бы Вы записались сейчас, должен включать следующие темы").UIHint(Controls.BigTextArea);
        	For(x => x.Question5)
				.Display("Помогите нам стать лучше! Напишите, чего нам не хватает, для того, чтобы Вы снова пришли к нам учиться?").UIHint(Controls.BigTextArea);
        }
    }
}