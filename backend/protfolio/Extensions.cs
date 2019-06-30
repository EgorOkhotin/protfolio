using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace protfolio
{
    public static class Extensions
    {
        public static string NormalizeString(this string val)
        {
            return val.ToUpper();
        }
    }
}
