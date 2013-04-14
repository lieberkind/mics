using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.ScriptSharp.CoreLib
{
    class CoreLib
    {
        public static string Text = @"

// Action.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action();

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T>(T arg);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
}
// Arguments.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Provides access to the arguments of the current function.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""arguments"")]
    public static class Arguments {

        /// <summary>
        /// Retrieves the arguments list.
        /// </summary>
        /// <returns>The arguments list.</returns>
        [ScriptAlias(""arguments"")]
        [ScriptField]
        public static object[] Current {
            get {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the number of actual arguments passed to the function.
        /// </summary>
        /// <returns>The count of arguments.</returns>
        [ScriptField]
        public static int Length {
            get {
                return 0;
            }
        }

        /// <summary>
        /// Retrieves the specified actual argument value passed to the
        /// function by index.
        /// </summary>
        /// <param name=""index"">The index of the argument to retrieve.</param>
        /// <returns>The value of the specified argument.</returns>
        public static object GetArgument(int index) {
            return null;
        }

        /// <summary>
        /// Retrieves the specified actual argument value passed to the
        /// function by index.
        /// </summary>
        /// <param name=""index"">The index of the argument to retrieve.</param>
        /// <typeparam name=""T"">The type of the return value.</typeparam>
        /// <returns>The value of the specified argument.</returns>
        public static T GetArgument<T>(int index) {
            return default(T);
        }

        [ScriptAlias(""Array.toArray"")]
        public static Array ToArray() {
            return null;
        }
    }
}
// Array.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.Runtime.CompilerServices;

namespace System {

    // NOTE: Keep in sync with ArrayList and List

    /// <summary>
    /// Equivalent to the Array type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Array"")]
    public sealed class Array : IEnumerable {

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public object this[int index] {
            get {
                return null;
            }
            set {
            }
        }

        public Array Concat(params object[] objects) {
            return null;
        }

        public bool Contains(object item) {
            return false;
        }

        public bool Every(ArrayFilterCallback filterCallback) {
            return false;
        }

        public bool Every(ArrayItemFilterCallback itemFilterCallback) {
            return false;
        }

        public Array Filter(ArrayFilterCallback filterCallback) {
            return null;
        }

        public Array Filter(ArrayItemFilterCallback itemFilterCallback) {
            return null;
        }

        public void ForEach(ArrayCallback callback) {
        }

        public void ForEach(ArrayItemCallback itemCallback) {
        }

        public IEnumerator GetEnumerator() {
            return null;
        }

        public Array GetRange(int index) {
            return null;
        }

        public Array GetRange(int index, int count) {
            return null;
        }

        public int IndexOf(object item) {
            return 0;
        }

        public int IndexOf(object item, int startIndex) {
            return 0;
        }

        public string Join() {
            return null;
        }

        public string Join(string delimiter) {
            return null;
        }

        public int LastIndexOf(object item) {
            return 0;
        }

        public int LastIndexOf(object item, int fromIndex) {
            return 0;
        }

        public Array Map(ArrayMapCallback mapCallback) {
            return null;
        }

        public Array Map(ArrayItemMapCallback mapItemCallback) {
            return null;
        }

        [ScriptAlias(""ss.array"")]
        public static Array Parse(string s) {
            return null;
        }

        public object Reduce(ArrayReduceCallback callback) {
            return null;
        }

        public object Reduce(ArrayReduceCallback callback, object initialValue) {
            return null;
        }

        public object Reduce(ArrayItemReduceCallback callback) {
            return null;
        }

        public object Reduce(ArrayItemReduceCallback callback, object initialValue) {
            return null;
        }

        public object ReduceRight(ArrayReduceCallback callback) {
            return null;
        }

        public object ReduceRight(ArrayReduceCallback callback, object initialValue) {
            return null;
        }

        public object ReduceRight(ArrayItemReduceCallback callback) {
            return null;
        }

        public object ReduceRight(ArrayItemReduceCallback callback, object initialValue) {
            return null;
        }

        public void Reverse() {
        }

        public object Shift() {
            return null;
        }

        public Array Slice(int start) {
            return null;
        }

        public Array Slice(int start, int end) {
            return null;
        }

        public bool Some(ArrayFilterCallback filterCallback) {
            return false;
        }

        public bool Some(ArrayItemFilterCallback itemFilterCallback) {
            return false;
        }

        public void Sort() {
        }

        public void Sort(CompareCallback compareCallback) {
        }

        public void Splice(int start, int deleteCount) {
        }

        public void Splice(int start, int deleteCount, params object[] itemsToInsert) {
        }

        [ScriptAlias(""ss.array"")]
        public static Array ToArray(object o) {
            return null;
        }

        public void Unshift(params object[] items) {
        }
    }
}
// Boolean.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the Boolean type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public struct Boolean {

        /// <summary>
        /// Enables you to parse a string representation of a boolean value.
        /// </summary>
        /// <param name=""s"">The string to be parsed.</param>
        /// <returns>The resulting boolean value.</returns>
        [ScriptAlias(""ss.boolean"")]
        public static bool Parse(string s) {
            return false;
        }
    }
}
// Byte.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The byte data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Byte {

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(byte i) {
            return null;
        }
    }
}
// Action.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void Callback(object arg);
}
// CancelEventArgs.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The event argument associated with cancelable events.
    /// </summary>
    [ScriptImport]
    public class CancelEventArgs : EventArgs {

        /// <summary>
        /// Whether the event has been canceled.
        /// </summary>
        [ScriptField]
        public bool Cancel {
            get {
                return false;
            }
            set {
            }
        }
    }
}
// Char.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The char data type which is mapped to the String type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""String"")]
    public struct Char {

        /// <internalonly />
        public static explicit operator String(char ch) {
            return null;
        }
    }
}
// CompareCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate int CompareCallback(object x, object y);
}
// Date.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the Date type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Date {

        /// <summary>
        /// Creates a new instance of Date initialized from the current time.
        /// </summary>
        public Date() {
        }

        /// <summary>
        /// Creates a new instance of Date initialized from the specified number of milliseconds.
        /// </summary>
        /// <param name=""milliseconds"">Milliseconds since January 1st, 1970.</param>
        public Date(int milliseconds) {
        }

        /// <summary>
        /// Creates a new instance of Date initialized from parsing the specified date.
        /// </summary>
        /// <param name=""date""></param>
        public Date(string date) {
        }

        /// <summary>
        /// Creates a new instance of Date.
        /// </summary>
        /// <param name=""year"">The full year.</param>
        /// <param name=""month"">The month (0 through 11)</param>
        /// <param name=""date"">The day of the month (1 through # of days in the specified month)</param>
        public Date(int year, int month, int date) {
        }

        /// <summary>
        /// Creates a new instance of Date.
        /// </summary>
        /// <param name=""year"">The full year.</param>
        /// <param name=""month"">The month (0 through 11)</param>
        /// <param name=""date"">The day of the month (1 through # of days in the specified month)</param>
        /// <param name=""hours"">The hours (0 through 23)</param>
        public Date(int year, int month, int date, int hours) {
        }

        /// <summary>
        /// Creates a new instance of Date.
        /// </summary>
        /// <param name=""year"">The full year.</param>
        /// <param name=""month"">The month (0 through 11)</param>
        /// <param name=""date"">The day of the month (1 through # of days in the specified month)</param>
        /// <param name=""hours"">The hours (0 through 23)</param>
        /// <param name=""minutes"">The minutes (0 through 59)</param>
        public Date(int year, int month, int date, int hours, int minutes) {
        }

        /// <summary>
        /// Creates a new instance of Date.
        /// </summary>
        /// <param name=""year"">The full year.</param>
        /// <param name=""month"">The month (0 through 11)</param>
        /// <param name=""date"">The day of the month (1 through # of days in the specified month)</param>
        /// <param name=""hours"">The hours (0 through 23)</param>
        /// <param name=""minutes"">The minutes (0 through 59)</param>
        /// <param name=""seconds"">The seconds (0 through 59)</param>
        public Date(int year, int month, int date, int hours, int minutes, int seconds) {
        }

        /// <summary>
        /// Creates a new instance of Date.
        /// </summary>
        /// <param name=""year"">The full year.</param>
        /// <param name=""month"">The month (0 through 11)</param>
        /// <param name=""date"">The day of the month (1 through # of days in the specified month)</param>
        /// <param name=""hours"">The hours (0 through 23)</param>
        /// <param name=""minutes"">The minutes (0 through 59)</param>
        /// <param name=""seconds"">The seconds (0 through 59)</param>
        /// <param name=""milliseconds"">The milliseconds (0 through 999)</param>
        public Date(int year, int month, int date, int hours, int minutes, int seconds, int milliseconds) {
        }

        /// <summary>
        /// Returns the current date and time.
        /// </summary>
        [ScriptField]
        [ScriptAlias(""ss.now()"")]
        public static Date Now {
            get {
                return null;
            }
        }

        /// <summary>
        /// Returns the current date with the time part set to 00:00:00.
        /// </summary>
        [ScriptField]
        [ScriptAlias(""ss.today()"")]
        public static Date Today {
            get {
                return null;
            }
        }

        public int GetDate() {
            return 0;
        }

        public int GetDay() {
            return 0;
        }

        public int GetFullYear() {
            return 0;
        }

        public int GetHours() {
            return 0;
        }

        public int GetMilliseconds() {
            return 0;
        }

        public int GetMinutes() {
            return 0;
        }

        public int GetMonth() {
            return 0;
        }

        public int GetSeconds() {
            return 0;
        }

        public int GetTime() {
            return 0;
        }

        public int GetTimezoneOffset() {
            return 0;
        }

        public int GetUTCDate() {
            return 0;
        }

        public int GetUTCDay() {
            return 0;
        }

        public int GetUTCFullYear() {
            return 0;
        }

        public int GetUTCHours() {
            return 0;
        }

        public int GetUTCMilliseconds() {
            return 0;
        }

        public int GetUTCMinutes() {
            return 0;
        }

        public int GetUTCMonth() {
            return 0;
        }

        public int GetUTCSeconds() {
            return 0;
        }

        [ScriptName(""parseDate"")]
        public static Date Parse(string value) {
            return null;
        }

        public void SetDate(int date) {
        }

        public void SetFullYear(int year) {
        }

        public void SetFullYear(int year, int month) {
        }

        public void SetFullYear(int year, int month, int day) {
        }

        public void SetHours(int hours) {
        }

        public void SetMilliseconds(int milliseconds) {
        }

        public void SetMinutes(int minutes) {
        }

        public void SetMonth(int month) {
        }

        public void SetSeconds(int seconds) {
        }

        public void SetTime(int milliseconds) {
        }

        public void SetUTCDate(int date) {
        }

        public void SetUTCFullYear(int year) {
        }

        public void SetUTCHours(int hours) {
        }

        public void SetUTCMilliseconds(int milliseconds) {
        }

        public void SetUTCMinutes(int minutes) {
        }

        public void SetUTCMonth(int month) {
        }

        public void SetUTCSeconds(int seconds) {
        }

        public void SetYear(int year) {
        }

        public string ToDateString() {
            return null;
        }

        public string ToISOString() {
            return null;
        }

        public string ToLocaleDateString() {
            return null;
        }

        public string ToLocaleTimeString() {
            return null;
        }

        public string ToTimeString() {
            return null;
        }

        public string ToUTCString() {
            return null;
        }

        [ScriptName(PreserveCase = true)]
        public static int UTC(int year, int month, int day) {
            return 0;
        }

        [ScriptName(PreserveCase = true)]
        public static int UTC(int year, int month, int day, int hours) {
            return 0;
        }

        [ScriptName(PreserveCase = true)]
        public static int UTC(int year, int month, int day, int hours, int minutes) {
            return 0;
        }

        [ScriptName(PreserveCase = true)]
        public static int UTC(int year, int month, int day, int hours, int minutes, int seconds) {
            return 0;
        }

        [ScriptName(PreserveCase = true)]
        public static int UTC(int year, int month, int day, int hours, int minutes, int seconds, int milliseconds) {
            return 0;
        }

        // NOTE: There is no + operator since in JavaScript that returns the
        //       concatenation of the date strings, which is pretty much useless.

        /// <summary>
        /// Returns the difference in milliseconds between two dates.
        /// </summary>
        public static int operator -(Date a, Date b) {
            return 0;
        }

        /// <summary>
        /// Compares two dates
        /// </summary>
        public static bool operator ==(Date a, Date b) {
            return false;
        }

        /// <summary>
        /// Compares two dates
        /// </summary>
        public static bool operator !=(Date a, Date b) {
            return false;
        }

        /// <summary>
        /// Compares two dates
        /// </summary>
        public static bool operator <(Date a, Date b) {
            return false;
        }

        /// <summary>
        /// Compares two dates
        /// </summary>
        public static bool operator >(Date a, Date b) {
            return false;
        }

        /// <summary>
        /// Compares two dates
        /// </summary>
        public static bool operator <=(Date a, Date b) {
            return false;
        }

        /// <summary>
        /// Compares two dates
        /// </summary>
        public static bool operator >=(Date a, Date b) {
            return false;
        }
    }
}
// Decimal.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The decimal data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Decimal {

        public Decimal(double d) {
        }

        public Decimal(int i) {
        }

        public Decimal(float f) {
        }

        public Decimal(long n) {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Decimal(int lo, int mid, int hi, bool isNegative, byte scale) {
        }

        public string Format(string format) {
            return null;
        }

        public string LocaleFormat(string format) {
            return null;
        }

        [ScriptAlias(""parseFloat"")]
        public static decimal Parse(string s) {
            return 0m;
        }

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(decimal i) {
            return null;
        }

        /// <internalonly />
        public static implicit operator decimal(int value) {
            return 0;
        }

        /// <internalonly />
        public static implicit operator decimal(double value) {
            return 0;
        }

        /// <internalonly />
        public static implicit operator decimal(float value) {
            return 0;
        }

        /// <internalonly />
        public static implicit operator decimal(long value) {
            return 0;
        }

        /// <internalonly />
        public static explicit operator int(decimal value) {
            return 0;
        }

        /// <internalonly />
        public static explicit operator double(decimal value) {
            return 0;
        }

        /// <internalonly />
        public static explicit operator float(decimal value) {
            return 0;
        }

        /// <internalonly />
        public static explicit operator long(decimal value) {
            return 0;
        }

        /// <internalonly />
        public static decimal operator +(decimal d) {
            return d;
        }

        /// <internalonly />
        public static decimal operator -(decimal d) {
            return d;
        }

        /// <internalonly />
        public static decimal operator +(decimal d1, decimal d2) {
            return d1;
        }

        /// <internalonly />
        public static decimal operator -(decimal d1, decimal d2) {
            return d1;
        }

        /// <internalonly />
        public static decimal operator ++(decimal d) {
            return d;
        }

        /// <internalonly />
        public static decimal operator --(decimal d) {
            return d;
        }

        /// <internalonly />
        public static decimal operator *(decimal d1, decimal d2) {
            return d1;
        }

        /// <internalonly />
        public static decimal operator /(decimal d1, decimal d2) {
            return d1;
        }

        /// <internalonly />
        public static decimal operator %(decimal d1, decimal d2) {
            return d1;
        }

        /// <internalonly />
        public static bool operator ==(decimal d1, decimal d2) {
            return false;
        }

        /// <internalonly />
        public static bool operator !=(decimal d1, decimal d2) {
            return false;
        }

        /// <internalonly />
        public static bool operator >(decimal d1, decimal d2) {
            return false;
        }

        /// <internalonly />
        public static bool operator >=(decimal d1, decimal d2) {
            return false;
        }

        /// <internalonly />
        public static bool operator <(decimal d1, decimal d2) {
            return false;
        }

        /// <internalonly />
        public static bool operator <=(decimal d1, decimal d2) {
            return false;
        }
    }
}
// Delegate.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace ScriptSharp.System {

    [ScriptImport]
    public abstract class Delegate {

        protected Delegate(object target, string method) {
        }

        protected Delegate(Type target, string method) {
        }

        [ScriptAlias(""ss.bindAdd"")]
        public static Delegate Combine(Delegate a, Delegate b) {
            return null;
        }

        [ScriptAlias(""ss.bind"")]
        public static Delegate Create(Function f, object instance) {
            return null;
        }

        [ScriptAlias(""ss.bindExport"")]
        public static Export Export(Delegate d) {
            return null;
        }

        [ScriptAlias(""ss.bindExport"")]
        public static Export Export(Delegate d, bool multiUse) {
            return null;
        }

        [ScriptAlias(""ss.bindExport"")]
        public static Export Export(Delegate d, bool multiUse, string name) {
            return null;
        }

        [ScriptAlias(""ss.bindExport"")]
        public static Export Export(Delegate d, bool multiUse, string name, object root) {
            return null;
        }

        [ScriptAlias(""ss.bindSub"")]
        public static Delegate Remove(Delegate source, Delegate value) {
            return null;
        }
    }
}
// Double.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The double data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Double {

        [ScriptName(""MAX_VALUE"")]
        public const double MaxValue = 0;

        [ScriptName(""MIN_VALUE"")]
        public const double MinValue = 0;

        [ScriptName(PreserveCase = true)]
        public const double NaN = 0;

        [ScriptName(""NEGATIVE_INFINITY"")]
        public const double NegativeInfinity = 0;

        [ScriptName(""POSITIVE_INFINITY"")]
        public const double PositiveInfinity = 0;

        [ScriptAlias(""parseFloat"")]
        public static double Parse(string s) {
            return 0;
        }

        /// <summary>
        /// Returns a string containing the value represented in exponential notation.
        /// </summary>
        /// <returns>The exponential representation</returns>
        public string ToExponential() {
            return null;
        }

        /// <summary>
        /// Returns a string containing the value represented in exponential notation.
        /// </summary>
        /// <param name=""fractionDigits"">The number of digits after the decimal point from 0 - 20</param>
        /// <returns>The exponential representation</returns>
        public string ToExponential(int fractionDigits) {
            return null;
        }

        /// <summary>
        /// Returns a string representing the value in fixed-point notation.
        /// </summary>
        /// <returns>The fixed-point notation</returns>
        public string ToFixed() {
            return null;
        }

        /// <summary>
        /// Returns a string representing the value in fixed-point notation.
        /// </summary>
        /// <param name=""fractionDigits"">The number of digits after the decimal point from 0 - 20</param>
        /// <returns>The fixed-point notation</returns>
        public string ToFixed(int fractionDigits) {
            return null;
        }

        /// <summary>
        /// Returns a string containing the value represented either in exponential or
        /// fixed-point notation with a specified number of digits.
        /// </summary>
        /// <returns>The string representation of the value.</returns>
        public string ToPrecision() {
            return null;
        }

        /// <summary>
        /// Returns a string containing the value represented either in exponential or
        /// fixed-point notation with a specified number of digits.
        /// </summary>
        /// <param name=""precision"">The number of significant digits (in the range 1 to 21)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToPrecision(int precision) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(double i) {
            return null;
        }
    }
}
// Enum.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    [ScriptImport]
    public abstract class Enum : ValueType {
    }
}
// EventArgs.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Used by event sources to pass event argument information.
    /// </summary>
    [ScriptImport]
    public class EventArgs {

        /// <summary>
        /// A static object of type <see cref=""EventArgs""/> that is used as a convenient way to
        /// specify an empty EventArgs instance.
        /// </summary>
        [ScriptName(PreserveCase = true)]
        public static readonly EventArgs Empty = null;
    }
}
// EventHandler.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Delegate for handling generic events.
    /// </summary>
    /// <param name=""sender"">The object that raised the event.</param>
    /// <param name=""e"">The <see cref=""EventArgs""/> object that contains the event data.</param>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void EventHandler(object sender, EventArgs e);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void EventHandler<TArgument>(object sender, TArgument e) where TArgument : EventArgs;
}
// Exception.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the Error type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Error"")]
    public sealed class Exception {

        public Exception(string message) {
        }

        [ScriptField]
        public Exception InnerException {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Message {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""stack"")]
        public string StackTrace {
            get {
                return null;
            }
        }

        [ScriptField]
        public object this[string key] {
            get {
                return null;
            }
        }

        [ScriptAlias(""ss.error"")]
        public static Exception Create(string message, Dictionary errorInfo) {
            return null;
        }

        [ScriptAlias(""ss.error"")]
        public static Exception Create(string message, Dictionary errorInfo, Exception innerException) {
            return null;
        }
    }
}
// Export.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;
using S = System;

