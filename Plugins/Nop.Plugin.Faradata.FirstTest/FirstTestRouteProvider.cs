using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest
{
    public class FirstTestRouteProvider : IRouteProvider
    {
        public int Priority => -1;

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Plugin.Payments.Faradata.category", "Plugins/FaradataFirstTest/Get",
                new { controller = "FaradataFirstTest", action = "Get" });
        }
    }
}
