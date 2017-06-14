using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Common.Extension;
using Specialist.Web.Cms.ViewModel;
using SimpleUtils;
using Specialist.Web.Common.Html;
using OrderDetail=Specialist.Entities.Context.OrderDetail;

namespace Specialist.Web.Cms.Controllers
{
    public class ManagerEntityController: Controller
    {
        [Dependency]
        public IDiscountService DiscountService { get; set; }

        [Dependency]
        public IStudentService StudentService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        public ActionResult DiscountList()
        {
            var model = new DiscountListVM();
          
            model.Discounts = new List<Discount>();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DiscountList(DiscountListVM model)
        {
          /*  if(model.PriceType_TC.IsNullOrEmpty())
            {
                ModelState.AddModelError(model.For(x => x.PriceType_TC), "Обятельное поле");
                model.Discounts = new List<Discount>();
                return View(model);

            }*/
            model.Student = StudentService.GetAll().ByPrimeryKey(model.StudentID);
//            model.Group = GroupService.GetAll().ByPrimeryKey(model.GroupID);
            var orderDetails =
                new OrderDetail
                {
                    Course_TC = model.Group != null ? model.Group.Course_TC : null, 
                    Group = model.Group,
                    PriceType_TC = "ЧЛ",
                    
                };
            orderDetails.Order = new Order();

            model.Discounts = DiscountService.GetDiscountsFor(orderDetails, model.Student);
        

            return View(model);

            
        }
    }
}