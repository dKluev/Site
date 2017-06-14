--select distinct c.Course_TC, BaseHours from vcurrentprices as p
--inner join tcourses as c on c.course_tc = p.course_tc
--where price < 0 and BaseHours = 0

select t.TeacherTC, case t.TeacherTC  when 'БОРЕА' then 1 else 0 end, 'update temployees set sitedescription = ''' +
	convert(varchar(max), t.Info) + ''' where employee_tc = ''' + TeacherTC + ''''
from specialist..Trainer as T
where t.TeacherTC is not null and t.Info is not null


--use SPECREPL_replicating
--update temployees set sitedescription = 'Специализируется на проведении тренингов для корпоративных клиентов Центра.' where employee_tc = 'ПЕВ'
update temployees set sitedescription = '<p>В области информационных технологий Сергей Евгеньевич начал работать с 1986 г.<br> С 1990 г. его основная специализация - системное администрирование и сетевая безопасность<br> комплексных инфраструктур. Принимал участие в сертификационных тренингах RIT Technologies, R&M,<br> семинарах Microsoft по направлениям TTT-2008, ESP, окончил авторизованные<br> курсы Microsoft по треку MCSA-2000.</p>    <p>Сергей Евгеньевич имеет опыт руководства ИТ-подразделениями предприятий среднего<br> и крупного масштаба. Принимал участие во многих крупных проектах, начиная от  консультаций<br> и заканчивая внедрением и эксплуатацией, для таких крупных компаний, как ЦОД ФНС,<br> ФСО Спецсвязь, Государственная Дума РФ, Федеральное казначейство России, ЦТУ ФТС, ФМС РФ,<br> Национальное центральное бюро Интерпола в РФ, Высший Арбитражный Суд РФ,<br> Сберегательный банк РФ, Компания ТНК-ВР,<br> ОАО «Газпром», аэропорт "Шереметьево",<br>ряд ВУЗов г. Москвы (РЭА им. Г.В. Плеханова, МСХА им. К.А. Тимирязева, МФЮА и пр.)</p>    <p>Сергей Евгеньевич старается в доступной манере рассказать о сложных вещах.<br>В своих курсах особое внимание уделяет практическому применению полученных теоретических знаний.</p><br>    <b>Имеет следующие сертификаты:</b><br><br>  <img src="http://images.specialist.ru/Trainers/Certifications/RIT_Smart_Cert.gif" alt="Certified Cabling Installer" hspace="10" vspace="10" border="1" style="border-color:#999999;"/>  <img src="http://images.specialist.ru/Trainers/Certifications/3Com_CSA.jpg" alt="3Com Certified Solutions Associate" hspace="10" vspace="10" border="1" style="border-color:#999999;"/>  ' where employee_tc = 'ШСЕ'