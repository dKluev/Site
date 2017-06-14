using System;
using Specialist.Entities.Context;
using Specialist.Entities.Profile;

namespace Specialist.Web.Root.Common.MailList {
	public class MailListVM {
		public MailTemplate Template { get; set; }

		public MailListType MailListType { get; set; }

		public bool IsStopped { get; set; }

		public double? SendedPercent { get; set; }

		public DateTime? LastSendDate { get; set; }
	}
}