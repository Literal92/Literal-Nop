using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
using Nop.Core.Plugins;
using Nop.Plugin.Faradata.AlarmShopping.Data;
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

namespace Nop.Plugin.Faradata.AlarmShopping
{
    public class FardataAlarmShoppingSetting : BasePlugin, IAdminMenuPlugin
    {
        private readonly ISettingService _settingService;
        //private readonly IAdminMenuPlugin _adminMenu;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly FaraObjectContext _objectContext;

        public FardataAlarmShoppingSetting( ISettingService settingService, IWebHelper webHelper,
                                            ILocalizationService localizationService,
                                            IWorkContext workContext, FaraObjectContext objectContext)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
            _workContext = workContext;
            _objectContext = objectContext;
        }

        public string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/FaraBot/Config";
        }

        public override void Install()
        {
            //ساخت جدول مورد نظر
            _objectContext.Install();

            base.Install();
        }

        public override void Uninstall()
        {
            //حذف جدول مورد نظر
            _objectContext.Uninstall();

            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            const string adminUrlPart = "Admin/";

            string pluginMenuName = _localizationService.GetResource("Plugins.FaradataAlarmShopping.Admin.Menu.Title", languageId: _workContext.WorkingLanguage.Id, defaultValue: "اطلاع رسانی فروش");
            var pluginMainMenu = new SiteMapNode
            {
                Title = pluginMenuName,
                Visible = true,
                SystemName = "FaradataTestInfo-Main-Menu",
                IconClass = "fa-genderless"
            };

            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = "تنظیمات",
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "FaraBot/Config",
                Visible = true,
                SystemName = "FaradataTestInfo-Configuration-Menu",
                IconClass = "fa-genderless"
            });

            pluginMainMenu.ChildNodes.Add(new SiteMapNode
            {
                Title = "کاربران تلگرام",
                Url = _webHelper.GetStoreLocation() + adminUrlPart + "FaraBot/Users",
                Visible = true,
                SystemName = "FaradataTestInfo-Get-Menu",
                IconClass = "fa-genderless"
            });
            
            rootNode.ChildNodes.Add(pluginMainMenu);
        }

    }
}
