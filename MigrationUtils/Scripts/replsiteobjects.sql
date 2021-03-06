USE [SPECREPL_replicating]
GO
/****** Object:  View [dbo].[vReplSiteObjects]    Script Date: 09/30/2009 17:17:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER VIEW [dbo].[vReplSiteObjects]
WITH SCHEMABINDING
as
select convert(varchar(100), type + convert(varchar(50), id)) as SiteObject_ID, 
	convert(varchar(50), t.type) as [Type],
	t.ID, t.name, t.IsActive, t.IsTag from (
select 
	'tCourses' as [Type], 
	convert(sql_variant, 
	Course_TC) as ID, 
	Course_TC + ' - ' + [Name] as [Name], 
	IsActive,
	convert(bit, 1) as IsTag
	from dbo.tcourses
union
select 'tEmployees', 
	Employee_TC , 
	Employee_TC + ' - ' + lastName + ' ' + FirstName + ' ' + MiddleName, 
	IsActive,
	convert(bit, 1)
from dbo.tEmployees
where EmpGroup_TC = 'ПРЕПОДЫ'
union
select 
	'tVendors', 
	vendor_id, 
	name, 
	IsActive,
	convert(bit, 1)
from dbo.tvendors
union
select 
	'tCertifications', 
	certification_id, 
	name, 
	convert(bit, 1),
	convert(bit, 1)
from dbo.tCertifications
union
select 
	'tCities', 
	city_tc, 
	name, 
	convert(bit, 1),
	convert(bit, 1)
from dbo.tcities
union
select 
	'tComplexes', 
	complex_tc, 
	name, 
	PublishInSite,
	convert(bit, 1)
from dbo.tcomplexes
) as t