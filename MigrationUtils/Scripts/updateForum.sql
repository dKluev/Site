--use Forum
--select * from dbo.Discussions as D
--delete from Specialist..MessageSections
--set identity_insert Specialist..MessageSections on
--use Specialist
--update dbo.MessageSections
--set ParentSectionID = 58
--where ParentSectionID is null and [SysName] is null
--
--select * from dbo.MessageSections as MS
--where ParentSectionID is null and SysName is null

--insert into Specialist..MessageSections (
--	MessageSectionID,
--	ParentSectionID,
--	[Name],
--	Description,
--	IsActive
--) 
--select DiscussionID, case parentdiscussionid when 0 then null else parentdiscussionid end, title, description, isactive from Forum..Discussions as D


use Specialist
set identity_insert Specialist..UserMessages on
insert into dbo.UserMessages ( UserMessageID,
	title,
	[Text],
	CreatorUserID,
	CreateDate,
	UpdateDate,
	ParentMessageID,
	MessageSectionID,
	IsActive)
select ThreadID, convert(varchar(100), title), convert(varchar(1000),Description), UserID, CreateDate, UpdateDate, null, DiscussionID, IsActive from forum..threads
where UserID is not null

set identity_insert Specialist..UserMessages off

insert into dbo.UserMessages ( 
title,
	[Text],
	
	CreatorUserID,
	CreateDate,
	UpdateDate,
	ParentMessageID,
	MessageSectionID,
	IsActive)
select convert(varchar(100), title), convert(varchar(1000),text), UserID, CreateDate, UpdateDate, ThreadID, null, IsActive from forum..Messages as M
where UserID is not null and ThreadID in (select UserMessageID from dbo.UserMessages as UM)

select top 1 * from forum..threads
select top 1 * from forum..messages