using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faktformer_0._0._1
{
    public class BoolNullToBool
    {
        public static bool Convert(bool? value)
        {
            if (value == null) throw new ArgumentNullException("value:null could not be cenverted to bool");
            else return (bool)value;
        }
    }
}
