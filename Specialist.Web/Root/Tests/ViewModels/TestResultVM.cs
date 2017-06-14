using System.Collections.Generic;
using System.Reflection;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using System.Linq;
using Specialist.Entities.Tests.Consts;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestResultVM {
		public UserTest UserTest { get; set; }

		public bool CanBuy {
			get {
				return IsOwned && UserTest.IsPass &&
					IsPrivatePerson && UserTest.NormalTest;
			}
		}

		public bool IsPrivatePerson { get; set; }

		public string CourseName { get; set; }

		public UserTestStats Stats { get; set; }
		public List<TestModule> Modules { get; set; }  

		public List<CourseLink> RecCourses { get; set; }

		public bool IsOwned { get; set; }

		public bool IsEnglish { get; set; }

		public bool WithLevels {
			get {
				return UserTest.TestId == TestRecomendations.EnglishTestWithLevels;
			}
		}


		public bool IsTrack { get; set; }
			
		private Dictionary<int, string> _names = null;
			 
        public Dictionary<int, string> Names {
            get {
	            return _names ??
		            (_names = IsTrack ? UserTestStatus.TrackNames : 
					NamedIdCache<UserTestStatus>.Dict.ToDictionary(x => x.Key, x => x.Value.Name) );
            }
        }

		public string ResultMessage {
			get {
				return WithLevels
					? TestRecomendations.GetLevel(UserTest.RightCount.GetValueOrDefault())
					: Names[UserTest.Status];
			}
		}


	}
}