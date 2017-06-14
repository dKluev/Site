using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.MetaData
{
    public class UserExamQuestionnaireMD: BaseMetaData<UserExamQuestionnaire>
    {
        public override void Init() {
	        For(x => x.LastName).Display("Last name").Example("Ivanov");
	        For(x => x.MiddleInitial).Display("Middle initial").Example("I.");
	        For(x => x.FirstName).Display("First name").Example("Ivan");
            For(x => x.Flat).Display("Flat/Office");
            For(x => x.PrometricCode).Display("Prometric ID")
                .Example("Обязательно, если вы сдавали экзамены в системе Prometric");
            For(x => x.McpCode).Display("MCP ID")
                .Example("Обязательно, если вы сдавали экзамены Microsoft");
        }
    }
}