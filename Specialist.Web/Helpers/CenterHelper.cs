using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Context;
using Specialist.Web.Common.Html;
using Specialist.Web.Entities.Center;
using System.Linq;

namespace Specialist.Web.Helpers
{
    public static class CenterHelper
    {
      /*  public static void ComplexMap(this ViewPage<Complex> viewPage)
        {
            var filename = "~/Content/Center/Map/" +
                           viewPage.Model.Complex_ID +".html";
            if(File.Exists(viewPage.Server.MapPath(filename)))
                viewPage.Response.WriteFile(filename);
        }*/

        public static SubSectionsWithNoteVM GetComplexes(this CityVM cityVM) {
            var complexes = cityVM.Complexes.Select(c => 
                new SubSectionWithNote(c, c.Address.Tag("p")))
                .ToList();

            return new SubSectionsWithNoteVM{List = complexes, 
                Title = "Комплексы",
                SmallImage = true
            };
        }

        public static string ComplexImages(this Complex model)
        {
            var complexImagePath = Urls.ImageFolder + "Complex/Image/" +
                model.UrlName;
            var imageDirectory = Urls.SysRoot +complexImagePath ;
/*
            var imageDirectory = 
                viewPage.Server.MapPath(ComplexImageFolder + 
                viewPage.Model.Complex_ID);
*/
            if(!Directory.Exists(imageDirectory))
                return null;
            var files = (new DirectoryInfo(imageDirectory)).GetFiles("*.jpg");
            if (files.Length == 0)
                return null;
            var result = string.Empty;
            foreach (var file in files)
            {
                result += HtmlControls.Image( Urls.ContentRoot(complexImagePath 
                    + "/" + file.Name), "").ToString()
                    .Tag("p");
            }
            return result;

        }

       /* public static string EmployeeImage(this ViewPage<Complex> viewPage)
        {
            if(viewPage.Model.Admin == null)
                return null;

            var filename = "/Content/Image/Center/Employee/" +
                           viewPage.Model.Admin.Employee_ID + ".gif";
            if (File.Exists(viewPage.Server.MapPath(filename)))
                return HtmlControls.Image(filename);
            return null;
        }*/
    }
}