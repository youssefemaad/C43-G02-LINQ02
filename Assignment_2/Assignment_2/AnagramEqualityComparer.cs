using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    public class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Concat(x.OrderBy(c => c)) == string.Concat(y.OrderBy(c => c));
        }

        public int GetHashCode(string obj)
        {
            return string.Concat(obj.OrderBy(c => c)).GetHashCode();
        }
    }
}