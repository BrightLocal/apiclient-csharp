using System;
using System.Collections.Generic;
using System.Text;

namespace Brightlocal
{
    public class ApiResponse
    {
        protected int statusCode = 0;
        protected Dictionary<string, object> result;

        public ApiResponse(int statusCode, Dictionary<string,object> result)
        {
            this.statusCode = statusCode;
            this.result = result;
        }
    }
}
