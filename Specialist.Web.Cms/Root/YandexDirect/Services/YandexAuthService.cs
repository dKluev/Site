using System.CodeDom;
using Newtonsoft.Json;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Console {
	public class YandexAuthService {
		public const string appId = "e35380a30da640d5b8858584fb154bae";
		public const string appPassword = "04a8e1e561974deb91cfff274a8fcd8c";
		public const string authUrl = "https://oauth.yandex.ru/authorize?response_type=code&client_id=" + appId;
		public const string tokenUrl = "https://oauth.yandex.ru/token";
		public const string SessionKey = "YandexTokenSessionKey";

		public string GetToken(string code) {
			var param = new {
				grant_type = "authorization_code",
				code = code,
				client_id = appId,
				client_secret = appPassword,
			};
			var result = HttpUtils.Post(tokenUrl, param);
			var token = JsonConvert.DeserializeObject(result).As<dynamic>().access_token;
			return token;
		}
	}
}