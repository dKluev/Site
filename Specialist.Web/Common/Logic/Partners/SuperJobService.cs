using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;
using System.Linq;
using Specialist.Services.Utils;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Services.Common {
	public class SuperJobService {

		[Dependency]
		public IRepository<Profession> ProfessionService { get; set; }
		
		[Cached]
		public virtual Dictionary<int, List<int>> Categories() {
			return ProfessionService.GetAll(x => x.IsActive)
				.ToDictionary(x => x.Profession_ID,
					y => (y.SuperJobID ?? "").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
						.Select(StringUtils.ParseInt)
						.Where(x => x.HasValue)
						.Select(x => NewCategoriesMapping.GetValueOrDefault(x.Value)).Where(x => x > 0).ToList());
		}

		  public static string GetHtml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = true;
            request.Proxy = null;

	
			  using (var response = request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream,
						Encoding.GetEncoding("windows-1251"))) {
						var htmlText = reader.ReadToEnd();
				
						return htmlText;
					}
				}
			}
          
        }


		public const string VacancyUrl =
			"http://www.superjob.ru/xml/?required_fields=cat1,cat2,cat3,cat4,cat5%2Cprofession%2Cpayment_from%2Ctown%2Cfirm_name&action=view_vacancies&t[]=4&t[]=14&t[]=55&t[]=73&rows_on_page=20&catalogues=";
		[Cached(24, true)]
		public virtual List<SuperJobVacancy> GetVacancies() {
#if(DEBUG)
                return new List<SuperJobVacancy>();
#endif
			var allCategories = Categories().SelectMany(c => c.Value).Distinct()
				.ToList();

			var result = new List<SuperJobVacancy>();
			try {
				foreach (var categoryId in allCategories) {
					var xml = XDocument.Parse(GetHtml(VacancyUrl + categoryId));
					if(xml.Root.Element("errors") != null)
						continue;
					result.AddRange(
					xml.Root.Element("vacancies").Elements().Select(e => 
					new SuperJobVacancy(
						e.Element("profession").GetOrDefault(x => x.Value), 
						e.Element("firm_name").GetOrDefault(x => x.Value), 
						e.Element("backlink").GetOrDefault(x => x.Value), 
						e.Element("payment_from").GetOrDefault(x => x.Value), 
						e.Element("town").GetOrDefault(x => x.Value), 
						e.Element("catalogue") == null 
						? _.List(categoryId) 
						: e.Element("catalogue").Elements()
						.Select(item => int.Parse(item.Attribute("id").Value)).ToList())) );
					
				}
		
				return result;
			}
			catch (Exception ex) {
				Logger.Exception(ex, "superjob xml");
				return new List<SuperJobVacancy>();
			}

		}


		public List<SuperJobVacancy> GetVacancies(List<int> professionIds, 
			string cityTC) {
			return GetVacancies().Where(v => v.CategoryIds
				.SelectMany(GetProfessions).Any(professionIds.Contains))
				.Where(v => cityTC == null || v.CityTC == cityTC)
				.Take(5).ToList();
		}

		public List<int> GetProfessions(int categoryId) {
			return Categories().Where(c => c.Value.Contains(categoryId))
				.Select(x => x.Key).ToList();
		}

		private static Dictionary<int,int> NewCategoriesMapping =  new Dictionary<int, int> {

#region list
		{5, 1},
{106, 2},
{1855, 3},
{1134, 4},
{1361, 4},
{1360, 4},
{1652, 4},
{525, 4},
{526, 5},
{524, 5},
{107, 6},
{510, 6},
{950, 6},
{108, 7},
{523, 8},
{102, 8},
{506, 8},
{1869, 8},
{2075, 8},
{1416, 9},
{509, 9},
{109, 9},
{1245, 10},
{4, 11},
{61, 12},
{94, 13},
{1259, 13},
{2029, 13},
{984, 13},
{97, 14},
{1262, 14},
{96, 14},
{1288, 14},
{2108, 14},
{805, 14},
{98, 14},
{1258, 14},
{2024, 14},
{836, 16},
{762, 17},
{1873, 17},
{1768, 19},
{860, 22},
{313, 23},
{312, 28},
{298, 29},
{100, 31},
{1414, 31},
{508, 32},
{507, 32},
{547, 33},
{583, 34},
{552, 35},
{549, 35},
{1250, 35},
{550, 36},
{2060, 37},
{1878, 37},
{561, 38},
{1294, 39},
{569, 41},
{1253, 41},
{1353, 41},
{581, 41},
{1354, 41},
{982, 41},
{554, 42},
{553, 42},
{1252, 42},
{1789, 42},
{2058, 42},
{2055, 42},
{1249, 42},
{1247, 42},
{557, 42},
{1355, 42},
{1750, 42},
{1795, 43},
{1212, 44},
{1805, 45},
{1774, 47},
{559, 48},
{560, 48},
{2054, 48},
{1337, 48},
{1248, 48},
{1285, 48},
{2326, 48},
{2074, 48},
{2084, 48},
{2059, 48},
{2056, 48},
{2057, 48},
{2028, 48},
{2027, 48},
{584, 48},
{1251, 51},
{2290, 51},
{573, 51},
{2272, 51},
{1336, 52},
{1334, 53},
{2204, 53},
{2321, 53},
{2325, 53},
{2053, 53},
{1335, 53},
{591, 56},
{1851, 57},
{565, 57},
{589, 57},
{1466, 60},
{1468, 60},
{566, 60},
{558, 60},
{582, 60},
{590, 60},
{1799, 63},
{1801, 64},
{1800, 64},
{1014, 65},
{132, 72},
{592, 72},
{6, 76},
{111, 76},
{110, 76},
{314, 77},
{649, 78},
{1792, 78},
{834, 79},
{119, 80},
{122, 80},
{118, 81},
{650, 81},
{113, 82},
{114, 82},
{1794, 82},
{124, 83},
{1923, 83},
{1417, 84},
{115, 84},
{120, 84},
{126, 84},
{1104, 86},
{11, 86},
{1115, 87},
{1118, 87},
{1143, 87},
{1188, 87},
{1187, 87},
{1117, 87},
{1126, 87},
{640, 87},
{1151, 87},
{1144, 87},
{1156, 87},
{1135, 87},
{1189, 87},
{1757, 87},
{1679, 87},
{1119, 87},
{1470, 87},
{1674, 87},
{1675, 87},
{1116, 88},
{1680, 88},
{1985, 88},
{169, 88},
{1260, 88},
{1256, 88},
{1257, 88},
{1287, 88},
{1758, 88},
{1255, 88},
{714, 88},
{1122, 88},
{1472, 88},
{1672, 88},
{1673, 88},
{1753, 89},
{1762, 89},
{1763, 89},
{1764, 89},
{1756, 89},
{411, 89},
{1513, 89},
{412, 89},
{830, 89},
{1570, 89},
{638, 90},
{645, 90},
{1681, 90},
{643, 90},
{1761, 90},
{1157, 90},
{1321, 90},
{1323, 90},
{644, 90},
{1474, 90},
{1322, 90},
{1676, 90},
{832, 90},
{786, 90},
{785, 90},
{1186, 90},
{787, 90},
{1130, 92},
{1131, 92},
{637, 92},
{168, 92},
{166, 92},
{1363, 92},
{171, 92},
{833, 92},
{170, 92},
{789, 92},
{1759, 92},
{790, 92},
{1876, 92},
{1362, 92},
{1105, 94},
{1106, 94},
{1107, 94},
{1108, 94},
{1110, 94},
{1113, 94},
{1109, 94},
{1111, 94},
{1147, 94},
{1678, 94},
{1476, 94},
{1677, 94},
{1760, 94},
{1771, 94},
{1146, 94},
{1141, 94},
{1142, 94},
{1114, 94},
{167, 95},
{420, 95},
{901, 95},
{653, 95},
{1261, 95},
{511, 95},
{415, 96},
{414, 96},
{2034, 96},
{2273, 98},
{1576, 98},
{2, 100},
{1412, 102},
{81, 103},
{2012, 103},
{727, 106},
{1211, 115},
{86, 121},
{87, 121},
{93, 133},
{518, 133},
{519, 133},
{520, 134},
{1511, 134},
{17, 136},
{614, 136},
{229, 137},
{1527, 137},
{226, 137},
{900, 137},
{842, 138},
{1525, 138},
{844, 138},
{846, 138},
{845, 138},
{850, 138},
{851, 138},
{1788, 138},
{1787, 138},
{1994, 139},
{228, 139},
{2332, 140},
{2291, 140},
{1602, 140},
{1868, 140},
{2236, 140},
{1852, 140},
{1595, 140},
{1848, 140},
{1588, 140},
{1594, 140},
{2329, 140},
{2237, 140},
{1599, 140},
{1824, 140},
{2104, 140},
{2245, 140},
{1591, 140},
{1587, 140},
{1870, 140},
{1590, 140},
{1589, 140},
{2239, 140},
{1597, 140},
{2076, 140},
{1592, 140},
{1853, 140},
{1601, 140},
{1605, 140},
{1990, 140},
{1596, 140},
{1065, 140},
{1585, 140},
{2238, 140},
{1888, 140},
{1604, 140},
{1593, 140},
{1586, 140},
{1600, 140},
{2087, 140},
{1598, 140},
{1874, 140},
{1854, 140},
{1609, 140},
{2040, 140},
{231, 140},
{778, 142},
{1624, 142},
{1626, 142},
{1625, 142},
{1579, 142},
{1490, 142},
{2299, 142},
{2300, 142},
{1297, 142},
{1739, 142},
{1700, 142},
{1303, 142},
{783, 142},
{780, 142},
{782, 142},
{1346, 142},
{1861, 142},
{781, 142},
{784, 142},
{1603, 143},
{800, 144},
{693, 145},
{1607, 145},
{1608, 145},
{227, 145},
{1563, 145},
{606, 148},
{1529, 148},
{613, 148},
{222, 148},
{2013, 148},
{223, 148},
{224, 148},
{2042, 148},
{2039, 148},
{897, 148},
{225, 148},
{911, 151},
{2110, 152},
{2119, 152},
{2121, 152},
{2118, 152},
{2120, 152},
{2124, 152},
{2128, 152},
{2125, 152},
{2126, 152},
{2127, 152},
{2122, 152},
{2131, 152},
{2129, 152},
{2123, 152},
{2130, 152},
{2132, 152},
{2133, 152},
{2113, 152},
{2186, 152},
{2191, 152},
{2136, 152},
{2134, 152},
{2188, 152},
{2190, 152},
{2135, 152},
{2187, 152},
{2137, 152},
{2138, 152},
{2189, 152},
{2139, 152},
{2114, 152},
{2142, 152},
{2141, 152},
{2140, 152},
{2143, 152},
{2111, 152},
{2147, 152},
{2146, 152},
{2145, 152},
{2144, 152},
{2148, 152},
{2112, 152},
{2193, 152},
{2192, 152},
{2151, 152},
{2194, 152},
{2195, 152},
{2149, 152},
{2152, 152},
{2150, 152},
{2153, 152},
{2115, 152},
{2196, 152},
{2161, 152},
{2154, 152},
{2158, 152},
{2162, 152},
{2156, 152},
{2159, 152},
{2160, 152},
{2155, 152},
{2157, 152},
{2117, 152},
{2163, 152},
{2164, 152},
{2171, 152},
{2197, 152},
{2198, 152},
{2173, 152},
{2170, 152},
{2172, 152},
{2174, 152},
{2175, 152},
{2169, 152},
{2167, 152},
{2178, 152},
{2179, 152},
{2166, 152},
{2165, 152},
{2180, 152},
{2177, 152},
{2168, 152},
{2199, 152},
{2181, 152},
{1038, 153},
{1132, 154},
{915, 154},
{912, 154},
{914, 154},
{1356, 154},
{917, 154},
{916, 154},
{922, 154},
{918, 154},
{919, 154},
{921, 154},
{1791, 154},
{1359, 154},
{1773, 155},
{1034, 159},
{1035, 159},
{1040, 159},
{1767, 159},
{1745, 159},
{1532, 159},
{1606, 159},
{920, 162},
{2240, 162},
{1867, 163},
{1766, 163},
{2099, 170},
{913, 173},
{923, 173},
{1568, 173},
{1847, 173},
{1893, 176},
{1903, 176},
{1898, 176},
{1900, 176},
{1899, 176},
{1902, 176},
{1901, 176},
{1905, 176},
{1894, 176},
{1906, 176},
{1897, 176},
{1895, 176},
{1904, 176},
{1896, 176},
{1907, 176},
{601, 182},
{1066, 183},
{1882, 183},
{1068, 183},
{951, 184},
{760, 185},
{761, 185},
{1241, 186},
{1240, 186},
{1238, 186},
{1786, 187},
{1880, 188},
{1096, 189},
{658, 190},
{664, 190},
{665, 190},
{480, 190},
{666, 190},
{1077, 191},
{1080, 191},
{1533, 191},
{1079, 191},
{1081, 191},
{1078, 192},
{2213, 193},
{657, 194},
{660, 194},
{661, 194},
{1531, 195},
{662, 195},
{13, 197},
{187, 199},
{188, 199},
{1682, 199},
{646, 199},
{1683, 199},
{189, 199},
{647, 199},
{648, 199},
{196, 199},
{975, 199},
{186, 201},
{2078, 201},
{799, 201},
{194, 201},
{2244, 201},
{888, 201},
{497, 201},
{1879, 201},
{1685, 201},
{190, 201},
{191, 201},
{193, 201},
{192, 201},
{1290, 201},
{2015, 201},
{940, 201},
{1569, 201},
{195, 201},
{1912, 201},
{14, 202},
{199, 202},
{200, 202},
{2202, 202},
{679, 202},
{678, 202},
{197, 202},
{1139, 202},
{1140, 202},
{202, 202},
{19, 205},
{1153, 206},
{303, 207},
{304, 207},
{2082, 208},
{1866, 208},
{1988, 209},
{1803, 210},
{302, 210},
{2083, 212},
{2073, 214},
{2080, 215},
{1370, 215},
{2081, 215},
{241, 216},
{495, 216},
{1889, 216},
{246, 216},
{839, 216},
{840, 216},
{1289, 216},
{1684, 216},
{1292, 216},
{493, 216},
{1969, 216},
{247, 216},
{841, 216},
{494, 216},
{248, 216},
{249, 216},
{1802, 217},
{1986, 218},
{1552, 220},
{306, 220},
{305, 220},
{22, 222},
{252, 223},
{253, 223},
{1536, 223},
{536, 223},
{1152, 223},
{255, 223},
{537, 223},
{256, 223},
{1026, 223},
{957, 223},
{257, 223},
{1925, 223},
{254, 223},
{535, 223},
{258, 223},
{456, 224},
{1614, 224},
{1651, 224},
{1613, 224},
{1584, 224},
{1480, 224},
{460, 224},
{463, 224},
{1694, 224},
{1325, 224},
{464, 224},
{459, 224},
{461, 224},
{462, 224},
{594, 225},
{1542, 225},
{596, 225},
{597, 225},
{1213, 225},
{598, 225},
{599, 225},
{1024, 225},
{1025, 225},
{960, 225},
{1793, 225},
{600, 225},
{628, 228},
{1633, 228},
{1634, 228},
{1635, 228},
{633, 228},
{1495, 228},
{2304, 228},
{2305, 228},
{2246, 228},
{2048, 228},
{1010, 228},
{635, 228},
{2247, 228},
{1703, 228},
{1329, 228},
{1305, 228},
{634, 228},
{2271, 228},
{1710, 228},
{1711, 228},
{1712, 228},
{1790, 228},
{631, 228},
{1746, 228},
{1343, 228},
{632, 228},
{1881, 228},
{636, 228},
{250, 229},
{1538, 229},
{259, 229},
{262, 229},
{263, 229},
{260, 229},
{958, 229},
{803, 229},
{261, 229},
{397, 229},
{251, 231},
{1540, 231},
{1775, 231},
{1776, 231},
{264, 231},
{1780, 231},
{1778, 231},
{265, 231},
{268, 231},
{2041, 231},
{2328, 231},
{804, 231},
{1779, 231},
{959, 231},
{1573, 231},
{538, 231},
{802, 231},
{266, 231},
{267, 231},
{1781, 231},
{1777, 231},
{269, 231},
{9, 234},
{626, 235},
{139, 235},
{1366, 236},
{1364, 237},
{130, 238},
{891, 238},
{80, 239},
{1577, 239},
{2250, 239},
{2211, 239},
{142, 240},
{145, 240},
{593, 242},
{2241, 242},
{556, 243},
{141, 243},
{294, 243},
{135, 245},
{143, 246},
{134, 247},
{1856, 247},
{131, 253},
{2021, 254},
{1369, 256},
{1972, 257},
{1462, 258},
{1885, 258},
{829, 258},
{147, 258},
{146, 258},
{136, 258},
{287, 258},
{887, 259},
{512, 259},
{138, 259},
{941, 260},
{402, 261},
{404, 262},
{947, 263},
{465, 263},
{401, 264},
{403, 265},
{946, 266},
{944, 266},
{943, 266},
{945, 266},
{1544, 267},
{948, 267},
{406, 267},
{405, 267},
{690, 269},
{21, 270},
{515, 270},
{20, 270},
{1016, 272},
{1046, 272},
{1049, 272},
{1047, 272},
{1048, 272},
{1018, 272},
{1017, 272},
{1019, 272},
{18, 274},
{726, 274},
{234, 274},
{233, 274},
{1154, 274},
{1653, 274},
{801, 274},
{232, 274},
{1368, 274},
{240, 274},
{1021, 275},
{237, 275},
{1886, 275},
{238, 275},
{1232, 275},
{889, 277},
{307, 278},
{300, 278},
{1367, 279},
{235, 280},
{236, 280},
{1023, 280},
{1233, 281},
{239, 281},
{1551, 282},
{301, 282},
{45, 284},
{1293, 286},
{1783, 286},
{1155, 287},
{1145, 294},
{812, 302},
{1291, 303},
{1751, 304},
{70, 304},
{79, 304},
{1128, 304},
{72, 304},
{1127, 304},
{724, 304},
{8, 306},
{752, 306},
{1713, 306},
{715, 306},
{15, 306},
{208, 307},
{129, 308},
{128, 308},
{1772, 308},
{754, 308},
{127, 308},
{1743, 308},
{1732, 309},
{1726, 309},
{728, 309},
{1736, 309},
{1149, 309},
{1721, 310},
{1206, 310},
{2205, 310},
{1722, 311},
{1207, 311},
{1735, 313},
{1734, 313},
{1730, 313},
{1728, 313},
{1725, 313},
{1208, 313},
{1717, 313},
{1718, 313},
{1729, 313},
{1719, 313},
{1733, 313},
{1727, 313},
{1720, 315},
{1723, 315},
{1997, 316},
{2004, 316},
{2000, 316},
{2001, 316},
{2002, 316},
{2206, 316},
{2003, 316},
{2005, 316},
{2006, 316},
{1655, 317},
{1663, 317},
{1659, 317},
{2031, 317},
{1887, 317},
{1922, 317},
{1658, 317},
{1662, 317},
{2072, 317},
{1784, 317},
{796, 317},
{1561, 317},
{1989, 317},
{1664, 317},
{1558, 317},
{1660, 317},
{1883, 317},
{1665, 317},
{1995, 317},
{1560, 317},
{1557, 317},
{2079, 317},
{1668, 317},
{1559, 317},
{1666, 317},
{1667, 317},
{2203, 317},
{2026, 317},
{1661, 318},
{751, 319},
{1890, 319},
{1102, 319},
{1565, 319},
{1150, 319},
{1411, 319},
{1076, 319},
{1806, 319},
{1341, 319},
{207, 319},
{793, 319},
{1744, 319},
{2016, 319},
{1097, 319},
{1101, 319},
{1148, 319},
{1098, 319},
{1286, 319},
{1737, 319},
{725, 319},
{791, 319},
{1099, 319},
{1100, 319},
{798, 319},
{1103, 319},
{1567, 319},
{2098, 319},
{206, 319},
{1748, 319},
{1575, 319},
{1747, 319},
{205, 319},
{2212, 319},
{794, 319},
{2251, 319},
{209, 319},
{1205, 320},
{1295, 320},
{720, 321},
{1724, 322},
{203, 322},
{2200, 322},
{1236, 322},
{204, 322},
{1345, 322},
{1914, 322},
{1574, 322},
{1765, 322},
{2101, 323},
{1013, 323},
{1242, 323},
{721, 323},
{1210, 323},
{1209, 323},
{730, 323},
{717, 323},
{1782, 323},
{1770, 323},
{719, 323},
{729, 323},
{2102, 324},
{703, 324},
{1716, 325},
{1522, 325},
{1523, 325},
{722, 325},
{755, 325},
{1731, 325},
{814, 327},
{10, 327},
{1926, 328},
{1933, 328},
{1929, 328},
{1930, 328},
{2044, 328},
{1931, 328},
{1956, 328},
{1960, 328},
{1944, 328},
{1934, 328},
{1932, 328},
{1935, 328},
{1952, 328},
{1938, 328},
{1951, 328},
{1945, 328},
{1949, 328},
{1963, 328},
{1943, 328},
{1946, 328},
{1962, 328},
{1968, 328},
{1967, 328},
{1942, 328},
{1947, 328},
{1966, 328},
{1950, 328},
{1941, 328},
{1965, 328},
{1964, 328},
{1961, 328},
{1955, 328},
{1970, 328},
{2320, 328},
{1957, 328},
{1937, 328},
{1953, 328},
{1936, 328},
{1940, 328},
{1954, 328},
{1939, 328},
{1971, 328},
{2215, 329},
{2220, 329},
{2218, 329},
{2221, 329},
{2216, 329},
{2223, 329},
{2316, 329},
{2224, 329},
{2229, 329},
{2219, 329},
{2231, 329},
{2230, 329},
{2226, 329},
{2228, 329},
{2225, 329},
{2227, 329},
{2222, 329},
{2233, 329},
{2232, 329},
{2234, 329},
{2235, 329},
{816, 331},
{1332, 333},
{1333, 333},
{731, 334},
{748, 334},
{749, 334},
{1632, 334},
{1615, 334},
{1482, 334},
{745, 334},
{735, 334},
{733, 334},
{737, 334},
{744, 334},
{742, 334},
{1695, 334},
{1326, 334},
{1875, 334},
{1884, 334},
{734, 334},
{738, 334},
{740, 334},
{741, 334},
{736, 334},
{746, 334},
{747, 334},
{743, 334},
{739, 334},
{750, 334},
{1168, 335},
{1616, 335},
{1617, 335},
{1172, 335},
{1177, 335},
{1618, 335},
{1174, 335},
{1184, 335},
{1181, 335},
{1178, 335},
{1175, 335},
{1176, 335},
{1173, 335},
{1696, 335},
{1327, 335},
{1300, 335},
{2103, 335},
{1180, 335},
{1993, 335},
{1185, 335},
{1183, 335},
{1182, 335},
{995, 336},
{1619, 336},
{1621, 336},
{2045, 336},
{1620, 336},
{1578, 336},
{1486, 336},
{2294, 336},
{1913, 336},
{997, 336},
{1071, 336},
{1738, 336},
{1000, 336},
{1697, 336},
{1044, 336},
{1301, 336},
{1045, 336},
{998, 336},
{2017, 336},
{2249, 336},
{1347, 336},
{1860, 336},
{2327, 336},
{999, 336},
{1001, 336},
{1191, 337},
{1194, 337},
{1623, 337},
{1622, 337},
{1204, 337},
{1488, 337},
{2296, 337},
{2297, 337},
{2046, 337},
{1203, 337},
{1195, 337},
{1196, 337},
{1197, 337},
{1698, 337},
{1200, 337},
{1302, 337},
{1699, 337},
{1198, 337},
{1689, 337},
{1349, 337},
{1201, 337},
{1202, 337},
{2014, 337},
{924, 338},
{1627, 338},
{1629, 338},
{2047, 338},
{1628, 338},
{1580, 338},
{1492, 338},
{2301, 338},
{2302, 338},
{1215, 338},
{1072, 338},
{926, 338},
{937, 338},
{1740, 338},
{927, 338},
{1701, 338},
{931, 338},
{1304, 338},
{933, 338},
{935, 338},
{934, 338},
{929, 338},
{928, 338},
{1351, 338},
{1862, 338},
{1365, 338},
{930, 338},
{978, 338},
{939, 338},
{359, 340},
{654, 340},
{655, 340},
{540, 340},
{1630, 340},
{1631, 340},
{1690, 340},
{2303, 340},
{1344, 340},
{361, 340},
{1070, 340},
{1702, 340},
{1328, 340},
{652, 340},
{363, 340},
{651, 340},
{2018, 340},
{365, 340},
{364, 340},
{395, 340},
{2007, 340},
{1872, 340},
{374, 345},
{542, 345},
{1636, 345},
{1012, 345},
{1691, 345},
{1507, 345},
{2307, 345},
{2306, 345},
{376, 345},
{1075, 345},
{378, 345},
{1704, 345},
{883, 345},
{1306, 345},
{380, 345},
{1350, 345},
{379, 345},
{980, 345},
{381, 345},
{1382, 347},
{1642, 347},
{1643, 347},
{2052, 347},
{1644, 347},
{1582, 347},
{1501, 347},
{2308, 347},
{2309, 347},
{1386, 347},
{1384, 347},
{1742, 347},
{2067, 347},
{2070, 347},
{2069, 347},
{2068, 347},
{1385, 347},
{2066, 347},
{1387, 347},
{1707, 347},
{1389, 347},
{1392, 347},
{1390, 347},
{1391, 347},
{1388, 347},
{1393, 347},
{1864, 347},
{1394, 347},
{1395, 347},
{818, 350},
{823, 350},
{819, 350},
{817, 350},
{1299, 350},
{1027, 353},
{1639, 353},
{1640, 353},
{2050, 353},
{1641, 353},
{1581, 353},
{1503, 353},
{2312, 353},
{2313, 353},
{1342, 353},
{1029, 353},
{1749, 353},
{1073, 353},
{1804, 353},
{1741, 353},
{2009, 353},
{1031, 353},
{1706, 353},
{1330, 353},
{1307, 353},
{1190, 353},
{1348, 353},
{1863, 353},
{1030, 353},
{1032, 353},
{1033, 353},
{981, 355},
{1054, 355},
{1645, 355},
{1646, 355},
{2049, 355},
{1647, 355},
{1654, 355},
{1583, 355},
{1499, 355},
{2310, 355},
{2311, 355},
{1061, 355},
{1060, 355},
{1056, 355},
{1687, 355},
{1982, 355},
{1708, 355},
{2106, 355},
{2008, 355},
{1058, 355},
{1062, 355},
{2105, 355},
{1983, 355},
{2019, 355},
{1298, 355},
{1059, 355},
{1865, 355},
{1877, 355},
{1057, 355},
{1063, 355},
{1064, 355},
{1397, 357},
{1648, 357},
{1650, 357},
{2051, 357},
{1649, 357},
{1497, 357},
{2314, 357},
{2315, 357},
{1413, 357},
{1405, 357},
{1709, 357},
{1399, 357},
{1401, 357},
{1400, 357},
{1402, 357},
{1404, 357},
{1403, 357},
{1406, 357},
{824, 360},
{1509, 360},
{149, 360},
{544, 360},
{1638, 360},
{1637, 360},
{1692, 360},
{1505, 360},
{2292, 360},
{2293, 360},
{158, 360},
{1082, 360},
{1243, 360},
{2043, 360},
{160, 360},
{1705, 360},
{882, 360},
{1214, 360},
{162, 360},
{976, 360},
{797, 360},
{1921, 360},
{1352, 360},
{1920, 360},
{161, 360},
{979, 360},
{164, 360},
{1434, 360},
{1435, 360},
{1842, 360},
{2062, 360},
{2319, 360},
{2065, 360},
{2088, 360},
{2030, 360},
{2107, 360},
{1396, 360},
{1843, 360},
{1407, 360},
{1381, 360},
{1408, 360},
{1410, 360},
{1374, 360},
{1409, 360},
{1924, 360},
{2063, 360},
{2090, 360},
{289, 360},
{1562, 360},
{2061, 360},
{1379, 360},
{1375, 360},
{1376, 360},
{2324, 360},
{2064, 360},
{1857, 360},
{2010, 360},
{1377, 360},
{1378, 360},
{1380, 360},
{2032, 360},
{2091, 360},
{952, 360},
{1892, 360},
{1436, 360},
{398, 362},
{985, 363},
{1083, 363},
{1669, 363},
{1086, 363},
{1087, 363},
{1089, 363},
{1090, 363},
{1992, 363},
{1093, 363},
{1092, 363},
{1670, 363},
{1085, 363},
{1671, 363},
{1091, 363},
{1088, 363},
{2208, 363},
{2209, 363},
{2011, 363},
{1094, 363},
{400, 365},
{408, 365},
{407, 365},
{409, 365},
{410, 365},
{2086, 366},
{490, 367},
{954, 367},
{955, 367},
{2085, 368},
{1891, 368},
{492, 374},
{489, 374},
{1002, 374},
{1005, 374},
{1006, 374},
{1007, 374},
{1004, 374},
{1008, 374},
{1973, 376},
{1979, 376},
{1976, 376},
{1977, 376},
{1978, 376},
{1980, 376},
{1981, 376},
{852, 377},
{892, 377},
{855, 377},
{856, 377},
{857, 377},
{858, 377},
{854, 377},
{859, 377},
{795, 377},
{491, 378},
{1296, 379},
{667, 379},
{291, 379},
{43, 381},
{2242, 382},
{986, 382},
{2243, 382},
{55, 383},
{835, 385},
{2033, 387},
{2322, 389},
{2023, 389},
{2077, 389},
{1752, 390},
{56, 392},
{1052, 393},
{1053, 393},
{60, 394},
{1358, 395},
{987, 397},
{62, 398},
{54, 400},
{53, 400},
{990, 400},
{1850, 401},
{1849, 401},
{57, 406},
{988, 406},
{1911, 406},
{59, 408},
{1263, 408},
{1264, 408},
{1265, 408},
{68, 409},
{1415, 409},
{2330, 411},
{148, 416},
{2025, 416},
{1611, 416},
{1610, 416},
{1915, 416},
{1612, 416},
{865, 416},
{1478, 416},
{2317, 416},
{2318, 416},
{2214, 416},
{1136, 416},
{1137, 416},
{1339, 416},
{863, 416},
{1312, 416},
{1311, 416},
{1074, 416},
{1309, 416},
{2097, 416},
{1686, 416},
{1310, 416},
{1308, 416},
{1051, 416},
{2093, 416},
{2185, 416},
{2095, 416},
{1693, 416},
{1324, 416},
{1340, 416},
{1313, 416},
{1314, 416},
{1917, 416},
{2092, 416},
{1315, 416},
{2331, 416},
{2094, 416},
{1138, 416},
{2096, 416},
{862, 416},
{1919, 416},
{1318, 416},
{867, 416},
{1316, 416},
{1317, 416},
{1859, 416},
{864, 416},
{1916, 416},
{977, 416},
{869, 416},
{3, 426},
{2201, 432},
{73, 432},
{1808, 432},
{813, 432},
{2020, 433},
{74, 433},
{1041, 433},
{75, 433},
{1433, 436},
{1043, 436},
{1042, 436},
{1984, 436},
{1254, 439},
{1129, 439},
{211, 439},
{1419, 439},
{1991, 439},
{435, 439},
{213, 439},
{215, 439},
{1810, 439},
{430, 439},
{668, 439},
{433, 439},
{436, 439},
{214, 439},
{962, 439},
{1267, 439},
{216, 439},
{315, 441},
{1420, 441},
{325, 441},
{319, 441},
{324, 441},
{1811, 441},
{318, 441},
{669, 441},
{320, 441},
{317, 441},
{963, 441},
{316, 441},
{390, 441},
{870, 443},
{1418, 443},
{875, 443},
{873, 443},
{1266, 443},
{1809, 443},
{878, 443},
{877, 443},
{876, 443},
{879, 443},
{880, 443},
{961, 443},
{874, 443},
{881, 443},
{849, 444},
{848, 444},
{847, 444},
{482, 445},
{1421, 445},
{487, 445},
{485, 445},
{486, 445},
{1812, 445},
{704, 445},
{670, 445},
{1269, 445},
{705, 445},
{964, 445},
{1268, 445},
{488, 445},
{449, 446},
{1427, 446},
{452, 446},
{453, 446},
{1277, 446},
{1818, 446},
{710, 446},
{674, 446},
{1278, 446},
{711, 446},
{969, 446},
{455, 446},
{454, 446},
{562, 447},
{563, 447},
{555, 447},
{585, 447},
{586, 447},
{587, 447},
{326, 447},
{1426, 447},
{336, 447},
{330, 447},
{335, 447},
{1817, 447},
{329, 447},
{673, 447},
{331, 447},
{328, 447},
{968, 447},
{327, 447},
{391, 447},
{1158, 448},
{1422, 448},
{1160, 448},
{1165, 448},
{1161, 448},
{1813, 448},
{1270, 448},
{1162, 448},
{1167, 448},
{1271, 448},
{1272, 448},
{1164, 448},
{1166, 448},
{466, 449},
{1423, 449},
{471, 449},
{469, 449},
{470, 449},
{1814, 449},
{706, 449},
{671, 449},
{1274, 449},
{707, 449},
{965, 449},
{1273, 449},
{472, 449},
{898, 450},
{899, 450},
{295, 450},
{895, 450},
{894, 450},
{896, 450},
{893, 450},
{416, 452},
{1424, 452},
{426, 452},
{418, 452},
{419, 452},
{1815, 452},
{421, 452},
{672, 452},
{424, 452},
{427, 452},
{966, 452},
{428, 452},
{429, 452},
{337, 453},
{1428, 453},
{1050, 453},
{347, 453},
{341, 453},
{346, 453},
{1819, 453},
{340, 453},
{675, 453},
{342, 453},
{339, 453},
{970, 453},
{338, 453},
{392, 453},
{820, 455},
{821, 455},
{822, 455},
{1009, 457},
{718, 457},
{616, 457},
{1429, 457},
{620, 457},
{618, 457},
{619, 457},
{1820, 457},
{625, 457},
{676, 457},
{624, 457},
{622, 457},
{971, 457},
{621, 457},
{623, 457},
{348, 458},
{1430, 458},
{358, 458},
{352, 458},
{357, 458},
{1821, 458},
{351, 458},
{1279, 458},
{353, 458},
{350, 458},
{972, 458},
{349, 458},
{393, 458},
{2274, 460},
{2278, 460},
{2279, 460},
{2280, 460},
{2281, 460},
{2282, 460},
{2283, 460},
{2284, 460},
{2285, 460},
{2286, 460},
{2287, 460},
{2288, 460},
{2289, 460},
{695, 460},
{1425, 460},
{702, 460},
{697, 460},
{884, 460},
{1816, 460},
{708, 460},
{700, 460},
{1275, 460},
{709, 460},
{967, 460},
{1276, 460},
{701, 460},
{1871, 462},
{210, 463},
{1432, 463},
{1283, 463},
{218, 463},
{1284, 463},
{1823, 463},
{712, 463},
{677, 463},
{219, 463},
{713, 463},
{974, 463},
{217, 463},
{221, 463},
{723, 464},
{828, 464},
{2182, 464},
{2207, 464},
{2183, 464},
{2184, 464},
{1827, 466},
{1830, 466},
{1831, 466},
{1832, 466},
{1833, 466},
{1834, 466},
{1835, 466},
{1836, 466},
{1837, 466},
{1838, 466},
{1839, 466},
{1840, 466},
{1841, 466},
{688, 468},
{689, 468},
{692, 468},
{902, 468},
{1431, 468},
{904, 468},
{905, 468},
{1280, 468},
{1822, 468},
{909, 468},
{906, 468},
{1281, 468},
{910, 468},
{973, 468},
{1282, 468},
{908, 468},
{1319, 469},
{1039, 470},
{516, 470},
{1246, 470},
{290, 472},
{1320, 472},
{2248, 472},
{1844, 473},
{1845, 474},
{513, 474},
{1846, 475},
{521, 479},
{1441, 479},
{1244, 480},
{1439, 480},
{2100, 480},
{297, 480},
{548, 481},
{1465, 481},
{580, 481},
{1467, 481},
{522, 483},
{1443, 483},
{125, 483},
{1444, 483},
{1120, 484},
{1469, 484},
{1121, 484},
{1471, 484},
{639, 484},
{1473, 484},
{1754, 484},
{1755, 484},
{1125, 484},
{1475, 484},
{413, 484},
{1512, 484},
{788, 484},
{165, 484},
{1514, 484},
{1015, 485},
{1510, 485},
{843, 486},
{1524, 486},
{448, 486},
{1528, 486},
{779, 486},
{1489, 486},
{1437, 487},
{1438, 487},
{659, 488},
{1530, 488},
{663, 488},
{530, 489},
{1515, 489},
{496, 489},
{1517, 489},
{201, 489},
{1518, 489},
{1547, 490},
{1548, 490},
{533, 490},
{1516, 490},
{534, 491},
{1535, 491},
{763, 491},
{1537, 491},
{764, 491},
{1539, 491},
{595, 491},
{1541, 491},
{457, 491},
{1479, 491},
{629, 491},
{1494, 491},
{1908, 492},
{1909, 492},
{1910, 492},
{293, 492},
{1461, 492},
{942, 493},
{1543, 493},
{539, 493},
{1549, 494},
{1550, 494},
{1020, 494},
{1545, 494},
{1022, 494},
{1546, 494},
{656, 495},
{1534, 495},
{1714, 496},
{1715, 496},
{753, 496},
{1519, 496},
{716, 496},
{1521, 496},
{1998, 496},
{1999, 496},
{1656, 496},
{1657, 496},
{529, 496},
{1520, 496},
{815, 497},
{1508, 497},
{1927, 497},
{1928, 497},
{2217, 497},
{732, 497},
{1481, 497},
{1169, 497},
{1483, 497},
{996, 497},
{1485, 497},
{1192, 497},
{1487, 497},
{925, 497},
{1491, 497},
{541, 497},
{1493, 497},
{543, 497},
{1506, 497},
{1383, 497},
{1500, 497},
{1028, 497},
{1502, 497},
{1055, 497},
{1498, 497},
{1398, 497},
{1496, 497},
{545, 497},
{1504, 497},
{861, 497},
{1477, 497},
{1554, 498},
{1555, 498},
{1084, 498},
{1556, 498},
{1974, 498},
{1975, 498},
{1003, 498},
{853, 498},
{546, 499},
{1440, 499},
{1011, 501},
{1807, 501},
{1460, 501},
{903, 502},
{2267, 502},
{1458, 502},
{438, 502},
{2254, 502},
{1446, 502},
{439, 502},
{2255, 502},
{1447, 502},
{871, 502},
{2253, 502},
{1445, 502},
{483, 502},
{2256, 502},
{1448, 502},
{450, 502},
{2262, 502},
{1454, 502},
{444, 502},
{2261, 502},
{1453, 502},
{1159, 502},
{2257, 502},
{1449, 502},
{467, 502},
{2258, 502},
{1450, 502},
{443, 502},
{2259, 502},
{1451, 502},
{445, 502},
{2263, 502},
{1455, 502},
{617, 502},
{2265, 502},
{1456, 502},
{446, 502},
{2266, 502},
{1457, 502},
{2275, 502},
{2276, 502},
{2277, 502},
{696, 502},
{2260, 502},
{1452, 502},
{447, 502},
{2269, 502},
{1459, 502},
{1828, 502},
{2264, 502},
{1829, 502},
	
#endregion
		};

/*
		private static Dictionary<int, List<int>> RevertedCategoryMapping = NewCategoriesMapping.GroupBy(x => x.Value)
			.ToDictionary(x => x.Key, x => x.Select(y => y.Key).ToList());
*/
	}
}