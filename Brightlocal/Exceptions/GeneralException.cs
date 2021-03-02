using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightlocal.Exceptions
{
    public class GeneralException: Exception
    {
        public GeneralException(string message, Exception inner) : base(message, inner) { }
    }
}
