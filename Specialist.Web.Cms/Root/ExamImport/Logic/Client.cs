using System.Collections.Generic;

namespace PrometricGrabber
{
    public class Client
    {
        public int Id { get; set; }



        public string Name { get; set; }

        public Dictionary<int, string> Programs { get; set; 
        }
        public Client()
        {
            Programs = new Dictionary<int, string>();
        }
    }
}