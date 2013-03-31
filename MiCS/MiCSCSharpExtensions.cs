using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    public static class MiCSCSharpExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        ///<remarks>http://stackoverflow.com/questions/1474863/addrange-to-a-collection</remarks>
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                destination.Add(item);
            }
        }
    }
}
