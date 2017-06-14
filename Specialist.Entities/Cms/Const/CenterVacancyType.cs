using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Specialist.Entities.Context
{
    public class CenterVacancyType
    {
        public string Name { get; set; }

        [Column(IsPrimaryKey = true)]
        public byte Type { get; set; }

        public CenterVacancyType(string name, byte CenterVacancyTypeID)
        {
            Name = name;
            Type = CenterVacancyTypeID;
        }

        public const byte Teacher = 0;
        public const byte Emloyee = 1;
        public const byte Probation = 2;

        public static List<CenterVacancyType> GetAll()
        {
            return
                new List<CenterVacancyType>
                {
                    new CenterVacancyType("Преподаватель", Teacher),
                    new CenterVacancyType("Сотрудник", Emloyee),
                    new CenterVacancyType("Стажировка", Probation),
                };
        }

    }
}
