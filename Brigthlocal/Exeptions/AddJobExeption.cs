using System;

namespace Brigthlocal.Exceptions
{
    class AddJobException : GeneralException
    {
        public AddJobException(string message, Exception inner) : base(message, inner) { }
    }
}
