using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.SecondWidget.Components
{
    [ViewComponent(Name = "SecondWidget")]
    public class SecondWidgetComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Faradata.SecondWidget/Views/configure/PublicInfo.cshtml");
        }

    }
}
