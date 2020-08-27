using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.Core.ExtensionMethods
{
    public static class GenericExtensions
    {
        public static bool IsInList<T>(this T obj, params T[] list) where T : IComparable
        {
            return list.Contains(obj);
        }
    }
}
