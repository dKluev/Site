using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education.Interface;

namespace Specialist.Services.Education
{
    public class StudentService: Repository<Student>, IStudentService
    {
        public StudentService(IContextProvider contextProvider) : base(contextProvider) {}
/*

        public Student GetByUser(int userID)
        {
            return GetAll()
                .FirstOrDefault(s => s.WebStudent_ID == userID);
        }
*/

        [Dependency]
	    public IRepository2<StudentInGroup> SigService { get; set; }

        public decimal? GetPreviousOrdersSum(decimal studentID)
        {
            var studentInGroups =
                from studentInGroup in context.GetTable<StudentInGroup>()
                where studentInGroup.Student_ID == studentID
                && studentInGroup.Org_ID == null
                select studentInGroup;

            return studentInGroups.Sum(sig => (decimal?)sig.StudentsInGroupsCalc.PaidSum);

        }

	    public List<Tuple<string, string>> GetPaidCourseAndTrainerTCs(decimal studentId) {
		    return SigService.GetAll(x => x.Student_ID == studentId
										&& BerthTypes.AllPaid.Contains(x.BerthType_TC))
										.Select(x => new { x.Group.Course_TC, x.Group.Teacher_TC})
										.ToList().Select(x => Tuple.Create(x.Course_TC, x.Teacher_TC)).ToList();

	    }  

    }
}