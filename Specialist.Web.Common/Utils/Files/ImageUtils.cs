using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;
using Specialist.Entities.Const;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Utils.Files;
using Image = System.Drawing.Image;

namespace SpecialistTest.Common.Utils {
	public class ImageUtils {
		public static Image DrawTestString(Image image, string fullname, 
			string testName, string date, int number)
		{
			var resultImage = new Bitmap(image);
			using (var g = Graphics.FromImage(resultImage)) {
				DrawString(g, fullname, 147, 565,34,false);
				DrawString(g, testName + Environment.NewLine + date, 147, 810,28,true);
				DrawString(g, "№ " + number, 147, 1180,20,false);
			}
			resultImage.SetResolution(150,150);
			return resultImage;
		}

		public static Image DrawRegCouponText(Image image, 
			string fullname, string code, DateTime endDate)
		{
			var resultImage = new Bitmap(image);
			using (var g = Graphics.FromImage(resultImage)) {
				DrawString(g, fullname + Environment.NewLine + code
					+ " действителен до " + endDate.DefaultString(), 50, 25,12,false);
			}
			return resultImage;
		}

		static Image Draw(Image image, Action<Graphics> f) {
			var resultImage = new Bitmap(image);
			using (var g = Graphics.FromImage(resultImage)) {
				f(g);
			}
			return resultImage;
			
		}

		public static Image DrawWebinarCertText(Image image, string fullname, string certName, DateTime date) {
			return Draw(image, g => {
				DrawStringByCenter(g, certName, 310, image.Width,12,"Pragmatica",
					ColorTranslator.FromHtml("#666666"),true, widthLimit:true);
				var fontSize = fullname.Length > 32 ? 18 : 26;
				DrawStringByCenter(g, fullname, 380, image.Width,fontSize,"Pragmatica",
					ColorTranslator.FromHtml("#0B4583"),true);
				DrawString(g,date.DefaultString(), 640, 482, 10,false, new Font("Pragmatica", 10));
			});
		}

		public static Image Best2016(Image image, string fullname) {
			return Draw(image, g => {
				DrawStringByCenter(g, fullname, 177, image.Width,12,"Pragmatica", center: 503);
			});
		}


		public static Image DrawGroupEndText(Image image, string name) {
			return Draw(image, g => {
				DrawString(g, name, 20, 20, 12,true,null,380,70,italic:false);
			});
		}


 
		public static Image DrawGroupCertEngTextOld(Image image, string fullname, string certName, string date,
			bool hd) {
 			return Draw(image, g => {
 				if (hd) {
					DrawString(g, fullname, 312, 936,28,false, new Font("Verdana", 77,FontStyle.Italic));
					DrawString(g, certName, 312, 1296,20,true,new Font("Verdana", 58,FontStyle.Italic), 1800, height: 350);
					DrawString(g, date, 552, 1768,16,false,new Font("Verdana", 38));
 				} else {
					DrawString(g, fullname, 65, 195,14,false, new Font("Verdana", 16,FontStyle.Italic));
					DrawString(g, certName, 65, 270,10,true,new Font("Verdana", 12,FontStyle.Italic), 430);
					DrawString(g, date, 115, 367,8,false,new Font("Verdana", 8));
 				}
 			});
 		}
 

//		public static Image DrawGroupCertEngTextMs(Image image, bool hd, VendorEngCertData d) {
////			if (hd) {
////				return Draw(image, g => {
////					var x = 3500.0/727;
////					Func<int, int> f = z => (int) (z*x);
////					var dateText = "Computer Training Center at Bauman MSTU " + d.Date;
////					DrawString(g, d.FullName, f(281), f(210), 14, false, new Font("Arial", f(14)));
////					DrawString(g, d.CertName, f(281), f(310), 10, true, new Font("Arial", f(9)), f(280));
////					DrawString(g, d.TrainerName, f(460), f(428), 14, false, new Font("Verdana", f(7)));
////				});
////			} else {
////				return RenderVendorEngCertTexts(image, d);
////				//					DrawString(g, d.fullname, 281, 210,14,false, new Font("Arial", 14));
////				//					DrawString(g, d.certName, 281, 310,10,true,new Font("Arial", 9), 280);
////				//					DrawString(g, d.trainerName, 460, 428,14,false, new Font("Verdana", 7));
////				//					DrawString(g, "Computer Training Center at Bauman MSTU " + d.date, 440, 483,4,false,new Font("Verdana", 6));
////			}
//		}

		public static Image RenderVendorEngCertTexts(
			Image image, 
			VendorEngCertData data) {
			var texts = data.GetTexts();

			return Draw(image, g => {
				foreach (var text in texts) {
					RenderText(g, text);
				}
			});
		}

