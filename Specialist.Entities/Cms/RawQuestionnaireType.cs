using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Specialist.Entities.Context
{
    public class RawQuestionnaireType
    {
        public string Name { get; set; }

        [Column(IsPrimaryKey = true)]
        public byte Type { get; set; }

        public RawQuestionnaireType(string name, byte rawQuestionnaireID)
        {
            Name = name;
            Type = rawQuestionnaireID;
        }

        public const byte Teacher = 0;
        public const byte SkillsComment = 1;
        public const byte CourseComment = 2;
        public const byte AdministrationComment = 3;
        public const byte OrganizingComment = 4;
        public const byte ExpectationComment = 5;
        public const byte StudentNotes = 6;

        public static List<string> GetResponseProperties()
        {
            return typeof(RawQuestionnaireType).GetFields()
                .Select(f => f.Name).Where(n => n != "Teacher").ToList();

        }

        public static byte GetType(string fieldName)
        {
            return (byte) typeof(RawQuestionnaireType).GetFields()
                .FirstOrDefault(fi => fi.Name == fieldName).GetValue(null);
        }

        public static string GetPropertyName(byte type)
        {
            return typeof(RawQuestionnaireType).GetFields()
                .FirstOrDefault(fi => (byte)fi.GetValue(null) == type).Name;
        }

        public static List<RawQuestionnaireType> GetAll()
        {
            return
                new List<RawQuestionnaireType>
                {
                    new RawQuestionnaireType("Преподаватель", Teacher),
                    new RawQuestionnaireType("Навыки", SkillsComment),
                    new RawQuestionnaireType("Курс", CourseComment),
                    new RawQuestionnaireType("Администрация", AdministrationComment),
                    new RawQuestionnaireType("Организация", OrganizingComment),
                    new RawQuestionnaireType("Ожидания", ExpectationComment),
                    new RawQuestionnaireType("Пожелания", StudentNotes),
                };
        }
    }
}