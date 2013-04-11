// FileInputElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Html.Data.Files;
using System.Runtime.CompilerServices;
using SHDF = System.Html.Data.Files;

namespace System.Html {

    [SHDF.ScriptIgnoreNamespace]
    [SHDF.ScriptImport]
    public sealed class FileInputElement : InputElement {

        private FileInputElement() {
        }
   
        [ScriptField]
        public FileList Files {
            get {
                return null;
            }
        }
    }
}
