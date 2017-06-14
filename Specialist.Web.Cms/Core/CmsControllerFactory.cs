using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcContrib.Unity;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;
using SimpleUtils.Extension;
using SimpleUtils;

namespace Specialist.Web.Cms.Core
{
    public class CmsControllerFactory : UnityControllerFactory
    {
        public override System.Web.Mvc.IController CreateController(System.Web.Routing.RequestContext context, string controllerName)
        {
            Type type = GetControllerType(context, controllerName);
            if (type != null) 
                if(!type.Namespace.In(Const.Common.ControllerNamespace,
                    Const.Common.CommonControllerNamespace) )
                   type = null;

            if (type == null)
            {
                var typeName = Regex.Replace(controllerName, 
					Const.Common.ControlPosfix + "$","");
                var entityType = typeof(SpecialistDataContext).Assembly.GetTypes()
                    .Where(t => t.Name == typeName).First();
                type = typeof(BaseController<>)
                    .MakeGenericType(entityType);
                
            }

            IUnityContainer container = GetContainer(context);
            return (IController)container.Resolve(type);
        }
    }
}