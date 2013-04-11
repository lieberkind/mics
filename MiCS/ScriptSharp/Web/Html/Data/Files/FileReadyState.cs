// FileReadyState.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;
using SC = System.ComponentModel;

namespace System.Html.Data.Files {

    /// <summary>
    /// This attribute indicates that the namespace of type within a system assembly
    /// should be ignored at script generation time. It is useful for creating namespaces
    /// for the purpose of c# code that don't exist at runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptIgnoreNamespaceAttribute : Attribute
    {
    }

    /// <summary>
    /// This attribute can be placed on types in system script assemblies that should not
    /// be imported. It is only meant to be used within mscorlib.dll.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    [ScriptImport]
    [SC.EditorBrowsable(SC.EditorBrowsableState.Never)]
    public sealed class ScriptIgnoreAttribute : Attribute
    {
    }

    /// <summary>
    /// This attribute can be placed on types that should not be emitted into generated
    /// script, as they represent existing script or native types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Struct)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class ScriptImportAttribute : Attribute
    {
    }

    /// <summary>
    /// This attribute is used to mark an enum as a set of constant values, i.e. if
    /// specified, the enum does not exist/is not generated, but rather its values
    /// are inlined as constants. If the UseName property is set to true, then instead
    /// of actual values, the names of the fields are used as string constants.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptConstantsAttribute : Attribute
    {

        private bool _useNames;

        public bool UseNames
        {
            get
            {
                return _useNames;
            }
            set
            {
                _useNames = value;
            }
        }
    }





    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum FileReadyState {

        Empty = 0,

        Loading = 1,

        Done = 2
    }
}
