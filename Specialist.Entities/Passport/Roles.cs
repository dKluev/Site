using System;

namespace Specialist.Entities.Passport {
	[Flags]
	public enum Role {
		None = 0,
		Admin = 1,
		Graduate = 2,
		Trainer = 4,
		Employee = 8,
		TestAdmin = 16,
		CorpManager = 32,
		OrderManager = 64,
		ForumAdmin = 128,
		ContentManager = 256,
		LightContManager = 512,
		TestCorpManager = 1024,
		AnyContManager = ContentManager | LightContManager,
		GraduateClubAccess = Employee | Graduate | Trainer
	}
}