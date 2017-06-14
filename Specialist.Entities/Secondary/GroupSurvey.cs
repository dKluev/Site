using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Context;

namespace Specialist.Entities.Secondary {
	public partial class GroupSurvey {

		public const string Replay1 = "Ваши ожидания от курса, какие темы интересуют Вас в первую очередь?";
		public const string Replay2 = "Напишите несколько вопросов, на которые хотели бы получить ответы на занятиях";


        private EntityRef<Group> _Group = default(EntityRef<Group>);
        [Association(Storage = "_Group", ThisKey = "Group_ID",
            OtherKey = "Group_ID", IsForeignKey = true)]
        public Group Group
        {
            get
            {
                return _Group.Entity;
            }
            set
            {
                _Group.Entity = value;
            }
        }
		 
	}
}