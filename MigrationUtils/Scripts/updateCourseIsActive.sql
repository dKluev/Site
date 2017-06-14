--declare @CurrentPriceListID decimal(18,0)
--
--set @CurrentPriceListID = (
--
--SELECT MAX(pl.PriceList_ID)
--
--FROM SPECREPL_replicating.[dbo].[tPriceLists] AS pl
--
--WHERE pl.IsActive = 1)
--
--update dbo.tCourses
--
--set IsActive = 0
--
--update dbo.tCourses
--
--set IsActive = 1
--
--where Course_TC in (select course_tc from tPrices as TP
--
--where PriceList_ID = @CurrentPriceListID
--
--union
--
--select Track_TC from [SSZ001\SQLEE].SPECREPL_replicating.dbo.tPrices as TP
--
--where PriceList_ID = @CurrentPriceListID)


UPDATE dbo.tCourses

SET IsActive = CASE

         WHEN  EXISTS(SELECT     1 AS Expr1

               FROM         dbo.tPrices AS P

               WHERE     (P.PriceList_ID IN

                            (SELECT     TOP (1) PriceList_ID

                             FROM          dbo.tPriceLists

                             WHERE      (IsActive = 1) and (PriceListDate <= GETDATE())

                             ORDER BY PriceListDate DESC)) AND 

                    (P.Course_TC = dbo.tCourses.Course_TC)   ) THEN 1

         ELSE 0

        END

WHERE IsActive <> CASE

           WHEN  EXISTS(SELECT     1 AS Expr1

                 FROM         dbo.tPrices AS P

                 WHERE     (P.PriceList_ID IN

                              (SELECT     TOP (1) PriceList_ID

                               FROM          dbo.tPriceLists

                               WHERE      (IsActive = 1) and (PriceListDate <= GETDATE())

                               ORDER BY PriceListDate DESC)) AND 

                      (P.Course_TC = dbo.tCourses.Course_TC)   ) THEN 1

           ELSE 0

          END 


