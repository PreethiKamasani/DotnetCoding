using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Contracts
{
    public class SearchParams
    {
        public string? Name { get; set; }

        public DateTime? RequestStartDate { get; set; }

        public DateTime? RequestEndDate { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }


    }
   
}
