using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;

namespace Console {
	public class CampaignGetItem {
		public long Id { get; set; }
	}


	public class TextAdGet {
		public string Href { get; set; }
	}

	public class AdGetItem {
		public long Id { get; set; }
		public long CampaignId { get; set; }
		public TextAdGet TextAd { get; set; }
	}

	public class AdsDirectService : DirectService {
		public AdsDirectService() : base("ads", "Ads") {}

		public List<AdGetItem> GetActive(List<long> CampaignIds) {
			return GetList<AdGetItem>("get", new {
				SelectionCriteria = new {
					States = new [] {"ON"},
					CampaignIds,
					Types = new [] {"TEXT_AD"}
				},
				FieldNames = new [] {"Id", "CampaignId"},
				TextAdFieldNames = new [] {"Href"}
			});
		}

		public void UpdateHrefs(List<AdGetItem> ads) {
			if (!ads.Any()) {
				return;
			}
			foreach (var row in ads.GetRows(1000)) {
				CallMethod("update", new {
					Ads = row.Select(x => new {Id = x.Id, TextAd = new {Href = x.TextAd.Href} })
				}, "UpdateResults");
			}
		}

		public static string GetQueryString(AdGetItem banner, bool isAdditionalLink = false) {
			var result = "?utm_source=yandex&utm_medium=cpc&utm_campaign=" + banner.CampaignId;
			if (isAdditionalLink)
				return result + AddParam(result, "utm_content", "yd_doplink");
			result += AddParam(result, "utm_content", banner.Id.ToString());
			return result;
		}

		public static string AddParam(string text, string name, string value) {
			return (text.IsEmpty() ? "?" : "&") + name + "=" + value;
		}

		private const string QueryStringText = @"\?.*";
		private static string GetNewHref(AdGetItem banner, string href, bool isAdditionalLink = false) {
			if(href.IsEmpty())
				return href;
			return RemoveQueryString(href) + GetQueryString(banner, isAdditionalLink);
		}


		static Regex regexp = new Regex(QueryStringText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static string RemoveQueryString(string text) {
			return regexp.Replace(text, "");
		}

		public void UpdateAllBannersHrefs() {
			var ids = new CampaignDirectService().GetActive().Select(x => x.Id).ToList();
			var ads = ids.GetRows(10).SelectMany(GetActive).ToList();
			var adsForUpdate = ads.Where(UpdateAdsHref).ToList();
			UpdateHrefs(adsForUpdate);
		}

		public static bool UpdateAdsHref(AdGetItem banner) {
			var href = banner.TextAd.Href;
			banner.TextAd.Href = GetNewHref(banner, href);
			return href != banner.TextAd.Href;
		}

	}


	public class CampaignDirectService : DirectService {
		public CampaignDirectService() : base("campaigns", "Campaigns") {}


		public List<CampaignGetItem> GetActive() {
			return GetList<CampaignGetItem>("get", new {
				SelectionCriteria = new {States = new [] {"ON"}},
				FieldNames = new [] {"Id"}
			});
		}
	}

	public class DirectService {

		public string Token = "AQBcn2oAACOOnGtuVFeLShyqW4JptQ7bDA";

		public string name = null;
		public string root = null;

		public DirectService(string name, string root) {
			this.name = name;
			this.root = root;
			this.ApiUrl = "https://api.direct.yandex.com/json/v5/" + name;
		}


		public List<T> GetList<T>(string method, object param) {
			return ToList<T>(CallMethod(method, param));
		}


		public List<T> ToList<T>(IEnumerable<JToken> array) {
			var serializer = new JsonSerializer();
			return array.Select(x => (T)serializer.Deserialize(new JTokenReader(x), typeof(T))).ToList();
		}

		public T GetItem<T>(string method, object param) {
			return CallMethod(method, param).Value<T>();
		}


		private bool SertificateValiation(object sender, X509Certificate certificate, X509Chain chain,
			SslPolicyErrors sslpolicyerrors) {
			return true;
		}

		private string GetJson(object postData) {
			return JsonConvert.SerializeObject(postData);
		}

		private string ApiUrl = null;

		public JToken CallMethod(string method, object @params, string resultName = null) {
			var data = new {
				method,
				@params,
			};
			var result = PostJson(ApiUrl, data, resultName);
			return result;
		}

		private JToken PostJson(string url, object postData, string resultName) {
			var request = WebRequest.Create(url);
			request.Headers.Add("Authorization", "Bearer " + Token);
			ServicePointManager.ServerCertificateValidationCallback = SertificateValiation;
			request.Method = "POST";
			request.ContentType = "application/json; charset=utf-8";
			request.Proxy = null;
			request.Timeout = 10*60*1000;
			((HttpWebRequest) request).KeepAlive = false;

			var json = GetJson(postData);

			using (var stream = new StreamWriter(request.GetRequestStream())) {
				stream.Write(json);
			}

			using (var response = (HttpWebResponse) request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream)) {
						var result = reader.ReadToEnd();
						if (result.Contains("error_code"))
							throw new Exception(result + " " + json);
						return ((JObject) JsonConvert.DeserializeObject(result))["result"][resultName ?? root];

					}
				}

			}

		}
	}
}