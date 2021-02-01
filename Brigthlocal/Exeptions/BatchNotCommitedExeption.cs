using System;

namespace Brigthlocal.Exeptions
{
    class BatchNotCommitedExeption : ApplicationException
    {
        public BatchNotCommitedExeption(string message, Exception inner) : base(message, inner) { }
    }
}
