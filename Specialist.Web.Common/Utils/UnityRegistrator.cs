using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using SimpleUtils;
using Specialist.Services;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Center;
using Specialist.Services.Center.ViewModel;
using Specialist.Services.Cms;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Education.Interface;
using Specialist.Services.Examination;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Interface.SitePart;
using Specialist.Services.Message;
using Specialist.Services.Order;
using Specialist.Services.Passport;
using Specialist.Services.SitePart;
using Specialist.Services.ViewModel;
using GroupVMService=Specialist.Services.GroupVMService;
using IGroupVMService=Specialist.Services.Interface.IGroupVMService;
using TrackService=Specialist.Services.TrackService;

namespace Specialist.Web.Common.Util
{
    public static class UnityRegistrator
    {
    	public static IUnityContainer Container { get; private set; }
        public static void RegisterServices(IUnityContainer container) {
        	Container = container;
            /*  container
               .AddNewExtension<Interception>()
               .AddNewExtension<InterceptOnRegister>()
               .Configure<InterceptOnRegister>()
               .SetDefaultInterceptor<VirtualMethodInterceptor>();*/

            container  
                //Cms
                .RegisterType<IBannerService, BannerService>()
                .RegisterType<ISimplePageService, SimplePageService>()
                //Core
                .RegisterType(typeof(IRepository<>), typeof(Repository<>))
                .RegisterType(typeof(IRepository2<>), typeof(Repository2<>))
                .RegisterType<IContextProvider, ContextProvider>()
                //Common
                .RegisterType<IMailService, MailService>()
                .RegisterType<IFileVMService, FileVMService>()
                .RegisterType<IFileService, FileService>()
                //Catalog
                .RegisterType<ISiteObjectService, SiteObjectService>()
                .RegisterType<ISiteObjectRelationService, SiteObjectRelationService>()
                .RegisterType<IMainPageService, MainPageService>()
                .RegisterType<INewsService, NewsService>()
                .RegisterType<IEntityCommonService, EntityCommonService>()

                //Center
                .RegisterType<ICityService, CityService>()
                .RegisterType<IUserWorkService, UserWorkService>()
                .RegisterType<IComplexService, ComplexService>()
                .RegisterType<IEmployeeVMService, EmployeeVMService>()
                .RegisterType<IEmployeeService, EmployeeService>()
                //Shop
                .RegisterType<IDiscountService, DiscountService>()
                .RegisterType<IEditExamVMService, EditExamVMService>()
                .RegisterType<ISpecialistExportService, SpecialistExportService>()
                .RegisterType<IOrderDetailService, OrderDetailService>()
                .RegisterType<IEditCourseVMService, EditCourseVMService>()
                //Education
                .RegisterType<IStudentService, StudentService>()
                //Profile
                .RegisterType<IMessageSectionService, MessageSectionService>()
                //Other
                .RegisterType<IOrderDetailService, OrderDetailService>()
                .RegisterType<ICartService, CartService>()
                .RegisterType<IContractVMService, ContractVMService>()
                .RegisterType<IOrderService, OrderService>()
                .RegisterType<IDictionariesService, DictionariesService>()
                .RegisterType<IRegistrationVMService, RegistrationVMService>()
//                .RegisterType<IProductService, ProductService>()
                .RegisterType<ICachingService, CachingService>()
                .RegisterType<ICourseService, CourseService>()
                .RegisterType<ITrackVMService, TrackVMService>()
                .RegisterType<IPriceService, PriceService>()
                .RegisterType<IGroupService, GroupService>()
                .RegisterType<IEditTrackVMService, EditTrackVMService>()
                .RegisterType<IUserService, UserService>()
                .RegisterType<IAuthService, AuthService>()
                .RegisterType<IGroupVMService, GroupVMService>()
                .RegisterType<ICertificationcVMService, 
                    CertificationVMService>()
                .RegisterType<IVendorVMService,
                    VendorVMService>()
                .RegisterType<IDayShiftService, DayShiftService>()
                .RegisterType<ITrackService, TrackService>()
                .RegisterType<IExamService, ExamService>()
                .RegisterType<ISectionService, SectionService>()
                .RegisterType<ICustomSectionService, CustomSectionService>()
                .RegisterType<ICustomSectionTypeService, CustomSectionTypeService>()
                .RegisterType<IUserSettingsService, UserSettingsService>()
                .RegisterType<ICertificationService, CertificationService>()
                .RegisterType<IVendorService, VendorService>()
                .RegisterType<ICourseListVMService, CourseListVMService>()
                .RegisterType<IResponseService, ResponseService>();

           
            container
                .AddNewExtension<Interception>()
                .Configure<Interception>()
                .SetDefaultInterceptorFor<MainPageService>(new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<MailTemplateService>(
                    new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<GroupService>(new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<PriceService>(new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<CityService>(new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<CourseService>(new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<TrackService>(new VirtualMethodInterceptor())
				.SetDefaultInterceptorFor<SectionService>(new VirtualMethodInterceptor())
				.SetDefaultInterceptorFor<ExtrasService>(new VirtualMethodInterceptor())
                .SetDefaultInterceptorFor<MessageSectionService>(
                    new VirtualMethodInterceptor());
        }

        public static void RegisterControls(IUnityContainer container, Assembly assembly)
        {
            Type[] assemblyTypes = assembly.GetTypes();
            foreach (Type type in assemblyTypes)
            {
                if (typeof(IController).IsAssignableFrom(type))
                {
                    container.RegisterType(type, type);
                }
            }
        }
    }
}