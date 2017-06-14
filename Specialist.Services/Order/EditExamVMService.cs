using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;

namespace Specialist.Services.Order
{
    public class EditExamVMService : IEditExamVMService
    {
        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        public OrderExam GetOrderExam(decimal examID)
        {
            var result = OrderService.GetCurrentOrder().OrderExams
                .FirstOrDefault(oe => oe.Exam_ID == examID);
            return result;
        }
        public EditExamVM Get(decimal examID)
        {
            var groups = 
                from g in GroupService.GetPlannedAndNotBegin()
                where g.Course_TC == CourseTC.Prometric
                orderby g.DateBeg 
                select g;
            
            return 
                new EditExamVM
                {
                    OrderExam = GetOrderExam(examID),
                    Groups = groups.Take(30).ToList() 
                };
        }

        public void Update(EditExamVM model)
        {
            var orderExam = GetOrderExam(model.OrderExam.Exam_ID);
            orderExam.Group_ID = model.OrderExam.Group_ID;
            OrderService.SubmitChanges();
        }
    }
}