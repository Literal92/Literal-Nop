using Nop.Core;
using Nop.Core.Plugins;
using Nop.Plugin.Company.Media.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Company.Media
{
    public class CompanyMediaSetting : BasePlugin, IAdminMenuPlugin
    {

        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly SettingMediaDbContex _objectContext;
        private readonly IWebHelper _webHelper;

        public CompanyMediaSetting(ILocalizationService localizationService,

            SettingMediaDbContex objectContext,
            IWorkContext workContext,
            IWebHelper webHelper)
        {
            this._localizationService = localizationService;
            this._objectContext = objectContext;
            this._webHelper = webHelper;
            this._workContext = workContext;
        }
        public override void Install()
        {
            _objectContext.Install();
            //locales

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.AddNew", "افزودن");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.Edit", "ویرایش");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.Delete", "حذف");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.Save", "ذخیره اطلاعات");


            //Medias
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Admin.Menu.MediaTitle", "مدیریت رسانه");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Admin.Menu.MediaCategory", "دسته بندی");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Admin.Menu.Media", "گالری");
            base.Install();
        }
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/Media/Configure";
        }
        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {
            _objectContext.Uninstall();
            _localizationService.DeletePluginLocaleResource("Plugins.Company.Admin.Menu.MediaTitle");
            _localizationService.DeletePluginLocaleResource("Plugins.Company.Admin.Menu.Media");
            _localizationService.DeletePluginLocaleResource("Plugins.Company.Admin.Menu.MediaCategory");

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.AddNew", "افزودن");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.Edit", "ویرایش");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.Delete", "حذف");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Company.Media.Save", "ذخیره اطلاعات");
            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            const string adminUrlPart = "Admin/";
            string MediaMenuName = _localizationService.GetResource("Plugins.Company.Admin.Menu.MediaTitle", languageId: _workContext.WorkingLanguage.Id, defaultValue: "مدیریت رسانه");
            string MediaCategoryList = _localizationService.GetResource("Plugins.Company.Admin.Menu.MediaCategory", languageId: _workContext.WorkingLanguage.Id, defaultValue: "دسته بندی");
            string MediaList = _localizationService.GetResource("Plugins.Company.Admin.Menu.Media", languageId: _workContext.WorkingLanguage.Id, defaultValue: "گالری");


            var MediaMainMenu = new SiteMapNode
            {
                Title = MediaMenuName,
                Visible = true,
                SystemName = "Media-Main-Menu",
                IconClass = "fa-play-circle"
            };
            MediaMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = MediaCategoryList,
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "MediaCategory/Configure",
                Visible = true,
                SystemName = "Media-Configure-Menu",
                IconClass = "fa-th-list"
            });
            MediaMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = MediaList,
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "Media/Configure",
                Visible = true,
                SystemName = "Media-Configure-Menu",
                IconClass = "fa-tv"
            });
            rootNode.ChildNodes.Add(MediaMainMenu);
        }


    }
}
