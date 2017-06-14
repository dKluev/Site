using System.Drawing.Printing;
using TuesPechkin;

namespace Specialist.Services {
	public static class PdfCreator {
//			new ThreadSafeConverter(
//				new RemotingToolset<PdfToolset>(
//					new StaticDeployment(System.AppDomain.CurrentDomain.BaseDirectory + "bin\\wkhtmltox.dll")));
		static IConverter converter =

			new ThreadSafeConverter(
				new RemotingToolset<PdfToolset>(
					new WinAnyCPUEmbeddedDeployment(new TempFolderDeployment())));

		public static object _lock = new object();


		public static byte[] Create(string html) {
			lock (_lock) {
			var document = new HtmlToPdfDocument {

				GlobalSettings = {
//					ProduceOutline = true,
					//					DocumentTitle = "Расписание",
//					PaperSize = PaperKind.A4, // Implicit conversion to PechkinPaperSize
//					Margins = {
//						All = 1.375,
//						Unit = Unit.Centimeters
//					}
				},
				Objects = { new ObjectSettings { HtmlText = html }}
			};
			return converter.Convert(document);
				
			}

		}
	}
}