using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Services.Interface;
using Specialist.Web.Util.Captcha;

namespace Specialist.Web.Controllers
{
    public class ChartController : Controller
    {
        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        public ActionResult Captcha() {
            var ci = new CaptchaImage(UserSettingsService.CaptchaText, 200, 50);
            var image = new MemoryStream();
            ci.Image.Save(image, ImageFormat.Png);
            ci.Dispose();
            image.Seek(0, SeekOrigin.Begin);
            return File(image, @"image/png");
        }


      /*  public ActionResult ManPercent(byte percent)
        {
            if (percent == 100)
                percent = 95;
            if (percent == 0)
                percent = 5;
            var chart = new Chart();

            chart.Height = 160;

            chart.Width = 200;

            chart.ImageType = ChartImageType.Png;

            chart.Palette = ChartColorPalette.Excel;

            chart.RenderType = RenderType.BinaryStreaming;

            chart.AntiAliasing = AntiAliasingStyles.All;

            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;

            double[] yValues = { percent, 100 - percent };
            string[] xValues = { "Мужчины", "Женщины" };
            var series = chart.Series.Add("Default");
            series.Points.DataBindXY(xValues, yValues);
            series["PieLabelStyle"] = "Disabled";

            var colors = new[]
            {
                Color.FromArgb(200, 89, 154, 241), 
                Color.FromArgb(200, 248, 187, 90), 
            };
            for (int i = 0; i < colors.Length; i++)
            {
                series.Points[i].Color = colors[i];
            }

            series.ChartType = SeriesChartType.Doughnut;
            var chartArea = chart.ChartAreas.Add("Default");
            chartArea.Area3DStyle.Enable3D = true;
            chartArea.Area3DStyle.Inclination = 60;

            var legend = chart.Legends.Add("Default");
            legend.Docking = Docking.Bottom;
            legend.MaximumAutoSize = 20;
            legend.IsTextAutoFit = true;
            legend.AutoFitMinFontSize = 5;

            using (var ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);

                return File(ms.GetBuffer(), @"image/png");
            }

        }*/
    }
}