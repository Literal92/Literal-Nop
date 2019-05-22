using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.Test.Domain
{
   public class CustomerToken: BaseEntity
    {
        [NopResourceDisplayName("شناسه مشتری")]
        public virtual int CustomerId { get; set; }

        [NopResourceDisplayName("شناسه دستگاه")]
        public virtual string DeviceId { get; set; }

        [NopResourceDisplayName("توکن")]
        public virtual string Token { get; set; }

        [NopResourceDisplayName("زمان ثبت")]
        public virtual DateTime CreateDate { get; set; }

        [NopResourceDisplayName("تاریخ اعتبار")]
        public virtual DateTime ExpireDate { get; set; }
    }
}
