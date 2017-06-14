using System;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Profile.Const;

namespace Specialist.Entities.Context {
	public partial class MessageSection {

		public int MessageCount { get; set; }

		public DateTime LastMessageDate { get; set; }

		public int Order {
			get {
				var order = MessageSections.Orders.GetValueOrDefault(MessageSectionID);
				return order > 0 ? order : MessageSectionID;
			}
		}

		public bool IsGraduateClubOrChildren {
			get { return IsGraduateClub || ParentSectionID == MessageSections.GraduateClub; }
		}

		public bool IsGraduateClub {
			get {
				return MessageSectionID == MessageSections.GraduateClub;
			}
		}

	}
}