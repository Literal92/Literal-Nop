using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widget.FaraSlider.Controllers
{
    
    public class FaraSliderController:BasePluginController
    {
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public ActionResult Configure()
        {
            return View("~/Plugins/Widget.FaraSlider/Views/Configure/Configure.cshtml");
        }
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            return View("~/Plugins/Widget.FaraSlider/Views/Configure/PublicInfo.cshtml");
        }
    }
}
