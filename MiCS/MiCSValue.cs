using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    /// <summary>
    /// Class used for staging/translating/converting server
    /// side values to client side. Server side variables 
    /// (and literals) are translated into client side literals.
    /// </summary>
    public class MiCSValue<T>
    {
        internal MiCSValue(T @value)
        {
            serverSideValue = @value;
        }
        private T serverSideValue;

        public static implicit operator T(MiCSValue<T> @value)
        {
            return @value.serverSideValue;
        }
    }

    public class MiCSValue
    {
        #region Region Factory Methods
        public static MiCSValue<string> String(string str)
        {
            return new MiCSValue<string>(str);
        }
        public static MiCSValue<int> Int(int i)
        {
            return new MiCSValue<int>(i);
        }
        public static MiCSValue<bool> Bool(bool b)
        {
            return new MiCSValue<bool>(b);
        }
        #endregion
    }
}
