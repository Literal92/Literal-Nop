using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Company.Media.Domain
{    
    public class MediaCategory : BaseEntity
    {
        public enum StatusType : byte
        {

            [NopResourceDisplayName("فعال")]
            Active = 1,
            [NopResourceDisplayName("غیرفعال")]
            DeActive = 2,
            [NopResourceDisplayName("حذف شده")]
            Deleted = 3,
        }
        [NopResourceDisplayName("وضعیت")]
        public virtual StatusType MediaCategoryStatus { get; set; }
        [NopResourceDisplayName("نام")]
        public virtual string MediaCategoryName { get; set; }
        [NopResourceDisplayName("نام لاتین")]
        public virtual string MediaCategoryLatinName { get; set; }
        [NopResourceDisplayName("کد")]
        public virtual int MediaCategoryCode { get; set; }
        [NopResourceDisplayName("سطح دسته بندی")]
        public virtual int MediaCategoryLevel { get; set; }
        [NopResourceDisplayName("دسته بالاتر")]
        public virtual int MediaCategoryParentId { get; set; }
        public virtual int MediaCategoryMediaId { get; set; }
        public virtual ICollection<MediaCategories> MediaCategories { get; set; }
        
    }
}
