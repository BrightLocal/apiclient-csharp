using System;

namespace Brightlocal.Exceptions
{
    class StopBatchException : GeneralException
    {
        public StopBatchException(string message, Exception inner) : base(message, inner) { }
    }
}