		public static void RenderText(Graphics g, CertTextPosition p) {
			var fontStyle = p.Italic ? FontStyle.Italic : (p.Bold ? FontStyle.Bold : FontStyle.Regular);
			var font = new Font(p.Font, p.FontSize, fontStyle);
			var format = p.Center
				? new StringFormat {Alignment = StringAlignment.Center}
				: new StringFormat();
			if(p.Width > 0)
				g.DrawString(p.Text, font,
					new SolidBrush(Color.Black), new RectangleF(p.X, p.Y,p.Width,1000), format);
			else
				g.DrawString(p.Text, font, new SolidBrush(Color.Black), p.X,p.Y, format);
		}
 

		public static Image DrawGroupCertEngTextNew(Image image, string fullname, string certName, 
			string date, string number, bool hd) {
			return Draw(image, g => {
				if (hd) {
					DrawString(g, fullname, 1277, 650,14,false, new Font("Verdana", 48,FontStyle.Italic));
					DrawString(g, certName, 1277, 874,10,true,new Font("Verdana", 36,FontStyle.Italic), 1648);
					DrawString(g, date, 1388, 1554,8,false,new Font("Verdana", 24));
					DrawString(g, number, 1318, 1972,8,false,new Font("Verdana", 18));
				} else {
					DrawString(g, fullname, 310, 155,14,false, new Font("Verdana", 16,FontStyle.Italic));
					DrawString(g, certName, 310, 212,10,true,new Font("Verdana", 12,FontStyle.Italic), 400);
					DrawString(g, date, 337, 373,8,false,new Font("Verdana", 8));
					DrawString(g, number, 320, 475,8,false,new Font("Verdana", 6));
				}
			});
		}

		const string SpecialistName =
			"Образовательное частное учреждение\nдополнительного профессионального\nобразования «Центр компьютерного обучения\n«Специалист» учебно-научного центра при\nМГТУ им Н.Э. Баумана» ";

		public static Image DrawGroupCertText2016(Image image, string fullname, string certName, 
			decimal hours, string date, string number, char sex, string orgName, bool isCert) {
			return Draw(image, g => {
				var fontName =  "Palatino Linotype";
				var fontSmall = "Verdana"; // "Myriad Web Pro";
				var fontSize = 6;

				var learn = (sex == Sex.W ? "прошла" : "прошел") + (isCert ? " обучение в" : " повышение квалификации в");
				var center = 520;
				var fullOrgName = SpecialistName + orgName;
				var hoursText = "в объеме " + hours.ToString("n0") + " ак. часов";
				var title = isCert ? "СВИДЕТЕЛЬСТВО" : "УДОСТОВЕРЕНИЕ";
				var title2 = isCert ? "" : "О ПОВЫШЕНИИ КВАЛИФИКАЦИИ";
				var title3 = isCert ? "" : "Документ о квалификации\n";
				DrawStringByCenter(g, fullOrgName, 205, image.Width,8,center:200,isBold:true, font:fontName);
				DrawStringByCenter(g, "Город Москва\nДата выдачи " + date, 430, image.Width,fontSize,center:200,font:fontSmall);
				DrawStringByCenter(g, title, 80, image.Width,11,center:center,isBold:true, font:fontName);
				DrawStringByCenter(g, title2, 100, image.Width,7,center:center,isBold:true, font:fontName);
//				DrawStringByCenter(g, , 127, image.Width,fontSize,center:center,font:fontName);
				DrawStringByCenter(g, title3 + "Регистрационный номер " + number, 135, image.Width,fontSize,center:center, font:fontSmall);
				DrawStringByCenter(g, "Настоящее " + (title.ToLower()) +" подтверждает то, что", 165, image.Width,fontSize,center:center,font:fontSmall);
				DrawStringByCenter(g, fullname, 205, image.Width,10,center:center, isBold: true, font:fontName);
				DrawStringByCenter(g, learn + Environment.NewLine + orgName, 250, image.Width,fontSize,center:center, font:fontSmall);
				var text1 = isCert ? "по дополнительной общеобразовательной программе" : "по дополнительной профессиональной программе";
				DrawStringByCenter(g, text1 , 280, image.Width,fontSize,center:center,font:fontSmall);
				Text(g, certName, center - 150, 290, 300,8,fontSmall,60,true);
				DrawStringByCenter(g, hoursText, 360, image.Width,fontSize,center:center,font:fontSmall);
			});
		}

		public static Image DrawGroupCertPostgresText(Image image, string fullname) {
			return Draw(image, g => {
				DrawString(g, fullname, 35 , 275,14,false, new Font("Arial", 12));
			});
		}



