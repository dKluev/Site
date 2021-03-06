using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text.RegularExpressions;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Extension;
using Specialist.Web.Common.Html;

namespace Console {
	public static class MicrosoftGroupsFile {
				public static void GroupsWrite() {
			var context = new SpecialistDataContext();
			var types = _.List("��");
			var courseNames = context.Courses.Where(c => 
				types.Contains( c.AuthorizationType_TC)  && c.Course_TC.StartsWith("�") && c.IsActive && c.IsTrack != true)
				.Select(x => new { x.Course_TC, x.Name, x.NameOfficialEn, x.UrlName}).ToList();
					var courses = courseNames.Select(x => x.Course_TC);
			var prices = context.PriceViews.Where(x => x.Track_TC == null && courses.Contains(x.Course_TC))
				.Select(x => new {x.PriceType_TC, x.Course_TC, x.PriceType.PriceListType_TC}).ToList();

			var groups = context.Groups.Where(x => courses.Contains(x.Course_TC)).NotBegin()
				.Where(x => x.DateEnd != null)
				.Select(x => new {x.Course_TC, x.DateBeg, 
					x.DateEnd, x.TimeBeg, x.TimeEnd}).OrderBy(x => x.DateBeg).ToList();

			var distanceCourses = prices.Where(x => x.PriceType_TC == PriceTypes.Webinar)
				.Select(x => x.Course_TC).Distinct().ToList();
			var fulltimeCourses = prices.Where(x => 
				 !PriceTypes.IsWebinar(x.PriceType_TC))
				.Select(x => x.Course_TC).Distinct().ToList();
			var data = groups.Select(k => {
				var courseTC = k.Course_TC.Trim();
				var course = courseNames.First(z => z.Course_TC == courseTC);
				var priceTypes = new List<string>();
				if (fulltimeCourses.Contains(courseTC)) {
					priceTypes.Add("Instructor-led");
				}
				if (distanceCourses.Contains(courseTC)) {
					priceTypes.Add("Virtual Instructor-led");
				}
				var culture = CultureInfo.CreateSpecificCulture("en-US");
				return _.List(
					"1050468",
					"516392",
					Regex.Replace(courseTC, @"[^\d]", ""),
					"",
					priceTypes.JoinWith(" and "),
					"Russian",
//					k.DateBeg.Value.ToString("MM\\/dd\\/yyyy"),
//					k.DateEnd.Value.ToString("MM\\/dd\\/yyyy"),
					k.DateBeg.Value.ToString("dd.MM.yyyy"),
					k.DateEnd.Value.ToString("dd.MM.yyyy"),
					k.TimeBeg.Value.ToString("h:mm tt",culture),
					k.TimeEnd.Value.ToString("h:mm tt",culture),
					"","","",
					"Moscow",
					"http://www.specialist.ru/course/" + course.UrlName.ToLower(),
					"",
					"Y"
					)
					.JoinWith("\t");
			});
			File.WriteAllLines("result.txt", data);
		}
		public static string ToText(this bool b) {
			return b ? "������" : "����";
			//return b.ToString().ToUpper();
		}
		public static readonly List<string> GetMicrosoftCourses = new List<string> {
			"50153",
"2072",
"2789",
"50292",
"827",
"2553",
"2552",
"2554",
"80246",
"80002",
"2542",
"2549",
"2957",
"50047",
"50064",
"50277",
"2311",
"2544",
"50260",
"2547",
"50099",
"50101",
"2710",
"50192",
"NAV09-ILT-APP-RU",
"2812",
"2813",
"50048",
"50050",
"50051",
"50049",
"10325",
"6434",
"50075",
"2016",
"50015",
"2557",
"50212",
"50254",
"2730",
"1905",
"NAV09-ILT-BI-RU",
"50150",
"50152",
"50032",
"50031",
"50392",
"50397",
"50033",
"10174",
"50322",
"6429",
"6421",
"6426",
"6427",
"6425",
"6428",
"5116",
"6419",
"10135",
"2541",
"2548",
"2956",
"2543",
"2546",
"50062",
"50060",
"50190",
"50191",
"2126",
"50431",
"2030",
"50193",
"8974",
"8912",
"50003",
"2014",
"50030",
"50093",
"50094",
"2821",
"4358",
"50371",
"2934",
"2185",
"2159",
"2143",
"2731",
"6331",
"828",
"2520",
"2362",
"5058",
"6418",
"5105",
"50103",
"50399",
"2794",
"5054",
"5053",
"1561",
"10231",
"2786",
"2282",
"2797",
"2150",
"6436",
"6437",
"6435",
"2796",
"2795",
"10233",
"2074",
"50401",
"2008",
"2788",
"1573",
"2782",
"2781",
"2830",
"2783",
"2395",
"50400",
"2350",
"2933",
"10265",
"2157",
"2432",
"2366",
"6066",
"52310",
"2565",
"2555",
"2556",
"6067",
"2300",
"50350",
"50197",
"2310",
"10264",
"50057",
"10262",
"10263",
"2524",
"6214",
"50083",
"2015",
"50100",
"50245",
"50244",
"50268",
"50251",
"50359",
"50370",
"50432",
"50162",
"1913",
"50194",
"2207",
"2209",
"2210",
"80033",
"8969",
"8531",
"50299",
"NAV09-ILT-FIN-RU",
"50020",
"50222",
"2810",
"6424",
"6420",
"2363",
"2562",
"4360",
"6232",
"2779",
"6449",
"2276",
"2153",
"50412",
"50403",
"50004",
"50435",
"2576",
"6215",
"2823",
"2820",
"6446",
"6438",
"6445",
"2785",
"5178",
"50214",
"5177",
"6234",
"6235",
"2791",
"2792",
"2793",
"6236",
"5179",
"6064",
"2215",
"10324",
"2400",
"10215",
"6423",
"6422",
"2295",
"2272",
"2093",
"50213",
"50382",
"50360",
"50357",
"50402",
"50017",
"2550",
"2087",
"2171",
"2152",
"5060",
"5061",
"50009",
"2591",
"2840",
"2277",
"50087",
"8911",
"50065",
"50028",
"6292",
"2285",
"5115",
"5117",
"80141",
"2609",
"50046",
"5047",
"50295",
"50263",
"NAV09-ILT-INTRO-RU",
"2717",
"6367",
"2667",
"4994",
"50238",
"2559",
"10267",
"50219",
"2500",
"50225",
"NAV09-ALT-INV-RU",
"50002",
"50058",
"50226",
"2199",
"50207",
"50208",
"50209",
"50135",
"50136",
"50123",
"50124",
"50125",
"50133",
"50132",
"50134",
"50129",
"50130",
"50131",
"50126",
"50127",
"50128",
"50138",
"50139",
"50140",
"50066",
"2275",
"6231",
"2780",
"6450",
"2606",
"2605",
"2274",
"2273",
"2299",
"6432",
"6431",
"4356",
"4357",
"50235",
"7197",
"5049",
"50023",
"2596",
"2855",
"2287",
"50255",
"NAV09-ALT-MANI-RU",
"NAV09-ALT-MANII-RU",
"80290",
"50018",
"50250",
"1303",
"50090",
"50201",
"50067",
"80294",
"80296",
"80142",
"50294",
"50378",
"50379",
"50422",
"50434",
"50438",
"50366",
"50196",
"50107",
"50237",
"50446",
"50442",
"50390",
"50266",
"50016",
"1846",
"50206",
"50068",
"50216",
"50005",
"2151",
"50414",
"8529",
"8556",
"8525",
"8530",
"8526",
"50141",
"2801",
"2802",
"2803",
"2804",
"2808",
"10175",
"7033",
"2433",
"2283",
"2090",
"50012",
"50011",
"5051",
"50024",
"50232",
"50010",
"50404",
"2597",
"50273",
"2278",
"6294",
"6430",
"6451",
"50217",
"2732",
"10508",
"2279",
"2092",
"50433",
"50025",
"1567",
"5131",
"5926",
"50040",
"50041",
"50042",
"50043",
"50044",
"50045",
"50220",
"10534",
"10232",
"50001",
"50086",
"2073",
"10266",
"2657",
"50146",
"50166",
"2124",
"2558",
"2389",
"2373",
"2415",
"2349",
"4995",
"6368",
"2663",
"50411",
"50417",
"50027",
"2071",
"5050",
"50223",
"50195",
"80291",
"2439",
"80292",
"50224",
"80293",
"50227",
"50228",
"50026",
"50149",
"50204",
"50205",
"50429",
"50351",
"50352",
"50353",
"50354",
"80297",
"50145",
"50278",
"50279",
"50102",
"50006",
"50008",
"2262",
"2261",
"2563",
"2564",
"5118",
"50105",
"50252",
"50231",
"50409",
"50022",
"NAV09-ALT-TRD-RU",
"50063",
"2790",
"2011",
"10533",
"2784",
"50267",
"50229",
"50013",
"2297",
"2208",
"2694",
"6417",
"10337",
"6416",
"3938",
"6158",
"50311",
"10159",
"50007",
"2123",
"50383",
"80298",
"50257",
"7031",
"2640",
"6317",
"50098",
"80240",
"2052",
"50419",
"50427",
"50059",
"6464",
"6463",
"6461",
"6460",
"6462",
"50189",
"8910",
"50389",
"50280",
"80164",
"80163",
"80301",
"80299",
"80165",
"80289",
"2364",
"6156",
"6157",
"50155",
"50154",
"50218",
"50218",
"50321",
"50021",
"50203",
"50151",
"50037",
"50202",
"50331",
"50439",
"2778",
"50061"

	};
	}
}