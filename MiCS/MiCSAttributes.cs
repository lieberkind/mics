using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class MixedSide : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class ClientSide : System.Attribute
    {
    }
}
