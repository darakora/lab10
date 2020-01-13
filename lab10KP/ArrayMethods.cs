using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab10KP
{
    class ArrayMethods
    {
        public static int SumOfElInArray(int[] arr)
        {
            int res = 0;
            var lArray = arr.ToList();
            lArray.ForEach((item) =>
            {
                if (item % 5 == 0)
                {
                    res += item;
                }
            });
            return res;
        }

        public static int CountElInArray(int[] arr)
        {
            return arr.Length;
        }
    }
}
