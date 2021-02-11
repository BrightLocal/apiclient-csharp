using System;
using System.Collections.Generic;
using System.Reflection;

namespace Brightlocal
{
    public class Parameters : Dictionary<string, object>
    {
        public Parameters() { }

        public Parameters(object obj)
        {
            foreach (PropertyInfo p in obj.GetType().GetProperties())
            {
                Add(p.Name, p.GetValue(obj));
            }
        }
    }
}
