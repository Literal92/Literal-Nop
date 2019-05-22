using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
using Nop.Core.Plugins;
using Nop.Plugin.Faradata.Test.Domain;
using Nop.Plugin.Pickup.PickupInStore.Services;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Shipping.Pickup;
using Nop.Services.Shipping.Tracking;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest
{
    public class FardataFirstTestSetting : BasePlugin, IAdminMenuPlugin
    {
        private readonly ISettingService _settingService;
        //private readonly IAdminMenuPlugin _adminMenu;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly CustomerTokenObjectContext _objectContext;

        public FardataFirstTestSetting(ISettingService settingService, IWebHelper webHelper, ILocalizationService localizationService,
                                    IWorkContext workContext,
                                    CustomerTokenObjectContext objectContext)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
            _workContext = workContext;
            _objectContext = objectContext;
        }

        //public PluginDescriptor PluginDescriptor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Api/FaradataFirstTest/Configure";
        }

        public override void Install()
        {
            _objectContext.Install();

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Title", "API");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Title", "API");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Configuration.Title", "Settings");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Get.Title", "Get");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Index.Title", "Index");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.PublicInfo.Title", "PublicInfo");

            //ساخت جدول مورد نظر


            //var settings = new FirstTsetSetting();
            //_settingService.SaveSetting(settings);
            base.Install();
        }

        public override void Uninstall()
        {
            //حذف جدول مورد نظر
            _objectContext.Uninstall();

            _localizationService.DeletePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Configuration.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Get.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.Index.Title");
            _localizationService.DeletePluginLocaleResource("Plugins.FaradataFirstTest.Admin.Menu.PublicInfo.Title");
            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            const string adminUrlPart = "Admin/";

            string pluginMenuName = _localizationService.GetResource("Plugins.FaradataFirstTest.Admin.Menu.Title", languageId: _workContext.WorkingLanguage.Id, defaultValue: "MyTest");
            var pluginMainMenu = new SiteMapNode
            {
                Title = pluginMenuName,
                Visible = true,
                SystemName = "FaradataTestInfo-Main-Menu",
                IconClass = "fa-genderless"
            };

            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = "Configuration",
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "FaradataFirstTest/Configuration",
                Visible = true,
                SystemName = "FaradataTestInfo-Configuration-Menu",
                IconClass = "fa-genderless"
            });
            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = "Get",
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "FaradataFirstTest/Get",
                Visible = true,
                SystemName = "FaradataTestInfo-Get-Menu",
                IconClass = "fa-genderless"
            });
            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = "Index",
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "FaradataFirstTest/Index",
                Visible = true,
                SystemName = "FaradataTestInfo-Index-Menu",
                IconClass = "fa-genderless"
            });
            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = "PublicInfo",
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "FaradataFirstTest/PublicInfo",
                Visible = true,
                SystemName = "FaradataTestInfo-PublicInfo-Menu",
                IconClass = "fa-genderless"
            });

            rootNode.ChildNodes.Add(pluginMainMenu);

            //var menuItem = new SiteMapNode()
            //{
            //    SystemName = "YourCustomSystemNameOmid",
            //    Title = "TestOmid",
            //    ControllerName = "FaradataFirstTest",
            //    ActionName = "Index",
            //    Visible = true,
            //    RouteValues = new RouteValueDictionary() { { "Admin", null } },
            //};
            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            //if (pluginNode != null)
            //    pluginNode.ChildNodes.Add(menuItem);
            //else
            //    rootNode.ChildNodes.Add(menuItem);
        }

    }
}
