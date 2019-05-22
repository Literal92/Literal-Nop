using Nop.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Test.omid
{
    public class ClassLib : BasePlugin
    {
        public string MyMethod()
        {
            return "Hello, Tizen Library!!!";
        }
    }
}
