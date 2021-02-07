using System;

namespace Brigthlocal.Exceptions
{
    class GetResultException : GeneralException
    {
        public GetResultException(string message, Exception inner) : base(message, inner) { }
    }
}
