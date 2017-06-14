--
--select top 10 * from tstudents
--order by Student_ID desc
--
--select * from Passport..users
--where UserID = 219001
use SPECREPL_replicating
select count(*) from Passport..users as u
left join dbo.tStudents as orde on WebLogin = Email
where orde.Student_ID is not null and orde.LastName = u.LastName
update Passport..users
set student_id = (select student_id from dbo.tStudents as s where s.weblogin = email)

--
--select top 10 * from dbo.tStudents as s
--where lastname = 'qwe'
--order by Student_ID desc
--
--update Passport..users
--set student_id = 248336
--where email = 'ptolochko@specialist.ru'