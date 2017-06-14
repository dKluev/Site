use SPECREPL_replicating
--use SpecialistWeb
--select * from dbo.vSiteObjects as VSO
--select * from Passport..users
--where Email like '%slouchak@specialist.ru%'
--delete dbo.tCourses

--update dbo.tCourses
--set IsActive = 0
               
--
--select * from [SSZ001\SQLEE].SPECREPL_replicating.dbo.tPriceLists as TPL
--
--update dbo.tCourses
--set IsActive = 1
--where Course_TC in (select course_tc from [SSZ001\SQLEE].SPECREPL_replicating.dbo.tPrices as TP
--where PriceList_ID = 255
--union
--select Track_TC from [SSZ001\SQLEE].SPECREPL_replicating.dbo.tPrices as TP
--where PriceList_ID = 255)


set identity_insert dbo.tCourses on

insert into dbo.tCourses (
	Course_ID,
	Course_TC,
	ParentCourse_TC,
	ReportsCourse_TC,
	GruppsSheet,
	CourseDirection_TC,
	OurOrg_TC,
	HostingType_TC,
	AuthorizationType_TC,
	CSSClass_TC,
	MaxDiscount,
	OrgPriceCoefficient,
	[Name],
	NameOfficial,
	NameOfficialEn,
	WebName,
	eLearningName,
	BaseHours,
	AdditionalHours,
	Weeks,
	Days,
	NormNumOfStudents,
	MaxNumOfStudents,
	PriceListOrder,
	DontShow,
	IsTrack,
	IsExam,
	IsNew,
	WebPublishSchedule,
	AllowWebOrder,
	ProgramAccessible,
	ProgramURL,
	ProgramFileName,
	ProgramTemplate_TC,
	Prerequisites,
	PrerequisitesShort,
	Introduction,
	WebTopic,
	WebComment,
	OnComplete,
	WebDescription,
	Picture,
	CopyCashPayment,
	IsBookkeeperCourse,
	BCTC_ContentExists,
	BCTC_SeviceExpiresMonths,
	BCTC_SeviceExpiresDateBeg,
	Notes,
	InputDate,
	Employee_TC,
	LastChangeDate,
	LastChanger_TC,
	WebYandexDescription,
	IsFeedingCourse,
	WebShortName,
	WebNameComment,
	CourseDirectionA_TC,
	IsFreeConsultationExists,
	CourseTest_TC,
	InternetSpeed_TC,
	CourseRequirements,
	IsActive,
	IsSimple,
	RatingCourse_TC,
	IsPaperQuestionnaire,
	IsQuestionnaired,
	AnnounceDescription,
	AnnounceImage,
	QualAdv_TC
) 
select Course_ID,
	Course_TC,
	ParentCourse_TC,
	ReportsCourse_TC,
	GruppsSheet,
	CourseDirection_TC,
	OurOrg_TC,
	HostingType_TC,
	AuthorizationType_TC,
	CSSClass_TC,
	MaxDiscount,
	OrgPriceCoefficient,
	[Name],
	NameOfficial,
	NameOfficialEn,
	WebName,
	eLearningName,
	BaseHours,
	AdditionalHours,
	Weeks,
	Days,
	NormNumOfStudents,
	MaxNumOfStudents,
	PriceListOrder,
	DontShow,
	IsTrack,
	IsExam,
	IsNew,
	WebPublishSchedule,
	AllowWebOrder,
	ProgramAccessible,
	ProgramURL,
	ProgramFileName,
	ProgramTemplate_TC,
	Prerequisites,
	PrerequisitesShort,
	Introduction,
	WebTopic,
	WebComment,
	OnComplete,
	WebDescription,
	Picture,
	CopyCashPayment,
	IsBookkeeperCourse,
	BCTC_ContentExists,
	BCTC_SeviceExpiresMonths,
	BCTC_SeviceExpiresDateBeg,
	Notes,
	InputDate,
	Employee_TC,
	LastChangeDate,
	LastChanger_TC,
	WebYandexDescription,
	IsFeedingCourse,
	WebShortName,
	WebNameComment,
	CourseDirectionA_TC,
	IsFreeConsultationExists,
	CourseTest_TC,
	InternetSpeed_TC,
	CourseRequirements,
	IsActive,
	IsSimple,
	RatingCourse_TC,
	IsPaperQuestionnaire,
	IsQuestionnaired,
	AnnounceDescription,
	AnnounceImage,
	QualAdv_TC
	from [SSZ001\SQLEE].SPECREPL_replicating.dbo.tcourses
where Course_TC not in (select course_tc from dbo.tCourses as TC)

set identity_insert dbo.tCourses off


set identity_insert dbo.tgroups on

insert into dbo.tGroups (
	Group_ID,
	Color_TC,
	BranchOffice_TC,
	Complex_TC,
	Course_TC,
	LectureType_TC,
	DayShift_TC,
	DateBeg,
	DateEnd,
	TimeBeg,
	TimeEnd,
	DistDateBeg,
	DistDateEnd,
	LiteratureSent,
	LoginPasswordSent,
	Teacher_TC,
	ClassRoom_TC,
	IsEmploymentCenter,
	ForOvertaking,
	SupportingGroup,
	IsBlazing,
	TeacherIsInformed,
	GroupIsInformed,
	Hours,
	HoursAdditional,
	HoursGiven,
	NumOfStudents,
	MaxNumOfStudents,
	DaySequence,
	DaySequence_TC,
	Notes,
	ScheduleHistory,
	IsCheckedByED,
	NotesED,
	Sequence_ID,
	ForUpdate,
	InputDate,
	Employee_TC,
	LastChangeDate,
	LastChanger_TC,
	BaseGroup_ID,
	Curator_TC,
	PrintAnnounce
) 
select 
group_id,
Color_TC,
	BranchOffice_TC,
	Complex_TC,
	Course_TC,
	LectureType_TC,
	DayShift_TC,
	DateBeg,
	DateEnd,
	TimeBeg,
	TimeEnd,
	DistDateBeg,
	DistDateEnd,
	LiteratureSent,
	LoginPasswordSent,
	Teacher_TC,
	ClassRoom_TC,
	IsEmploymentCenter,
	ForOvertaking,
	SupportingGroup,
	IsBlazing,
	TeacherIsInformed,
	GroupIsInformed,
	Hours,
	HoursAdditional,
	HoursGiven,
	NumOfStudents,
	MaxNumOfStudents,
	DaySequence,
	DaySequence_TC,
	Notes,
	ScheduleHistory,
	IsCheckedByED,
	NotesED,
	Sequence_ID,
	ForUpdate,
	InputDate,
	Employee_TC,
	LastChangeDate,
	LastChanger_TC,
	BaseGroup_ID,
	Curator_TC,
	PrintAnnounce
	from [SSZ001\SQLEE].SPECREPL_replicating.dbo.tgroups
where Group_ID not in (select Group_ID from dbo.tGroups as TG)
and DateBeg > getdate()

set identity_insert dbo.tgroups off
