using System;
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.ViewModel {
    public class TrainerInfoVM {
        
        public Employee Trainer { get; set; }

        public bool HasPhoto { get; set; }

        public bool HasDescription { get; set; }

        public int ResponseCount { get; set; }

        public string Link { get; set; }

        public int Groups { get; set; }
    }
}