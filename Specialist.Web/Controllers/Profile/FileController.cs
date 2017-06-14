using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcContrib.Attributes;
using MvcContrib.Filters;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Services.Center.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Exceptions;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Util;
using SimpleUtils.Extension;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Const;
using Specialist.Web.Common.Utils.Files;

namespace Specialist.Web.Controllers
{ 
    [Auth(RoleList = Role.Trainer)]
    public class FileController: ViewController
    {
		protected override bool IsBootStrap {
			get { return true; }
		}
        [Dependency]
        public IRepository<UserFile> FileService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IRepository2<Lecture> LectureService { get; set; }

        [Dependency]
        public IEmployeeVMService EmployeeVMService { get; set; }

        [Dependency]
        public IRepository<CourseFile> CourseFileService { get; set; }

        [Dependency]
        public IRepository<GroupFile> GroupFileService { get; set; }

        [Dependency]
        public IRepository2<LectureFile> LectureFileService { get; set; }

        public ActionResult List(int? pageIndex)
        {
			if(pageIndex.GetValueOrDefault() <= 0) return RedirectToAction(() => List(1));
            var files = FileService.GetAll().Where(f => f.UserID == User.UserID)
				.OrderByDescending(x => x.UserFileID)
                .ToPagedList(pageIndex.Value - 1,20);
            return BaseView(Views.File.List, new UserFileListVM { Files = files });
        }

        [ModelStateToTempData]
        public ActionResult Add() {
	        var model = new UserFileVM {IsNew = true};
	        InitModel(model);
	        return BaseView(Views.File.Edit, model);
        }

	    private void InitModel(UserFileVM model) {
		    var employeeTC = User.Employee_TC;
		    var week = DateTime.Today.AddDays(14);
		    var pastWeek = DateTime.Today.AddDays(-14);
		    var groups = EmployeeVMService.GetTrainerGroups(employeeTC)
				.Where(x => x.LectureType_TC != LectureTypes.OpenLearning)
			    .Where(x => 
					(pastWeek <= x.DateBeg && x.DateBeg <= week) ||
					(pastWeek <= x.DateEnd && x.DateEnd <= week) ).ToList()
				.OrderBy(x => x.DateBeg).ToList();
		    var courses = EmployeeVMService.GetCourses().CourseHasVideos.Select(x => x.Item1).ToList();
		    model.Groups = groups;
		    model.Courses = courses;
	    }

	    public ActionResult AddFileTo(FileListVM model)
        {
            var file = FileService.GetByPK(model.SelectedFileID);
            if(model.Course != null)
            {
                file.CourseFiles.Add(
                    new CourseFile
                    {
                        Course_TC = model.Course.Course_TC,
                        UserFileID = model.SelectedFileID
                    });
            }
            
            if(model.Group != null)
            {
                file.GroupFiles.Add(
                    new GroupFile()
                    {
                        Group_ID = model.Group.Group_ID,
                        UserFileID = model.SelectedFileID
                    });
            }
            FileService.SubmitChanges();
            return RedirectBack();
        }

        public ActionResult DeleteFileFrom(int fileID, string courseTC, decimal? groupID)
        {
            if (courseTC != null)
            {
                var courseFile = CourseFileService.GetAll().Where(cf =>
                    cf.Course_TC == courseTC && cf.UserFileID == fileID);
                CourseFileService.DeleteAndSubmit(courseFile);
            }

            if (groupID != null)
            {
                var groupFile = GroupFileService.GetAll().Where(cf =>
                    cf.Group_ID == groupID && cf.UserFileID == fileID);
                GroupFileService.DeleteAndSubmit(groupFile);
            }
            FileService.SubmitChanges();
            return RedirectBack();
        }

        public ActionResult Delete(int fileID)
        {
            var file = FileService.GetByPK(fileID);
            if (file.CourseFiles.Count > 0 || file.GroupFiles.Count > 0)
                return RedirectBack();
            if(file.UserID != User.UserID)
                throw new NotOwnerException("File");
            FileService.DeleteAndSubmit(file);
            UserFiles.DeleteFile(file.UserFileID);
            return RedirectBack();
        }

        [ModelStateToTempData]
        public ActionResult Edit(int fileID)
        {
            var file = FileService.GetByPK(fileID);
			if (file.UserID != User.UserID) {
				throw new PermissionException("file edit");
			}
	        var model = new UserFileVM {UserFile = file};
			InitModel(model);
			model.GroupIds = GroupFileService.GetAll(x => x.UserFileID == fileID).Select(x => x.Group_ID).ToList();
			model.CourseTCs = CourseFileService.GetAll(x => x.UserFileID == fileID)
				.Select(x => x.Course_TC).ToList();
	        return BaseView(Views.File.Edit, model);
        }

       