		public static Image DrawGroupCertText(Image image, string fullname, string certName, 
			decimal hours, string date, string number, char sex, string orgName) {
			return Draw(image, g => {
				var fontName = "Myriad Web Pro";

				var learn = sex == Sex.W ? "обучалась" : "обучался";
				var get = sex == Sex.W ? "получила" : "получил";
				var text1 = _.List(date + " " + learn + " в",
					"Центре компьютерного обучения " + orgName, "при МГТУ им. Н.Э.Баумана по специализации:")
					.JoinWith(Environment.NewLine);
				var text2 = _.List("в объеме " + hours.ToString("n0") + " учебных часов и " + get + " знания", "и навыки в соответствии с программой курса")
					.JoinWith(Environment.NewLine);
				DrawStringByCenter(g, "№ " + number, 130, image.Width,8,center2:true,isBold:true, font:fontName);
				DrawStringByCenter(g, fullname, 215, image.Width,10,center2:true, isBold:true,font:fontName);
				Text(g, text1, 370, 250, 340,7,fontName);
				Text(g, certName, 390, 280, 300,8,fontName,(360 - 280),true);
				Text(g, text2, 390, 360, 300,7,fontName);
			});
		}


		private static void Text(Graphics g, string txt, int x, int y, int width,
			int fontSize, string font, int? height = null, bool isBold = false) {
			var color = Color.Black;
			var stringFormat = new StringFormat{Alignment = StringAlignment.Center};
			if (height.HasValue) {
				stringFormat.LineAlignment = StringAlignment.Center;
			}
			g.DrawString(txt, new Font(font, fontSize, isBold ? FontStyle.Bold : FontStyle.Regular),
				new SolidBrush(color),new RectangleF(x, y,width,height.GetValueOrDefault(1000)), 
				stringFormat);
		}

		public static Image DrawFullNameStringBest(string name, Image image, string fullname) {
			var height = 730;
			if(name == UserImages.Best2011)
				height = 560;
			if(name == UserImages.BestGraduate)
				height = 1350;
			if(name == UserImages.RealSpecialist)
				height = 1180;
			var resultImage = new Bitmap(image);
			using (var g = Graphics.FromImage(resultImage)) {
				var fontSize = 80;
				if(name.In(UserImages.Webinar2011, UserImages.Best2011))
					fontSize = 40;
				DrawStringByCenter(g, fullname, height, resultImage.Width, fontSize);

			}
			return resultImage;
		}




		private static void DrawStringByCenter(Graphics g, string fullname, int y, int width,
			int fontSize = 80, string font = "Times New Roman", Color color = new Color(), 
			bool isBold = false,
			bool center2 = false,
			bool widthLimit = false,
			int? center = null) {
			if (color.IsEmpty) {
				color = Color.Black;
			}
			var x = width/2;
			if (center2) x = x + x/2;
			if (center.HasValue) x = center.Value;
			if(widthLimit)
				g.DrawString(fullname, new Font(font, fontSize, isBold ? FontStyle.Bold : FontStyle.Regular),
					new SolidBrush(color),new RectangleF(20, y, width - 40, 0), 
					new StringFormat{Alignment = StringAlignment.Center});
			else
				g.DrawString(fullname, new Font(font, fontSize, isBold ? FontStyle.Bold : FontStyle.Regular),
					new SolidBrush(color),x,y, 
					new StringFormat{Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
		}
		private static void DrawString(Graphics g, string fullname, int x, int y, int fontSize, bool widthLimit,
			Font font = null, int width = 900, int height = 240, bool italic = true, 
			string fontName = "Times New Roman") {
			font = font ?? new Font(fontName, fontSize, italic ? FontStyle.Italic : FontStyle.Regular);
			if(widthLimit)
				g.DrawString(fullname, font,
					new SolidBrush(Color.Black), new RectangleF(x, y,width,height));
			else
				g.DrawString(fullname, font,
					new SolidBrush(Color.Black), x,y);
		}

		static SizeF GetSize(string text, Font font) {
			using (var img = new Bitmap(1, 1)) {
				using (var drawing = Graphics.FromImage(img)) {
					return drawing.MeasureString(text, font);
				}
			}

		}

		public static void WriteImage(String text, string path) {
			var font = new Font("Verdana", 30);
			var textSize = GetSize(text, font);
			var width = (int)textSize.Width + 100;
			var height = (int)textSize.Height + 100;
			using (var img = new Bitmap(width,height)) {
				using (var drawing = Graphics.FromImage(img)) {
					drawing.Clear(Color.White);
					drawing.DrawRectangle(new Pen(Color.Black),1,1,width-2,height-2);
					drawing.DrawString(text, font, new SolidBrush(Color.SlateBlue),
							new RectangleF(0,0,width, height),
				new StringFormat{Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}
							);
					drawing.Save();
					img.Save(path);
				}

			}
		}

	}
}
