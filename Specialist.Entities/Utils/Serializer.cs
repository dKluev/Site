using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Specialist.Entities.Utils {
	public static class Serializer {
		public static void Serialize<T>(string filename, T objectToSerialize) {
			using (var stream = File.Open(filename, FileMode.Create)) {
				var bFormatter = new BinaryFormatter();
				bFormatter.Serialize(stream, objectToSerialize);
			}
		}

		public static T Deserialize<T>(string filename) {
			using (var stream = File.Open(filename, FileMode.Open)) {
				var bFormatter = new BinaryFormatter();
				return (T) bFormatter.Deserialize(stream);
			}
		}
	}
}