        [HttpPost]
        [ModelStateToTempData]
        public ActionResult Edit(UserFileVM model)
        {
            if (model.UserFile == null)
                model.UserFile = new UserFile();
            model.UploadFile = UserFiles.GetUploadFile(Request.Files[0]);
            if(!FluentValidate(model))
            {
                return RedirectBack();
            }


            UserFile userFile = null;
	        var courseTCs = model.CourseTCs;
	        var groupIds = model.GroupIds;
	        if(model.IsNew)
            {
                userFile = model.UserFile;
                userFile.UserID = User.UserID;
                userFile.SysFileName = model.UploadFile.Name;
		        var courses = GetCourses(model.CourseTCs);
	            var groups = GetGroups(model.GroupIds);
				userFile.CourseFiles.AddRange(courses);
				userFile.GroupFiles.AddRange(groups);
                FileService.InsertAndSubmit(userFile);
            }
            else
            {
	            var userFileId = model.UserFile.UserFileID;
	            userFile = FileService.GetByPK(userFileId);
                if(userFile.UserID != User.UserID)
                    throw new NotOwnerException("File");
                userFile.Update(model.UserFile, x => x.Name, x => x.Description);

                if(!model.UploadFile.IsEmpty)
                    userFile.SysFileName = model.UploadFile.Name;


		        FileService.SubmitChanges();
	            ChangeCourses(userFileId, courseTCs);
	            ChangeGroups(userFileId, groupIds);
            }

            if(!model.UploadFile.IsEmpty) {
                UserFiles.DeleteFile(userFile.UserFileID);
                var hpf = Request.Files[0];
                hpf.SaveAs(UserFiles.GetUserFileSys(userFile.UserFileID, 
                    model.UploadFile.Name));
            }

            return RedirectToAction(() => List(1));
        }

	    private void ChangeGroups(int userFileId, List<decimal> newGroupIds) {
		    var groupFiles = GroupFileService.GetAll(x => x.UserFileID == userFileId)
			    .ToList();
		    var currentGroupIds = groupFiles.Select(x => x.Group_ID);
		    var remove = groupFiles.Where(tc => !newGroupIds.Contains(tc.Group_ID)).ToList();
		    remove.ForEach(GroupFileService.Delete);
		    var add = GetGroups(newGroupIds.Except(currentGroupIds)).ToList();
		    add.ForEach(x => x.UserFileID = userFileId);
		    add.ForEach(GroupFileService.Insert);
		    GroupFileService.SubmitChanges();
	    }
	    private void ChangeCourses(int userFileId, List<string> newCourseTCs){
		    var courseFiles = CourseFileService.GetAll(x => x.UserFileID == userFileId)
			    .ToList();
		    var currentCourseIds = courseFiles.Select(x => x.Course_TC);
		    var remove = courseFiles.Where(tc => !newCourseTCs.Contains(tc.Course_TC)).ToList();
		    remove.ForEach(CourseFileService.Delete);
		    var add = GetCourses(newCourseTCs.Except(currentCourseIds)).ToList();
		    add.ForEach(x => x.UserFileID = userFileId);
		    add.ForEach(CourseFileService.Insert);
		    CourseFileService.SubmitChanges();
	    }

	    private static IEnumerable<CourseFile> GetCourses(IEnumerable<string> tcs) {
		    return tcs.Select(x => new CourseFile {Course_TC = x});
	    }

	    private static IEnumerable<GroupFile> GetGroups(IEnumerable<decimal> groupIds) {
		    return groupIds.Select(x => new GroupFile {Group_ID = x});
	    }

	    public void CheckLecturePermission(decimal lectureId) {
		    if (LectureService.GetValues(lectureId, x => x.Group.Teacher_TC) != User.Employee_TC) {
			   throw new PermissionException("lecture file");
		    }
	    }
	    public ActionResult AddLectureFile(decimal lectureId) {
		    var file = Request.Files[0];
		    var errors = UserFiles.Validate(file, LectureFiles.size, LectureFiles.ext);
		    if (errors.Any()) {
			    return Json(errors.JoinWith(Environment.NewLine));
		    }
			CheckLecturePermission(lectureId);
			LectureFileService.EnableTracking();
		    var lectureFile = LectureFileService.FirstOrDefault(x => x.Lecture_ID == lectureId);
		    if (lectureFile == null) {
			    lectureFile = new LectureFile();
			    lectureFile.Lecture_ID = lectureId;
				LectureFileService.InsertAndSubmit(lectureFile);
		    }
			file.SaveAs(LectureFiles.GetFile(lectureFile.Id).Path);
		    return Json("ok");
	    }
    }
}