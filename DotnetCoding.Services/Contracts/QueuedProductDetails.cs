using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Contracts
{
    public class QueuedProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public string Reason { get; set; }

        public DateTime RequestedDate { get; set; }
    }
}
