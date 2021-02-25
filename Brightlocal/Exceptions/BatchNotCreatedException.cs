using System;

namespace Brightlocal.Exceptions
{
    class BatchNotCreatedException : GeneralException
    {
        public BatchNotCreatedException(string message, Exception inner) : base(message, inner) { }
    }
}
