using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.AlarmShopping.Domain
{
   public class FaraBotConfig: BaseEntity
    {
        [NopResourceDisplayName("توکن بات")]
        public virtual string TokenApi { get; set; }

        [NopResourceDisplayName("فعال/غیرفعال")]
        public virtual bool? IsActive { get; set; }
    }
}
