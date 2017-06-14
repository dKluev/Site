using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Entities.Tests.Consts;
using System.Linq;

namespace Specialist.Entities.Tests {
	public partial class Test {

		public bool IsActive {
			get { return Status == TestStatus.Active; }
		}

		public bool IsGraduateOnly {
			get { return CourseTCSplitList.Any() && !NoRestriction; }
		}

		public bool HasChecker {
			get { return !Checker_TC.IsEmpty(); }
		}

		private List<string> _courseTCListSplit;
		private List<int> _testIds;

        public List<string> CourseTCSplitList {
            get {
	            return _courseTCListSplit ??
		            (_courseTCListSplit = StringUtils.SafeSplit(CourseTCList));
            }
        }

        public List<int> TestIds {
            get {
	            return _testIds ??
		            (_testIds = StringUtils.SafeSplit(TestIdList)
					  .Select(x => StringUtils.ParseInt(x).GetValueOrDefault()).Where(x => x > 0).ToList());
            }
        }


		public bool IsEset {get { return Name.Contains("ESET"); }}
		public bool IsEnglish {get { return Id == TestRecomendations.newTest; }}

		    private EntityRef<Employee> _Author = default(EntityRef<Employee>);
        [Association(Storage = "_Author", ThisKey = "Author_TC",
            OtherKey = "Employee_TC", IsForeignKey = true)]
        public Employee Author {
            get { return _Author.Entity; }
            set { _Author.Entity = value; }
        }
	}
}