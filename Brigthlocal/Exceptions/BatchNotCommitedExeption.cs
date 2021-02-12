using System;

namespace Brigthlocal.Exceptions
{
    class BatchNotCommitedException : GeneralException
    {
        public BatchNotCommitedException(string message, Exception inner) : base(message, inner) { }
    }
}
