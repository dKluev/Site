using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.Common.ViewModel
{
    public class ErrorVM:IViewModel
    {
        public int StatusCode { get; set; }

        public string AspxErrorPath { get; set; }

        public ErrorVM(int statusCode, string aspxErrorPath) {
            StatusCode = statusCode;
            AspxErrorPath = aspxErrorPath;
        }

        public string Title {
            get { return "Ошибка " + StatusCode; }
        }

        public bool IsNotFound {
            get {
                return StatusCode == 404;}
        }
    }
}