namespace ScriptSharp.System {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    public sealed class Export : S.IDisposable {

        private Export() {
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
        }

        public void Detach() {
        }

        public void Dispose() {
        }
    }
}
// Func.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<TResult>();

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T, TResult>(T arg);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, T3, T4, T5, T6, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TResult Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
}
// Function.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace ScriptSharp.System {

    /// <summary>
    /// Equivalent to the Function type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Function {

        /// <summary>
        /// Creates a new function with the specified implementation.
        /// </summary>
        /// <param name=""functionBody"">The implementation of the function.</param>
        public Function(string functionBody) {
        }

        /// <summary>
        /// Creates a new function with the specified implementation, and the
        /// set of named parameters.
        /// </summary>
        /// <param name=""functionBody"">The implementation of the function.</param>
        /// <param name=""argNames"">The names of the arguments required by the function.</param>
        public Function(string functionBody, params string[] argNames) {
        }

        /// <summary>
        /// Gets the number of parameters expected by the function.
        /// </summary>
        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        /// <summary>
        /// Invokes the function against the specified object instance.
        /// </summary>
        /// <param name=""instance"">The object used as the value of 'this' within the function.</param>
        /// <returns>Any return value returned from the function.</returns>
        public object Apply(object instance) {
            return null;
        }

        /// <summary>
        /// Invokes the function against the specified object instance.
        /// </summary>
        /// <param name=""instance"">The object used as the value of 'this' within the function.</param>
        /// <param name=""arguments"">The set of arguments to pass in into the function.</param>
        /// <returns>Any return value returned from the function.</returns>
        public object Apply(object instance, object[] arguments) {
            return null;
        }

        /// <summary>
        /// Invokes the function against the specified object instance.
        /// </summary>
        /// <param name=""instance"">The object used as the value of 'this' within the function.</param>
        /// <returns>Any return value returned from the function.</returns>
        public object Call(object instance) {
            return null;
        }

        /// <summary>
        /// Invokes the function against the specified object instance.
        /// </summary>
        /// <param name=""instance"">The object used as the value of 'this' within the function.</param>
        /// <param name=""arguments"">One or more arguments to pass in into the function.</param>
        /// <returns>Any return value returned from the function.</returns>
        public object Call(object instance, params object[] arguments) {
            return null;
        }

        public static explicit operator Type(Function f) {
            return null;
        }
    }
}
// IDisposable.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    // Script Equivalent: IDisposable
    [ScriptImport]
    public interface IDisposable {

        void Dispose();
    }
}
// Int16.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The short data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Int16 {

        [ScriptName(""MAX_VALUE"")]
        public const int MaxValue = 0;

        [ScriptName(""MIN_VALUE"")]
        public const int MinValue = 0;

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(short i) {
            return null;
        }
    }
}
// Int32.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The int data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Int32 {

        [ScriptName(""MAX_VALUE"")]
        public const int MaxValue = 0;

        [ScriptName(""MIN_VALUE"")]
        public const int MinValue = 0;

        [ScriptAlias(""parseInt"")]
        public static int Parse(string s) {
            return 0;
        }

        [ScriptAlias(""parseInt"")]
        public static int Parse(string s, int radix) {
            return 0;
        }

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(int i) {
            return null;
        }
    }
}
// Int64.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The long data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Int64 {

        [ScriptName(""MAX_VALUE"")]
        public const long MaxValue = 0;

        [ScriptName(""MIN_VALUE"")]
        public const long MinValue = 0;

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(long i) {
            return null;
        }
    }
}
// Math.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the Math object in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public static class Math {

        [ScriptName(PreserveCase = true)]
        public static double E;

        [ScriptName(PreserveCase = true)]
        public static double LN2;

        [ScriptName(PreserveCase = true)]
        public static double LN10;

        [ScriptName(PreserveCase = true)]
        public static double LOG2E;

        [ScriptName(PreserveCase = true)]
        public static double LOG10E;

        [ScriptName(PreserveCase = true)]
        public static double PI;

        [ScriptName(PreserveCase = true)]
        public static double SQRT1_2;

        [ScriptName(PreserveCase = true)]
        public static double SQRT2;

        public static Number Abs(Number n) {
            return 0;
        }

        public static Number Acos(Number n) {
            return 0;
        }

        public static Number Asin(Number n) {
            return 0;
        }

        public static Number Atan(Number n) {
            return 0;
        }

        public static Number Atan2(Number x, Number y) {
            return 0;
        }

        public static Number Ceil(Number n) {
            return 0;
        }

        public static Number Cos(Number n) {
            return 0;
        }

        public static Number Exp(Number n) {
            return 0;
        }

        public static Number Floor(Number n) {
            return 0;
        }

        public static Number Log(Number n) {
            return 0;
        }

        public static Number Max(params Number[] numbers) {
            return 0;
        }

        public static Number Min(params Number[] numbers) {
            return 0;
        }

        public static Number Pow(Number baseNumber, Number exponent) {
            return 0;
        }

        public static Number Random() {
            return 0;
        }

        public static Number Round(Number n) {
            return 0;
        }

        public static Number Sin(Number n) {
            return 0;
        }

        public static Number Sqrt(Number n) {
            return 0;
        }

        public static Number Tan(Number n) {
            return 0;
        }

        [ScriptAlias(""ss.truncate"")]
        public static int Truncate(Number n) {
            return 0;
        }

        [ScriptAlias(""ss.truncate"")]
        public static int Truncate(double n) {
            return 0;
        }

        [ScriptAlias(""ss.truncate"")]
        public static int Truncate(float n) {
            return 0;
        }
    }
}
// Nullable.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public struct Nullable<T> where T : struct {

        public Nullable(T value) {
        }

        public bool HasValue {
            get {
                return false;
            }
        }

        public T Value {
            get {
                return default(T);
            }
        }

        public T GetValueOrDefault() {
            return default(T);
        }

        public static implicit operator T?(T value) {
            return null;
        }

        public static explicit operator T(T? value) {
            return default(T);
        }
    }
}
// Number.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Number {

        [ScriptName(""MAX_VALUE"")]
        public const double MaxValue = 0;

        [ScriptName(""MIN_VALUE"")]
        public const double MinValue = 0;

        [ScriptName(PreserveCase = true)]
        public const double NaN = 0;

        [ScriptName(""NEGATIVE_INFINITY"")]
        public const double NegativeInfinity = 0;

        [ScriptName(""POSITIVE_INFINITY"")]
        public const double PositiveInfinity = 0;

        [ScriptAlias(""isFinite"")]
        public static bool IsFinite(Number n) {
            return false;
        }

        [ScriptAlias(""isNaN"")]
        public static bool IsNaN(Number n) {
            return false;
        }

        [ScriptAlias(""ss.number"")]
        public static Number Parse(string s) {
            return null;
        }

        [ScriptAlias(""parseFloat"")]
        public static double ParseDouble(string s) {
            return 0;
        }

        [ScriptAlias(""parseFloat"")]
        public static float ParseFloat(string s) {
            return 0;
        }

        [ScriptAlias(""parseInt"")]
        public static int ParseInt(float f) {
            return 0;
        }

        [ScriptAlias(""parseInt"")]
        public static int ParseInt(double d) {
            return 0;
        }

        [ScriptAlias(""parseInt"")]
        public static int ParseInt(string s) {
            return 0;
        }

        [ScriptAlias(""parseInt"")]
        public static int ParseInt(string s, int radix) {
            return 0;
        }

        /// <summary>
        /// Returns a string containing the number represented in exponential notation.
        /// </summary>
        /// <returns>The exponential representation</returns>
        public string ToExponential() {
            return null;
        }

        /// <summary>
        /// Returns a string containing the number represented in exponential notation.
        /// </summary>
        /// <param name=""fractionDigits"">The number of digits after the decimal point (0 - 20)</param>
        /// <returns>The exponential representation</returns>
        public string ToExponential(int fractionDigits) {
            return null;
        }

        /// <summary>
        /// Returns a string representing the number in fixed-point notation.
        /// </summary>
        /// <returns>The fixed-point notation</returns>
        public string ToFixed() {
            return null;
        }

        /// <summary>
        /// Returns a string representing the number in fixed-point notation.
        /// </summary>
        /// <param name=""fractionDigits"">The number of digits after the decimal point from 0 - 20</param>
        /// <returns>The fixed-point notation</returns>
        public string ToFixed(int fractionDigits) {
            return null;
        }

        /// <summary>
        /// Returns a string containing the number represented either in exponential or
        /// fixed-point notation with a specified number of digits.
        /// </summary>
        /// <returns>The string representation of the value.</returns>
        public string ToPrecision() {
            return null;
        }

        /// <summary>
        /// Returns a string containing the number represented either in exponential or
        /// fixed-point notation with a specified number of digits.
        /// </summary>
        /// <param name=""precision"">The number of significant digits (in the range 1 to 21)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToPrecision(int precision) {
            return null;
        }

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        public static implicit operator int(Number n) {
            return 0;
        }
    }
}
// Object.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the Object type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class Object {

        /// <summary>
        /// Retrieves the type associated with an object instance.
        /// </summary>
        /// <returns>The type of the object.</returns>
        [ScriptAlias(""ss.typeOf"")]
        public Type GetType() {
            return null;
        }

        /// <summary>
        /// Converts an object to its string representation.
        /// </summary>
        /// <returns>The string representation of the object.</returns>
        public virtual string ToString() {
            return null;
        }

        /// <summary>
        /// Converts an object to its culture-sensitive string representation.
        /// </summary>
        /// <returns>The culture-sensitive string representation of the object.</returns>
        public virtual string ToLocaleString() {
            return null;
        }
    }
}
// RegExp.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using MiCS;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the RegExp type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""RegExp"")]
    public sealed class RegExp {

        public RegExp(string pattern) {
        }

        public RegExp(string pattern, string flags) {
        }

        [ScriptField]
        public int LastIndex {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public bool Global {
            get {
                return false;
            }
        }

        [ScriptField]
        public bool IgnoreCase {
            get {
                return false;
            }
        }

        [ScriptField]
        public bool Multiline {
            get {
                return false;
            }
        }

        [ScriptField]
        public string Pattern {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Source {
            get {
                return null;
            }
        }

        public string[] Exec(string s) {
            return null;
        }

        [ScriptAlias(""ss.regexp"")]
        public static RegExp Parse(string s) {
            return null;
        }

        public bool Test(string s) {
            return false;
        }
    }
}
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
    /// ""browsable"", i.e. present in the type descriptor associated with
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
// SByte.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The signed byte data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct SByte {

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        [CLSCompliant(false)]
        public static implicit operator Number(sbyte i) {
            return null;
        }
    }
}
// Script.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The Script class contains various methods that represent global
    /// methods present in the underlying script engine.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public static class Script {

        [ScriptField]
        [ScriptAlias(""$global"")]
        public static object Global {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptAlias(""ss.modules"")]
        public static Dictionary<string, object> Modules {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptAlias(""undefined"")]
        public static object Undefined {
            get {
                return null;
            }
        }

        /// <summary>
        /// Converts an object into a boolean.
        /// </summary>
        /// <param name=""o"">The object to convert.</param>
        /// <returns>true if the object is not null, zero, empty string or undefined.</returns>
        public static bool Boolean(object o) {
            return false;
        }

        [ScriptAlias(""clearInterval"")]
        public static void ClearInterval(int intervalID) {
        }

        [ScriptAlias(""clearTimeout"")]
        public static void ClearTimeout(int timeoutID) {
        }

        public static object CreateInstance(Type type, params object[] arguments) {
            return null;
        }

        public static void DeleteField(object instance, string name) {
        }

        public static void DeleteField(Type type, string name) {
        }

        /// <summary>
        /// Enables you to evaluate (or execute) an arbitrary script
        /// literal. This includes JSON literals, where the return
        /// value is the deserialized object graph.
        /// </summary>
        /// <param name=""s"">The script to be evaluated.</param>
        /// <returns>The result of the evaluation.</returns>
        [ScriptAlias(""eval"")]
        public static object Eval(string s) {
            return null;
        }

        public static object GetField(object instance, string name) {
            return null;
        }

        public static T GetField<T>(object instance, string name) {
            return default(T);
        }

        public static object GetField(Type type, string name) {
            return null;
        }

        public static T GetField<T>(Type type, string name) {
            return default(T);
        }

        public static string GetScriptType(object instance) {
            return null;
        }

        public static bool HasField(object instance, string name) {
            return false;
        }

        public static bool HasField(Type type, string name) {
            return false;
        }

        public static bool HasMethod(object instance, string name) {
            return false;
        }

        public static bool HasMethod(Type type, string name) {
            return false;
        }

        public static object InvokeMethod(object instance, string name, params object[] args) {
            return null;
        }

        public static T InvokeMethod<T>(object instance, string name, params object[] args) {
            return default(T);
        }

        public static object InvokeMethod(Type type, string name, params object[] args) {
            return null;
        }

        public static T InvokeMethod<T>(Type type, string name, params object[] args) {
            return default(T);
        }

        /// <summary>
        /// Checks if the specified object is null.
        /// </summary>
        /// <param name=""o"">The object to test against null.</param>
        /// <returns>true if the object is null; false otherwise.</returns>
        [ScriptAlias(""ss.isNull"")]
        public static bool IsNull(object o) {
            return false;
        }

        /// <summary>
        /// Checks if the specified object is null or undefined.
        /// The object passed in should be a local variable, and not
        /// a member of a class (to avoid potential script warnings).
        /// </summary>
        /// <param name=""o"">The object to test against null or undefined.</param>
        /// <returns>true if the object is null or undefined; false otherwise.</returns>
        [ScriptAlias(""ss.isNullOrUndefined"")]
        public static bool IsNullOrUndefined(object o) {
            return false;
        }

        /// <summary>
        /// Checks if the specified object is undefined.
        /// The object passed in should be a local variable, and not
        /// a member of a class (to avoid potential script warnings).
        /// </summary>
        /// <param name=""o"">The object to test against undefined.</param>
        /// <returns>true if the object is undefined; false otherwise.</returns>
        [ScriptAlias(""ss.isUndefined"")]
        public static bool IsUndefined(object o) {
            return false;
        }

        /// <summary>
        /// Checks if the specified object has a value, i.e. it is not
        /// null or undefined.
        /// </summary>
        /// <param name=""o"">The object to test.</param>
        /// <returns>true if the object represents a value; false otherwise.</returns>
        [ScriptAlias(""ss.isValue"")]
        public static bool IsValue(object o) {
            return false;
        }

        /// <summary>
        /// Enables you to generate an arbitrary (literal) script expression.
        /// The script can contain simple String.Format style tokens (such as
        /// {0}, {1}, ...) to be replaced with the specified arguments.
        /// </summary>
        /// <param name=""script"">The script expression to be evaluated.</param>
        /// <param name=""args"">Optional arguments matching tokens in the script.</param>
        /// <returns>The result of the script expression.</returns>
        public static object Literal(string script, params object[] args) {
            return null;
        }

        public static void SetField(object instance, string name, object value) {
        }

        public static void SetField(Type type, string name, object value) {
        }

        [ScriptAlias(""setInterval"")]
        public static int SetInterval(string code, int milliseconds) {
            return 0;
        }

        [ScriptAlias(""setInterval"")]
        public static int SetInterval(Action callback, int milliseconds) {
            return 0;
        }

        [ScriptAlias(""setInterval"")]
        public static int SetInterval<T>(Action<T> callback, int milliseconds, T arg) {
            return 0;
        }

        [ScriptAlias(""setInterval"")]
        public static int SetInterval<T1, T2>(Action<T1, T2> callback, int milliseconds, T1 arg1, T2 arg2) {
            return 0;
        }

        [ScriptAlias(""setInterval"")]
        public static int SetInterval(Delegate d, int milliseconds, params object[] args) {
            return 0;
        }

        [ScriptAlias(""setTimeout"")]
        public static int SetTimeout(string code, int milliseconds) {
            return 0;
        }

        [ScriptAlias(""setTimeout"")]
        public static int SetTimeout(Action callback, int milliseconds) {
            return 0;
        }

        [ScriptAlias(""setTimeout"")]
        public static int SetTimeout<T>(Action<T> callback, int milliseconds, T arg) {
            return 0;
        }

        [ScriptAlias(""setTimeout"")]
        public static int SetTimeout<T1, T2>(Action<T1, T2> callback, int milliseconds, T1 arg1, T2 arg2) {
            return 0;
        }

        [ScriptAlias(""setTimeout"")]
        public static int SetTimeout(Delegate d, int milliseconds, params object[] args) {
            return 0;
        }

        /// <summary>
        /// Gets the first valid (non-null, non-undefined, non-empty) value.
        /// </summary>
        /// <typeparam name=""TValue"">The type of the value.</typeparam>
        /// <param name=""value"">The value to check for validity.</param>
        /// <param name=""alternateValue"">The alternate value to use if the first is invalid.</param>
        /// <param name=""alternateValues"">Additional alternative values to use if the first is invalid.</param>
        /// <returns>The first valid value.</returns>
        public static TValue Value<TValue>(TValue value, TValue alternateValue, params TValue[] alternateValues) {
            return default(TValue);
        }
    }
}
// ScriptMetadata.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using S = System;

