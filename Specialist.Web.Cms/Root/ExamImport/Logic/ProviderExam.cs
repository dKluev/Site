using System.Collections.Generic;
using PrometricImport;

namespace PrometricGrabber
{
    public class ProviderExam
    {
        public List<string> Languages { get; set; }

        public int Price { get; set; }

        public string Name { get; private set; }

        public int Id { get; set; }

    	public string Number { get; private set; }

    	public ProviderExam(string name, string number = null) {
    		Name = name;
			Number = number ?? Name.Between(" - ");
            Languages = new List<string>();
    	}

    }
}