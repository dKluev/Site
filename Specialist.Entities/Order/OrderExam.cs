using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Context;

namespace Specialist.Entities.Context
{
    public partial class OrderExam
    {
        
        private EntityRef<Exam> _Exam = default(EntityRef<Exam>);
        [Association(Storage = "_Exam", ThisKey = "Exam_ID", 
            OtherKey = "Exam_ID", IsForeignKey = true)]
        public Exam Exam
        {
            get
            {
                return _Exam.Entity;
            }
        }

        private EntityRef<Group> _Group = default(EntityRef<Group>);
        [Association(Storage = "_Group", ThisKey = "Group_ID", 
            OtherKey = "Group_ID", IsForeignKey = true)]
        public Group Group
        {
            get
            {
                return _Group.Entity;
            }
        }
    }
}