using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{

    /// <summary>
    /// MiCS core type mapping specification. Details which C#
    /// core type mappings that are supported and how these
    /// mappings should be performed to match the ScriptSharp
    /// core types.
    /// </summary>
    class MiCSCoreMapping : List<MiCSCoreTypeMapping>
    {
        public static MiCSCoreMapping Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = Instantiate();

                return _Instance;
            }
        }
        private static MiCSCoreMapping _Instance;
        private MiCSCoreMapping() { }

        private static MiCSCoreMapping Instantiate()
        {
            var mapping = new MiCSCoreMapping();

            mapping.Add(new MiCSCoreTypeMapping()
            {
                Name = "Regex",
                NameScript = "RegExp",
                NamespaceName = "System.Text.RegularExpressions",
                NamespaceNameScript = "System",
                Members = new List<MiCSCoreMemberMapping>()
                {
                    new MiCSCoreMemberMapping()
                    {
                        Name = "IsMatch",
                        NameScript = "test",
                        ReturnType = typeof(bool),
                        Arguments = new List<Type>()
                        {
                            typeof(String)
                        }
                    }
                }
            });

            mapping.Add(new MiCSCoreTypeMapping()
            {
                Name = "String",
                NameScript = "String",
                NamespaceName = "System",
                NamespaceNameScript = "System",
                Members = new List<MiCSCoreMemberMapping>()
                {
                    new MiCSCoreMemberMapping()
                    {
                        Name = "Length",
                        NameScript = "length",
                        ReturnType = typeof(int),
                        Arguments = new List<Type>()
                    },
                    new MiCSCoreMemberMapping()
                    {
                        Name = "IndexOf",
                        NameScript = "indexOf",
                        ReturnType = typeof(int),
                        Arguments = new List<Type>()
                        {
                            typeof(char)
                        }
                    }
                }
            });

            // Todo: Add Array type mapping.
            mapping.Add(new MiCSCoreTypeMapping()
            {
                Name = "Array",
                NameScript = "Array",
                NamespaceName = "System",
                NamespaceNameScript = "System",
                Members = new List<MiCSCoreMemberMapping>()
            });


            return mapping;
        }

    }

    
}
