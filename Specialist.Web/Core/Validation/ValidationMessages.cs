namespace SpecialistTest.Web.Core.Utils {
    public class ValidationMessages {

    	public const string PropertyName = "{PropertyName}";
        public static string notempty_error
        {
            get
            {
                return "Поле '{PropertyName}' не должно быть пустым";
            }
        }

    }
}