using System;
using System.Collections.Generic;
using System.ComponentModel;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Filter
{
    public class GroupFilter
    {
        [DisplayName("�������� �����")]
        public string CourseName { get; set; }

        public string CourseTC { get; set; }

	    public static class StudyType {
		    public const int Intramural = 1;
		    public const int Webinar = 2;
		    public const int OpenLearning = 3;
		    public const int IntraExtra = 4;
	    }
		
    	public DateTime? BeginDate { get; set; }

    	public DateTime? EndDate { get; set; }

		public static Dictionary<int,string> StudyTypes = new Dictionary<int, string> {
			{0, "�����"},
			{StudyType.Intramural, "����"},
			{StudyType.Webinar, "�������"},
			{StudyType.OpenLearning, "�������� ��������"},
			{StudyType.IntraExtra, "����-������� ��������"},
		};  

        public int StudyTypeId { get; set; }

        [DisplayName("����� ���������� �������")]
        public string DayShiftTC { get; set; }

        [DisplayName("��������")]
        public string DaySequenceTC { get; set; }

        [DisplayName("��������")]
        public string ComplexTC { get; set; }

    	public int? SectionId { get; set; }

		[DisplayName("�������������")]
		public string EmployeeTC { get; set; }



    	public bool? ForPrint { get; set; }
    	public bool? ForPdf { get; set; }

		public GroupFilter SetForPrint() {
			ForPrint = true;
			return this;
		}

        public List<Complex> Complexes { get; set; }

        public List<DayShift> DayShifts { get; set; }

    	public List<Section> Sections {get;set;}

		public List<Employee> Employees { get; set; } 
    }
       
}