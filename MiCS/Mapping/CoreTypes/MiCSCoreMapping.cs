﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    // Todo: Add return type specification to mapping.

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
                        Arguments = new List<Type>()
                    }
                }
            });

            // Todo: Add Array type mapping.



            return mapping;
        }

    }

    
}
