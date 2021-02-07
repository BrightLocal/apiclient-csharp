using System;

namespace Brigthlocal.Exceptions
{
    class StopBatchExeption : GeneralException
    {
        public StopBatchExeption(string message, Exception inner) : base(message, inner) { }
    }
}
