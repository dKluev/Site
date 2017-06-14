using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Utils;

namespace Specialist.Web.Common.Utils.Files {
	public class VendorCertBase {

		public string FullName { get; set; }
		public string CertName { get; set; }
		public int CertType { get; set; }
		public DateTime Date { get; set; }
		public bool HD { get; set; }
		public string TrainerName { get; set; }
		protected CertTextPosition Text(string txt, int x, int y, int fontSize, 
			string font = "Verdana", int width = 0, bool bold = false, bool center = false, bool italic = false) {
			return new CertTextPosition(CertType, HD, txt, x, y, fontSize, font,width, bold,center, italic);
		}

		public VendorCertBase(
			bool hd, int certType, 
			string fullName, string certName, 
			DateTime date, string trainerName) {
			HD = hd;
			FullName = fullName;
			CertType = certType;
			CertName = certName;
			Date = date;
			TrainerName = trainerName;
		}
	}
}