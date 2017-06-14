using System;

namespace Specialist.Web.Common.Services {
	public class CaptchaException: Exception {
		public decimal Id { get; set; }

		public string Url { get; set; }

		public CaptchaException(decimal id, string url) {
			Id = id;
			Url = url;
		}
	}
}