using System;
using System.Threading;

namespace Specialist.Services.Utils {
	public class ThreadPoolEx {
		public static void QueueUserWorkItemWithCatch(WaitCallback callBack) {
			ThreadPool.QueueUserWorkItem(x => {
			try {
				callBack(x);
			}catch(Exception e) {
				Logger.Exception(e, "QueueUserWorkException");
			} });
		}
		
	}
}