using DotnetCoding.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Contracts
{
    public class ApprovalRequest
    {
        public int ProductId { get; set; }

        public ApprovalStatus Status { get; set; }

        public int ApprovedBy { get; set; }
    }
}
