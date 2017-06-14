using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcContrib.Attributes;
using MvcContrib.Filters;
using SimpleUtils.Common.Extensions;
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

namespace Specialist.Web.Controllers
{ 
    [Auth]
    public class CompanyFileController: ViewController
    {
        [Dependency]
        public IRepository<CompanyFile> CompanyFileService { get; set; }


        [Dependency]
        public IRepository<Company> CompanyService { get; set; }

        public ActionResult List()
        {
            var files = CompanyFileService.GetAll().Where(f => f.UserID == User.UserID
				|| f.CompanyID == User.CompanyID)
				.OrderByDescending(x => x.Id)
                .ToList();
            return BaseView(Views.CompanyFile.List, new CompanyFileListVM { Files = files, User = User });
        }

        [ModelStateToTempData]
        public ActionResult Add() {
	        var model = new CompanyFileVM {IsNew = true, User = User};
			InitModel(model);
	        return BaseView(Views.CompanyFile.Edit, model);
        }

	    public void InitModel(CompanyFileVM model) {
		    model.Companies = CompanyService.GetAll(x => x.Users.Any(y => y.Org_ID.HasValue)).ToList();
	    }


        public ActionResult Delete(int fileID)
        {
            var file = CompanyFileService.GetByPK(fileID);
            if(file.UserID != User.UserID)
                throw new NotOwnerException("File");
            CompanyFileService.DeleteAndSubmit(file);
            CompanyFiles.DeleteFile(file.Id);
            return RedirectBack();
        }

        [ModelStateToTempData]
        public ActionResult Edit(int fileID)
        {
            var file = CompanyFileService.GetByPK(fileID);
			if (file.UserID != User.UserID) {
				throw new PermissionException("file edit");
			}
	        var model = new CompanyFileVM {
		        CompanyFile = file,
		        User = User
	        };
			InitModel(model);
	        return BaseView(Views.CompanyFile.Edit, model);
        }

       

        [HttpPost]
        [ModelStateToTempData]
        public ActionResult Edit(CompanyFileVM model)
        {
            if (model.CompanyFile == null)
                model.CompanyFile = new CompanyFile();
            model.UploadFile = CompanyFiles.GetUploadFile(Request.Files[0]);
            if(!FluentValidate(model))
            {
                return RedirectBack();
            }


            CompanyFile companyFile = null;
	        if (model.CompanyFile.Name.IsEmpty()) {
		        model.CompanyFile.Name = model.UploadFile.Name;
	        }
	        if(model.IsNew)
            {
                companyFile = model.CompanyFile;
                companyFile.UserID = User.UserID;
                companyFile.SysFileName = model.UploadFile.Name;
                CompanyFileService.InsertAndSubmit(companyFile);
            }
            else
            {
	            var userFileId = model.CompanyFile.Id;
	            companyFile = CompanyFileService.GetByPK(userFileId);
                if(companyFile.UserID != User.UserID)
                    throw new NotOwnerException("File");
                companyFile.Update(model.CompanyFile, x => x.Name, x => x.CompanyID);

                if(!model.UploadFile.IsEmpty)
                    companyFile.SysFileName = model.UploadFile.Name;


		        CompanyFileService.SubmitChanges();
            }

            if(!model.UploadFile.IsEmpty) {
                CompanyFiles.DeleteFile(companyFile.Id);
                var hpf = Request.Files[0];
                hpf.SaveAs(CompanyFiles.GetUserFileSys(companyFile.Id, model.UploadFile.Name));
            }

            return RedirectToAction(() => List());
        }

    }
}