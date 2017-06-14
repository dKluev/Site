using System;

namespace Specialist.Services.Utils {
	public class PermissionException: Exception {
		public PermissionException() {}
		public PermissionException(string message) : base(message) {}
	}
}