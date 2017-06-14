//using System;
//using FluentValidation;
//using Specialist.Entities.Cms;

//namespace Specialist.Web.Cms.Util
//{
//    public class CmsValidatorFactory: IValidatorFactory
//    {
//        public IValidator<T> GetValidator<T>()
//        {
//            return (IValidator<T>) GetValidator(typeof(T));
//        }

//        public IValidator GetValidator(Type type)
//        {
//            if (type == typeof(News))
//                return new NewsValidator();
//            throw new Exception();
//        }
//    }
//}