// Runtime.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using S = System;
using System.ComponentModel;
using SC = System.ComponentModel;
using System.Runtime.CompilerServices;

/*
 * Prefixed ScriptSharp to all of the below
 * namespace to minimize the amount of naming
 * conflicts as much as possible.
 */

namespace ScriptSharp.System {

    [S.AttributeUsage(S.AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class FlagsAttribute : S.Attribute {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public abstract class MarshalByRefObject {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public abstract class ValueType {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public struct IntPtr {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public struct UIntPtr {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public abstract class MulticastDelegate : Delegate {

        protected MulticastDelegate(object target, string method)
            : base(target, method) {
        }

        protected MulticastDelegate(Type target, string method)
            : base(target, method) {
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public struct RuntimeTypeHandle {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public struct RuntimeFieldHandle {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public abstract class Attribute {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public sealed class ParamArrayAttribute : Attribute {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public enum AttributeTargets
    {
        Assembly = 0x0001,
        Module = 0x0002,
        Class = 0x0004,
        Struct = 0x0008,
        Enum = 0x0010,
        Constructor = 0x0020,
        Method = 0x0040,
        Property = 0x0080,
        Field = 0x0100,
        Event = 0x0200,
        Interface = 0x0400,
        Parameter = 0x0800,
        Delegate = 0x1000,
        ReturnValue = 0x2000,
        GenericParameter = 0x4000,
        Type = Class | Struct | Enum | Interface | Delegate,
        All = Assembly | Module | Class | Struct | Enum | Constructor |
              Method | Property | Field | Event | Interface | Parameter |
              Delegate | ReturnValue | GenericParameter,
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [S.AttributeUsage(S.AttributeTargets.Class, Inherited = true)]
    [ScriptIgnore]
    public sealed class AttributeUsageAttribute : S.Attribute {

        private AttributeTargets _attributeTarget = AttributeTargets.All;
        private bool _allowMultiple;
        private bool _inherited;

        public AttributeUsageAttribute(AttributeTargets validOn) {
            _attributeTarget = validOn;
            _inherited = true;
        }

        public AttributeTargets ValidOn {
            get {
                return _attributeTarget;
            }
        }

        public bool AllowMultiple {
            get {
                return _allowMultiple;
            }
            set {
                _allowMultiple = value;
            }
        }

        public bool Inherited {
            get {
                return _inherited;
            }
            set {
                _inherited = value;
            }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [S.AttributeUsage(S.AttributeTargets.Delegate | S.AttributeTargets.Interface | S.AttributeTargets.Event | S.AttributeTargets.Field | S.AttributeTargets.Property | S.AttributeTargets.Method | S.AttributeTargets.Constructor | S.AttributeTargets.Enum | S.AttributeTargets.Struct | S.AttributeTargets.Class, Inherited = false)]
    [ScriptIgnore]
    public sealed class ObsoleteAttribute : S.Attribute {

        private bool _error;
        private string _message;

        public ObsoleteAttribute() {
        }

        public ObsoleteAttribute(string message) {
            _message = message;
        }

        public ObsoleteAttribute(string message, bool error) {
            _message = message;
            _error = error;
        }

        public bool IsError {
            get {
                return _error;
            }
        }

        public string Message {
            get {
                return _message;
            }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [S.AttributeUsage(S.AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class CLSCompliantAttribute : S.Attribute {

        private bool _isCompliant;

        public CLSCompliantAttribute(bool isCompliant) {
            _isCompliant = isCompliant;
        }

        public bool IsCompliant {
            get {
                return _isCompliant;
            }
        }
    }
}

namespace ScripySharp.System.CodeDom.Compiler {

    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class GeneratedCodeAttribute : Attribute {

        private string _tool;
        private string _version;

        public GeneratedCodeAttribute(string tool, string version) {
            _tool = tool;
            _version = version;
        }

        public string Tool {
            get {
                return _tool;
            }
        }

        public string Version {
            get {
                return _version;
            }
        }
    }
}

namespace ScriptSharp.System.ComponentModel {

    /// <summary>
    /// This attribute marks a field, property, event or method as
    /// "browsable", i.e. present in the type descriptor associated with
    /// the type.
    /// </summary>
    [SC.EditorBrowsable(SC.EditorBrowsableState.Never)]
    [S.AttributeUsage(S.AttributeTargets.Field | S.AttributeTargets.Property | S.AttributeTargets.Event | S.AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class BrowsableAttribute : S.Attribute {
    }

    [SC.EditorBrowsable(SC.EditorBrowsableState.Never)]
    [S.AttributeUsage(S.AttributeTargets.Class | S.AttributeTargets.Struct | S.AttributeTargets.Enum | S.AttributeTargets.Constructor | S.AttributeTargets.Method | S.AttributeTargets.Property | S.AttributeTargets.Field | S.AttributeTargets.Event | S.AttributeTargets.Delegate | S.AttributeTargets.Interface)]
    [ScriptIgnore]
    public sealed class EditorBrowsableAttribute : S.Attribute {

        private EditorBrowsableState _browsableState;

        public EditorBrowsableAttribute(EditorBrowsableState state) {
            _browsableState = state;
        }

        public EditorBrowsableState State {
            get {
                return _browsableState;
            }
        }
    }

    [SC.EditorBrowsable(SC.EditorBrowsableState.Never)]
    [ScriptIgnore]
    public enum EditorBrowsableState {
        Always = 0,
        Never = 1,
        Advanced = 2
    }
}

namespace ScriptSharp.System.Reflection {

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public sealed class DefaultMemberAttribute {

        private string _memberName;

        public DefaultMemberAttribute(string memberName) {
            _memberName = memberName;
        }

        public string MemberName {
            get {
                return _memberName;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyCopyrightAttribute : Attribute {

        private string _copyright;

        public AssemblyCopyrightAttribute(string copyright) {
            _copyright = copyright;
        }

        public string Copyright {
            get {
                return _copyright;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyTrademarkAttribute : Attribute {

        private string _trademark;

        public AssemblyTrademarkAttribute(string trademark) {
            _trademark = trademark;
        }

        public string Trademark {
            get {
                return _trademark;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyProductAttribute : Attribute {

        private string _product;

        public AssemblyProductAttribute(string product) {
            _product = product;
        }

        public string Product {
            get {
                return _product;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyCompanyAttribute : Attribute {

        private string _company;

        public AssemblyCompanyAttribute(string company) {
            _company = company;
        }

        public string Company {
            get {
                return _company;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyDescriptionAttribute : Attribute {

        private string _description;

        public AssemblyDescriptionAttribute(string description) {
            _description = description;
        }

        public string Description {
            get {
                return _description;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyTitleAttribute : Attribute {

        private string _title;

        public AssemblyTitleAttribute(string title) {
            _title = title;
        }

        public string Title {
            get {
                return _title;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyConfigurationAttribute : Attribute {

        private string _configuration;

        public AssemblyConfigurationAttribute(string configuration) {
            _configuration = configuration;
        }

        public string Configuration {
            get {
                return _configuration;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyFileVersionAttribute : Attribute {

        private string _version;

        public AssemblyFileVersionAttribute(string version) {
            _version = version;
        }

        public string Version {
            get {
                return _version;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyInformationalVersionAttribute : Attribute {

        private string _informationalVersion;

        public AssemblyInformationalVersionAttribute(string informationalVersion) {
            _informationalVersion = informationalVersion;
        }

        public string InformationalVersion {
            get {
                return _informationalVersion;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyCultureAttribute : Attribute {

        private string _culture;

        public AssemblyCultureAttribute(string culture) {
            _culture = culture;
        }

        public string Culture {
            get {
                return _culture;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyVersionAttribute : Attribute {

        private string _version;

        public AssemblyVersionAttribute(string version) {
            _version = version;
        }

        public string Version {
            get {
                return _version;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyKeyFileAttribute : Attribute {

        private string _keyFile;

        public AssemblyKeyFileAttribute(string keyFile) {
            _keyFile = keyFile;
        }

        public string KeyFile {
            get {
                return _keyFile;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    [ScriptIgnore]
    public sealed class AssemblyDelaySignAttribute : Attribute {

        private bool _delaySign;

        public AssemblyDelaySignAttribute(bool delaySign) {
            _delaySign = delaySign;
        }

        public bool DelaySign {
            get {
                return _delaySign;
            }
        }
    }
}

namespace ScriptSharp.System.Runtime.CompilerServices {

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public class RuntimeHelpers {

        public static void InitializeArray(Array array, RuntimeFieldHandle handle) {
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public sealed class CompilerGeneratedAttribute : Attribute {

        public CompilerGeneratedAttribute() {
        }
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field, Inherited = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public sealed class DecimalConstantAttribute : Attribute {

        public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low) {
        }

        [CLSCompliant(false)]
        public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low) {
        }

        public decimal Value {
            get {
                return 0m;
            }
        }
    }
}

namespace ScriptSharp.System.Runtime.InteropServices {

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ScriptIgnore]
    public class OutAttribute {
    }
}

namespace ScriptSharp.System.Runtime.Versioning {

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class TargetFrameworkAttribute : Attribute {

        private string _frameworkName;
        private string _frameworkDisplayName;

        public TargetFrameworkAttribute(string frameworkName) {
            _frameworkName = frameworkName;
        }

        public string FrameworkDisplayName {
            get {
                return _frameworkDisplayName;
            }
            set {
                _frameworkDisplayName = value;
            }
        }

        public string FrameworkName {
            get {
                return _frameworkName;
            }
        }
    }
}
