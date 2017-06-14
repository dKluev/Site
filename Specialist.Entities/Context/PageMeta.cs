using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Context {
	public partial class PageMeta {
		partial void OnUrlChanged()
		{
			if (!_Url.IsEmpty())
				_Url = _Url.ToLower();	
		}
	}
}