using System.Collections.Generic;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;

namespace Specialist.Entities.Lms {
	public class TestMethodType {
 		public char TC { get; set; }
		public string Name { get; set; }
		public TestMethodType(char tc, string name) {
			TC = tc;
			Name = name;
		}

		public static List<TestMethodType> All = _.List(
			new TestMethodType('О', "Опрос"),
			new TestMethodType('Л', "Лабораторная работа"),
			new TestMethodType('T', "Творческое задание"),
			new TestMethodType('П', "Практикум"));
	}
}