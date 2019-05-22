using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Company.Media.Domain
{
    public class Media : BaseEntity
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

        [NopResourceDisplayName("عنوان")]
        public virtual string Title { get; set; }

        [NopResourceDisplayName("خلاصه متن")]
        public virtual string AltText { get; set; }

        [NopResourceDisplayName("آدرس")]
        public virtual string URL { get; set; }

        [NopResourceDisplayName("آدرس نسخه")]
        public virtual string ImageThumb { get; set; }

        [NopResourceDisplayName("نام فایل")]
        public virtual string FileName { get; set; }

        [NopResourceDisplayName("فرمت فایل")]
        public virtual string FileExtention { get; set; }

        [NopResourceDisplayName("تاریخ ثبت")]
        public virtual DateTime CreateDate { get; set; }

        [NopResourceDisplayName("تاریخ انتشار")]
        public virtual DateTime PublishDate { get; set; }

        [NopResourceDisplayName("تاریخ آخرین ویرایش")]
        public virtual DateTime UpdateDate { get; set; }

        [NopResourceDisplayName("نوع فایل")]
        public virtual string ContentType { get; set; }

        [NopResourceDisplayName("وضعیت")]
        public virtual StatusType MediaStatus { get; set; }

        [NopResourceDisplayName("کاربر")]
        public virtual int UserId { get; set; }

        [NopResourceDisplayName("تعداد بازدید")]
        public virtual int Viewer { get; set; }

        [NopResourceDisplayName("تعداد دانلود")]
        public virtual int DownloadNumber { get; set; }

        public virtual ICollection<MediaCategories> MediaCategories { get; set; }
    }
}
