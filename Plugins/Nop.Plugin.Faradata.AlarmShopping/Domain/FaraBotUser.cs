using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.AlarmShopping.Domain
{
   public class FaraBotUser: BaseEntity
    {
        [NopResourceDisplayName("شناسه مشتری")]
        public virtual int? CustomerId { get; set; }

        [NopResourceDisplayName("شناسه تلگرام کاربر")]
        public virtual long ChatId { get; set; }

        [NopResourceDisplayName("نام کاربری")]
        public virtual string username { get; set; }

        [NopResourceDisplayName("فعال/غیرفعال")]
        public virtual bool? IsActive { get; set; }

        [NopResourceDisplayName("مرحله")]
        public virtual byte Step { get; set; }

        [NopResourceDisplayName("زمان ثبت")]
        public virtual DateTime? CreateDate { get; set; }
    }
}
