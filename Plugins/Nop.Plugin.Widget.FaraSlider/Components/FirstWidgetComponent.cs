using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widget.FaraSlider.Components
{
    [ViewComponent(Name = "FaraSlider")]
    public class FaraSliderComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Widget.FaraSlider/Views/configure/PublicInfo.cshtml");
        }
    }
}
