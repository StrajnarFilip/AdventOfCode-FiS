using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fprog.Algorithms.Common
{
    public static class IEnumerableFurtherExtensions
    {
        public static string FormattedString<T>(this IEnumerable<T> values)
        {
            return $"[ {String.Join(", ", values)} ]";
        }
    }
}
