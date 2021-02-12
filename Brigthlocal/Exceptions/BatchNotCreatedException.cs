using System;

namespace Brigthlocal.Exceptions
{
    class BatchNotCreatedException : GeneralException
    {
        public BatchNotCreatedException(string message, Exception inner) : base(message, inner) { }
    }
}
