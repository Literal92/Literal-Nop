using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Core.Domain.Directory;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Company.Media.Domain;
using Nop.Plugin.Company.Media.Services.MediaCategory;
using Nop.Plugin.Company.Media.ViewModel;

namespace Nop.Plugin.Company.Media.Controller
{   
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class MediaCategoryController : BasePluginController
    {
        private readonly IMediaCategoryService _MediaCategoryService;
        private readonly IPermissionService _permissionService;
        public MediaCategoryController(
            IMediaCategoryService MediaCategoryService,
            IPermissionService permissionService
            )
        {
            this._MediaCategoryService = MediaCategoryService;
            this._permissionService = permissionService;
        }
        public IActionResult Configure(int Id = 0)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();
            CategoryViewModel model = new CategoryViewModel
            {
                Id = Id,
                Navigation = _MediaCategoryService.CategoryNavigation(Id)
            };

            return View("~/Plugins/Company.Media/Views/MediaCategory/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Configure(MediaCategory model)
        {
            return Configure();
        }
        [HttpPost]
        public IActionResult List(DataSourceRequest command, int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedKendoGridJson();

            if (Id == 0)
            {
                var pickupPoints = _MediaCategoryService.GetMediaCategoryByLevelAndParentId(0, pageIndex: command.Page - 1, pageSize: command.PageSize);
                var model = pickupPoints.Select(point =>
                {
                    var store = _MediaCategoryService.GetMediaCategoryById(point.Id);
                    return new MediaCategory()
                    {
                        Id = point.Id,
                        MediaCategoryName = point.MediaCategoryName,
                        MediaCategoryLatinName = point.MediaCategoryLatinName,
                        MediaCategoryCode = point.MediaCategoryCode,
                    };
                }).ToList();

                return Json(new DataSourceResult
                {
                    Data = model,
                    Total = pickupPoints.TotalCount
                });
            }
            else
            {
                var Category = _MediaCategoryService.GetMediaCategoryById(Id);
                var pickupPoints = _MediaCategoryService.GetMediaCategoryByLevelAndParentId(Category.Id, pageIndex: command.Page - 1, pageSize: command.PageSize); var model = pickupPoints.Select(point =>
                {
                    var store = _MediaCategoryService.GetMediaCategoryById(point.Id);
                    return new MediaCategory()
                    {
                        Id = point.Id,
                        MediaCategoryName = point.MediaCategoryName,
                        MediaCategoryLatinName = point.MediaCategoryLatinName,
                        MediaCategoryCode = point.MediaCategoryCode
                    };
                }).ToList();

                return Json(new DataSourceResult
                {
                    Data = model,
                    Total = pickupPoints.TotalCount
                });

            }

        }

        public IActionResult Create(int Id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();
            var model = new MediaCategory();
            model.MediaCategoryParentId = Id;
            return View("~/Plugins/Company.Media/Views/MediaCategory/Create.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Create(MediaCategory model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();


            var MediaCategory = new MediaCategory()
            {
                MediaCategoryName = model.MediaCategoryName,
                MediaCategoryLatinName = model.MediaCategoryLatinName,
                MediaCategoryCode = model.MediaCategoryCode,
                MediaCategoryStatus = Domain.MediaCategory.StatusType.Active,

            };
            if (model.MediaCategoryParentId != 0)
            {
                MediaCategory.MediaCategoryParentId = model.MediaCategoryParentId;
                var parent = _MediaCategoryService.GetMediaCategoryById(model.MediaCategoryParentId);
                MediaCategory.MediaCategoryLevel = parent.MediaCategoryLevel + 1;
            }
            else
            {
                MediaCategory.MediaCategoryLevel = 0;
            }
            _MediaCategoryService.InsertMediaCategory(MediaCategory);
            ViewBag.RefreshPage = true;
            return View("~/Plugins/Company.Media/Views/MediaCategory/Create.cshtml", model);
        }

        public IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var MediaCategory = _MediaCategoryService.GetMediaCategoryById(id);
            if (MediaCategory == null)
                return RedirectToAction("Configure");

            var model = new MediaCategory
            {
                Id = MediaCategory.Id,
                MediaCategoryName = MediaCategory.MediaCategoryName,
                MediaCategoryLatinName = MediaCategory.MediaCategoryLatinName,
                MediaCategoryCode = MediaCategory.MediaCategoryCode,
            };
            return View("~/Plugins/Company.Media/Views/MediaCategory/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Edit(MediaCategory model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var MediaCategory = _MediaCategoryService.GetMediaCategoryById(model.Id);
            if (MediaCategory == null)
                return RedirectToAction("Configure");


            MediaCategory.MediaCategoryName = model.MediaCategoryName;
            MediaCategory.MediaCategoryLatinName = model.MediaCategoryLatinName;
            MediaCategory.MediaCategoryCode = model.MediaCategoryCode;

            _MediaCategoryService.UpdateMediaCategory(MediaCategory);
            ViewBag.RefreshPage = true;
            return View("~/Plugins/Company.Media/Views/MediaCategory/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var MediaCategorys = _MediaCategoryService.GetMediaCategoryById(id);
            if (MediaCategorys == null)
                return RedirectToAction("Configure");

            MediaCategorys.MediaCategoryStatus = MediaCategory.StatusType.Deleted;
            _MediaCategoryService.UpdateMediaCategory(MediaCategorys);
            return new NullJsonResult();
        }
    }
}
