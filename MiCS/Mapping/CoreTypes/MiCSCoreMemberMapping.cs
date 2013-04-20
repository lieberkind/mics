using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class MiCSCoreMemberMapping
    {
        public string Name { get; set; }
        public string NameScript { get; set; }

        public List<Type> Arguments { get; set; }

        public Type ReturnType { get; set; }

        public MiCSCoreMemberMapping()
        {

        }
    }
}
