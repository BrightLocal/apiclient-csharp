﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brigthlocal.Exceptions
{
    public class GeneralException: ApplicationException
    {
        public GeneralException(string message, Exception inner) : base(message, inner) { }
    }
}
