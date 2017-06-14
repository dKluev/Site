use SPECREPL_replicating

select 'update tcourses set websortorder = ' + convert(varchar(max), websortorder) 
	+' where course_tc = ''' + course_TC + ''''
from dbo.tCourses as TC  

select 'update tcourses set ' + case  when webshortname is null then '' else 'webshortname = ''' + webshortname 
	+ '''' end 
	+' where course_tc = ''' + course_TC + ''''
from dbo.tCourses as TC  
where WebShortName is not null