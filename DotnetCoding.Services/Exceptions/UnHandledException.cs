using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Exceptions
{
    public class UnHandledException : Exception
    {
        public UnHandledException(string message) : base(message)
        {
        }
    }
}
