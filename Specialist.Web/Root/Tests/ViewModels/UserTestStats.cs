using System.Collections.Generic;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class UserTestStats:Dictionary<int, UserTestStats.RightWrong> {
		 public class RightWrong {
		 	public int R { get; set; }
		 	public int W { get; set; }

			public override string ToString() {
				return R + " \\ " + W;
			}
		 }

	}
}