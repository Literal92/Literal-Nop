using Nop.Web.Framework.Models;

namespace Nop.Plugin.Company.Media.ViewModel
{   
    public class CategoryViewModel : BaseNopEntityModel
    {
        public string Navigation { get; set; }
        public int ParentId { get; set; }
    }
}
