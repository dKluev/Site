--use Specialist
--
--
--
--set identity_insert  SpecialistWeb..news on
--
--insert into SpecialistWeb..News (newsid,
--	Title,
--	Description,
--	ShortDescription,
--	PublishDate,
--	[Type],
--	IsActive
--)
--select newsid,
--	Header,
--	convert(varchar(5000), News),
--	convert(varchar(2000), isnull(shortnews, news)),
--	NewsDate,
--	[Type],
--	1
--from news
--where Header is not null and NewsDate is not null
--
----select * from dbo.News
----where newsdate is null
--
--set identity_insert SpecialistWeb..News off

use SpecialistWeb

select newsid, ',' + RelCourses from Specialist..News 
where RelCourses is not null and RelCourses <> ''


select * from dbo.tSiteObjectRelations as TSOR
where ObjectType = 'News' or RelationObjectType = 'News'