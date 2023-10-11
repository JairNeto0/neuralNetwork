using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace awfRNA.Utilities
{
    internal static class DoubleTool
    {
        public static double SortValue(int length, int min, double floating)
        {
            return (Random.Shared.Next(length + 1) + min) * floating;
        }

        public static double ShortValue(double value)
        {
            throw new NotImplementedException();
        }
    }
}
