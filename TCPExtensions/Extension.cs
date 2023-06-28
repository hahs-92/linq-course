using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPExtensions
{
    public static class Extension
    {
        public static List<T> Filter<T>(this List<T> Records, Func<T, bool> func)
        {
            List<T> filteredList = new List<T>();

            foreach (T record in Records)
            {
                if(func(record)) filteredList.Add(record);
            }

            return filteredList;
        }
    }
}
