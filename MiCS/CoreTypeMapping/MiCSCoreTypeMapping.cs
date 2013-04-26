using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class MiCSCoreTypeMapping
    {
        public string NamespaceName { get; set; }
        public string NamespaceNameScript { get; set; }

        public string Name { get; set; }
        public string NameScript { get; set; }

        public List<MiCSCoreMemberMapping> Members { get; set; }
    }
}
