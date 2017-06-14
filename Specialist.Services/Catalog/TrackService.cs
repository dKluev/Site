using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Common.Extension;
using Specialist.Services.Interface;
using Specialist.Services.Order.Extension;
using Specialist.Services.UnityInterception;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Web.Common.Utils;

namespace Specialist.Services {
	public class TrackService : ITrackService {
		[Dependency]
		public IPriceService PriceService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public IUserSettingsService UserSettingsService { get; set; }

		private SpecialistDataContext context = new SpecialistDataContext();

		public const int TrackCount = 3;


		public Dictionary<string, Dictionary<string, decimal?>> TrackFullPrices() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var tracks = CourseService.GetActiveTrackCourses();
				return tracks.ToDictionary(x => x.Key, x => GetCourseSum(x.Value));
			});
		}

		public Dictionary<string, Dictionary<string,Tuple<decimal,decimal>>> TrackLastCourseDiscounts() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var tracks = CourseService.GetActiveTrackCourses();
				var discounts = tracks.Select(x => {
					var trackTC = x.Key;
					var lastCourseTC = x.Value.LastOrDefault();
					if (lastCourseTC == null) return null;
					var fullPrices = TrackFullPrices().GetValueOrDefault(trackTC);
					if (fullPrices == null) {
						return null;
					}	
					var trackDiscounts = fullPrices.Select(price => {
						var fullTrackPrice = price.Value;
						var type = price.Key;
						var trackPrice = PriceService.GetPriceByType(trackTC,type, null);
						if (fullTrackPrice == null || trackPrice == null) {
							return Tuple.Create(type,decimal.Zero,decimal.Zero);
						}
						var save = fullTrackPrice.Value - trackPrice.Value;
						var coursePrice = PriceService.GetPriceByType(lastCourseTC, type, null);
						if (coursePrice == null) return Tuple.Create(type,decimal.Zero, decimal.Zero);
						var discount = save*100/coursePrice.Value;
						if (discount <= 0) return Tuple.Create(type,decimal.Zero, decimal.Zero);
						return Tuple.Create(type, discount,save);
					}).ToDictionary(z => z.Item1, z => Tuple.Create(z.Item2, z.Item3));

					return Tuple.Create(trackTC, trackDiscounts);
				}).Where(x => x != null).ToDictionary(x => x.Item1, x => x.Item2);
				return discounts;
			});
		}

		Dictionary<string, decimal?> GetCourseSum(List<string> courses) {

			var prices = courses.Distinct().ToDictionary(x => x,
				x => PriceService.GetAllPricesForCourse(x, null)
					.GroupBy(z => z.PriceType_TC)
					.ToDictionary(z => z.Key, z => (decimal?) z.First().Price));

			return PriceTypes.CourseTable.ToDictionary(x => x, x => courses.Select(c =>
				prices.GetValueOrDefault(c).GetValueOrDefault(x)).Sum());

		} 

		public IQueryable<Course> GetAllTracksWithCourse(string courseTC) {
			return
				from track in context.Tracks
				where track.Course_TC == courseTC && track.TrackCourse.IsActive
				select track.TrackCourse;
		}

		public List<TrackDiscount> GetTrackDiscountForCourse(string courseTC) {
			var tracks = GetAllTracksWithCourse(courseTC).ToList();
			return GetTrackDiscounts(tracks).Where(x => x.Saving > 0).Take(TrackCount).ToList();
		}

		public IEnumerable<TrackDiscount> GetTrackDiscounts(IEnumerable<Course> tracks) {
			return tracks
				.Select(GetTrackDiscount)
				.Where(td => td.Price > 0 && td.DiscountPrice > 0);
		}

		public TrackDiscount GetTrackDiscount(Course track) {
			var cityTC = UserSettingsService.CityTC;
			if (cityTC == null)
				cityTC = Cities.Moscow;
			var result = new TrackDiscount {Track = track,};
			var trackCourses = CourseService.GetActiveTrackCourses().GetValueOrDefault(track.Course_TC);
			if (trackCourses == null)
				return result;
			var firstCourseTC = trackCourses.First();
			var coursePrice = PriceService.GetAllPricesForCourse(firstCourseTC, null)
				.FirstOrDefault(p => p.CommonPriceTypeTC == PriceTypes.PrivatePersonWeekend);

			if (coursePrice != null) {
				var trackPrices = PriceService.GetTrackCoursesPrices(track.Course_TC, PriceTypes.PrivatePersonWeekend);
				if (!trackPrices.Any()) {
					trackPrices = PriceService.GetTrackCoursesPrices(track.Course_TC, PriceTypes.IntraExtra);
				}
				
				if (trackPrices.Count > 0) {
					result.DiscountPrice = trackPrices.Sum(p => p.Price);
					var trackCoursePrice = trackPrices
						.FirstOrDefault(p => p.Course_TC == firstCourseTC);
					if (trackCoursePrice != null)
						result.Price = result.DiscountPrice*
							coursePrice.Price/trackCoursePrice.Price;
				}
			}
			return result;
		}


		public Course GetByUrlName(string urlName) {
			var course = context.Courses.
				Where(c => c.UrlName == urlName).IsActive()
				.OrderBy(x => x.Course_ID).FirstOrDefault();

			return course;
		}
	}
}