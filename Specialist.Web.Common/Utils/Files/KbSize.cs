namespace Specialist.Web.Common.Html {
    public class KbSize {
        public int Value { get; set; }

        public KbSize(int value) {
            Value = value;
        }

        static public implicit operator KbSize(int value)
        {
            return new KbSize(value);
        }

        public double MBytes {
            get {
                return Value/1000.0;
            }
        }

        public int Bytes {
            get {
                return Value*1024;
            }
        }
    }
}