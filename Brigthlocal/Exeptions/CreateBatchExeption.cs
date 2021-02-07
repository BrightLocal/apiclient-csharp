using System;

namespace Brigthlocal.Exceptions
{
    class CreateBatchExeption : GeneralException
    {
        public CreateBatchExeption(string message, Exception inner) : base(message, inner) { }
    }
}
