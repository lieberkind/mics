using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    /// <summary>
    /// Type mapping specification from C# core type to ScriptSharp core type.
    /// Name property values are mapped to NameScript property values.
    /// </summary>
    class MiCSCoreTypeMapping
    {
        public string NamespaceName { get; set; }
        public string NamespaceNameScript { get; set; }

        public string Name { get; set; }
        public string NameScript { get; set; }

        public List<MiCSCoreMemberMapping> Members { get; set; }
    }
}
