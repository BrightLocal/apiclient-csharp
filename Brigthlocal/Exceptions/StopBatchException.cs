using System;

namespace Brigthlocal.Exceptions
{
    class StopBatchException : GeneralException
    {
        public StopBatchException(string message, Exception inner) : base(message, inner) { }
    }
}
