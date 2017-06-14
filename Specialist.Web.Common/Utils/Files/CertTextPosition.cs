using System;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Common.Utils.Files {
	public class CertTextPosition {
		public string Text { get; set; } 
		public int X { get; set; }
		public int Y { get; set; }
        public int FontSize { get; set; }
        public string Font { get; set; }
        public int Width { get; set; }
        public bool Bold { get; set; }
        public bool Center { get; set; }
        public bool Italic { get; set; }
		public CertTextPosition(int certType, bool hd, string text, int x, int y, int fontSize, string font, int width, bool bold
			, bool center, bool italic) {
			var hdScale = VendorEngCertData.SpecialScale.GetValueOrDefault(certType);
			if (hdScale <= 0) {
				hdScale = 4;
			}
			Func<int, int> f = z => hd ? (int) (z*hdScale) : z;
			Text = text;
			X = f(x);
			Y = f(y);
			FontSize = f(fontSize);
			Font = font;
			Width = f(width);
			Bold = bold;
			Center = center;
			Italic = italic;
		}
	}
}