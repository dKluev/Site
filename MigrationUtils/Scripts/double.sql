--use SPECREPL_replicating
--ALTER TABLE dbo.tSiteObjectRelations
--drop CONSTRAINT cSiteObjectRelations_UniqueColumns
--UNIQUE (object_id, objecttype,relationobject_id, relationobjecttype)


--select max(SiteObjectRelation_ID), count(*) from dbo.tSiteObjectRelations as TSOR
--group by [Object_ID], ObjectType, RelationObject_ID, RelationObjectType
--having count(*) > 1

--select max(len(type)) from dbo.vSiteObjects as VSO

--delete dbo.tSiteObjectRelations
--where SiteObjectRelation_ID = 2223
--
--use SpecialistWeb
--delete dbo.tSiteObjectRelations
--where SiteObjectRelation_ID in (
--select siteobjectrelation_id from tsiteobjectrelations
--where not exists (select 1 from vsiteobjects
--		where (id = relationobject_id and type = relationobjecttype)
--		)
--union		
--select siteobjectrelation_id from tsiteobjectrelations
--where not exists (select 1 from vsiteobjects
--		where (id = object_id and type = objecttype)))

use SpecialistWeb
		
select * from tsiteobjectrelations
where not exists (select 1 from vsiteobjects
		where (id = relationobject_id and type = relationobjecttype)
		)
union		
select * from tsiteobjectrelations
where not exists (select 1 from vsiteobjects
		where (id = object_id and type = objecttype))
		
--		
--		select * from tcertifications
--update dbo.UserMessages
--set Title = 'qweqwe'
--where UserMessageID = 48166
--select * from dbo.vSiteObjects as VSO
--where [Type] = 'tCourses'