namespace System {

    /// <summary>
    /// Marks an assembly as a script assembly that can be used with Script#.
    /// Additionally, each script must have a unique name that can be used as
    /// a dependency name.
    /// This name is also used to generate unique names for internal types defined
    /// within the assembly. The ScriptQualifier attribute can be used to provide a
    /// shorter name if needed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class ScriptAssemblyAttribute : Attribute {

        private string _name;
        private string _identifier;

        public ScriptAssemblyAttribute(string name) {
            _name = name;
        }

        public string Name {
            get {
                return _name;
            }
        }

        public string Identifier {
            get {
                return _identifier;
            }
            set {
                _identifier = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class ScriptReferenceAttribute : Attribute {

        private string _name;

        private string _identifier;
        private string _path;
        private bool _delayLoad;

        public ScriptReferenceAttribute(string name) {
            _name = name;
        }

        public bool DelayLoad {
            get {
                return _delayLoad;
            }
            set {
                _delayLoad = value;
            }
        }

        public string Identifier {
            get {
                return _identifier;
            }
            set {
                _identifier = value;
            }
        }

        public string Name {
            get {
                return _name;
            }
        }

        public string Path {
            get {
                return _path;
            }
            set {
                _path = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptTemplateAttribute : Attribute {

        private string _template;

        public ScriptTemplateAttribute(string template) {
            _template = template;
        }

        public string Template {
            get {
                return _template;
            }
        }
    }

    /// <summary>
    /// This attribute can be placed on a static class that only contains static string
    /// fields representing a set of resource strings.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [ScriptIgnore]
    public sealed class ScriptResourcesAttribute : Attribute {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptExtensionAttribute : Attribute {

        private string _expression;

        public ScriptExtensionAttribute(string extendeeExpression) {
            _expression = extendeeExpression;
        }

        public string Expression {
            get {
                return _expression;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptModuleAttribute : Attribute {
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptObjectAttribute : Attribute {
    }

    /// <summary>
    /// This attribute is used to mark an enum as a set of constant values, i.e. if
    /// specified, the enum does not exist/is not generated, but rather its values
    /// are inlined as constants. If the UseName property is set to true, then instead
    /// of actual values, the names of the fields are used as string constants.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptConstantsAttribute : Attribute {

        private bool _useNames;

        public bool UseNames {
            get {
                return _useNames;
            }
            set {
                _useNames = value;
            }
        }
    }


    /// <summary>
    /// Allows specifying the name to use for a type or member in the generated script.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptNameAttribute : Attribute {

        private string _name;
        private bool _preserveCase;
        private bool _preserveName;

        public ScriptNameAttribute() {
        }

        public ScriptNameAttribute(string name) {
            _name = name;
        }

        public string Name {
            get {
                return _name;
            }
        }

        public bool PreserveCase {
            get {
                return _preserveCase;
            }
            set {
                _preserveCase = true;
            }
        }

        public bool PreserveName {
            get {
                return _preserveName;
            }
            set {
                _preserveName = value;
            }
        }
    }
}

namespace System.Runtime.CompilerServices {

    /// <summary>
    /// This attribute can be placed on types in system script assemblies that should not
    /// be imported. It is only meant to be used within mscorlib.dll.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    [ScriptImport]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ScriptIgnoreAttribute : Attribute {
    }

    /// <summary>
    /// This attribute can be placed on types that should not be emitted into generated
    /// script, as they represent existing script or native types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Struct)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class ScriptImportAttribute : Attribute {
    }

    /// <summary>
    /// Marks a type with a script dependency that is required at runtime.
    /// The specified name is used as the name of the dependency, and the runtime identifier.
    /// </summary>
    // Todo: ASSC: Changed to make it compile!
    //[AttributeUsage(AttributeTargets.Type, Inherited = false, AllowMultiple = false)]
    [S.AttributeUsage(S.AttributeTargets.Class | S.AttributeTargets.Struct | S.AttributeTargets.Enum | S.AttributeTargets.Interface | S.AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class ScriptDependencyAttribute : Attribute {

        private string _name;
        private string _identifier;

        public ScriptDependencyAttribute(string name) {
            _name = name;
        }

        public string Name {
            get {
                return _name;
            }
        }

        public string Identifier {
            get {
                return _identifier;
            }
            set {
                _identifier = value;
            }
        }
    }

    /// <summary>
    /// This attribute indicates that the namespace of type within a system assembly
    /// should be ignored at script generation time. It is useful for creating namespaces
    /// for the purpose of c# code that don't exist at runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptIgnoreNamespaceAttribute : Attribute {
    }

    /// <summary>
    /// This attribute allows specifying a script name for an imported method.
    /// The method is interpreted as a global method. As a result it this attribute
    /// only applies to static methods.
    /// </summary>
    // REVIEW: Eventually do we want to support this on properties/field and instance methods as well?
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptAliasAttribute : Attribute {

        private string _alias;

        public ScriptAliasAttribute(string alias) {
            _alias = alias;
        }

        public string Alias {
            get {
                return _alias;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptSkipAttribute : Attribute {
    }

    /// <summary>
    /// This attribute denotes a C# property that manifests like a field in the generated
    /// JavaScript (i.e. is not accessed via get/set methods). This is really meant only
    /// for use when defining OM corresponding to native objects exposed to script.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptFieldAttribute : Attribute {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptMethodAttribute : Attribute {

        private string _selector;

        public ScriptMethodAttribute(string selector) {
            _selector = selector;
        }

        public string Selector {
            get {
                return _selector;
            }
        }
    }


    [AttributeUsage(AttributeTargets.Event, Inherited = true, AllowMultiple = false)]
    [ScriptIgnore]
    public sealed class ScriptEventAttribute : Attribute {

        private string _addAccessor;
        private string _removeAccessor;

        public ScriptEventAttribute(string addAccessor, string removeAccessor) {
            _addAccessor = addAccessor;
            _removeAccessor = removeAccessor;
        }

        public string AddAccessor {
            get {
                return _addAccessor;
            }
        }

        public string RemoveAccessor {
            get {
                return _removeAccessor;
            }
        }
    }
}
// Single.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The float data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct Single {

        [ScriptAlias(""parseFloat"")]
        public static float Parse(string s) {
            return 0;
        }

        /// <summary>
        /// Returns a string containing the value represented in exponential notation.
        /// </summary>
        /// <returns>The exponential representation</returns>
        public string ToExponential() {
            return null;
        }

        /// <summary>
        /// Returns a string containing the value represented in exponential notation.
        /// </summary>
        /// <param name=""fractionDigits"">The number of digits after the decimal point (0 - 20)</param>
        /// <returns>The exponential representation</returns>
        public string ToExponential(int fractionDigits) {
            return null;
        }

        /// <summary>
        /// Returns a string representing the value in fixed-point notation.
        /// </summary>
        /// <returns>The fixed-point notation</returns>
        public string ToFixed() {
            return null;
        }

        /// <summary>
        /// Returns a string representing the value in fixed-point notation.
        /// </summary>
        /// <param name=""fractionDigits"">The number of digits after the decimal point from 0 - 20</param>
        /// <returns>The fixed-point notation</returns>
        public string ToFixed(int fractionDigits) {
            return null;
        }

        /// <summary>
        /// Returns a string containing the value represented either in exponential or
        /// fixed-point notation with a specified number of digits.
        /// </summary>
        /// <returns>The string representation of the value.</returns>
        public string ToPrecision() {
            return null;
        }

        /// <summary>
        /// Returns a string containing the value represented either in exponential or
        /// fixed-point notation with a specified number of digits.
        /// </summary>
        /// <param name=""precision"">The number of significant digits (in the range 1 to 21)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToPrecision(int precision) {
            return null;
        }

        /// <internalonly />
        public static implicit operator Number(float i) {
            return null;
        }
    }
}
// String.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Equivalent to the String type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class String {

        /// <summary>
        /// An empty zero-length string.
        /// </summary>
        public static readonly String Empty = """";

        /// <summary>
        /// The number of characters in the string.
        /// </summary>
        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        /// <summary>
        /// Retrieves the character at the specified position.
        /// </summary>
        /// <param name=""index"">The specified 0-based position.</param>
        /// <returns>The character within the string.</returns>
        [ScriptField]
        public char this[int index] {
            get {
                return '\0';
            }
        }

        /// <summary>
        /// Retrieves the character at the specified position.
        /// </summary>
        /// <param name=""index"">The specified 0-based position.</param>
        /// <returns>The character within the string.</returns>
        public char CharAt(int index) {
            return '\0';
        }

        /// <summary>
        /// Retrieves the character code of the character at the specified position.
        /// </summary>
        /// <param name=""index"">The specified 0-based position.</param>
        /// <returns>The character code of the character within the string.</returns>
        public int CharCodeAt(int index) {
            return 0;
        }

        [ScriptAlias(""ss.compareStrings"")]
        public static int Compare(string s1, string s2) {
            return 0;
        }

        [ScriptAlias(""ss.compareStrings"")]
        public static int Compare(string s1, string s2, bool ignoreCase) {
            return 0;
        }

        [ScriptAlias(""ss.string"")]
        public static string Concat(string s1, string s2) {
            return null;
        }

        [ScriptAlias(""ss.string"")]
        public static string Concat(string s1, string s2, string s3) {
            return null;
        }

        [ScriptAlias(""ss.string"")]
        public static string Concat(string s1, string s2, string s3, string s4) {
            return null;
        }

        /// <summary>
        /// Concatenates a set of individual strings into a single string.
        /// </summary>
        /// <param name=""strings"">The sequence of strings</param>
        /// <returns>The concatenated string.</returns>
        [ScriptAlias(""ss.string"")]
        public static string Concat(params string[] strings) {
            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ScriptAlias(""ss.string"")]
        public static string Concat(object o1, object o2) {
            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ScriptAlias(""ss.string"")]
        public static string Concat(object o1, object o2, object o3) {
            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ScriptAlias(""ss.string"")]
        public static string Concat(object o1, object o2, object o3, object o4) {
            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ScriptAlias(""ss.string"")]
        public static string Concat(params object[] o) {
            return null;
        }

        /// <summary>
        /// Returns the unencoded version of a complete encoded URI.
        /// </summary>
        /// <returns>The unencoded string.</returns>
        [ScriptAlias(""decodeURI"")]
        public string DecodeUri() {
            return null;
        }

        /// <summary>
        /// Returns the unencoded version of a single part or component of an encoded URI.
        /// </summary>
        /// <returns>The unencoded string.</returns>
        [ScriptAlias(""decodeURIComponent"")]
        public string DecodeUriComponent() {
            return null;
        }

        /// <summary>
        /// Encodes the complete URI.
        /// </summary>
        /// <returns>The encoded string.</returns>
        [ScriptAlias(""encodeURI"")]
        public string EncodeUri() {
            return null;
        }

        /// <summary>
        /// Encodes a single part or component of a URI.
        /// </summary>
        /// <returns>The encoded string.</returns>
        [ScriptAlias(""encodeURIComponent"")]
        public string EncodeUriComponent() {
            return null;
        }

        /// <summary>
        /// Determines if the string ends with the specified character.
        /// </summary>
        /// <param name=""ch"">The character to test for.</param>
        /// <returns>true if the string ends with the character; false otherwise.</returns>
        [ScriptAlias(""ss.endsWith"")]
        public bool EndsWith(char ch) {
            return false;
        }

        /// <summary>
        /// Determines if the string ends with the specified substring or suffix.
        /// </summary>
        /// <param name=""suffix"">The string to test for.</param>
        /// <returns>true if the string ends with the suffix; false otherwise.</returns>
        [ScriptAlias(""ss.endsWith"")]
        public bool EndsWith(string suffix) {
            return false;
        }

        /// <summary>
        /// Encodes a string by replacing punctuation, spaces etc. with their escaped equivalents.
        /// </summary>
        /// <returns>The escaped string.</returns>
        [ScriptAlias(""escape"")]
        public string Escape() {
            return null;
        }

        [ScriptAlias(""ss.format"")]
        public static string Format(string format, params object[] values) {
            return null;
        }

        [ScriptAlias(""ss.format"")]
        public static string Format(CultureInfo culture, string format, params object[] values) {
            return null;
        }

        [ScriptAlias(""ss.string"")]
        public static string FromChar(char ch, int count) {
            return null;
        }

        public static string FromCharCode(int charCode) {
            return null;
        }

        public static string FromCharCode(params int[] charCodes) {
            return null;
        }

        public int IndexOf(char ch) {
            return 0;
        }

        public int IndexOf(string subString) {
            return 0;
        }

        public int IndexOf(char ch, int startIndex) {
            return 0;
        }

        public int IndexOf(string subString, int startIndex) {
            return 0;
        }

        [ScriptAlias(""ss.insertString"")]
        public string Insert(int index, string value) {
            return null;
        }

        [ScriptAlias(""ss.emptyString"")]
        public static bool IsNullOrEmpty(string s) {
            return false;
        }

        [ScriptAlias(""ss.whitespace"")]
        public static bool IsNullOrWhiteSpace(string s) {
            return false;
        }

        public int LastIndexOf(Char ch) {
            return 0;
        }

        public int LastIndexOf(string subString) {
            return 0;
        }

        public int LastIndexOf(char ch, int startIndex) {
            return 0;
        }

        public int LastIndexOf(string subString, int startIndex) {
            return 0;
        }

        public string[] Match(RegExp regex) {
            return null;
        }

        [ScriptAlias(""ss.padLeft"")]
        public string PadLeft(int totalWidth) {
            return null;
        }

        [ScriptAlias(""ss.padLeft"")]
        public string PadLeft(int totalWidth, char ch) {
            return null;
        }

        [ScriptAlias(""ss.padRight"")]
        public string PadRight(int totalWidth) {
            return null;
        }

        [ScriptAlias(""ss.padRight"")]
        public string PadRight(int totalWidth, char ch) {
            return null;
        }

        [ScriptAlias(""ss.removeString"")]
        public string Remove(int index) {
            return null;
        }

        [ScriptAlias(""ss.removeString"")]
        public string Remove(int index, int count) {
            return null;
        }

        [ScriptAlias(""ss.replaceString"")]
        public string Replace(string oldText, string replaceText) {
            return null;
        }

        [ScriptName(""replace"")]
        public string ReplaceFirst(string oldText, string replaceText) {
            return null;
        }

        [ScriptName(""replace"")]
        public string ReplaceRegex(RegExp regex, string replaceText) {
            return null;
        }

        [ScriptName(""replace"")]
        public string ReplaceRegex(RegExp regex, StringReplaceCallback callback) {
            return null;
        }

        public int Search(RegExp regex) {
            return 0;
        }

        public string[] Split(char ch) {
            return null;
        }

        public string[] Split(string separator) {
            return null;
        }

        public string[] Split(char ch, int limit) {
            return null;
        }

        public string[] Split(string separator, int limit) {
            return null;
        }

        public string[] Split(RegExp regex) {
            return null;
        }

        public string[] Split(RegExp regex, int limit) {
            return null;
        }

        [ScriptAlias(""ss.startsWith"")]
        public bool StartsWith(char ch) {
            return false;
        }

        [ScriptAlias(""ss.startsWith"")]
        public bool StartsWith(string prefix) {
            return false;
        }

        public string Substr(int startIndex) {
            return null;
        }

        public string Substr(int startIndex, int length) {
            return null;
        }

        public string Substring(int startIndex) {
            return null;
        }

        public string Substring(int startIndex, int endIndex) {
            return null;
        }

        public string ToLocaleLowerCase() {
            return null;
        }

        public string ToLocaleUpperCase() {
            return null;
        }

        public string ToLowerCase() {
            return null;
        }

        public string ToUpperCase() {
            return null;
        }

        public string Trim() {
            return null;
        }

        [ScriptAlias(""trimEnd"")]
        public string TrimEnd() {
            return null;
        }

        [ScriptAlias(""ss.trimStart"")]
        public string TrimStart() {
            return null;
        }

        /// <summary>
        /// Decodes a string by replacing escaped parts with their equivalent textual representation.
        /// </summary>
        /// <returns>The unescaped string.</returns>
        [ScriptAlias(""unescape"")]
        public string Unescape() {
            return null;
        }

        /// <internalonly />
        public static bool operator ==(string s1, string s2) {
            return false;
        }

        /// <internalonly />
        public static bool operator !=(string s1, string s2) {
            return false;
        }
    }
}
// StringBuilder.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// Provides an optimized mechanism to concatenate strings.
    /// </summary>
    [ScriptImport]
    public sealed class StringBuilder {

        /// <summary>
        /// Initializes a new instance of the <see cref=""StringBuilder""/> class.
        /// </summary>
        public StringBuilder() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref=""StringBuilder""/> class.
        /// </summary>
        /// <param name=""initialText"">
        /// The string that is used to initialize the value of the instance.
        /// </param>
        public StringBuilder(string initialText) {
        }

        /// <summary>
        /// Gets whether the <see cref=""StringBuilder""/> object has any content.
        /// </summary>
        /// <returns>true if the StringBuilder instance contains no text; otherwise, false.</returns>
        [ScriptField]
        public bool IsEmpty {
            get {
                return false;
            }
        }

        /// <summary>
        /// Appends a boolean value to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""b"">The boolean value to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(bool b) {
            return null;
        }

        /// <summary>
        /// Appends a character to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""c"">The character to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(char c) {
            return null;
        }

        /// <summary>
        /// Appends a number to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""n"">The number to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(Number n) {
            return null;
        }

        /// <summary>
        /// Appends an object's string representation to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""o"">The object to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(object o) {
            return null;
        }

        /// <summary>
        /// Appends the specified string to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""s"">The string to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder Append(string s) {
            return null;
        }

        /// <summary>
        /// Appends a string with a line terminator to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine() {
            return null;
        }

        /// <summary>
        /// Appends a boolean with a line terminator to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""b"">The boolean value to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(bool b) {
            return null;
        }

        /// <summary>
        /// Appends a character with a line terminator to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""c"">The character to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(char c) {
            return null;
        }

        /// <summary>
        /// Appends a number with a line terminator to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""n"">The number to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(Number n) {
            return null;
        }

        /// <summary>
        /// Appends an object's string representation with a line terminator to the end of the
        /// <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""o"">The object to append to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(object o) {
            return null;
        }

        /// <summary>
        /// Appends a string with a line terminator to the end of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <param name=""s"">The string to append with a line terminator to the end of the StringBuilder instance.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        public StringBuilder AppendLine(string s) {
            return null;
        }

        /// <summary>
        /// Clears the contents of the <see cref=""StringBuilder""/> instance.
        /// </summary>
        public void Clear() {
        }

        /// <summary>
        /// Creates a string from the contents of a <see cref=""StringBuilder""/> instance.
        /// </summary>
        /// <returns>A string representation of the StringBuilder instance.</returns>
        public override string ToString() {
            return null;
        }

        /// <summary>
        /// Creates a string from the contents of a <see cref=""StringBuilder""/> instance, and
        /// optionally inserts a delimeter between each element of the created string.
        /// </summary>
        /// <param name=""separator"">A string to append between each element of the string that is returned.</param>
        /// <returns>
        /// A string representation of the StringBuilder instance. If <paramref name=""separator""/>
        /// is specified, the delimeter string is inserted between each element of the returned string.
        /// </returns>
        public string ToString(string separator) {
            return null;
        }
    }
}
// StringReplaceCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    // TODO: The actual signature needs to be
    //       string callback(match, m1, m2... mN, offset, fullString)
    //       but there isn't a way to express the varying number of parameters in the
    //       middle of the signature!

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate string StringReplaceCallback(string matchedValue);
}
// Tuple.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    [ScriptName(""Tuple"")]
    public sealed class Tuple<T1, T2> {

        public Tuple() {
        }

        public Tuple(T1 item1, T2 item2) {
        }

        [ScriptField]
        public T1 Item1 {
            get {
                return default(T1);
            }
            set {
            }
        }

        [ScriptField]
        public T2 Item2 {
            get {
                return default(T2);
            }
            set {
            }
        }
    }

    [ScriptImport]
    [ScriptIgnoreNamespace]
    [ScriptName(""Tuple"")]
    public sealed class Tuple<T1, T2, T3> {

        public Tuple() {
        }

        public Tuple(T1 item1, T2 item2, T3 item3) {
        }

        [ScriptField]
        public T1 Item1 {
            get {
                return default(T1);
            }
            set {
            }
        }

        [ScriptField]
        public T2 Item2 {
            get {
                return default(T2);
            }
            set {
            }
        }

        [ScriptField]
        public T3 Item3 {
            get {
                return default(T3);
            }
            set {
            }
        }
    }

    [ScriptImport]
    [ScriptIgnoreNamespace]
    [ScriptName(""Tuple"")]
    public sealed class Tuple<T1, T2, T3, T4> {

        public Tuple() {
        }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) {
        }

        [ScriptField]
        public T1 Item1 {
            get {
                return default(T1);
            }
            set {
            }
        }

        [ScriptField]
        public T2 Item2 {
            get {
                return default(T2);
            }
            set {
            }
        }

        [ScriptField]
        public T3 Item3 {
            get {
                return default(T3);
            }
            set {
            }
        }

        [ScriptField]
        public T4 Item4 {
            get {
                return default(T4);
            }
            set {
            }
        }
    }
}
// Type.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using S = System;

namespace ScriptSharp.System {

    /// <summary>
    /// The Type data type which is mapped to the Function type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Type {

        [S.ScriptName(""$base"")]
        [ScriptField]
        public Type BaseType {
            get {
                return null;
            }
        }

        public string Name {
            get {
                return null;
            }
        }

        /// <summary>
        /// Gets the prototype associated with the type.
        /// </summary>
        [ScriptField]
        public Dictionary Prototype
        {
            get
            {
                return null;
            }
        }

        [ScriptAlias(""ss.type"")]
        public static Type GetType(string typeName) {
            return null;
        }

        [ScriptAlias(""ss.canAssign"")]
        public bool IsAssignableFrom(Type type) {
            return false;
        }

        [ScriptAlias(""ss.isClass"")]
        public static bool IsClass(Type type) {
            return false;
        }

        [ScriptAlias(""ss.isInterface"")]
        public static bool IsInterface(Type type) {
            return false;
        }

        [ScriptAlias(""ss.instanceOf"")]
        public bool IsInstanceOfType(object instance) {
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Type GetTypeFromHandle(RuntimeTypeHandle typeHandle) {
            return null;
        }
    }
}
// UInt16.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The ushort data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct UInt16 {

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        [CLSCompliant(false)]
        public static implicit operator Number(ushort i) {
            return null;
        }
    }
}
// UInt32.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The uint data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct UInt32 {

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        [CLSCompliant(false)]
        public static implicit operator Number(uint i) {
            return null;
        }
    }
}
// UInt64.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System {

    /// <summary>
    /// The ulong data type which is mapped to the Number type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Number"")]
    public struct UInt64 {

        /// <summary>
        /// Converts the value to its string representation.
        /// </summary>
        /// <param name=""radix"">The radix used in the conversion (eg. 10 for decimal, 16 for hexadecimal)</param>
        /// <returns>The string representation of the value.</returns>
        public string ToString(int radix) {
            return null;
        }

        /// <internalonly />
        [CLSCompliant(false)]
        public static implicit operator Number(ulong i) {
            return null;
        }
    }
}
// Void.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System {

    // This doesn't map to an actual type, but needs to be present
    // in the set of types, so that the C# void type can be mapped
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public struct Void {
    }
}
// ArrayCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void ArrayCallback(object value, int index, Array array);
}
// ArrayFilterCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate bool ArrayFilterCallback(object value, int index, Array array);
}
// ArrayItemCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void ArrayItemCallback(object value);
}
// ArrayItemFilterCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate bool ArrayItemFilterCallback(object value);
}
// ArrayItemMapCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate object ArrayItemMapCallback(object value);
}
// ArrayItemReduceCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate object ArrayItemReduceCallback(object previousValue, object value);
}
// ArrayList.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    // NOTE: Keep in sync with Array and List

    /// <summary>
    /// Equivalent to the Array type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Array"")]
    public sealed class ArrayList : IEnumerable {

        public ArrayList() {
        }

        public ArrayList(int capacity) {
        }

        public ArrayList(params object[] items) {
        }

        [ScriptField]
        [ScriptName(""length"")]
        public int Count {
            get {
                return 0;
            }
        }

        [ScriptField]
        public object this[int index] {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""push"")]
        public void Add(object item) {
        }

        [ScriptName(""push"")]
        public void AddRange(params object[] items) {
        }

        public void Clear() {
        }

        public ArrayList Concat(params object[] objects) {
            return null;
        }

        public bool Contains(object item) {
            return false;
        }

        public bool Every(ArrayFilterCallback filterCallback) {
            return false;
        }

        public bool Every(ArrayItemFilterCallback itemFilterCallback) {
            return false;
        }

        public Array Filter(ArrayFilterCallback filterCallback) {
            return null;
        }

        public Array Filter(ArrayItemFilterCallback itemFilterCallback) {
            return null;
        }

        public void ForEach(ArrayCallback callback) {
        }

        public void ForEach(ArrayItemCallback itemCallback) {
        }

        public IEnumerator GetEnumerator() {
            return null;
        }

        public Array GetRange(int index) {
            return null;
        }

        public Array GetRange(int index, int count) {
            return null;
        }

        public int IndexOf(object item) {
            return 0;
        }

        public int IndexOf(object item, int startIndex) {
            return 0;
        }

        public void Insert(int index, object item) {
        }

        public void InsertRange(int index, params object[] items) {
        }

        public string Join() {
            return null;
        }

        public string Join(string delimiter) {
            return null;
        }

        public int LastIndexOf(object item) {
            return 0;
        }

        public int LastIndexOf(object item, int fromIndex) {
            return 0;
        }

        public Array Map(ArrayMapCallback mapCallback) {
            return null;
        }

        public Array Map(ArrayItemMapCallback mapItemCallback) {
            return null;
        }

        public static ArrayList Parse(string s) {
            return null;
        }

        public object Reduce(ArrayReduceCallback callback) {
            return null;
        }

        public object Reduce(ArrayReduceCallback callback, object initialValue) {
            return null;
        }

        public object Reduce(ArrayItemReduceCallback callback) {
            return null;
        }

        public object Reduce(ArrayItemReduceCallback callback, object initialValue) {
            return null;
        }

        public object ReduceRight(ArrayReduceCallback callback) {
            return null;
        }

        public object ReduceRight(ArrayReduceCallback callback, object initialValue) {
            return null;
        }

        public object ReduceRight(ArrayItemReduceCallback callback) {
            return null;
        }

        public object ReduceRight(ArrayItemReduceCallback callback, object initialValue) {
            return null;
        }

        [ScriptAlias(""ss.remove"")]
        public bool Remove(object item) {
            return false;
        }

        public void RemoveAt(int index) {
        }

        public Array RemoveRange(int index, int count) {
            return null;
        }

        public void Reverse() {
        }

        public object Shift() {
            return null;
        }

        public Array Slice(int start) {
            return null;
        }

        public Array Slice(int start, int end) {
            return null;
        }

        public bool Some(ArrayFilterCallback filterCallback) {
            return false;
        }

        public bool Some(ArrayItemFilterCallback itemFilterCallback) {
            return false;
        }

        public void Sort() {
        }

        public void Sort(CompareCallback compareCallback) {
        }

        public void Splice(int start, int deleteCount) {
        }

        public void Splice(int start, int deleteCount, params object[] itemsToInsert) {
        }

        public void Unshift(params object[] items) {
        }

        public static explicit operator Array(ArrayList list) {
            return null;
        }
    }
}
// ArrayMapCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate object ArrayMapCallback(object value, int index, Array array);
}
// ArrayReduceCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate object ArrayReduceCallback(object previousValue, object value, int index, Array array);
}
// Dictionary.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    /// <summary>
    /// The Dictionary data type which is mapped to the Object type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class Dictionary : IEnumerable {

        public Dictionary() {
        }

        public Dictionary(params object[] nameValuePairs) {
        }

        public int Count {
            get {
                return 0;
            }
        }

        public string[] Keys {
            get {
                return null;
            }
        }

        [ScriptField]
        public object this[string key] {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptAlias(""ss.clearKeys"")]
        public void Clear() {
        }

        [ScriptAlias(""ss.keyExists"")]
        public bool ContainsKey(string key) {
            return false;
        }

        public static Dictionary GetDictionary(object o) {
            return null;
        }

        public void Remove(string key) {
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return null;
        }
    }
}
// DictionaryEntry.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DictionaryEntry {

        internal DictionaryEntry() {
        }

        [ScriptField]
        public string Key {
            get {
                return null;
            }
        }

        [ScriptField]
        public object Value {
            get {
                return null;
            }
        }
    }
}
// ICollection.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    [ScriptName(""ICollection"")]
    public interface ICollection : IEnumerable {

        [ScriptField]
        [ScriptName(""length"")]
        int Count {
            get;
        }

        [ScriptField]
        object this[int index] {
            get;
            set;
        }
    }
}
// IEnumerable.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    public interface IEnumerable {

        IEnumerator GetEnumerator();
    }
}
// IEnumerator.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    public interface IEnumerator {

        [ScriptField]
        object Current {
            get;
        }

        bool MoveNext();

        void Reset();
    }
}
// IReadonlyCollection.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    [ScriptName(""ICollection"")]
    public interface IReadonlyCollection : IEnumerable {

        [ScriptField]
        [ScriptName(""length"")]
        int Count {
            get;
        }

        [ScriptField]
        object this[int index] {
            get;
        }
    }
}
// ObservableCollection.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    [ScriptName(""ObservableCollection"")]
    public sealed class ObservableCollection : IEnumerable {

        public ObservableCollection() {
        }

        public ObservableCollection(Array items) {
        }

        [ScriptName(""length"")]
        public int Count {
            get {
                return 0;
            }
        }

        public object this[int index] {
            get {
                return null;
            }
            set {
            }
        }

        public void Add(object item) {
        }

        public void Clear() {
        }

        public bool Contains(object item) {
            return false;
        }

        public IEnumerator GetEnumerator() {
            return null;
        }

        public int IndexOf(object item) {
            return 0;
        }

        public void Insert(int index, object item) {
        }

        public bool Remove(object item) {
            return false;
        }

        public void RemoveAt(int index) {
        }

        public Array ToArray() {
            return null;
        }
    }
}
// Queue.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    public sealed class Queue {

        [ScriptField]
        public int Count {
            get {
                return 0;
            }
        }

        public void Clear() {
        }

        public bool Contains(object item) {
            return false;
        }

        public object Dequeue() {
            return null;
        }

        public void Enqueue(object item) {
        }

        public object Peek() {
            return null;
        }
    }
}
// Stack.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections {

    [ScriptImport]
    public sealed class Stack {

        [ScriptField]
        public int Count {
            get {
                return 0;
            }
        }

        public void Clear() {
        }

        public bool Contains(object item) {
            return false;
        }

        public object Peek() {
            return null;
        }

        public object Pop() {
            return null;
        }

        public void Push(object item) {
        }
    }
}
// CompareCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate int CompareCallback<T>(T x, T y);
}
// Dictionary.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    /// <summary>
    /// The Dictionary data type which is mapped to the Object type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> {

        public Dictionary() {
        }

        public Dictionary(params object[] nameValuePairs) {
        }

        [Obsolete(""This is only for use by the c# compiler, and cannot be used for generating script."", /* error */ true)]
        public Dictionary(int count) {
        }

        public int Count {
            get {
                return 0;
            }
        }

        public IReadonlyCollection<TKey> Keys {
            get {
                return null;
            }
        }

        [ScriptField]
        public TValue this[TKey key] {
            get {
                return default(TValue);
            }
            set {
            }
        }

        [Obsolete(""This is only for use by the c# compiler, and cannot be used for generating script."", /* error */ true)]
        public void Add(TKey key, TValue value) {
        }

        [ScriptAlias(""ss.clearKeys"")]
        public void Clear() {
        }

        [ScriptAlias(""ss.keyExists"")]
        public bool ContainsKey(TKey key) {
            return false;
        }

        public static Dictionary<TKey, TValue> GetDictionary(object o) {
            return null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return null;
        }

        public void Remove(TKey key) {
        }

        [Obsolete(""This is only for use by the c# compiler, and cannot be used for generating script."", /* error */ true)]
        public bool TryGetValue(TKey key, out TValue value) {
            value = default(TValue);
            return false;
        }

        public static implicit operator Dictionary(Dictionary<TKey, TValue> dictionary) {
            return null;
        }

        public static implicit operator Dictionary<TKey, TValue>(Dictionary dictionary) {
            return null;
        }
    }
}
// ICollection.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""ICollection"")]
    public interface ICollection<T> : IEnumerable<T> {

        [ScriptField]
        [ScriptName(""length"")]
        int Count {
            get;
        }

        [ScriptField]
        T this[int index] {
            get;
            set;
        }
    }
}
// IEnumerable.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""IEnumerable"")]
    public interface IEnumerable<T> {

        IEnumerator<T> GetEnumerator();
    }
}
// IEnumerator.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""IEnumerator"")]
    public interface IEnumerator<T> {

        [ScriptField]
        T Current {
            get;
        }

        bool MoveNext();

        void Reset();
    }
}
// IReadonlyCollection.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""ICollection"")]
    public interface IReadonlyCollection<T> : IEnumerable<T> {

        [ScriptField]
        [ScriptName(""length"")]
        int Count {
            get;
        }

        [ScriptField]
        T this[int index] {
            get;
        }
    }
}
// KeyValuePair.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class KeyValuePair<TKey, TValue> {

        internal KeyValuePair() {
        }

        [ScriptField]
        public TKey Key {
            get {
                return default(TKey);
            }
        }

        [ScriptField]
        public TValue Value {
            get {
                return default(TValue);
            }
        }
    }
}
// List.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    // NOTE: Keep in sync with ArrayList and Array

    /// <summary>
    /// Equivalent to the Array type in Javascript.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Array"")]
    public sealed class List<T> : ICollection<T> {

        public List() {
        }

        public List(int capacity) {
        }

        public List(params T[] items) {
        }

        [ScriptField]
        [ScriptName(""length"")]
        public int Count {
            get {
                return 0;
            }
        }

        [ScriptField]
        public T this[int index] {
            get {
                return default(T);
            }
            set {
            }
        }

        [ScriptName(""push"")]
        public void Add(T item) {
        }

        [ScriptName(""push"")]
        public void AddRange(params T[] items) {
        }

        public void Clear() {
        }

        public List<T> Concat(params T[] objects) {
            return null;
        }

        public bool Contains(object item) {
            return false;
        }

        public bool Every(ListFilterCallback<T> filterCallback) {
            return false;
        }

        public bool Every(ListItemFilterCallback<T> itemFilterCallback) {
            return false;
        }

        public List<T> Filter(ListFilterCallback<T> filterCallback) {
            return null;
        }

        public List<T> Filter(ListItemFilterCallback<T> itemFilterCallback) {
            return null;
        }

        public void ForEach(ListCallback<T> callback) {
        }

        public void ForEach(ListItemCallback<T> itemCallback) {
        }

        public IEnumerator<T> GetEnumerator() {
            return null;
        }

        public List<T> GetRange(int index) {
            return null;
        }

        public List<T> GetRange(int index, int count) {
            return null;
        }

        public int IndexOf(T item) {
            return 0;
        }

        public int IndexOf(T item, int startIndex) {
            return 0;
        }

        public void Insert(int index, T item) {
        }

        public void InsertRange(int index, params T[] items) {
        }

        public string Join() {
            return null;
        }

        public string Join(string delimiter) {
            return null;
        }

        public int LastIndexOf(object item) {
            return 0;
        }

        public int LastIndexOf(object item, int fromIndex) {
            return 0;
        }

        public List<TTarget> Map<TTarget>(ListMapCallback<T, TTarget> mapCallback) {
            return null;
        }

        public List<TTarget> Map<TTarget>(ListItemMapCallback<T, TTarget> mapItemCallback) {
            return null;
        }

        public static List<T> Parse(string s) {
            return null;
        }

        public TReduced Reduce<TReduced>(ListReduceCallback<TReduced, T> callback) {
            return default(TReduced);
        }

        public TReduced Reduce<TReduced>(ListReduceCallback<TReduced, T> callback, TReduced initialValue) {
            return default(TReduced);
        }

        public TReduced Reduce<TReduced>(ListItemReduceCallback<TReduced, T> callback) {
            return default(TReduced);
        }

        public TReduced Reduce<TReduced>(ListItemReduceCallback<TReduced, T> callback, TReduced initialValue) {
            return default(TReduced);
        }

        public TReduced ReduceRight<TReduced>(ListReduceCallback<TReduced, T> callback) {
            return default(TReduced);
        }

        public TReduced ReduceRight<TReduced>(ListReduceCallback<TReduced, T> callback, TReduced initialValue) {
            return default(TReduced);
        }

        public TReduced ReduceRight<TReduced>(ListItemReduceCallback<TReduced, T> callback) {
            return default(TReduced);
        }

        public TReduced ReduceRight<TReduced>(ListItemReduceCallback<TReduced, T> callback, TReduced initialValue) {
            return default(TReduced);
        }

        [ScriptAlias(""ss.remove"")]
        public bool Remove(T item) {
            return false;
        }

        public void RemoveAt(int index) {
        }

        public List<T> RemoveRange(int index, int count) {
            return null;
        }

        public void Reverse() {
        }

        public List<T> Slice(int start) {
            return null;
        }

        public List<T> Slice(int start, int end) {
            return null;
        }

        public bool Some(ListFilterCallback<T> filterCallback) {
            return false;
        }

        public bool Some(ListItemFilterCallback<T> itemFilterCallback) {
            return false;
        }

        public void Sort() {
        }

        public void Sort(CompareCallback<T> compareCallback) {
        }

        public void Splice(int start, int deleteCount) {
        }

        public void Splice(int start, int deleteCount, params T[] itemsToInsert) {
        }

        public void Unshift(params T[] items) {
        }

        public static explicit operator Array(List<T> list) {
            return null;
        }
    }
}
// ListCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void ListCallback<T>(T value, int index, IReadonlyCollection<T> list);
}
// ListFilterCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate bool ListFilterCallback<T>(T value, int index, IReadonlyCollection<T> list);
}
// ListItemCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void ListItemCallback<T>(T value);
}
// ListItemFilterCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate bool ListItemFilterCallback<T>(T value);
}
// ListItemMapCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TTarget ListItemMapCallback<TSource, TTarget>(TSource value);
}
// ListItemReduceCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TReduced ListItemReduceCallback<TReduced, TValue>(TReduced previousValue, TValue value);
}
// ListMapCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TTarget ListMapCallback<TSource, TTarget>(TSource value, int index, IReadonlyCollection<TSource> list);
}
// ListReduceCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate TReduced ListReduceCallback<TReduced, TValue>(TReduced previousValue, TValue value, int index, List<TValue> list);
}
// ObservableCollection.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""ObservableCollection"")]
    public sealed class ObservableCollection<T> : IEnumerable<T> {

        public ObservableCollection() {
        }

        public ObservableCollection(T[] items) {
        }

        [ScriptName(""length"")]
        public int Count {
            get {
                return 0;
            }
        }

        public T this[int index] {
            get {
                return default(T);
            }
            set {
            }
        }

        public void Add(T item) {
        }

        public void Clear() {
        }

        public bool Contains(T item) {
            return false;
        }

        public IEnumerator<T> GetEnumerator() {
            return null;
        }

        public int IndexOf(T item) {
            return 0;
        }

        public void Insert(int index, T item) {
        }

        public bool Remove(T item) {
            return false;
        }

        public void RemoveAt(int index) {
        }

        public T[] ToArray() {
            return null;
        }
    }
}
// Queue.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""Queue"")]
    public sealed class Queue<T> {

        [ScriptField]
        public int Count {
            get {
                return 0;
            }
        }

        public void Clear() {
        }

        public bool Contains(T item) {
            return false;
        }

        public T Dequeue() {
            return default(T);
        }

        public void Enqueue(T item) {
        }

        public T Peek() {
            return default(T);
        }
    }
}
// Stack.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Collections.Generic {

    [ScriptImport]
    [ScriptName(""Stack"")]
    public sealed class Stack<T> {

        [ScriptField]
        public int Count {
            get {
                return 0;
            }
        }

        public void Clear() {
        }

        public bool Contains(T item) {
            return false;
        }

        public T Peek() {
            return default(T);
        }

        public T Pop() {
            return default(T);
        }

        public void Push(T item) {
        }
    }
}
// DependencyAttribute.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    /// <summary>
    /// This attribute can be placed on a property, or constructor parameter.
    /// When placed on a property or parameter this can be used to mark a dependency
    /// and whether it is optional or not.
    /// </summary>
    [ScriptImport]
    [ScriptIgnore]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class DependencyAttribute : Attribute {

        private bool _optional;

        /// <summary>
        /// Gets or sets whether the dependency is optional.
        /// </summary>
        public bool Optional {
            get {
                return _optional;
            }
            set {
                _optional = value;
            }
        }
    }
}
// IApplication.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    /// <summary>
    /// Defines contextual information about the current application.
    /// </summary>
    [ScriptImport]
    public interface IApplication {

        /// <summary>
        /// Gets the value of the specified setting value.
        /// </summary>
        /// <param name=""name"">The name of the setting.</param>
        /// <returns>The value of the setting if it is available.</returns>
        string GetSetting(string name);
    }
}
// IContainer.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    /// <summary>
    /// Encapsulates the functionality of a container that defines a scope of
    /// composition where objects can be registered and dependencies can be resolved.
    /// </summary>
    [ScriptImport]
    public interface IContainer {

        /// <summary>
        /// Gets an instance of an object for the specified object type.
        /// </summary>
        /// <param name=""objectType"">The type of object to retrieve.</param>
        /// <returns>The resulting object; null if the object could not be retrieved.</returns>
        object GetObject(Type objectType);

        /// <summary>
        /// Registers an object instance for the specified type with the container.
        /// </summary>
        /// <param name=""objectType"">The type of object this instance corresponds to.</param>
        /// <param name=""objectInstance"">The object to register.</param>
        void RegisterObject(Type objectType, object objectInstance);

        /// <summary>
        /// Registers an object factory for the specified type with the container.
        /// </summary>
        /// <param name=""objectType"">The type of object this factory corresponds to.</param>
        /// <param name=""objectFactory"">The factory to register.</param>
        void RegisterFactory(Type objectType, Func<IContainer, Type, object> objectFactory);
    }
}
// IEventManager.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    /// <summary>
    /// Provides a simple pub/sub mechanism to allow objects to broadcast and
    /// listen to messages or events without being coupled to each other.
    /// </summary>
    [ScriptImport]
    public interface IEventManager {

        /// <summary>
        /// Broadcasts an event. The event is sequentially handled by all subscribers.
        /// Any errors that occur are ignored.
        /// </summary>
        /// <param name=""eventArgs"">The data associated with the event.</param>
        void PublishEvent(EventArgs eventArgs);

        /// <summary>
        /// Subscribes the specified handler to listen to events of the specified type.
        /// </summary>
        /// <param name=""eventType"">The type of the event to listen to.</param>
        /// <param name=""eventHandler"">The event handler to be invoked when the event occurs.</param>
        /// <returns>An opaque cookie that can be used to unsubscribe subsequently.</returns>
        object SubscribeEvent(Type eventType, Callback eventHandler);

        /// <summary>
        /// Unsubscribes a previous event handler from subsequent events.
        /// </summary>
        /// <param name=""subscriptionCookie"">The cookie that represents the subscription.</param>
        void UnsubscribeEvent(object subscriptionCookie);
    }
}
// IInitializable.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    /// <summary>
    /// Implemented by objects that supports a simple, transacted notification for batch
    /// initialization. 
    /// </summary>
    [ScriptImport]
    public interface IInitializable {

        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        /// <param name=""options"">An optional set of name/value pairs containing initialization data.</param>
        void BeginInitialization(Dictionary<string, object> options);

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        void EndInitialization();
    }
}
// IObserver.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    [ScriptImport]
    [ScriptName(""IObserver"")]
    public interface IObserver {

        void InvalidateObserver();
    }
}
// Observable.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    [ScriptImport]
    [ScriptName(""Observable"")]
    public sealed class Observable<T> {

        public Observable() {
        }

        public Observable(T value) {
        }

        public T GetValue() {
            return default(T);
        }

        public void SetValue(T value) {
        }
    }
}
// ObserverManager.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel {

    [ScriptImport]
    [ScriptName(""Observable"")]
    public static class ObserverManager {

        public static IDisposable RegisterObserver(IObserver observer) {
            return null;
        }
    }
}
// ConditionalAttribute.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Diagnostics {

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class ConditionalAttribute : Attribute {

        private string _conditionString;

        public ConditionalAttribute(string conditionString) {
            _conditionString = conditionString;
        }

        public string ConditionString {
            get {
                return _conditionString;
            }
        }
    }
}
// Debug.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    [ScriptName(""console"")]
    public static class Debug {

        [Conditional(""DEBUG"")]
        public static void Assert(bool condition) {
        }

        [Conditional(""DEBUG"")]
        public static void Assert(bool condition, string message) {
        }

        [Conditional(""DEBUG"")]
        [ScriptAlias(""ss.fail"")]
        public static void Fail(string message) {
        }

        [Conditional(""DEBUG"")]
        [ScriptName(""log"")]
        public static void WriteLine(string message) {
        }
    }
}
// SuppressMessageAttribute.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis {

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class SuppressMessageAttribute : Attribute {

        private string _category;
        private string _checkId;
        private string _scope;
        private string _target;
        private string _messageId;
        private string _justification;

        public SuppressMessageAttribute(string category, string checkId) {
            _category = category;
            _checkId = checkId;
        }

        public string Category {
            get {
                return _category;
            }
        }

        public string CheckId {
            get {
                return _checkId;
            }
        }

        public string Justification {
            get {
                return _justification;
            }
            set {
                _justification = value;
            }
        }

        public string Scope {
            get {
                return _scope;
            }
            set {
                _scope = value;
            }
        }

        public string Target {
            get {
                return _target;
            }
            set {
                _target = value;
            }
        }

        public string MessageId {
            get {
                return _messageId;
            }
            set {
                _messageId = value;
            }
        }
    }
}
// SyntaxValidationAttribute.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis {

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    [ScriptIgnore]
    [ScriptImport]
    public sealed class SyntaxValidationAttribute : Attribute {

        private string _syntax;

        public SyntaxValidationAttribute(string syntax) {
            _syntax = syntax;
        }

        public string Syntax {
            get {
                return _syntax;
            }
        }
    }
}
// CultureInfo.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Globalization {

    [ScriptImport]
    [ScriptName(""culture"")]
    public sealed class CultureInfo {

        private CultureInfo() {
        }

        [ScriptField]
        [ScriptName(""current"")]
        public static CultureInfo CurrentCulture {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""dtf"")]
        public DateFormatInfo DateFormat {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""neutral"")]
        public static CultureInfo InvariantCulture {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""nf"")]
        public NumberFormatInfo NumberFormat {
            get {
                return null;
            }
        }
    }
}
// DateFormatInfo.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Globalization {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DateFormatInfo {

        private DateFormatInfo() {
        }

        [ScriptField]
        [ScriptName(""am"")]
        public string AMDesignator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""pm"")]
        public string PMDesignator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""ds"")]
        public string DateSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""ts"")]
        public string TimeSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""gmt"")]
        public string GMTDateTimePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""uni"")]
        public string UniversalDateTimePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""sort"")]
        public string SortableDateTimePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""dt"")]
        public string DateTimePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""ld"")]
        public string LongDatePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""sd"")]
        public string ShortDatePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""lt"")]
        public string LongTimePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""st"")]
        public string ShortTimePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""day0"")]
        public int FirstDayOfWeek {
            get {
                return 0;
            }
        }

        [ScriptField]
        [ScriptName(""day"")]
        public string[] DayNames {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""sday"")]
        public string[] ShortDayNames {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""mday"")]
        public string[] MinimizedDayNames {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""mon"")]
        public string[] MonthNames {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""smon"")]
        public string[] ShortMonthNames {
            get {
                return null;
            }
        }
    }
}
// NumberFormatInfo.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Globalization {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class NumberFormatInfo {

        private NumberFormatInfo() {
        }

        [ScriptField]
        [ScriptName(""nan"")]
        public string NaNSymbol {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""neg"")]
        public string NegativeSign {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""pos"")]
        public string PositiveSign {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""negInf"")]
        public string NegativeInfinityText {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""posInf"")]
        public string PositiveInfinityText {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""per"")]
        public string PercentSymbol {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""perGW"")]
        public int[] PercentGroupSizes {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""perDD"")]
        public int PercentDecimalDigits {
            get {
                return 0;
            }
        }

        [ScriptField]
        [ScriptName(""perDS"")]
        public string PercentDecimalSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""perGS"")]
        public string PercentGroupSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""perNP"")]
        public string PercentNegativePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""perPP"")]
        public string PercentPositivePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""cur"")]
        public string CurrencySymbol {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""curGW"")]
        public int[] CurrencyGroupSizes {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""curDD"")]
        public int CurrencyDecimalDigits {
            get {
                return 0;
            }
        }

        [ScriptField]
        [ScriptName(""curDS"")]
        public string CurrencyDecimalSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""curGS"")]
        public string CurrencyGroupSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""curNP"")]
        public string CurrencyNegativePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""curPP"")]
        public string CurrencyPositivePattern {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""gw"")]
        public int[] NumberGroupSizes {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""dd"")]
        public int NumberDecimalDigits {
            get {
                return 0;
            }
        }

        [ScriptField]
        [ScriptName(""ds"")]
        public string NumberDecimalSeparator {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""gs"")]
        public string NumberGroupSeparator {
            get {
                return null;
            }
        }
    }
}
    ����          dMicrosoft.VisualStudio.CommonIDE, Version=11.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a   8Microsoft.VisualStudio.Build.ComInteropWrapper.RarInputs   allowedAssemblyExtensionsappConfigFilecandidateAssemblyFilesfullFrameworkAssemblyTablesfullFrameworkFoldersfullTargetFrameworkSubsetNames$ignoreDefaultInstalledAssemblyTables*ignoreDefaultInstalledAssemblySubsetTablesinstalledAssemblyTablesinstalledAssemblySubsetTables latestTargetFrameworkDirectoriespdtarSearchPathsgdtarSearchPathsprofileNameregistrySearchPath	stateFiletargetFrameworkDirectoriestargetFrameworkMoniker!targetFrameworkMonikerDisplayNametargetFrameworkVersiontargetProcessorArchitecturetargetedRuntimeVersiontargetFrameworkSubsets  �System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]�System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]�System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	   
	   	   	   	     	   		   	
   	   
       B{Registry:Software\Microsoft\.NETFramework,v2.0,AssemblyFoldersEx}   �C:\Users\L520\Documents\Visual Studio 2012\Projects\scriptsharp-cc_org\scriptsharp-cc\src\Core\CoreLib\obj\Debug\DesignTimeResolveAssemblyReferences.cache	      .NETFramework,Version=v2.0   .NETFramework v2.0   v2.0   msil   
v2.0.50727	            .winmd   .dll   .exe          �System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   _items_size_version  �System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]][]	                    .C:\Windows\Microsoft.NET\Framework\v2.0.50727\         Full      	           	      	           
                {CandidateAssemblyFiles}   {HintPathFromItem}   {TargetFrameworkDirectory}    B{Registry:Software\Microsoft\.NETFramework,v2.0,AssemblyFoldersEx}!   {RawFileName}""   `C:\Users\L520\Documents\Visual Studio 2012\Projects\scriptsharp-cc_org\scriptsharp-cc\bin\Debug\      #   .C:\Windows\Microsoft.NET\Framework\v2.0.50727\                  �System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]// AssemblyInfo.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle(""mscorlib"")]
[assembly: AssemblyDescription(""Script# Core Assembly"")]
[assembly: ScriptAssembly(""ss"")]
// Json.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Serialization {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    [ScriptName(""JSON"")]
    public static class Json {

        /// <summary>
        /// Parses the specified JSON text.
        /// </summary>
        /// <param name=""json"">The JSON text to be parsed.</param>
        /// <returns>The deserialized object.</returns>
        public static object Parse(string json) {
            return null;
        }

        /// <summary>
        /// Parses the specified JSON text.
        /// </summary>
        /// <param name=""json"">The JSON text to be parsed.</param>
        /// <returns>The deserialized object.</returns>
        [ScriptName(""parse"")]
        public static TData ParseData<TData>(string json) {
            return default(TData);
        }

        /// <summary>
        /// Parses the specified JSON text.
        /// </summary>
        /// <param name=""json"">The JSON text to be parsed.</param>
        /// <param name=""parseCallback"">A callback to invoke on each value that is deserialized.</param>
        /// <returns>The deserialized object.</returns>
        public static object Parse(string json, JsonParseCallback parseCallback) {
            return null;
        }

        /// <summary>
        /// Parses the specified JSON text.
        /// </summary>
        /// <param name=""json"">The JSON text to be parsed.</param>
        /// <param name=""parseCallback"">A callback to invoke on each value that is deserialized.</param>
        /// <returns>The deserialized object.</returns>
        [ScriptName(""parse"")]
        public static TData ParseData<TData>(string json, JsonParseCallback parseCallback) {
            return default(TData);
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o) {
            return null;
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <param name=""serializableMembers"">The specific members to serialize and their order.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o, string[] serializableMembers) {
            return null;
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <param name=""serializableMembers"">The specific members to serialize and their order.</param>
        /// <param name=""indentSpaces"">The number of spaces to use for indentation.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o, string[] serializableMembers, int indentSpaces) {
            return null;
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <param name=""serializableMembers"">The specific members to serialize and their order.</param>
        /// <param name=""indentText"">The string to use for indentation.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o, string[] serializableMembers, string indentText) {
            return null;
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <param name=""callback"">A callback to invoke for each value being serialized.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o, JsonStringifyCallback callback) {
            return null;
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <param name=""callback"">A callback to invoke for each value being serialized.</param>
        /// <param name=""indentSpaces"">The number of spaces to use for indentation.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o, JsonStringifyCallback callback, int indentSpaces) {
            return null;
        }

        /// <summary>
        /// Serializes the specified object into JSON representation.
        /// </summary>
        /// <param name=""o"">The object to serialize.</param>
        /// <param name=""callback"">A callback to invoke for each value being serialized.</param>
        /// <param name=""indentText"">The string to use for indentation.</param>
        /// <returns>The serialized value as JSON text.</returns>
        public static string Stringify(object o, JsonStringifyCallback callback, string indentText) {
            return null;
        }
    }
}
// JsonParseCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Serialization {

    /// <summary>
    /// A function that filters and transforms objects deserialized from JSON text.
    /// If the callback returns the same value, the member is left unmodified. If
    /// the callback returns null, the member is removed. Otherwise the new value
    /// returned from the callback is used instead.
    /// </summary>
    /// <param name=""name"">The name of the member.</param>
    /// <param name=""value"">The value of the member.</param>
    /// <returns>The transformed value.</returns>
    [ScriptImport]
    [ScriptIgnoreNamespace]
    public delegate object JsonParseCallback(string name, object value);
}
// JsonStringifyCallback.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Serialization {

    /// <summary>
    /// A function that filters and serializes objects being serialized into JSON text.
    /// If the callback returns undefined, the member is not serialized. Otherwise the new
    /// value returned from the callback is serialized instead.
    /// </summary>
    /// <param name=""name"">The name of the member.</param>
    /// <param name=""value"">The value of the member.</param>
    /// <returns>The value to be serialized.</returns>
    [ScriptImport]
    [ScriptIgnoreNamespace]
    public delegate object JsonStringifyCallback(string name, object value);
}
// Assert.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Testing {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public static class Assert {

        [ScriptAlias(""QUnit.equal"")]
        public static void AreEqual(object actual, object expected) {
        }

        [ScriptAlias(""QUnit.equal"")]
        public static void AreEqual(object actual, object expected, string message) {
        }

        [ScriptAlias(""QUnit.notEqual"")]
        public static void AreNotEqual(object actual, object expected) {
        }

        [ScriptAlias(""QUnit.notEqual"")]
        public static void AreNotEqual(object actual, object expected, string message) {
        }

        [ScriptAlias(""QUnit.expect"")]
        public static void ExpectAsserts(int assertions) {
        }

        [ScriptAlias(""QUnit.ok"")]
        public static void IsTrue(bool condition) {
        }

        [ScriptAlias(""QUnit.ok"")]
        public static void IsTrue(bool condition, string message) {
        }
    }
}
// TestClass.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Testing {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    public abstract class TestClass {

        public virtual void Cleanup() {
        }

        public virtual void Setup() {
        }
    }
}
// TestEngine.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Testing {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public static class TestEngine {

        [ScriptAlias(""QUnit.log"")]
        public static void Log(string message) {
        }

        [ScriptAlias(""QUnit.start"")]
        public static void ResumeOnAsyncCompleted() {
        }

        [ScriptAlias(""QUnit.triggerEvent"")]
        public static void TriggerEvent(object element, string eventName) {
        }

        [ScriptAlias(""QUnit.stop"")]
        public static void WaitForAsyncCompletion() {
        }

        [ScriptAlias(""QUnit.stop"")]
        public static void WaitForAsyncCompletion(int timeout) {
        }
    }
}
// Deferred.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Threading {

    [ScriptImport]
    public abstract class Deferred {

        internal Deferred() {
        }

        [ScriptField]
        public Task Task {
            get {
                return null;
            }
        }

        [ScriptAlias(""ss.deferred"")]
        public static Deferred Create() {
            return null;
        }

        [ScriptAlias(""ss.deferred"")]
        public static Deferred<T> Create<T>() {
            return null;
        }

        [ScriptAlias(""ss.deferred"")]
        public static Deferred<T> Create<T>(T result) {
            return null;
        }

        public void Reject() {
        }

        public void Reject(Exception error) {
        }

        public void Resolve() {
        }
    }

    [ScriptImport]
    [ScriptName(""Deferred"")]
    public sealed class Deferred<T> : Deferred {

        private Deferred() {
        }

        [ScriptField]
        public new Task<T> Task {
            get {
                return null;
            }
        }

        public void Resolve(T result) {
        }
    }
}
// Task.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Threading {

    [ScriptImport]
    public class Task {

        internal Task() {
        }

        public bool Completed {
            get {
                return false;
            }
        }

        [ScriptField]
        public Exception Error {
            get {
                return null;
            }
        }

        [ScriptField]
        public TaskStatus Status {
            get {
                return TaskStatus.Pending;
            }
        }

        public static Task<bool> All(params Task[] tasks) {
            return null;
        }

        public static Task<bool> All(int timeout, params Task[] tasks) {
            return null;
        }

        public static Task<Task> Any(params Task[] tasks) {
            return null;
        }

        public static Task<Task> Any(int timeout, params Task[] tasks) {
            return null;
        }

        public Task ContinueWith(Action<Task> continuation) {
            return null;
        }

        public static Task Delay(int timeout) {
            return null;
        }

        public Task Done(Action callback) {
            return null;
        }

        public Task Fail(Action<Exception> callback) {
            return null;
        }

        public Task Then(Action doneCallback, Action<Exception> failCallback) {
            return null;
        }
    }

    [ScriptImport]
    [ScriptName(""Task"")]
    public sealed class Task<T> : Task {

        internal Task() {
        }

        [ScriptField]
        public T Result {
            get {
                return default(T);
            }
        }

        public Task<T> ContinueWith(Action<Task<T>> continuation) {
            return null;
        }

        public Task<T> Done(Action<T> callback) {
            return null;
        }
    }
}
// TaskStatus.cs
// Script#/Libraries/CoreLib
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Threading {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    [ScriptConstants(UseNames = true)]
    public enum TaskStatus {

        Pending,

        Done,

        Failed
    }
}


        ";
    }
}
