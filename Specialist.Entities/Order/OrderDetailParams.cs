using Newtonsoft.Json;

namespace Specialist.Entities.Context {
	public class OrderDetailParams {

		[JsonProperty("T")]
		public byte Type { get; set; }

		[JsonProperty("L")]
		public byte Lang { get; set; }

		[JsonIgnore]
		public bool IsPaper {
			get { return Type == Tests.Consts.TestCertType.Papper; }
		}
		[JsonIgnore]
		public bool IsImage {
			get { return Type == Tests.Consts.TestCertType.Image; }
		}
		 
	}
}