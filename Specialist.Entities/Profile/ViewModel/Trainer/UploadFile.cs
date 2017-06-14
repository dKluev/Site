using System;
using System.IO;

namespace Specialist.Entities.Profile.ViewModel
{
    public class UploadFile
    {
        public string Name { get; set; }

        public int ContentLength { get; set; }

        public bool IsEmpty
        {
            get { return ContentLength == 0; }
        }

        public Stream Stream { get; set; }
    }
}