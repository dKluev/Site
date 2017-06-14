using System.Collections.Generic;

namespace Specialist.Web.Cms.MetaData.Validators {
	public interface ICmsValidator<T> {
		List<string> Validate(T entity);
	}
}