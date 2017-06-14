using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Education.Interface
{
    public interface IStudentService: IRepository<Student>
    {
//        Student GetByUser(int userID);
        decimal? GetPreviousOrdersSum(decimal studentID);
	    List<Tuple<string, string>> GetPaidCourseAndTrainerTCs(decimal studentId);
    }
}