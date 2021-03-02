using System;

namespace Brightlocal.Exceptions
{
    class CreateBatchException : GeneralException
    {
        public CreateBatchException(string message, Exception inner) : base(message, inner) { }
    }
}
