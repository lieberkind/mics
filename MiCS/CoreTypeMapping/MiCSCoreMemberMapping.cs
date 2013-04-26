using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    /// <summary>
    /// Member mapping specification from C# core type member to ScriptSharp core type member.
    /// Name property value is mapped to the NameScript property value. Argument types and 
    /// return type should be the types from the ScriptSharp core types (which then is required
    /// to be matched on the equivalent C# core type member by the TypeManager for the core 
    /// type usage to be valid).
    /// </summary>
    class MiCSCoreMemberMapping
    {
        public string Name { get; set; }
        public string NameScript { get; set; }

        public List<Type> Arguments { get; set; }

        public Type ReturnType { get; set; }
    }
}
