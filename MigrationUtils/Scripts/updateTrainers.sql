--select distinct c.Course_TC, BaseHours from vcurrentprices as p
--inner join tcourses as c on c.course_tc = p.course_tc
--where price < 0 and BaseHours = 0

select t.TeacherTC, case t.TeacherTC  when '�����' then 1 else 0 end, 'update temployees set sitedescription = ''' +
	convert(varchar(max), t.Info) + ''' where employee_tc = ''' + TeacherTC + ''''
from specialist..Trainer as T
where t.TeacherTC is not null and t.Info is not null


--use SPECREPL_replicating
--update temployees set sitedescription = '���������������� �� ���������� ��������� ��� ������������� �������� ������.' where employee_tc = '���'
update temployees set sitedescription = '<p>� ������� �������������� ���������� ������ ���������� ����� �������� � 1986 �.<br> � 1990 �. ��� �������� ������������� - ��������� ����������������� � ������� ������������<br> ����������� �������������. �������� ������� � ���������������� ��������� RIT Technologies, R&M,<br> ��������� Microsoft �� ������������ TTT-2008, ESP, ������� ��������������<br> ����� Microsoft �� ����� MCSA-2000.</p>    <p>������ ���������� ����� ���� ����������� ��-��������������� ����������� ��������<br> � �������� ��������. �������� ������� �� ������ ������� ��������, ������� ��  ������������<br> � ���������� ���������� � �������������, ��� ����� ������� ��������, ��� ��� ���,<br> ��� ���������, ��������������� ���� ��, ����������� ������������ ������, ��� ���, ��� ��,<br> ������������ ����������� ���� ��������� � ��, ������ ����������� ��� ��,<br> �������������� ���� ��, �������� ���-��,<br> ��� ��������, �������� "�����������",<br>��� ����� �. ������ (��� ��. �.�. ���������, ���� ��. �.�. ����������, ���� � ��.)</p>    <p>������ ���������� ��������� � ��������� ������ ���������� � ������� �����.<br>� ����� ������ ������ �������� ������� ������������� ���������� ���������� ������������� ������.</p><br>    <b>����� ��������� �����������:</b><br><br>  <img src="http://images.specialist.ru/Trainers/Certifications/RIT_Smart_Cert.gif" alt="Certified Cabling Installer" hspace="10" vspace="10" border="1" style="border-color:#999999;"/>  <img src="http://images.specialist.ru/Trainers/Certifications/3Com_CSA.jpg" alt="3Com Certified Solutions Associate" hspace="10" vspace="10" border="1" style="border-color:#999999;"/>  ' where employee_tc = '���'