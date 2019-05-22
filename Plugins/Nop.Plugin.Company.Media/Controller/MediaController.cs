using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System.Linq;
using Nop.Services.Security;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Company.Media.Domain;
using Nop.Plugin.Company.Media.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Nop.Core.Infrastructure;
using Nop.Core;
using Nop.Services.Localization;
using Castle.Core.Logging;
using System.Globalization;
using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Company.Media.Services.MediaCategory;
using Nop.Services.Common;
using Nop.Plugin.Company.Media.Services.MediaCategories;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Nop.Plugin.Company.Media.Model;

namespace Nop.Plugin.Company.Media.Controller
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DisplayAttribute>(false)
                      ?.Name
                      ?? val.ToString();
        }
    }

    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class MediaController : BasePluginController
    {
        private readonly IMediaService _MediaService;                
        private readonly IMediaCategoryService _MediaCategoryService;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IMediaCategoriesService _MediaCategoriesService;

        public MediaController(ILocalizationService localizationService,
            IMediaService MediaService,
            IPermissionService permissionService,            
            IGenericAttributeService genericAttributeService,            
            IMediaCategoryService MediaCategoryService,
            IMediaCategoriesService MediaCategoriesService
            )
        {
            this._localizationService = localizationService;
            this._MediaService = MediaService;
            this._permissionService = permissionService;
            this._MediaService = MediaService;            
            this._genericAttributeService = genericAttributeService;            
            this._MediaCategoryService = MediaCategoryService;
            this._MediaCategoriesService = MediaCategoriesService;
        }


        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();


            return View("~/Plugins/Company.Media/Views/Media/Configure.cshtml");
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Configure(MediaModel model)
        {
            return Configure();
        }
        [HttpPost]
        [AdminAntiForgery]
        public IActionResult List(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedKendoGridJson();

            var pickupPoints = _MediaService.GetAllsMedia(pageIndex: command.Page - 1, pageSize: command.PageSize);
            var model = pickupPoints.Select(point =>
            {
                return new MediaModel()
                {
                    Id = point.Id,
                    Title = point.Title,
                    CreateDates = Tools.Core.Date.ConvertDateTime.ToShorShamsi(point.CreateDate),
                    FileExtention=point.FileExtention,
                    CategoryName=_MediaCategoryService.CategoryNavigation(_MediaCategoriesService.GetLastCategory(point.Id).Id),
                    CustomerName = _genericAttributeService.GetAttributesForEntity(point.UserId, "Customer").Where(a => a.Key == "LastName").FirstOrDefault().Value,
                };
            }).ToList();

            return Json(new DataSourceResult
            {
                Data = model,
                Total = pickupPoints.TotalCount
            });
        }

        public IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();
            var model = new MediaModel();
            model.Id = 0;
            model.CategoryList = new System.Collections.Generic.List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = "0",
                    Text = "بدون انتخاب",
                    Selected = true,
                }
            };
            model.CategoryList.AddRange(_MediaCategoryService.GetAllsMediaCategory().Select(x => new SelectListItem()
            {
                Text = _MediaCategoryService.CategoryNavigation(x.Id),
                Value = x.Id.ToString(),
                Selected = false
            }).ToList());
            return View("~/Plugins/Company.Media/Views/Media/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [AdminAntiForgery]
        public async Task<IActionResult> CreateAsync(MediaModel model, IFormFile file, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();
            try
            {
                if (model.Title == null)
                    return RedirectToAction("Create");
                if (model.CategoryId == 0)
                    return RedirectToAction("Create");
                #region FileUpload
                if (file == null || file.Length == 0)
                    return RedirectToAction("Create");

                var Extention = Path.GetExtension(file.FileName);


                if (!Directory.Exists("wwwroot/Media/Company.Media/Media"))
                {
                    Directory.CreateDirectory("wwwroot/Media/Company.Media/Media");
                }
                var path = "";
                if (System.IO.File.Exists("wwwroot/Media/Company.Media/Media/" + file.FileName))
                {

                    var Exist = true;
                    int Count = 1;
                    var StepCount = 0;
                    while (Exist)
                    {
                        if (!System.IO.File.Exists("wwwroot/Media/Company.Media/Media/" + Count.ToString() + "_" + file.FileName))
                        {
                            Exist = false;
                            StepCount = Count;
                        }
                        Count++;
                    }
                    var Existpath = StepCount.ToString() + "_" + file.FileName;
                    path = Path.Combine(
                                           Directory.GetCurrentDirectory(), "wwwroot/Media/Company.Media/Media",
                                           Existpath);
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media/Company.Media/Media", file.FileName);
                }


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                #endregion

                var Customer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

                #region MediaTable

                var MediaModel = new Media.Domain.Media()
                {
                    URL = $"/Media/Company.Media/Media/{file.FileName}",
                    FileName = file.FileName,
                    CreateDate = DateTime.Now,
                    PublishDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    ContentType = file.ContentType,
                    MediaStatus = Media.Domain.Media.StatusType.Active,
                    UserId = Customer.Id,
                    Title = model.Title,
                    AltText=model.Text,
                    FileExtention= Extention
                       
                };
                _MediaService.InsertMedia(MediaModel);
                #endregion


                var parent = true;
                while (parent)
                {
                    var Mediacategories = new MediaCategories()
                    {
                        MediaId = MediaModel.Id,
                        MediaCategoryId = model.CategoryId
                    };
                    _MediaCategoriesService.InsertMediaCategories(Mediacategories);
                    var category = _MediaCategoryService.GetMediaCategoryById(model.CategoryId);
                    if (category.MediaCategoryParentId != 0)
                    {
                        model.CategoryId = category.MediaCategoryParentId;
                    }
                    else
                    {
                        parent = false;
                    }
                }
                ViewBag.RefreshPage = true;
                model.CategoryList = new System.Collections.Generic.List<SelectListItem>();
                return View("~/Plugins/Company.Media/Views/Media/Create.cshtml", model);
            }
            catch (Exception exception)
            {
                ViewBag.RefreshPage = true;
                model.CategoryList = new System.Collections.Generic.List<SelectListItem>();
                return View("~/Plugins/Company.Media/Views/Media/Create.cshtml", model);
            }
        }

        public IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var Media = _MediaService.GetMediaById(id);
            if (Media == null)
                return RedirectToAction("Configure");
            

            var model = new MediaModel
            {
                Id=Media.Id,
                Title=Media.Title,
                URL=Media.URL
            };
            var mediacategory = _MediaCategoriesService.GetLastCategory(Media.Id);
            model.CategoryList = new System.Collections.Generic.List<SelectListItem>
            {
                new SelectListItem()
                {
                    Value = mediacategory.Id.ToString(),
                    Text = _MediaCategoryService.CategoryNavigation(mediacategory.Id),
                    Selected = true,
                }
            };
            model.CategoryList.AddRange(_MediaCategoryService.GetAllsMediaCategory().Where(a=>a.Id != mediacategory.Id).Select(x => new SelectListItem()
            {
                Text = _MediaCategoryService.CategoryNavigation(x.Id),
                Value = x.Id.ToString(),
                Selected = false
            }).ToList());


            return View("~/Plugins/Company.Media/Views/Media/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public IActionResult Edit(MediaModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var Medias = _MediaService.GetMediaById(model.Id);
            if (Medias == null)
                return RedirectToAction("Configure");
            Medias.Title = model.Title;
            Medias.AltText = model.Text;


            //MediasCategoryUpdate
            var LastCategory = _MediaCategoriesService.GetLastCategory(Medias.Id);

            if (model.CategoryId != LastCategory.Id)
            {
                //Delete Pervious
                var PerviousCategory = _MediaCategoriesService.GetAllsMediaCategories().Where(a=>a.MediaId == Medias.Id);
                foreach (var item in PerviousCategory)
                {
                    var categoris = new MediaCategories();
                    categoris = _MediaCategoriesService.GetMediaCategoriesById(item.Id);
                    _MediaCategoriesService.DeleteMediaCategories(categoris);
                }
                //AddNew
                var parent = true;
                while (parent)
                {
                    var Mediacategories = new MediaCategories()
                    {
                        MediaId = Medias.Id,
                        MediaCategoryId = model.CategoryId
                    };
                    _MediaCategoriesService.InsertMediaCategories(Mediacategories);
                    var category = _MediaCategoryService.GetMediaCategoryById(model.CategoryId);
                    if (category.MediaCategoryParentId != 0)
                    {
                        model.CategoryId = category.MediaCategoryParentId;
                    }
                    else
                    {
                        parent = false;
                    }
                }

            }


            _MediaService.UpdateMedia(Medias);

            ViewBag.RefreshPage = true;
            model.CategoryList = new System.Collections.Generic.List<SelectListItem>();
            return View("~/Plugins/Company.Media/Views/Media/Edit.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            var Media = _MediaService.GetMediaById(id);
            if (Media == null)
                return RedirectToAction("Configure");
            Media.MediaStatus = Domain.Media.StatusType.Deleted;
            _MediaService.UpdateMedia(Media);
            //_MediaService.DeleteMedia(Media);
            return new NullJsonResult();
        }
    }
}
