using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Shipping.Pickup;
using Nop.Services.Shipping.Tracking;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.FirstWidget
{
    public class PluginSetting : BasePlugin , IWidgetPlugin/*, IAdminMenuPlugin*/
    {
        private readonly ISettingService _settingService;
        //private readonly IAdminMenuPlugin _adminMenu;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;

        public PluginSetting(ISettingService settingService, IWebHelper webHelper, ILocalizationService localizationService,
                                    IWorkContext workContext)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
            _workContext = workContext;
        }

        //public PluginDescriptor PluginDescriptor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/FirstWidget/Configure";
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.ProductReviewsPageTop,PublicWidgetZones.ProductDetailsTop,PublicWidgetZones.CategoryDetailsTop};
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            GetDisplayWidgetRoute(PublicWidgetZones.HomePageBeforeNews, out actionName, out controllerName, out routeValues);
            //actionName = "Configure";
            //controllerName = "FirstWidget";
            //routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Faradata.FirstWidget.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "FirstWidget";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Faradata.FirstWidget.Controllers"},
                {"area", null},
                {"widgetZone",  widgetZone }
            };
        }



        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "FirstWidget";
        }
    }
}
