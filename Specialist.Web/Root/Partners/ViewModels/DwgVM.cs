using System.Collections.Generic;
using SimpleUtils.Common;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using System.Linq;

namespace Specialist.Web.Root.Partners.ViewModels {
	public class DwgVM {
		public static List<Tuple<string, List<string>>> CourseTCs =
			_.List(
				Tuple.New("������� ����", _.List(
					"����20111",
					"����20112",
					"����20113�",
					"����",
				//	"�-����-�",
					"����"
					)), Tuple.New("���������� ���� (��������������)", _.List(
						"���-�",
						"����-�",
						"����-�",
						"����-�",
						"��1-�",
						"����1",
						"����2",
						"����3�",
						"����3�",
						"����3�",
						"�����",
						"����-�"
						)), Tuple.New("������������� ����, ��� ������������� � ������������ �������������", _.List(
							"���1-�",
							"���2-�",
							"����",
						//	"�-������",
						//	"�-���-�",
							"���2010",
							"�����-�",
							"���2010",
							"������"
							)), Tuple.New("3D �������������, ������������ � ��������", _.List(
								"3��1-�",
								"��3�",
								"3��2-�",
								"3��3�-�",
								"3��3�-�",
								"3��4-�",
								"����1-�",
								"����2",
								"���1-�",
								"�����",
								"�����-�",
//								"�-3��-�",
//								"�-3���3-�",
//								"�-3���-�",
//								"�-3��3-�",
								"���1-�",
								"��3�"
								)), Tuple.New("������� �������", _.List(
									"���",
									"����1",
//									"�-����-�",
									"�������"
									)), Tuple.New("������������ �������", _.List(
										"��1-�",
										"��-�",
										"��2-�",
										"��1-�"
										))
				);

		public List<Tuple<string, List<Tuple<CourseLink, List<Group>>>>> Courses { get; set; }
	}
}