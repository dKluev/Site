USE [SpecialistWeb]
GO
/****** Object:  View [dbo].[vSiteObjects]    Script Date: 10/22/2009 14:10:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter VIEW [dbo].[vTags]
as
select convert(varchar(100), type + convert(varchar(50), id)) as TagID, 
	convert(varchar(50), t.type) as [Type],
	t.ID, t.Name, t.UrlName from (

select 
	'tCourses' as [Type], 
	convert(sql_variant, 
	Course_TC) as ID, 
	isnull(webshortname, WebName) as name,
	UrlName
	from specrepl_replicating.dbo.tcourses
	where isactive = 1
union
select 
	'tVendors', 
	vendor_id, 
	name, 
	UrlName
	from specrepl_replicating.dbo.tvendors
	where isactive = 1
union
select 
	'tCertifications', 
	certification_id, 
	name, 
	UrlName
	from dbo.tCertifications
	where isactive = 1
union
select 
	'tSiteTerms' as [Type], 
	convert(sql_variant, 
	siteterm_id) as ID, 
	name as [Name], 
	UrlName
from dbo.tsiteterms
union
select 
	'tSections', 
	section_id, 
	name, 
	UrlName
	from dbo.tsections
	where isactive = 1
union
select 
	'tProducts', 
	product_id, 
	name, 
	UrlName
	from dbo.tProducts
	where isactive = 1
union
select 
	'tProfessions', 
	profession_id, 
	name, 
	UrlName
	from dbo.tProfessions
	where isactive = 1
) as t