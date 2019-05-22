using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Company.Media.Model
{
    public class MediaModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("عنوان")]
        public string Title { get; set; }

        [NopResourceDisplayName("توضیحات")]
        public string Text { get; set; }


        [NopResourceDisplayName("تاریخ ثبت")]
        public string CreateDates { get; set; }

        public string URL { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        [NopResourceDisplayName("نام دسته")]
        public int CategoryId { get; set; }

        [NopResourceDisplayName("فرمت فایل")]
        public virtual string FileExtention { get; set; }

        [NopResourceDisplayName("نام دسته")]
        public virtual string CategoryName { get; set; }

        [NopResourceDisplayName("کاربر ثبت کننده")]
        public virtual string CustomerName { get; set; }
        

    }
}
