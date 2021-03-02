using System;

namespace Brightlocal.Exceptions
{
    class BatchNotCommitedException : GeneralException
    {
        public BatchNotCommitedException(string message, Exception inner) : base(message, inner) { }
    }
}
