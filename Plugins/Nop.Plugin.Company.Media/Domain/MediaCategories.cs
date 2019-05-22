using Nop.Core;

namespace Nop.Plugin.Company.Media.Domain
{    
    public class MediaCategories : BaseEntity
    {
        public virtual int MediaCategoryId { get; set; }
        public virtual MediaCategory MediaCategory { get; set; }
        public virtual int MediaId { get; set; }
        public virtual Media Media { get; set; }
    }
}
 