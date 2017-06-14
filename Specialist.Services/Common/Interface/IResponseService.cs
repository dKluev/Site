using System.Collections.Generic;
using System.Linq;
using Specialist.Entities;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface
{
    public interface IResponseService: IRepository<Response>
    {
        IQueryable<Response> GetAllForCourse(string courseTC);
        List<Response> GetRandomForWebinar();
        Response GetRandomForMainPage(List<string> courseTCs);
        List<Response> GetRandomResponsesByCourse(string CourseTC, int rows);
    	IQueryable<Response> GetAllForEmployee(string employeeTC);
    	OrgResponse GetOrgRandomResponseForMainPage();
    	List<Advice> GetRandomAdvices();
    }
}