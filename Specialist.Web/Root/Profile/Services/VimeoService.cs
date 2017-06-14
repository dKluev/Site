using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Targets.Wrappers;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Web.Root.Profile.Services {
	public class VimeoService {

		public class Video {
//			public class Embed {
//				public string html { get; set; }
//			}
//			public Embed embed { get; set; }
			public string uri { get; set; }
			public string name { get; set; }

			public string GetId() {
				return uri.Remove("/videos/");
			}
		}

		public static Dictionary<string, List<string>> albumVideosCache = new Dictionary<string, List<string>>();
		public List<string> Tokens = _.List( 
			"cd491777606d12745949535d432898d5"
//			"47c41a8b879ac06770edf21e667e6537",
//			"659436681f9e7d3a8ce91f2f2765858c",
//			"aff13fc45a93fbed0d4db0947ef12edb",
//			"6996f32e03b6133ddd641207056e17d5",
//			"6cd897254c25359453f82990ffbe5c26",
//			"ae226a6fddc26c429935bbba1c108b31",
//			"c0b0a10cb450b3b6737e2894c34fdd6b",
//			"ef531c28d33a57af2d614ce0c2adc667",
//			"2a539451f00798bd706d98c1a9b626ae",
//			"4ece678bcb6ef274b75eb567fef090e6"
			); 
//		public const string Token = Token3;
		private static int tokenIndex = 0;

		public string GetToken() {
			return Tokens[tokenIndex ++ % Tokens.Count];
		}
		public const string Root = "https://api.vimeo.com";
		static readonly object _lock = new object();
		public List<string> GetVideos(string albumId) {
			var r = albumVideosCache.GetValueOrDefault(albumId);
			if (r == null) {
				lock (_lock) {
					r = ToList<Video>(PostJson(Root + "/me/albums/{0}/videos?per_page=100&fields=uri&sort=alphabetical".FormatWith(albumId)))
						.Select(x => x.GetId()).ToList();
					albumVideosCache[albumId] = r;
				}
			}
			return r;
		} 

//		public List<string> GetAllVideos() {
//			return Enumerable.Range(1,70).SelectMany(i => ToList<Video>(PostJson(Root + "/me/videos?fields=uri&per_page=100&page=" + i)).Select(x => x.GetId())).ToList();
//		}

		public void PutPresets() {
			var ids = File.ReadAllLines("vimeo.txt").ToList();
			var i = 0;
			foreach (var id in ids.ToList()) {
				PostJson(Root + "/videos/{0}/presets/120321600".FormatWith(id), "PUT");
				ids.Remove(id);
				Console.WriteLine(ids.Count);
				File.WriteAllLines("vimeo.txt", ids);
				Thread.Sleep(300);
			}
		}

		private string GetJson(object postData) {
			return JsonConvert.SerializeObject(postData);
		}


		public List<T> ToList<T>(IEnumerable<JToken> array) {
			var serializer = new JsonSerializer();
			return array.Select(x => (T)serializer.Deserialize(new JTokenReader(x), typeof(T))).ToList();
		}

		private bool SertificateValiation(object sender, X509Certificate certificate, X509Chain chain,
			SslPolicyErrors sslpolicyerrors) {
			return true;
		}

		private JToken PostJson(string url, string method = "GET") {
			var request = WebRequest.Create(url);
			request.Headers.Add("Authorization", "Bearer " + GetToken());
			ServicePointManager.ServerCertificateValidationCallback = SertificateValiation;
			request.Method = method;
			request.Proxy = null;
			request.Timeout = 10*60*1000;
			((HttpWebRequest) request).KeepAlive = false;


			using (var response = (HttpWebResponse) request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream)) {
						var result = reader.ReadToEnd();
						if (result.Contains("error_code"))
							throw new Exception(result);
						if (result.IsEmpty()) {
							return null;
						}
						return ((JObject) JsonConvert.DeserializeObject(result))["data"];

					}
				}

			}

		}
	}
}
