select * from dbo.tSiteObjectRelations as TSOR
where ([Object_ID] = 334 and ObjectType = 'tSections' )
or (RelationObject_ID = 334 and RelationObjectType = 'tSections')

select * from dbo.tSiteObjectRelations as TSOR
where ([Object_ID] = 336 and ObjectType = 'tSections' )
or (RelationObject_ID = 336 and RelationObjectType = 'tSections')
--
--SELECT [t2].[SiteObject_ID], [t2].[Type], [t2].[ID], [t2].[Name], [t2].[IsActive], [t2].[IsTag], [t1].[value] AS [weight]
--FROM (
--    SELECT COUNT(*) AS [value], [t0].[ObjectType], [t0].[Object_ID]
--    FROM [SpecialistWeb].[dbo].[tSiteObjectRelations] AS [t0]
--    GROUP BY [t0].[ObjectType], [t0].[Object_ID]
--    ) AS [t1]
--INNER JOIN [SpecialistWeb].[dbo].[vSiteObjects] AS [t2] ON ([t2].[Type] = [t1].[ObjectType]) AND ([t2].[ID] = [t1].[Object_ID])

SELECT [t2].[SiteObject_ID], [t2].[Type], [t2].[ID], [t2].[Name], [t2].[IsActive], [t2].[IsTag], [t1].[value] AS [weight]
FROM (
    SELECT COUNT(*) AS [value], [t0].[RelationObjectType], [t0].[RelationObject_ID]
    FROM [SpecialistWeb].[dbo].[tSiteObjectRelations] AS [t0]
    GROUP BY [t0].[RelationObjectType], [t0].[RelationObject_ID]
    ) AS [t1]
INNER JOIN [SpecialistWeb].[dbo].[vSiteObjects] AS [t2] ON ([t2].[Type] = [t1].[RelationObjectType]) AND ([t2].[ID] = [t1].[RelationObject_ID])

