using System;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Common.ViewModel
{
    public class ExpressOrderVM
    {
    	public ExpressOrderVM() {}

    	public ExpressOrderVM(User user) {
    		if(user != null) {
				//Contact = user.Email;
				Name = user.FullName;
			}
    	}

    	public string ExpressCaptcha { get; set; }
    	public decimal? StudentInGroupId { get; set; }

    	public string Contact { get; set; }

        public string Name { get; set; }

    	public string CourseTC { get; set; }

		public decimal? GroupId { get;set; }
		public bool Subscibe { get; set; }

	    public string Message {
		    get { return Subscibe ? "Вы подписались на рассылку" : "Ваше сообщение отправлено"; }
	    }
    }
}