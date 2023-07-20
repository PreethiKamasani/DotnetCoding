using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Models
{
    public partial class UserDetail : BaseEntity
    {
        public UserDetail()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public int? Type { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
