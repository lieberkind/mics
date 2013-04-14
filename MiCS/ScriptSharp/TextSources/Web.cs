using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.ScriptSharp.TextSources
{

    class Web
    {
        public static string Text = @"

// ActiveXObject.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ActiveXObject {

        public ActiveXObject(string progID) {
        }
    }
}
// AnchorElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class AnchorElement : Element {

        private AnchorElement() {
        }

        [ScriptField]
        public string Href {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Rel {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Target {
            get {
                return null;
            }
            set {
            }
        }
        
        [ScriptField]
        public string Download {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// AreaElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class AreaElement : Element {

        private AreaElement() {
        }

        [ScriptField]
        public string Shape {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Coords {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Alt {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Href {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string NoHref {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// AudioElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptImport]
    [ScriptIgnoreNamespace]
    public sealed class AudioElement : Element {

        private AudioElement() {
        }

        [ScriptField]
        public double CurrentTime {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public double Duration {
            get {
                return 0;
            }
        }

        [ScriptField]
        public bool Ended {
            get {
                return false;
            }
        }

        [ScriptField]
        public bool Paused {
            get {
                return false;
            }
        }

        [ScriptField]
        public string Src {
            get {
                return """";
            }
            set {
            }
        }

        [ScriptField]
        public float Volume {
            get {
                return 0;
            }
            set {
            }
        }

        public void Load() {
        }

        public void Pause() {
        }

        public void Play() {
        }
    }
}
// CanvasElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html.Media.Graphics;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class CanvasElement : Element {

        private CanvasElement() {
        }

        [ScriptField]
        public int Height {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int Width {
            get {
                return 0;
            }
            set {
            }
        }

        public CanvasContext GetContext(string contextID) {
            return null;
        }

        public CanvasContext GetContext(Rendering renderingType) {
            return null;
        }

        [ScriptName(""toDataURL"")]
        public string GetDataUrl() {
            return null;
        }

        [ScriptName(""toDataURL"")]
        public string GetDataUrl(string type) {
            return null;
        }

        [ScriptName(""toDataURL"")]
        public string GetDataUrl(string type, params object[] typeArguments) {
            return null;
        }
    }
}
// CheckBoxElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class CheckBoxElement : InputElement {

        internal CheckBoxElement() {
        }

        [ScriptField]
        public bool Checked {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public bool DefaultChecked {
            get {
                return false;
            }
        }
    }
}
// ClientRect.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ClientRect {

        private ClientRect() {
        }

        [ScriptField]
        public double Bottom {
            get {
                return 0;
            }
        }

        [ScriptField]
        public double Left {
            get {
                return 0;
            }
        }

        [ScriptField]
        public double Right {
            get {
                return 0;
            }
        }

        [ScriptField]
        public double Top {
            get {
                return 0;
            }
        }
    }
}
// ClientRectList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ClientRectList {

        private ClientRectList() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public ClientRect this[int index] {
            get {
                return null;
            }
        }
    }
}
// CustomEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class CustomEvent : ElementEvent {

        internal CustomEvent() {
        }

        [ScriptField]
        public object Data {
            get {
                return null;
            }
        }
    }
}
// DataFormat.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum DataFormat {

        Text = 0,

        URL = 1
    }
}
// DataTransfer.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DataTransfer {

        private DataTransfer() {
        }

        [ScriptField]
        public DropEffect DropEffect {
            get {
                return DropEffect.None;
            }
            set {
            }
        }

        [ScriptField]
        public DropEffects EffectAllowed {
            get {
                return DropEffects.None;
            }
            set {
            }
        }

        public void ClearData() {
        }

        public void ClearData(DataFormat format) {
        }

        public string GetData(DataFormat format) {
            return null;
        }

        public bool SetData(DataFormat format, string data) {
            return false;
        }
    }
}
// DivElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DivElement : Element {

        private DivElement() {
        }

        [ScriptField]
        public string Align {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public bool NoWrap {
            get {
                return false;
            }
            set {
            }
        }
    }
}
// Document.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html.StyleSheets;
using System.Html.Editing;
using MiCS;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""document"")]
    public static class Document {

        [ScriptField]
        public static Element ActiveElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Element Body {
            get {
                return null;
            }
        }

        [ScriptField]
        public static string Cookie {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public static string Doctype {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Element DocumentElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public static string DesignMode {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public static string Domain {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public static DocumentImplementation Implementation {
            get {
                return null;
            }
        }

        [ScriptField]
        public static WindowInstance ParentWindow {
            get {
                return null;
            }
        }

        [ScriptField]
        public static string ReadyState {
            get {
                return null;
            }
        }

        [ScriptField]
        public static string Referrer {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Selection Selection {
            get {
                return null;
            }
        }

        [ScriptField]
        public static StyleSheetList StyleSheets {
            get {
                return null;
            }
        }

        [ScriptField]
        public static string Title {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public static string URL {
            get {
                return null;
            }
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public static void AddEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public static void AddEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        public static void AttachEvent(string eventName, ElementEventHandler handler) {
        }

        public static ElementAttribute CreateAttribute(string name) {
            return null;
        }

        public static DocumentFragment CreateDocumentFragment() {
            return null;
        }

        public static Element CreateElement(string tagName) {
            return null;
        }

        public static MutableEvent CreateEvent(string eventType) {
            return null;
        }

        public static Element CreateTextNode(string data) {
            return null;
        }

        public static void DetachEvent(string eventName, ElementEventHandler handler) {
        }

        public static bool DispatchEvent(MutableEvent eventObject) {
            return false;
        }

        public static Element ElementFromPoint(int x, int y) {
            return null;
        }

        public static bool ExecCommand(string command, bool displayUserInterface, object value) {
            return false;
        }

        public static void Focus() {
        }

        public static Element GetElementById(string id) {
            return null;
        }

        public static TElement GetElementById<TElement>(string id) where TElement : Element {
            return null;
        }

        public static ElementCollection GetElementsByClassName(string className) {
            return null;
        }

        public static ElementCollection GetElementsByName(string name) {
            return null;
        }

        public static ElementCollection GetElementsByTagName(string tagName) {
            return null;
        }

        public static bool HasFocus() {
            return false;
        }

        public static bool QueryCommandEnabled(string command) {
            return false;
        }

        public static bool QueryCommandIndeterm(string command) {
            return false;
        }

        public static bool QueryCommandState(string command) {
            return false;
        }

        public static bool QueryCommandSupported(string command) {
            return false;
        }

        public static object QueryCommandValue(string command) {
            return null;
        }

        public static Element QuerySelector(string selector) {
            return null;
        }

        public static ElementCollection QuerySelectorAll(string selector) {
            return null;
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public static void RemoveEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public static void RemoveEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }
    }
}
// DOMDocumentFragment.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class DocumentFragment {

        protected internal DocumentFragment() {
        }

        [ScriptField]
        public ElementAttributeCollection Attributes {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementCollection ChildNodes {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element FirstChild {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element LastChild {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element NextSibling {
            get {
                return null;
            }
        }

        [ScriptField]
        public string NodeName {
            get {
                return null;
            }
        }

        [ScriptField]
        public int NodeType {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string NodeValue {
            get {
                return null;
            }
        }

        [ScriptField]
        public DocumentInstance OwnerDocument {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element ParentNode {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element PreviousSibling {
            get {
                return null;
            }
        }

        public Element AppendChild(Element child) {
            return null;
        }

        public DocumentFragment CloneNode() {
            return null;
        }

        public Element CloneNode(bool deep) {
            return null;
        }

        public bool Contains(Element element) {
            return false;
        }

        public Element GetElementById(string id) {
            return null;
        }

        public ElementCollection GetElementsByTagName(string tagName) {
            return null;
        }

        public bool HasAttributes() {
            return false;
        }

        public bool HasChildNodes() {
            return false;
        }

        public Element InsertBefore(Element newChild) {
            return null;
        }

        public Element InsertBefore(Element newChild, Element referenceChild) {
            return null;
        }

        public Element RemoveChild(Element child) {
            return null;
        }

        public Element ReplaceChild(Element newChild, Element oldChild) {
            return null;
        }
    }
}
// DocumentImplementation.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DocumentImplementation {

        internal DocumentImplementation() {
        }

        public bool HasFeature(string feature) {
            return false;
        }

        public bool HasFeature(string feature, string version) {
            return false;
        }
    }
}
// DocumentInstance.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html.Editing;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DocumentInstance {

        private DocumentInstance() {
        }

        [ScriptField]
        public Element ActiveElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element Body {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Cookie {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string DesignMode {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Doctype {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element DocumentElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Domain {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public DocumentImplementation Implementation {
            get {
                return null;
            }
        }

        [ScriptField]
        public WindowInstance ParentWindow {
            get {
                return null;
            }
        }

        [ScriptField]
        public string ReadyState {
            get {
                return null;
            }
        }
        
        [ScriptField]
        public string Referrer {
            get {
                return null;
            }
        }

        [ScriptField]
        public Selection Selection {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Title {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string URL {
            get {
                return null;
            }
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public void AddEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void AddEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        public void AttachEvent(string eventName, ElementEventHandler handler) {
        }

        public void Close() {
        }

        public ElementAttribute CreateAttribute(string name) {
            return null;
        }

        public DocumentFragment CreateDocumentFragment() {
            return null;
        }

        public Element CreateElement(string tagName) {
            return null;
        }

        public MutableEvent CreateEvent(string eventType) {
            return null;
        }

        public Element CreateTextNode(string tagName) {
            return null;
        }

        public void DetachEvent(string eventName, ElementEventHandler handler) {
        }

        public bool DispatchEvent(MutableEvent eventObject) {
            return false;
        }

        public Element ElementFromPoint(int x, int y) {
            return null;
        }

        public bool ExecCommand(string command, bool displayUserInterface, object value) {
            return false;
        }

        public void Focus() {
        }

        public Element GetElementById(string id) {
            return null;
        }

        public TElement GetElementById<TElement>(string id) where TElement : Element {
            return null;
        }

        public ElementCollection GetElementsByClassName(string className) {
            return null;
        }

        public ElementCollection GetElementsByName(string name) {
            return null;
        }

        public ElementCollection GetElementsByTagName(string tagName) {
            return null;
        }

        public bool HasFocus() {
            return false;
        }

        public void Open() {
        }

        public bool QueryCommandEnabled(string command) {
            return false;
        }

        public bool QueryCommandIndeterm(string command) {
            return false;
        }

        public bool QueryCommandState(string command) {
            return false;
        }

        public bool QueryCommandSupported(string command) {
            return false;
        }

        public object QueryCommandValue(string command) {
            return null;
        }

        public Element QuerySelector(string selector) {
            return null;
        }

        public ElementCollection QuerySelectorAll(string selector) {
            return null;
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public void RemoveEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void RemoveEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        public void Write(string text) {
        }

        public void Writeln(string text) {
        }
    }
}
// DropEffect.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum DropEffect {

        Copy = 0,

        Link = 1,

        Move = 2,

        None = 3
    }
}
// DropEffects.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum DropEffects {

        Copy = 0,

        Link = 1,

        Move = 2,

        CopyLink = 3,

        CopyMove = 4,

        LinkMove = 5,

        All = 6,

        None = 7
    }
}
// Element.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html.Media.Filters;
using MiCS;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public class Element {

        protected internal Element() {
        }

        [ScriptField]
        public string AccessKey {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public ElementAttributeCollection Attributes {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementCollection ChildNodes {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementCollection Children {
            get {
                return null;
            }
        }

        [ScriptField]
        public string ClassName {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public TokenList ClassList {
            get {
                return null;
            }
        }

        [ScriptField]
        public int ClientHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ClientLeft {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ClientTop {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ClientWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Style CurrentStyle {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Dir {
            get {
                return null;
            }
            set {
            }
        }

        // TODO: Is this on Element or just some types of elements?
        [ScriptField]
        public bool Disabled {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public VisualFilterCollection Filters {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element FirstChild {
            get {
                return null;
            }
        }

        [ScriptField]
        public string ID {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string InnerHTML {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string InnerText {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public Element LastChild {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element NextSibling {
            get {
                return null;
            }
        }

        [ScriptField]
        public string NodeName {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementType NodeType {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string NodeValue {
            get {
                return null;
            }
        }

        [ScriptField]
        public int OffsetHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int OffsetLeft {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Element OffsetParent {
            get {
                return null;
            }
        }

        [ScriptField]
        public int OffsetTop {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int OffsetWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public DocumentInstance OwnerDocument {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element ParentNode {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element PreviousSibling {
            get {
                return null;
            }
        }

        [ScriptField]
        public Style RuntimeStyle {
            get {
                return null;
            }
        }

        [ScriptField]
        public int ScrollHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ScrollLeft {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int ScrollTop {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int ScrollWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Style Style {
            get {
                return null;
            }
        }

        [ScriptField]
        public int TabIndex {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public string TagName {
            get {
                return null;
            }
        }

        [ScriptField]
        public string TextContent {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Title {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void AddEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""handler"">The handler to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the handler wants to initiate capturing the event.</param>
        public void AddEventListener(string eventName, IElementEventHandler handler, bool useCapture) {
        }

        public Element AppendChild(Element child) {
            return null;
        }

        public Element AppendChild(DocumentFragment child) {
            return null;
        }

        [ScriptSkip]
        public TElement As<TElement>() where TElement : Element {
            return null;
        }

        public void AttachEvent(string eventName, ElementEventHandler handler) {
        }

        public void Blur() {
        }

        public void Click() {
        }

        public Element CloneNode() {
            return null;
        }

        public Element CloneNode(bool deep) {
            return null;
        }

        public bool Contains(Element element) {
            return false;
        }

        public void DetachEvent(string eventName, ElementEventHandler handler) {
        }

        public bool DispatchEvent(MutableEvent eventObject) {
            return false;
        }

        public bool DragDrop() {
            return false;
        }

        public void Focus() {
        }

        public object GetAttribute(string name) {
            return null;
        }

        public object GetAttribute(ElementAttributeName name) {
            return null;
        }

        public ElementAttribute GetAttributeNode(string name) {
            return null;
        }

        public ElementAttribute GetAttributeNode(ElementAttributeName name) {
            return null;
        }

        public ClientRectList GetClientRects() {
            return null;
        }

        public ElementCollection GetElementsByClassName(string className) {
            return null;
        }

        public ElementCollection GetElementsByTagName(string tagName) {
            return null;
        }

        public bool HasAttribute(string name) {
            return false;
        }

        public bool HasAttribute(ElementAttributeName name) {
            return false;
        }

        public bool HasChildNodes() {
            return false;
        }

        public Element InsertBefore(Element newChild) {
            return null;
        }

        public Element InsertBefore(Element newChild, Element referenceChild) {
            return null;
        }

        public Element QuerySelector(string selector) {
            return null;
        }

        public ElementCollection QuerySelectorAll(string selector) {
            return null;
        }

        public bool RemoveAttribute(string name) {
            return false;
        }

        public bool RemoveAttribute(ElementAttributeName name) {
            return false;
        }

        public ElementAttribute RemoveAttributeNode(ElementAttribute attribute) {
            return null;
        }

        public Element RemoveChild(Element child) {
            return null;
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void RemoveEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""handler"">The handler to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the handler wants to initiate capturing the event.</param>
        public void RemoveEventListener(string eventName, IElementEventHandler handler, bool useCapture) {
        }

        public Element ReplaceChild(Element newChild, Element oldChild) {
            return null;
        }

        public void ScrollIntoView() {
        }

        public void ScrollIntoView(bool alignTop) {
        }

        public void SetActive() {
        }

        public void SetAttribute(string name, object value) {
        }

        public void SetAttribute(ElementAttributeName name, object value) {
        }

        public ElementAttribute SetAttributeNode(ElementAttribute attribute) {
            return null;
        }
    }
}
// ElementAttribute.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ElementAttribute {

        internal ElementAttribute() {
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
        }

        [ScriptField]
        public bool Specified {
            get {
                return false;
            }
        }

        [ScriptField]
        public string Value {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// ElementAttributeCollection.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ElementAttributeCollection {

        internal ElementAttributeCollection() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public ElementAttribute this[int index] {
            get {
                return null;
            }
        }

        public ElementAttribute GetNamedItem(string name) {
            return null;
        }

        public ElementAttribute RemoveNamedItem(string name) {
            return null;
        }

        public ElementAttribute SetNamedItem(ElementAttribute attribute) {
            return null;
        }
    }
}
// ElementAttributeName.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    // TODO: Complete this enumeration

    [ScriptIgnoreNamespace]
    [ScriptConstants(UseNames = true)]
    [ScriptImport]
    public enum ElementAttributeName {

        Autocomplete = 0,

        ReadyState = 0,

        Src = 0,

        Unselectable = 0
    }
}
// ElementCollection.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ElementCollection {

        internal ElementCollection() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Element this[int index] {
            get {
                return null;
            }
        }
    }
}
// ElementEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class ElementEvent {

        internal ElementEvent() {
        }

        [ScriptField]
        public bool AltKey {
            get {
                return false;
            }
        }

        [ScriptField]
        public int Button {
            get {
                return 0;
            }
        }

        [ScriptField]
        public bool CancelBubble {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public bool CtrlKey {
            get {
                return false;
            }
        }

        [ScriptField]
        public Element CurrentTarget {
            get {
                return null;
            }
        }

        [ScriptField]
        public DataTransfer DataTransfer {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Detail {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element FromElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public int KeyCode {
            get {
                return 0;
            }
        }

        [ScriptField]
        public bool MetaKey {
            get {
                return false;
            }
        }

        [ScriptField]
        public int OffsetX {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int OffsetY {
            get {
                return 0;
            }
        }

        [ScriptField]
        public bool ReturnValue {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public bool ShiftKey {
            get {
                return false;
            }
        }

        [ScriptField]
        public Element SrcElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element Target {
            get {
                return null;
            }
        }

        [ScriptField]
        public Date TimeStamp {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element ToElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Type {
            get {
                return null;
            }
        }

        public void PreventDefault() {
        }

        public void StopImmediatePropagation() {
        }

        public void StopPropagation() {
        }
    }
}
// ElementEventHandler.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void ElementEventHandler();
}
// ElementEventListener.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void ElementEventListener(ElementEvent e);
}
// ElementType.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum ElementType {

        Element = 1,

        Attribute = 2,

        Text = 3,

        CharacterData = 4,

        EntityReference = 5,

        Entity = 6,

        ProcessingInstruction = 7,

        Comment = 8,

        Document = 9,

        DocumentType = 10,

        DocumentFragment = 11,

        Notation = 12
    }
}
// ErrorHandler.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    /// <summary>
    /// Delegate for the window unhandled exception handler.
    /// </summary>
    /// <param name=""message"">The error message.</param>
    /// <param name=""url"">The URL of the script where the error occurred.</param>
    /// <param name=""line"">The line number where the error occurred.</param>
    /// <returns>true if the error was handled; false otherwise.</returns>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate bool ErrorHandler(string message, string url, int line);
}
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
// FormElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class FormElement : Element {

        internal FormElement() {
        }

        [ScriptField]
        public string AcceptCharset {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Action {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public Element[] Elements {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string EncType {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Encoding {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Method {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Target {
            get {
                return null;
            }
            set {
            }
        }

        public void Reset() {
        }

        public void Submit() {
        }
    }
}
// GestureEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class GestureEvent : ElementEvent {

        internal GestureEvent() {
        }

        [ScriptField]
        public double Rotation {
            get {
                return 0;
            }
        }

        [ScriptField]
        public double Scale {
            get {
                return 0;
            }
        }
    }
}
// History.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class History {

        private History() {
        }

        /// <summary>
        /// Retrieves the number of elements in the history list.
        /// </summary>
        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        /// <summary>
        /// Retrieves the current state object.
        /// </summary>
        [ScriptField]
        public object State {
            get {
                return null;
            }
        }

        /// <summary>
        /// Navigates to the previous page in history.
        /// </summary>
        public void Back() {
        }

        /// <summary>
        /// Navigates to the next page in history.
        /// </summary>
        public void Forward() {
        }

        /// <summary>
        /// Navigates to a page in history relative to the current page.
        /// </summary>
        /// <param name=""delta"">The number of pages in history to go back (negative) or forward (positive).</param>
        public void Go(int delta) {
        }

        /// <summary>
        /// Pushes the given data onto the session history stack.
        /// </summary>
        /// <param name=""data"">The data to serialize into JSON format.</param>
        /// <param name=""title"">The title to place into history.</param>
        public void PushState(object data, string title) {
        }

        /// <summary>
        /// Pushes the given data onto the session history stack.
        /// </summary>
        /// <param name=""data"">The data to serialize into JSON format.</param>
        /// <param name=""title"">The title to place into history.</param>
        /// <param name=""url"">The URL to place into history.</param>
        public void PushState(object data, string title, string url) {
        }

        /// <summary>
        /// Updates the most recent entry on the history stack.
        /// </summary>
        /// <param name=""data"">The data to serialize into JSON format.</param>
        /// <param name=""title"">The title to place into history.</param>
        public void ReplaceState(object data, string title) {
        }

        /// <summary>
        /// Updates the most recent entry on the history stack.
        /// </summary>
        /// <param name=""data"">The data to serialize into JSON format.</param>
        /// <param name=""title"">The title to place into history.</param>
        /// <param name=""url"">The URL to place into history.</param>
        public void ReplaceState(object data, string title, string url) {
        }
    }
}
// IElementEventHandler.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    public interface IElementEventHandler {

        void HandleEvent(ElementEvent e);
    }
}
// IFrameElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class IFrameElement : Element {

        private IFrameElement() {
        }

        [ScriptField]
        public WindowInstance ContentWindow {
            get {
                return null;
            }
        }

        [ScriptField]
        public string FrameBorder {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Scrolling {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Src {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// ImageElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Image"")]
    public sealed class ImageElement : Element {

        public ImageElement() {
        }

        public ImageElement(int width) {
        }

        public ImageElement(int width, int height) {
        }

        [ScriptField]
        public string Alt {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public bool Complete {
            get {
                return false;
            }
        }

        [ScriptField]
        public string Src {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public int Height {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int NaturalHeight {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int NaturalWidth {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int Width {
            get {
                return 0;
            }
            set {
            }
        }
    }
}
// InputElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class InputElement : Element {

        protected internal InputElement() {
        }

        [ScriptField]
        public string DefaultValue {
            get {
                return null;
            }
        }

        [ScriptField]
        public FormElement Form {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Type {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Value {
            get {
                return null;
            }
            set {
            }
        }

        public void Select() {
        }
    }
}
// Location.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Location {

        private Location() {
        }

        [ScriptField]
        public string Hash {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Hostname {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""host"")]
        public string HostnameAndPort {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Href {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Pathname {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Port {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Protocol {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Search {
            get {
                return null;
            }
        }

        /// <summary>
        /// Navigates the window to a new location and updates the browser's history.
        /// </summary>
        /// <param name=""url"">The URL to navigate to.</param>
        public void Assign(string url) {
        }

        /// <summary>
        /// Reload the current document.
        /// </summary>
        public void Reload() {
        }

        /// <summary>
        /// Reload the current document.
        /// </summary>
        /// <param name=""forceGet"">If true, the document will be reloaded from the server, otherwise it may be loaded from the browser's cache.</param>
        public void Reload(bool forceGet) {
        }

        /// <summary>
        /// Navigates the window to a new location without updating the browser's history.
        /// </summary>
        /// <param name=""url"">The URL to navigate to.</param>
        public void Replace(string url) {
        }
    }
}
// MapElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class MapElement : Element {

        private MapElement() {
        }

        [ScriptField]
        public string Name {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// MessageEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class MessageEvent : ElementEvent {

        internal MessageEvent() {
        }

        [ScriptField]
        public string Data {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Origin {
            get {
                return null;
            }
        }

        [ScriptField]
        public WindowInstance Source {
            get {
                return null;
            }
        }
    }
}
// MutableEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class MutableEvent {

        internal MutableEvent() {
        }

        public void InitCustomEvent(string eventType, bool canBubble, bool canCancel, string detail) {
        }

        public void InitEvent(string eventType, bool canBubble, bool canCancel) {
        }

        public void InitFocusEvent(string eventType, bool canBubble, bool canCancel, WindowInstance view, string detail) {
        }

        public void InitKeyboardEvent(string eventType, bool canBubble, bool canCancel, WindowInstance view, string detail, string key, string location, string modifiers, int repeat, string locale) {
        }

        public void InitMouseEvent(string eventType, bool canBubble, bool canCancel, WindowInstance view, string detail, int screenX, int screenY, int clientX, int clientY, bool ctrlKey, bool altKey, bool shiftKey, bool metaKey, string button, Element relatedTarget) {
        }

        public void InitMouseWheelEvent(string eventType, bool canBubble, bool canCancel, WindowInstance view, string detail, int screenX, int screenY, int clientX, int clientY, bool ctrlKey, bool altKey, bool shiftKey, bool metaKey, string button, Element relatedTarget, string modifiers, int wheelDelta) {
        }

        public void InitUIEvent(string eventType, bool canBubble, bool canCancel, WindowInstance view, string detail) {
        }

        public void InitWheelEvent(string eventType, bool canBubble, bool canCancel, WindowInstance view, string detail, int screenX, int screenY, int clientX, int clientY, bool ctrlKey, bool altKey, bool shiftKey, bool metaKey, string button, Element relatedTarget, string modifiers, int deltaX, int deltaY, int deltaZ, int deltaMode) {
        }
    }
}
// Navigator.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Html.Services;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Navigator {

        private Navigator() {
        }

        /// <summary>
        /// Returns the name of the browser.
        /// </summary>
        [ScriptField]
        public string AppName {
            get {
                return null;
            }
        }

        /// <summary>
        /// Returns the version of the browser.
        /// </summary>
        [ScriptField]
        public string AppVersion {
            get {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the current language (applies to IE and Opera).
        /// </summary>
        [ScriptField]
        public string BrowserLanguage {
            get {
                return null;
            }
        }

        [ScriptField]
        public bool CookieEnabled {
            get {
                return false;
            }
        }

        /// <summary>
        /// Returns a string representing the language of the browser (applies to Gecko, Opera, and WebKit).
        /// </summary>
        [ScriptField]
        public string Language {
            get {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the default language used by the operating system (applies to IE).
        /// </summary>
        [ScriptField]
        public string SystemLanguage {
            get {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the operating system's natural language setting (applies to IE and Opera).
        /// </summary>
        [ScriptField]
        public string UserLanguage {
            get {
                return null;
            }
        }

        [ScriptField]
        public GeolocationService Geolocation {
            get {
                return null;
            }
        }

        [ScriptField]
        public bool OnLine {
            get {
                return false;
            }
        }

        /// <summary>
        /// Returns the name of the platform.
        /// </summary>
        [ScriptField]
        public string Platform {
            get {
                return null;
            }
        }

        /// <summary>
        /// Returns a PluginArray object, listing the plugins installed in the application.
        /// </summary>
        [ScriptField]
        public PluginArray Plugins {
            get {
                return null;
            }
        }

        [ScriptField]
        public bool Standalone {
            get {
                return false;
            }
        }

        /// <summary>
        /// Returns the complete User-Agent header.
        /// </summary>
        [ScriptField]
        public string UserAgent {
            get {
                return null;
            }
        }
    }
}
// OptionElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class OptionElement : Element {

        private OptionElement() {
        }

        [ScriptField]
        public FormElement Form {
            get {
                return null;
            }
        }

        [ScriptField]
        public bool Selected {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public string Text {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Value {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// Orientation.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum Orientation {

        Portrait = 0,

        LeftLandscape = 90,

        RightLandscape = -90,

        UpsideDownPortrait = 180
    }
}
// Plugin.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Microsoft
// Public License. A copy of the license can be found in License.txt.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class Plugin {

        private Plugin() {
        }

        /// <summary>
        /// A human readable description of the plugin.
        /// </summary>
        [ScriptField]
        public string Description {
            get {
                return null;
            }
        }

        /// <summary>
        /// The filename of the plugin file.
        /// </summary>
        [ScriptField]
        [ScriptName(""filename"")]
        public string FileName {
            get {
                return null;
            }
        }

        /// <summary>
        /// The name of the plugin.
        /// </summary>
        [ScriptField]
        public string Name {
            get {
                return null;
            }
        }

        /// <summary>
        /// The plugin's version number string.
        /// </summary>
        [ScriptField]
        public string Version {
            get {
                return null;
            }
        }
    }
}
// PluginArray.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Microsoft
// Public License. A copy of the license can be found in License.txt.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class PluginArray {

        private PluginArray() {
        }

        /// <summary>
        /// The number of plugins in the array.
        /// </summary>
        [ScriptField]
        public long Length {
            get {
                return 0;
            }
        }

        /// <summary>
        /// Returns the Plugin at the specified index into the array.
        /// </summary>
        [ScriptField]
        public Plugin this[int index] {
            get {
                return null;
            }
        }

        /// <summary>
        /// Returns the Plugin with the specified name.
        /// </summary>
        /// <param name=""name""></param>
        /// <returns></returns>
        [ScriptField]
        public Plugin this[string name] {
            get {
                return null;
            }
        }

        /// <summary>
        /// Returns the Plugin at the specified index into the array.
        /// </summary>
        [ScriptName(""item"")]
        public Plugin ItemAt(int index) {
            return null;
        }

        /// <summary>
        /// Returns the Plugin with the specified name.
        /// </summary>
        public Plugin NamedItem(string name) {
            return null;
        }
    }
}
// Screen.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    /// <summary>
    /// The screen object represents information about the current desktop.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class Screen {

        [ScriptField]
        public int AvailHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int AvailWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ColorDepth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int Height {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int Width {
            get {
                return 0;
            }
        }
    }
}
// ScriptElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ScriptElement : Element {

        private ScriptElement() {
        }

        [ScriptField]
        public string Src {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Type {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string ReadyState {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// SelectElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SelectElement : InputElement {

        private SelectElement() {
        }

        [ScriptField]
        public bool Multiple {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public ElementCollection Options {
            get {
                return null;
            }
        }

        [ScriptField]
        public int SelectedIndex {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int Size {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>
        /// Adds an element to the end of the <see cref=""Options""/> collection.
        /// </summary>
        /// <param name=""option"">Element to add to the <see cref=""Options""/> collection.</param>
        public void Add(OptionElement option) {
        }

        /// <summary>
        /// Adds an element to the <see cref=""Options""/> collection (IE only).
        /// </summary>
        /// <param name=""option"">Element to add to the <see cref=""Options""/> collection.</param>
        /// <param name=""index"">Specifies the index position in the collection where the element is placed.</param>
        public void Add(OptionElement option, int index) {
        }

        /// <summary>
        /// Adds an element to the <see cref=""Options""/> collection (Firefox only).
        /// </summary>
        /// <param name=""option"">Element to add to the <see cref=""Options""/> collection.</param>
        /// <param name=""before"">The element before which to add <paramref name=""option""/>.</param>
        public void Add(OptionElement option, OptionElement before) {
        }

        /// <summary>
        /// Removes an <see cref=""OptionElement""/> from the <see cref=""Options""/> collection.
        /// </summary>
        /// <param name=""index""></param>
        public void Remove(int index) {
        }
    }
}
// Style.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Style {

        private Style() {
        }

        /// <summary>Whether the object contains an accelerator key.</summary>
        [ScriptField]
        public bool Accelerator {
            get {
                return false;
            }
            set {
            }
        }

        /// <summary>The background properties of an object.</summary>
        [ScriptField]
        public string Background {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>How the background image is attached to the object within the document.</summary>
        [ScriptField]
        public string BackgroundAttachment {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The color behind the content of the object.</summary>
        [ScriptField]
        public string BackgroundColor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The background image of the object.</summary>
        [ScriptField]
        public string BackgroundImage {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The position of the background of the object.</summary>
        [ScriptField]
        public string BackgroundPosition {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The x-coordinate of the backgroundPosition property.</summary>
        [ScriptField]
        public string BackgroundPositionX {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The y-coordinate of the backgroundPosition property.</summary>
        [ScriptField]
        public string BackgroundPositionY {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>How the background of the object is tiled.</summary>
        [ScriptField]
        public string BackgroundRepeat {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The properties to draw a border around the object.</summary>
        [ScriptField]
        public string Border {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The properties of the bottom border of the object.</summary>
        [ScriptField]
        public string BorderBottom {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The color of the bottom border of the object.</summary>
        [ScriptField]
        public string BorderBottomColor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The style of the bottom border of the object.</summary>
        [ScriptField]
        public string BorderBottomStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the bottom border of the object.</summary>
        [ScriptField]
        public string BorderBottomWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the row and cell borders of a table are joined in a single border or detached.</summary>
        [ScriptField]
        public string BorderCollapse {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The border color of the object.</summary>
        [ScriptField]
        public string BorderColor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The properties of the left border of the object.</summary>
        [ScriptField]
        public string BorderLeft {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The color of the left border of the object.</summary>
        [ScriptField]
        public string BorderLeftColor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The style of the left border of the object.</summary>
        [ScriptField]
        public string BorderLeftStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the left border of the object.</summary>
        [ScriptField]
        public string BorderLeftWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The properties of the right border of the object.</summary>
        [ScriptField]
        public string BorderRight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The color of the right border of the object.</summary>
        [ScriptField]
        public string BorderRightColor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The style of the right border of the object.</summary>
        [ScriptField]
        public string BorderRightStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the right border of the object.</summary>
        [ScriptField]
        public string BorderRightWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The style of the borders of the object.</summary>
        [ScriptField]
        public string BorderStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The properties of the top border of the object.</summary>
        [ScriptField]
        public string BorderTop {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The color of the top border of the object.</summary>
        [ScriptField]
        public string BorderTopColor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The style of the top border of the object.</summary>
        [ScriptField]
        public string BorderTopStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the top border of the object.</summary>
        [ScriptField]
        public string BorderTopWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the borders of the object.</summary>
        [ScriptField]
        public string BorderWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The bottom position of the object in relation to the bottom of the next positioned object.</summary>
        [ScriptField]
        public string Bottom {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the object allows floating objects on its left side, right side, or both, so that the next text displays past the floating objects.</summary>
        [ScriptField]
        public string Clear {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Which part of a positioned object is visible.</summary>
        [ScriptField]
        public string Clip {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The color of the text of the object.</summary>
        [ScriptField]
        public string Color {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The side of the object the text will flow.</summary>
        [ScriptField]
        public string CssFloat {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The persisted representation of this style.</summary>
        [ScriptField]
        public string CssText {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The type of cursor to display as the mouse pointer moves over the object.</summary>
        [ScriptField]
        public string Cursor {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The reading order of content within the object.</summary>
        [ScriptField]
        public string Direction {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the object is rendered.</summary>
        [ScriptField]
        public string Display {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The collection of filters applied to an object. (Specific to Internet Explorer)</summary>
        [ScriptField]
        public string Filter {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The font properties of the object or one or more of six user-preference fonts.</summary>
        [ScriptField]
        public string Font {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The name of the font used for text in the object.</summary>
        [ScriptField]
        public string FontFamily {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The font size used for text in the object.</summary>
        [ScriptField]
        public string FontSize {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The font style of the object as italic, normal, or oblique.</summary>
        [ScriptField]
        public string FontStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the text of the object is in small capital letters.</summary>
        [ScriptField]
        public string FontVariant {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The weight of the font of the object.</summary>
        [ScriptField]
        public string FontWeight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The height of the object.</summary>
        [ScriptField]
        public string Height {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The position of the object relative to the left edge of the next positioned object in the document hierarchy.</summary>
        [ScriptField]
        public string Left {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of additional space between letters in the object.</summary>
        [ScriptField]
        public string LetterSpacing {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The distance between lines in the object.</summary>
        [ScriptField]
        public string LineHeight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The listStyle properties of the object.</summary>
        [ScriptField]
        public string ListStyle {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The image to use as a list-item marker for the object.</summary>
        [ScriptField]
        public string ListStyleImage {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>SHow the list-item marker is drawn relative to the content of the object.</summary>
        [ScriptField]
        public string ListStylePosition {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The type of the list-item marker for the object.</summary>
        [ScriptField]
        public string ListStyleType {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the top, right, bottom, and left margins of the object.</summary>
        [ScriptField]
        public string Margin {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The height of the bottom margin of the object.</summary>
        [ScriptField]
        public string MarginBottom {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the left margin of the object.</summary>
        [ScriptField]
        public string MarginLeft {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the right margin of the object.</summary>
        [ScriptField]
        public string MarginRight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The height of the top margin of the object.</summary>
        [ScriptField]
        public string MarginTop {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The maximum height for displayable block level elements.</summary>
        [ScriptField]
        public string MaxHeight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The maximum width for displayable block level elements.</summary>
        [ScriptField]
        public string MaxWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The minimum height for an element.</summary>
        [ScriptField]
        public string MinHeight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The minimum width for displayable block level element.</summary>
        [ScriptField]
        public string MinWidth {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The interpolation (resampling) method used to stretch images. (Specific to Internet Explorer)</summary>
        [ScriptField]
        public string MsInterpolationMode {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>How to blend the object into the rendering.</summary>
        [ScriptField]
        public string Opacity {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>How to manage the content of the object when the content exceeds the height or width of the object.</summary>
        [ScriptField]
        public string Overflow {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>How to manage the content of the object when the content exceeds the width of the object.</summary>
        [ScriptField]
        public string OverflowX {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>How to manage the content of the object when the content exceeds the height of the object.</summary>
        [ScriptField]
        public string OverflowY {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of space to insert between the object and its margin or, if there is a border, between the object and its border.</summary>
        [ScriptField]
        public string Padding {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of space to insert between the bottom border of the object and the content.</summary>
        [ScriptField]
        public string PaddingBottom {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of space to insert between the left border of the object and the content.</summary>
        [ScriptField]
        public string PaddingLeft {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of space to insert between the right border of the object and the content.</summary>
        [ScriptField]
        public string PaddingRight {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of space to insert between the top border of the object and the content.</summary>
        [ScriptField]
        public string PaddingTop {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether a page break occurs after the object.</summary>
        [ScriptField]
        public string PageBreakAfter {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether a page break occurs before the object.</summary>
        [ScriptField]
        public string PageBreakBefore {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The bottom position of the object.</summary>
        [ScriptField]
        public int PixelBottom {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The height of the object.</summary>
        [ScriptField]
        public int PixelHeight {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The left position of the object.</summary>
        [ScriptField]
        public int PixelLeft {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The right position of the object.</summary>
        [ScriptField]
        public int PixelRight {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The top position of the object.</summary>
        [ScriptField]
        public int PixelTop {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The width of the object.</summary>
        [ScriptField]
        public int PixelWidth {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The bottom position of the object in the units specified by the bottom attribute.</summary>
        [ScriptField]
        public int PosBottom {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The height of the object in the units specified by the height attribute.</summary>
        [ScriptField]
        public int PosHeight {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The type of positioning used for the object.</summary>
        [ScriptField]
        public string Position {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The left position of the object in the units specified by the left attribute.</summary>
        [ScriptField]
        public int PosLeft {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The right position of the object in the units specified by the right attribute.</summary>
        [ScriptField]
        public int PosRight {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The top position of the object in the units specified by the top attribute.</summary>
        [ScriptField]
        public int PosTop {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The width of the object in the units specified by the width attribute.</summary>
        [ScriptField]
        public int PosWidth {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The position of the object relative to the right edge of the next positioned object in the document hierarchy.</summary>
        [ScriptField]
        public string Right {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The side of the object the text will flow.</summary>
        [ScriptField]
        public string StyleFloat {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the table layout is fixed.</summary>
        [ScriptField]
        public string TableLayout {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether The text in the object is left-aligned, right-aligned, centered, or justified.</summary>
        [ScriptField]
        public string TextAlign {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Indicates whether the text in the object has blink, line-through, overline, or underline decorations.</summary>
        [ScriptField]
        public string TextDecoration {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the object's text ""blinks.""</summary>
        [ScriptField]
        public string TextDecorationBlink {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the text in the object has a line drawn through it.</summary>
        [ScriptField]
        public string TextDecorationLineThrough {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the textDecoration property for the object has been set to none.</summary>
        [ScriptField]
        public string TextDecorationNone {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the text in the object has a line drawn over it.</summary>
        [ScriptField]
        public string TextDecorationOverline {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the text in the object is underlined.</summary>
        [ScriptField]
        public string TextDecorationUnderline {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The indentation of the first line of text in the object.</summary>
        [ScriptField]
        public string TextIndent {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The type of alignment used to justify text in the object.</summary>
        [ScriptField]
        public string TextJustify {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Indicates whether to render ellipses(...) to indicate text overflow.</summary>
        [ScriptField]
        public string TextOverflow {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The rendering of the text in the object.</summary>
        [ScriptField]
        public string TextTransform {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string this[string name] {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The position of the object relative to the top of the next positioned object in the document hierarchy.</summary>
        [ScriptField]
        public string Top {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The vertical alignment of the object.</summary>
        [ScriptField]
        public string VerticalAlign {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether the content of the object is displayed.</summary>
        [ScriptField]
        public string Visibility {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Indicates whether lines are automatically broken inside the object.</summary>
        [ScriptField]
        public string WhiteSpace {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The width of the object.</summary>
        [ScriptField]
        public string Width {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The amount of additional space between words in the object.</summary>
        [ScriptField]
        public string WordSpacing {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>Whether to break words when the content exceeds the boundaries of its container.</summary>
        [ScriptField]
        public string WordWrap {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The direction and flow of the content in the object.</summary>
        [ScriptField]
        public string WritingMode {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>The stacking order of positioned objects.</summary>
        [ScriptField]
        public short ZIndex {
            get {
                return 0;
            }
            set {
            }
        }

        /// <summary>The magnification scale of the object.</summary>
        [ScriptField]
        public string Zoom {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// TableCellElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TableCellElement : Element {

        private TableCellElement() {
        }

        [ScriptField]
        public int ColSpan {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public bool NoWrap {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public int RowSpan {
            get {
                return 0;
            }
            set {
            }
        }
    }
}
// TableElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TableElement : Element {

        private TableElement() {
        }

        [ScriptField]
        public ElementCollection Cells {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementCollection Rows {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementCollection tBodies {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element tFoot {
            get {
                return null;
            }
        }

        [ScriptField]
        public ElementCollection tHead {
            get {
                return null;
            }
        }

        public void DeleteRow() {
        }

        public void DeleteRow(int index) {
        }

        public TableRowElement InsertRow() {
            return null;
        }

        public TableRowElement InsertRow(int index) {
            return null;
        }
    }
}
// TableRowElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TableRowElement : Element {

        private TableRowElement() {
        }

        [ScriptField]
        public ElementCollection Cells {
            get {
                return null;
            }
        }

        [ScriptField]
        public int RowIndex {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int SectionRowIndex {
            get {
                return 0;
            }
        }

        public void DeleteCell() {
        }

        public void DeleteCell(int index) {
        }

        public TableCellElement InsertCell() {
            return null;
        }

        public TableCellElement InsertCell(int index) {
            return null;
        }
    }
}
// TableSectionElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TableSectionElement : Element {

        private TableSectionElement() {
        }

        [ScriptField]
        public ElementCollection Rows {
            get {
                return null;
            }
        }
    }
}
// TextAreaElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TextAreaElement : InputElement {

        private TextAreaElement() {
        }

        [ScriptField]
        public int Cols {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public bool ReadOnly {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public int Rows {
            get {
                return 0;
            }
            set {
            }
        }
    }
}
// InputElement.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class TextElement : InputElement {

        internal TextElement() {
        }

        [ScriptField]
        public int MaxLength {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public bool ReadOnly {
            get {
                return false;
            }
            set {
            }
        }
    }
}
// TokenList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TokenList {

        private TokenList() {
        }

        [ScriptField]
        [ScriptName(""length"")]
        public int Count {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string this[int index] {
            get {
                return null;
            }
        }

        public bool Contains(string token) {
            return false;
        }

        public void Add(string token) {
        }

        public void Remove(string token) {
        }

        public bool Toggle(string token) {
            return false;
        }

        public override string ToString() {
            return null;
        }
    }
}
// TouchEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TouchEvent : ElementEvent {

        internal TouchEvent() {
        }

        [ScriptField]
        public TouchInfo[] ChangedTouches {
            get {
                return null;
            }
        }

        [ScriptField]
        public TouchInfo[] TargetTouches {
            get {
                return null;
            }
        }

        [ScriptField]
        public TouchInfo[] Touches {
            get {
                return null;
            }
        }
    }
}
// TouchInfo.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TouchInfo {

        internal TouchInfo() {
        }

        [ScriptField]
        public int ClientX {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ClientY {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int Identifier {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int PageX {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int PageY {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int ScreenX {
            get {
                return 0;
            }
        }

        [ScriptField]
        public long ScreenY {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Element Target {
            get {
                return null;
            }
        }
    }
}
// Window.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Html.Data;
using System.Html.Data.Files;
using System.Html.Data.IndexedDB;
using System.Html.Data.Sql;
using System.Runtime.CompilerServices;
using SHDF = System.Html.Data.Files;

namespace System.Html {

    /// <summary>
    /// The window object represents the current browser window, and is the top-level
    /// scripting object.
    /// </summary>
    [SHDF.ScriptIgnoreNamespace]
    [SHDF.ScriptImport]
    [ScriptName(""window"")]
    public sealed class Window {

        private Window() {
        }

        [ScriptField]
        public static ApplicationCache ApplicationCache {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Blob Blob {
            get {
                return null;
            }
        }

        /// <summary>
        /// IE only.
        /// </summary>
        [ScriptField]
        public static DataTransfer ClipboardData {
            get {
                return null;
            }
        }

        [ScriptField]
        public static bool Closed {
            get {
                return false;
            }
        }

        [ScriptField]
        public static string DefaultStatus {
            get { 
                return null; 
            }
            set { 
            }
        }

        [ScriptField]
        public static object DialogArguments {
            get { 
                return null; 
            }
        }

        [ScriptField]
        public static DocumentInstance Document {
            get {
                return null;
            }
        }

        /// <summary>
        /// Provides information about the current event being handled.
        /// </summary>
        [ScriptField]
        public static ElementEvent Event {
            get {
                return null;
            }
        }

        [ScriptField]
        public static File File {
            get {
                return null;
            }
        }

        [ScriptField]
        public static FileList FileList {
            get {
                return null;
            }
        }

        [ScriptField]
        public static FileReader FileReader {
            get {
                return null;
            }
        }

        [ScriptField]
        public static IFrameElement FrameElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public static History History {
            get {
                return null;
            }
        }

        [ScriptField]
        public static int InnerHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static int InnerWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static Storage LocalStorage {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Location Location {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Navigator Navigator {
            get {
                return null;
            }
        }

        [ScriptField]
        public static WindowInstance Parent {
            get {
                return null;
            }
        }

        [ScriptField]
        public static ErrorHandler Onerror {
            get { 
                return null; 
            }
            set { 
            } 
        }

        [ScriptField]
        public static WindowInstance Opener {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Orientation Orientation {
            get {
                return Orientation.Portrait;
            }
        }

        [ScriptField]
        public static int OuterHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static int OuterWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static int PageXOffset {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static int PageYOffset {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static Screen Screen {
            get {
                return null;
            }
        }

        [ScriptField]
        public static WindowInstance Self {
            get {
                return null;
            }
        }

        [ScriptField]
        public static Storage SessionStorage {
            get {
                return null;
            }
        }

        [ScriptField]
        public static string Status {
            get { 
                return null; 
            }
            set { 
            }
        }

        [ScriptField]
        public static WindowInstance Top {
            get {
                return null;
            }
        }

        [ScriptField]
        public static WindowInstance[] Frames {
            get {
                return null;
            }
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public static void AddEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public static void AddEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        /// <summary>
        /// Displays a message box containing the specified message text.
        /// </summary>
        /// <param name=""message"">The text of the message.</param>
        [ScriptAlias(""alert"")]
        public static void Alert(string message) {
        }

        /// <summary>
        /// Displays a message box containing the specified value converted
        /// into a string.
        /// </summary>
        /// <param name=""b"">The boolean to display.</param>
        [ScriptAlias(""alert"")]
        public static void Alert(bool b) {
        }

        /// <summary>
        /// Displays a message box containing the specified value converted
        /// into a string.
        /// </summary>
        /// <param name=""d"">The date to display.</param>
        [ScriptAlias(""alert"")]
        public static void Alert(Date d) {
        }

        /// <summary>
        /// Displays a message box containing the specified value converted
        /// into a string.
        /// </summary>
        /// <param name=""n"">The number to display.</param>
        [ScriptAlias(""alert"")]
        public static void Alert(Number n) {
        }

        /// <summary>
        /// Displays a message box containing the specified value converted
        /// into a string.
        /// </summary>
        /// <param name=""o"">The object to display.</param>
        [ScriptAlias(""alert"")]
        public static void Alert(object o) {
        }

        public static void AttachEvent(string eventName, ElementEventHandler handler) {
        }

        public static void Close() {
        }

        /// <summary>
        /// Prompts the user with a yes/no question.
        /// </summary>
        /// <param name=""message"">The text of the question.</param>
        /// <returns>true if the user chooses yes; false otherwise.</returns>
        [ScriptAlias(""confirm"")]
        public static bool Confirm(string message) {
            return false;
        }

        public static void DetachEvent(string eventName, ElementEventHandler handler) {
        }

        public static bool DispatchEvent(MutableEvent eventObject) {
            return false;
        }

        public static void Navigate(string url) {
        }

        public static WindowInstance Open(string url) {
            return null;
        }

        public static WindowInstance Open(string url, string targetName) {
            return null;
        }

        public static WindowInstance Open(string url, string targetName, string features) {
            return null;
        }

        public static WindowInstance Open(string url, string targetName, string features, bool replace) {
            return null;
        }

        public static void Print() {
        }

        /// <summary>
        /// Prompts the user to enter a value.
        /// </summary>
        /// <param name=""message"">The text of the prompt.</param>
        /// <returns>The value entered by the user.</returns>
        [ScriptAlias(""prompt"")]
        public static string Prompt(string message) {
            return null;
        }

        /// <summary>
        /// Prompts the user to enter a value.
        /// </summary>
        /// <param name=""message"">The text of the prompt.</param>
        /// <param name=""defaultValue"">The default value for the prompt.</param>
        /// <returns>The value entered by the user.</returns>
        [ScriptAlias(""prompt"")]
        public static string Prompt(string message, string defaultValue) {
            return null;
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public static void RemoveEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public static void RemoveEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        [ScriptAlias(""require"")]
        public static void Require(string[] names, Action callback) {
        }

        [ScriptAlias(""require"")]
        public static void Require<T>(string name, Action<T> callback) {
        }

        [ScriptAlias(""require"")]
        public static void Require(string name, Action callback) {
        }

        public static void Scroll(int x, int y) {
        }

        public static void ScrollBy(int x, int y) {
        }

        public static void ScrollTo(int x, int y) {
        }

        public static SqlDatabase OpenDatabase(string name, string version) {
            return null;
        }

        public static SqlDatabase OpenDatabase(string name, string version, string displayName) {
            return null;
        }

        public static SqlDatabase OpenDatabase(string name, string version, string displayName, int estimatedSize) {
            return null;
        }

        public static SqlDatabase OpenDatabase(string name, string version, string displayName, int estimatedSize, SqlDatabaseCallback creationCallback) {
            return null;
        }

        [ScriptField]
        public static DBFactory IndexedDB {
            get {
                return null;
            }
        }


        public static void PostMessage(string message, string targetOrigin) {
        }

        /// <summary>
        /// Delegate that indicates the ability of the browser
        /// to show a modal dialog.
        /// </summary>
        /// <remarks>
        /// Not all browsers support this function, so code using
        /// this needs to check for existence of the feature or the browser.
        /// </remarks>
        public static WindowInstance ShowModalDialog(string url, object dialogArguments, string features) {
            return null; 
        }
    }
}
// WindowInstance.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html {

    /// <summary>
    /// The window object represents the current browser window, and is the top-level
    /// scripting object.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class WindowInstance {

        private WindowInstance() {
        }

        [ScriptField]
        public bool Closed {
            get {
                return false;
            }
        }

        [ScriptField]
        public string DefaultStatus {
            get { 
                return null; 
            }
            set { 
            }
        }

        [ScriptField]
        public DocumentInstance Document {
            get {
                return null;
            }
        }

        [ScriptField]
        public IFrameElement FrameElement {
            get {
                return null;
            }
        }

        [ScriptField]
        public int InnerHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int InnerWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Location Location {
            get {
                return null;
            }
        }

        [ScriptField]
        public WindowInstance Parent {
            get {
                return null;
            }
        }

        [ScriptField]
        public WindowInstance Opener {
            get {
                return null;
            }
        }

        [ScriptField]
        public static int OuterHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public static int OuterWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int PageXOffset {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int PageYOffset {
            get {
                return 0;
            }
        }

        [ScriptField]
        public WindowInstance Self {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Status {
            get { 
                return null; 
            }
            set { 
            }
        }

        [ScriptField]
        public WindowInstance Top {
            get {
                return null;
            }
        }

        [ScriptField]
        public WindowInstance[] Frames {
            get {
                return null;
            }
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public void AddEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void AddEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        public void AttachEvent(string eventName, ElementEventHandler handler) {
        }

        public void Close() {
        }

        public void DetachEvent(string eventName, ElementEventHandler handler) {
        }

        public bool DispatchEvent(MutableEvent eventObject) {
            return false;
        }

        public void Navigate(string url) {
        }

        public void Print() {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        public void RemoveEventListener(string eventName, ElementEventListener listener) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void RemoveEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        public void Scroll(int x, int y) {
        }

        public void ScrollBy(int x, int y) {
        }

        public void ScrollTo(int x, int y) {
        }

        public void PostMessage(string message, string targetOrigin) {
        }
    }
}
// ApplicationCache.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ApplicationCache {

        private ApplicationCache() {
        }

        /// <summary>
        /// Gets the current status of the application cache.
        /// </summary>
        [ScriptField]
        public ApplicationCacheStatus Status {
            get {
                return ApplicationCacheStatus.Uncached;
            }
        }

        public void Add(string uri) {
        }

        public void AddEventListener(ApplicationCacheEvent eventName, ElementEventListener listener, bool useCapture) {
        }

        public void RemoveEventListener(ApplicationCacheEvent eventName, ElementEventListener listener, bool useCapture) {
        }

        /// <summary>
        /// Replaces the active cache with the latest version.
        /// </summary>
        public void SwapCache() {
        }

        /// <summary>
        /// Manually triggers the update process.
        /// </summary>
        public void Update() {
        }
    }
}
// ApplicationCacheEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data {

    /// <summary>
    /// Represents an event raised by the Application Cache.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum ApplicationCacheEvent {

        /// <summary>
        /// Raised when the update process finishes for the first time
        /// </summary>
        [ScriptName(""cached"")]
        Cached,

        /// <summary>
        /// Raised when the cache update process begins.
        /// </summary>
        [ScriptName(""checking"")]
        Checking,

        /// <summary>
        /// Raised when the update process begins downloading resources
        /// in the manifest file.
        /// </summary>
        [ScriptName(""downloading"")]
        Downloading,

        /// <summary>
        /// Raised when an error occurs.
        /// </summary>
        [ScriptName(""error"")]
        Error,

        /// <summary>
        /// Raised when the update process finishes but the manifest
        /// file does not change.
        /// </summary>
        [ScriptName(""noupdate"")]
        NoUpdate,

        /// <summary>
        /// Raised when each resource in the manifest file begins to download.
        /// </summary>
        [ScriptName(""progress"")]
        Progress,

        /// <summary>
        /// Raised when there is an existing application cache,
        /// the update process finishes, and there is a new application
        /// cache ready for use.
        /// </summary>
        [ScriptName(""updateready"")]
        UpdateReady
    }
}
// ApplicationCacheStatus.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data {

    /// <summary>
    /// Indicates the status of the application cache.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum ApplicationCacheStatus {

        /// <summary>
        /// The object isn�t associated with an application cache.
        /// </summary>
        Uncached = 0,

        /// <summary>
        /// The cache is idle, i.e. there are no outstanding checks or
        /// downloads in progress.
        /// </summary>
        Idle = 1,

        /// <summary>
        /// The update has started but the resources are not downloaded yet.
        /// </summary>
        Checking = 2,

        /// <summary>
        /// The resources are being downloaded into the cache.
        /// </summary>
        Downloading = 3,

        /// <summary>
        /// Resources have finished downloading and the new cache
        /// is ready to be used.
        /// </summary>
        UpdateReady = 4,

        /// <summary>
        /// Resources hare finished downloading, but are obsolete.
        /// </summary>
        Obsolete = 5
    }
}
// Storage.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Storage {

        private Storage() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
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

        public void Clear() {
        }

        public object GetItem(string key) {
            return null;
        }

        [ScriptName(""key"")]
        public string GetKey(int index) {
            return null;
        }

        public void RemoveItem(string key) {
        }

        public void SetItem(string key, object value) {
        }
    }
}
// Blob.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class Blob {

        public Blob() {
        }

        public Blob(object blobParts) {
        }

        public Blob(object blobParts, object options) {
        }
        
        [CLSCompliant(false)]
        [ScriptField]
        public ulong Size {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string Type {
            get {
                return String.Empty;
            }
        }

        public Blob Slice() {
            return null;
        }

        public Blob Slice(long start) {
            return null;
        }

        public Blob Slice(long start, long end) {
            return null;
        }

        public Blob Slice(long start, long end, string contentType) {
            return null;
        }
    }
}
// File.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class File : Blob {

        private File() {
        }

        [ScriptField]
        public Date LastModifiedDate {
            get {
                return Date.Now;
            }
        }

        [ScriptField]
        public String Name {
            get {
                return String.Empty;
            }
        }
    }
}
// FileError.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class FileError {

        private FileError() {
        }

        [ScriptField]
        public string Name {
            get {
                return String.Empty;
            }
        }
    }
}
// FileList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class FileList {

        private FileList() {
        }

        [ScriptField]
        public long Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public File this[int index] {
            get {
                return null;
            }
        }
    }
}
// FileProgressEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class FileProgressEvent : ElementEvent {

        private FileProgressEvent() {
        }

        public bool Bubbles;
        public bool Cancelable;
        public bool DefaultPrevented;
        public bool LengthComputable;

        public int EventPhase;
        public int Loaded;
        public int Total;

        public object ClipboardData;
    }
}
// FileReader.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class FileReader {

        [ScriptField]
        public FileError Error {
            get {
                return null;
            }
        }

        /// <summary>
        /// Indicates the state of the FileReader. This will be one of the State constants. Read only.
        /// </summary>
        [ScriptField]
        public int ReadyState {
            get {
                return (int)FileReadyState.Empty;
            }
        }

        // File or Blob data
        [ScriptField]
        public object Result {
            get {
                return null;
            }
        }

        [ScriptName(""onabort"")]
        public Action<FileProgressEvent> OnAbort {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onerror"")]
        public Action<FileProgressEvent> OnError {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onload"")]
        public Action<FileProgressEvent> OnLoad {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onloadend"")]
        public Action<FileProgressEvent> OnLoadEnd {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onloadstart"")]
        public Action<FileProgressEvent> OnLoadStart {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onprogress"")]
        public Action<FileProgressEvent> OnProgress {
            get {
                return null;
            }
            set {
            }
        }

        public void Abort() {
        }

        /// <summary>
        /// Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void AddEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        /// <summary>
        ///     Adds a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""handler"">The handler to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the handler wants to initiate capturing the event.</param>
        public void AddEventListener(string eventName, IElementEventHandler handler, bool useCapture) {
        }

        public bool DispatchEvent(MutableEvent eventObject) {
            return false;
        }
        
        public void ReadAsArrayBuffer(Blob blob) {
        }

        public void ReadAsBinaryString(Blob blob) {
        }

        public void ReadAsDataURL(Blob blob) {
        }

        public void ReadAsText(Blob blob) {
        }

        public void ReadAsText(Blob blob, string encoding) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""listener"">The listener to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the listener wants to initiate capturing the event.</param>
        public void RemoveEventListener(string eventName, ElementEventListener listener, bool useCapture) {
        }

        /// <summary>
        /// Removes a listener for the specified event.
        /// </summary>
        /// <param name=""eventName"">The name of the event such as 'load'.</param>
        /// <param name=""handler"">The handler to be invoked in response to the event.</param>
        /// <param name=""useCapture"">Whether the handler wants to initiate capturing the event.</param>
        public void RemoveEventListener(string eventName, IElementEventHandler handler, bool useCapture) {
        }
    }
}
// FileReader.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Runtime.CompilerServices;

namespace System.Html.Data.Files {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class FileReaderSync {

        public object ReadAsArrayBuffer(Blob blob) {
            return null;
        }

        public string ReadAsDataURL(Blob blob) {
            return null;
        }

        public string ReadAsText(Blob blob) {
            return null;
        }

        public string ReadAsText(Blob blob, string encoding) {
            return null;
        }
    }
}
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
// DBCursor.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class DBCursor {

        internal DBCursor() {
        }

        [ScriptField]
        public string Direction {
            get {
                return null;
            }
        }

        [ScriptField]
        public object Key {
            get {
                return null;
            }
        }

        [ScriptField]
        public object PrimaryKey {
            get {
                return null;
            }
        }

        [ScriptField]
        public object Source {
            get {
                return null;
            }
        }

        public void Advance(long count) {
        }

        public void Continue() {
        }

        public void Continue(object key) {
        }

        public DBRequest Delete(object value) {
            return null;
        }

        public DBRequest Update(object value) {
            return null;
        }
    }
}
// DBCursorWithValue.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBCursorWithValue : DBCursor {

        private DBCursorWithValue() {
        }

        [ScriptField]
        public object Value {
            get {
                return null;
            }
        }
    }
}
// DBDatabase.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBDatabase : DBEventTarget {

        private DBDatabase() {
        }

        [ScriptField]
        public string Name {
            get {
                return default(string);
            }
        }

        [ScriptField]
        public string[] ObjectStoreNames {
            get {
                return default(string[]);
            }
        }

        [ScriptName(""onabort"")]
        [ScriptField]
        public DBDatabaseCallback OnAbort {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onerror"")]
        [ScriptField]
        public DBDatabaseCallback OnError {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onversionchange"")]
        [ScriptField]
        public DBDatabaseVersionChangeCallback OnVersionChange {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public long Version {
            get {
                return default(long);
            }
        }

        public void Close() {
        }

        public DBObjectStore CreateObjectStore(string name) {
            return default(DBObjectStore);
        }

        public DBObjectStore CreateObjectStore(string name, DBObjectStoreParameters optionalParameters) {
            return default(DBObjectStore);
        }

        public void DeleteObjectStore(string name) {
        }

        public DBTransaction Transaction(string[] storenames) {
            return default(DBTransaction);
        }

        public DBTransaction Transaction(string[] storenames, string mode) {
            return default(DBTransaction);
        }
    }

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void DBDatabaseCallback(DBEvent<DBDatabase> e);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void DBDatabaseVersionChangeCallback(DBVersionChangeEvent<DBDatabase> e);
}
// DBEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class DBEvent<T> {

        internal DBEvent() {
        }

        [ScriptField]
        public T Target {
            get {
                return default(T);
            }
        }

        [ScriptField]
        public string Type {
            get {
                return null;
            }
        }
    }
}
// DBEventTarget.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public abstract class DBEventTarget {

        internal DBEventTarget() {
        }

        public void AddEventListener<T>(string type, Action<T> listener) {
        }

        public void AddEventListener<T>(string type, Action<T> listener, bool useCapture) {
        }

        public void RemoveEventListener<T>(string type, Action<T> listener) {
        }

        public void RemoveEventListener<T>(string type, Action<T> listener, bool useCapture) {
        }
    }
}
// DBFactory.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBFactory {

        private DBFactory() {
        }

        [ScriptName(""cmp"")]
        public int Compare(object first, object last) {
            return default(short);
        }

        public DBOpenDBRequest DeleteDatabase(string name) {
            return null;
        }

        public DBOpenDBRequest Open(string name) {
            return null;
        }

        public DBOpenDBRequest Open(string name, long version) {
            return null;
        }
    }
}
// DBIndex.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBIndex {

        private DBIndex() {
        }

        [ScriptField]
        public string KeyPath {
            get {
                return default(string);
            }
        }

        [ScriptField]
        public bool MultiEntry {
            get {
                return default(bool);
            }
        }

        [ScriptField]
        public string Name {
            get {
                return default(string);
            }
        }

        [ScriptField]
        public DBObjectStore ObjectStore {
            get {
                return default(DBObjectStore);
            }
        }

        [ScriptField]
        public bool Unique {
            get {
                return default(bool);
            }
        }

        public DBRequest Count() {
            return null;
        }

        public DBRequest Count(object key) {
            return null;
        }

        public DBRequest Get(object key) {
            return null;
        }

        public DBRequest GetKey(object key) {
            return null;
        }

        public DBRequest OpenCursor() {
            return null;
        }

        public DBRequest OpenCursor(object range) {
            return null;
        }

        public DBRequest OpenCursor(object range, string direction) {
            return null;
        }

        public DBRequest OpenKeyCursor() {
            return null;
        }

        public DBRequest OpenKeyCursor(object range) {
            return null;
        }

        public DBRequest OpenKeyCursor(object range, string direction) {
            return null;
        }
    }
}
// DBIndexParameters.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class DBIndexParameters {

        [ScriptField]
        public bool MultiEntry {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public bool Unique {
            get {
                return false;
            }
            set {
            }
        }
    }
}
// DBObjectStore.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBObjectStore {

        private DBObjectStore() {
        }

        [ScriptField]
        public bool AutoIncremenent {
            get {
                return default(bool);
            }
        }

        [ScriptField]
        public string[] IndexNames {
            get {
                return default(string[]);
            }
        }

        [ScriptField]
        public string KeyPath {
            get {
                return default(string);
            }
        }

        [ScriptField]
        public string Name {
            get {
                return default(string);
            }
        }

        [ScriptField]
        public DBTransaction Transaction {
            get {
                return default(DBTransaction);
            }
        }

        public DBRequest Add(object value) {
            return null;
        }

        public DBRequest Add(object value, object key) {
            return null;
        }

        public DBRequest Clear() {
            return null;
        }

        public DBRequest Count() {
            return null;
        }

        public DBRequest Count(object key) {
            return null;
        }

        public DBIndex CreateIndex(string name, object keyPath) {
            return default(DBIndex);
        }

        public DBIndex CreateIndex(string name, object keyPath, DBIndexParameters optionalParameters) {
            return default(DBIndex);
        }

        public DBRequest Delete(object key) {
            return null;
        }

        public void DeleteIndex(string indexName) {
        }

        public DBRequest Get(object key) {
            return null;
        }

        public DBIndex Index(string name) {
            return default(DBIndex);
        }

        public DBRequest OpenCursor() {
            return null;
        }

        public DBRequest OpenCursor(object range) {
            return null;
        }

        public DBRequest OpenCursor(object range, string direction) {
            return null;
        }

        public DBRequest Put(object value) {
            return null;
        }

        public DBRequest Put(object value, object key) {
            return null;
        }
    }
}
// DBObjectStoreParameters.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class DBObjectStoreParameters {

        [ScriptField]
        public bool AutoIncrement {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public string KeyPath {
            get {
                return null;
            }
            set {
            }
        }
    }
}

// DBOpenDBRequest.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBOpenDBRequest : DBRequest {

        private DBOpenDBRequest() {
        }

        [ScriptName(""onblocked"")]
        [ScriptField]
        public DBOpenDBRequestCallback OnBlocked {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onupgradeneeded"")]
        [ScriptField]
        public DBOpenDBRequestVersionChangeCallback OnUpgradeNeeded {
            get {
                return null;
            }
            set {
            }
        }
    }

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void DBOpenDBRequestCallback(DBEvent<DBOpenDBRequest> e);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void DBOpenDBRequestVersionChangeCallback(DBVersionChangeEvent<DBOpenDBRequest> e);
}
// DBRequest.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class DBRequest : DBEventTarget {

        internal DBRequest() {
        }

        [ScriptField]
        public object Error {
            get {
                return null;
            }
        }

        [ScriptName(""onerror"")]
        [ScriptField]
        public DBRequestCallback OnError {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onsuccess"")]
        [ScriptField]
        public DBRequestCallback OnSuccess {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string ReadyState {
            get {
                return null;
            }
        }

        [ScriptField]
        public object Result {
            get {
                return null;
            }
        }

        [ScriptField]
        public object Source {
            get {
                return null;
            }
        }

        [ScriptField]
        public DBTransaction Transaction {
            get {
                return null;
            }
        }
    }

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void DBRequestCallback(DBEvent<DBRequest> e);
}
// DBTransaction.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBTransaction : DBEventTarget {

        private DBTransaction() {
        }

        [ScriptField]
        [ScriptName(""db"")]
        public DBDatabase Database {
            get {
                return null;
            }
        }

        [ScriptField]
        public object Error {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Mode {
            get {
                return null;
            }
        }

        [ScriptName(""onabort"")]
        [ScriptField]
        public DBTransactionCallback OnAbort {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""oncomplete"")]
        [ScriptField]
        public DBTransactionCallback OnComplete {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptName(""onerror"")]
        [ScriptField]
        public DBTransactionCallback OnError {
            get {
                return null;
            }
            set {
            }
        }

        public void Abort() {
        }

        public DBObjectStore ObjectStore(string name) {
            return null;
        }
    }

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public delegate void DBTransactionCallback(DBEvent<DBTransaction> e);
}
// DBVersionChangeEvent.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.IndexedDB {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class DBVersionChangeEvent<T> : DBEvent<T> {

        private DBVersionChangeEvent() {
        }

        [ScriptField]
        public long NewVersion {
            get {
                return default(long);
            }
        }

        [ScriptField]
        public long OldVersion {
            get {
                return default(long);
            }
        }
    }
}
// SqlDatabase.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.Sql {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SqlDatabase {

        private SqlDatabase() {
        }

        [ScriptField]
        public string Version {
            get {
                return null;
            }
        }

        public void ChangeVersion(string oldVersion, string newVersion, SqlTransactionCallback callback, SqlErrorCallback errorCallback, Action successCallback) {
        }

        public void ReadTransaction(SqlTransactionCallback callback, SqlErrorCallback errorCallback, Action successCallback) {
        }

        public void Transaction(SqlTransactionCallback callback, SqlErrorCallback errorCallback, Action successCallback) {
        }
    }

    public delegate bool SqlDatabaseCallback(SqlDatabase db);
}
// SqlError.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.Sql {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SqlError {

        private SqlError() {
        }

        [ScriptField]
        public int Code {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string Message {
            get {
                return null;
            }
        }
    }

    public delegate bool SqlErrorCallback(SqlError error);
}
// SqlResultSet.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Collections;

namespace System.Html.Data.Sql {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SqlResultSet {

        private SqlResultSet() {
        }

        [ScriptField]
        public int InsertId {
            get {
                return 0;
            }
        }

        [ScriptField]
        public SqlResultSetRowList Rows {
            get {
                return null;
            }
        }

        [ScriptField]
        public int RowsAffected {
            get {
                return 0;
            }
        }
    }
}
// SqlResultSetRow.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Collections;

namespace System.Html.Data.Sql {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SqlResultSetRow {

        private SqlResultSetRow() {
        }

        [ScriptField]
        public object this[string name] {
            get {
                return null;
            }
        }
    }
}
// SqlResultSetRowList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Collections;

namespace System.Html.Data.Sql {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SqlResultSetRowList {

        private SqlResultSetRowList() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        public SqlResultSetRow Item(int index) {
            return null;
        }
    }
}
// SqlTransaction.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Data.Sql {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class SqlTransaction {

        private SqlTransaction() {
        }

        public void ExecuteSql(string sql, object[] arguments, SqlStatementCallback callback, SqlStatementErrorCallback errorCallback) {
        }
    }

    public delegate bool SqlTransactionCallback(SqlTransaction transaction);

    public delegate bool SqlStatementCallback(SqlTransaction transaction, SqlResultSet resultSet);

    public delegate bool SqlStatementErrorCallback(SqlTransaction transaction, SqlError error);
}
// ControlRange.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html;

namespace System.Html.Editing {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ControlRange : Range {

        private ControlRange() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public Element this[int index] {
            get {
                return null;
            }
        }

        public void Add(Element element) {
        }

        public void Add(Element element, int index) {
        }

        public void Remove(int index) {
        }
    }
}
// Range.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Editing {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public abstract class Range {

        internal Range() {
        }

        public bool ExecCommand(string command, bool displayUserInterface, object value) {
            return false;
        }

        public bool QueryCommandEnabled(string command) {
            return false;
        }

        public bool QueryCommandIndeterm(string command) {
            return false;
        }

        public bool QueryCommandState(string command) {
            return false;
        }

        public bool QueryCommandSupported(string command) {
            return false;
        }

        public object QueryCommandValue(string command) {
            return null;
        }

        public void ScrollIntoView(bool alignToTop) {
        }

        public void Select() {
        }
    }
}
// Selection.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html;

namespace System.Html.Editing {

    /// <summary>
    /// Represents the active selection, which is a highlighted block of text, and/or other elements in the document on which a user or a script
    /// can carry out some action.
    /// </summary>
    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Selection {

        private Selection() {
        }

        [ScriptField]
        public string Type {
            get {
                return null;
            }
        }

        public void Clear() {
        }

        public Range CreateRange() {
            return null;
        }

        public void Empty() {
        }
    }
}
// TextRange.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html;

namespace System.Html.Editing {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TextRange : Range {

        private TextRange() {
        }

        [ScriptField]
        public int BoundingHeight {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int BoundingLeft {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int BoundingTop {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int BoundingWidth {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string HTMLText {
            get {
                return null;
            }
        }

        [ScriptField]
        public int OffsetLeft {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int OffsetTop {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string Text {
            get {
                return null;
            }
            set {
            }
        }

        public void Collapse(bool moveInsertionPointToStart) {
        }

        public int CompareEndPoints(string compareType, TextRange range) {
            return 0;
        }

        public TextRange Duplicate() {
            return null;
        }

        public bool Expand(string unit) {
            return false;
        }

        public bool FindText(string text) {
            return false;
        }

        public bool FindText(string text, int searchExtent) {
            return false;
        }

        public bool FindText(string text, int searchExtent, int flags) {
            return false;
        }

        public string GetBookmark() {
            return null;
        }

        public object GetBoundingClientRect() {
            return null;
        }

        public Array GetClientRects() {
            return null;
        }

        public bool InRange(TextRange range) {
            return false;
        }

        public bool IsEqual(TextRange range) {
            return false;
        }

        public int Move(string unit) {
            return 0;
        }

        public int Move(string unit, int count) {
            return 0;
        }

        public int MoveEnd(string unit) {
            return 0;
        }

        public int MoveEnd(string unit, int count) {
            return 0;
        }

        public int MoveStart(string unit) {
            return 0;
        }

        public int MoveStart(string unit, int count) {
            return 0;
        }

        public void MoveToElementText(Element element) {
        }

        public bool MoveToBookmark(string bookmark) {
            return false;
        }

        public void MoveToPoint(int x, int y) {
        }

        public Element ParentElement() {
            return null;
        }

        public void PasteHTML(string html) {
        }
    }
}
// VisualFilter.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Filters {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public class VisualFilter {

        internal VisualFilter() {
        }

        [ScriptField]
        public bool Enabled {
            get {
                return false;
            }
            set {
            }
        }
    }
}
// VisualFilterCollection.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Filters {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class VisualFilterCollection {

        private VisualFilterCollection() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public VisualFilter this[int index] {
            get {
                return null;
            }
        }
    }
}
// VisualTransition.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Filters {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class VisualTransition : VisualFilter {

        private VisualTransition() {
        }

        [ScriptField]
        public double Duration {
            get {
                return 0f;
            }
            set {
            }
        }

        [ScriptField]
        public int Percent {
            get {
                return 0;
            }
        }

        [ScriptField]
        public VisualTransitionState Status {
            get {
                return VisualTransitionState.Stopped;
            }
        }

        public void Apply() {
        }

        public void Play() {
        }

        public void Stop() {
        }
    }
}
// VisualTransitionState.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Filters {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum VisualTransitionState {

        Stopped = 0,

        Applied = 1,

        Playing = 2
    }
}
// CanvasContext.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public abstract class CanvasContext {

        internal CanvasContext() {
        }
    }
}
// CanvasContext2D.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;
using System.Html;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class CanvasContext2D : CanvasContext {

        private CanvasContext2D() {
        }

        [ScriptName(""globalAlpha"")]
        [ScriptField]
        public double Alpha {
            get;
            set;
        }

        [ScriptName(""globalCompositeOperation"")]
        [ScriptField]
        public CompositeOperation CompositeOperation {
            get;
            set;
        }

        [ScriptField]
        public object FillStyle {
            get;
            set;
        }

        [ScriptField]
        public string Font {
            get;
            set;
        }

        [ScriptField]
        public LineCap LineCap {
            get;
            set;
        }

        [ScriptField]
        public LineJoin LineJoin {
            get;
            set;
        }

        [ScriptField]
        public double LineWidth {
            get;
            set;
        }

        [ScriptField]
        public int MiterLimit {
            get;
            set;
        }

        [ScriptField]
        public double ShadowBlur {
            get;
            set;
        }

        [ScriptField]
        public string ShadowColor {
            get;
            set;
        }

        [ScriptField]
        public double ShadowOffsetX {
            get;
            set;
        }

        [ScriptField]
        public double ShadowOffsetY {
            get;
            set;
        }

        [ScriptField]
        public object StrokeStyle {
            get;
            set;
        }

        [ScriptField]
        public TextAlign TextAlign {
            get;
            set;
        }

        [ScriptField]
        public TextBaseline TextBaseline {
            get;
            set;
        }

        public void Arc(double x, double y, double radius, double startAngle, double endAngle, bool anticlockwise) {
        }

        public void ArcTo(double x1, double y1, double x2, double y2, double radius) {
        }

        public void BeginPath() {
        }

        public void BezierCurveTo(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y) {
        }

        public void ClearRect(double x, double y, double w, double h) {
        }

        public void Clip() {
        }

        public void ClosePath() {
        }

        public Gradient CreateLinearGradient(double x0, double y0, double x1, double y1) {
            return null;
        }

        public Gradient CreateRadialGradient(double x0, double y0, double r0, double x1, double y1, double r1) {
            return null;
        }

        public ImageData CreateImageData(double sw, double sh) {
            return null;
        }

        public ImageData CreateImageData(ImageData imagedata) {
            return null;
        }

        public Pattern CreatePattern(CanvasElement canvas, string repetition) {
            return null;
        }

        public Pattern CreatePattern(ImageElement image, string repetition) {
            return null;
        }

        public void DrawImage(ImageElement image, double dx, double dy) {
        }

        public void DrawImage(ImageElement image, double dx, double dy, double dw, double dh) {
        }

        public void DrawImage(ImageElement image, double sx, double sy, double sw, double sh, double dx, double dy, double dw, double dh) {
        }

        public void DrawImage(CanvasElement image, double dx, double dy) {
        }

        public void DrawImage(CanvasElement image, double dx, double dy, double dw, double dh) {
        }

        public void DrawImage(CanvasElement image, double sx, double sy, double sw, double sh, double dx, double dy, double dw, double dh) {
        }

        public void Fill() {
        }

        public void FillRect(double x, double y, double w, double h) {
        }

        public void FillText(string text, double x, double y) {
        }

        public void FillText(string text, double x, double y, double maxWidth) {
        }

        public ImageData GetImageData(double sx, double sy, double sw, double sh) {
            return null;
        }

        public bool IsPointInPath(double x, double y) {
            return false;
        }

        public void LineTo(double x, double y) {
        }

        public TextMetrics MeasureText(string text) {
            return null;
        }

        public void MoveTo(double x, double y) {
        }

        public void PutImageData(ImageData imagedata, double dx, double dy) {
        }

        public void PutImageData(ImageData imagedata, double dx, double dy, double dirtyX, double dirtyY, double dirtyWidth, double dirtyHeight) {
        }

        public void QuadraticCurveTo(double cpx, double cpy, double x, double y) {
        }

        public void Rect(double x, double y, double w, double h) {
        }

        public void Restore() {
        }

        public void Rotate(double angle) {
        }

        public void Save() {
        }

        public void Scale(double x, double y) {
        }

        public void SetTransform(double m11, double m12, double m21, double m22, double dx, double dy) {
        }

        public void Stroke() {
        }

        public void StrokeRect(double x, double y, double w, double h) {
        }

        public void StrokeText(string text, double x, double y) {
        }

        public void StrokeText(string text, double x, double y, double maxWidth) {
        }

        public void Transform(double m11, double m12, double m21, double m22, double dx, double dy) {
        }

        public void Translate(double x, double y) {
        }
    }
}
// CompositeOperation.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum CompositeOperation {

        /// <summary>
        /// A (B is ignored). Display the source image instead of the destination image.
        /// </summary>
        Copy = 0,

        /// <summary>
        /// B atop A. Display the destination image wherever both images are opaque.
        /// Display the source image wherever the source image is opaque but the
        /// destination image is transparent. Display transparency elsewhere.
        /// </summary>
        [ScriptName(""destination-atop"")]
        DestinationAtop = 1,

        /// <summary>
        /// B in A. Display the destination image wherever both the destination image and
        /// source image are opaque. Display transparency elsewhere.
        /// </summary>
        [ScriptName(""destination-in"")]
        DestinationIn = 2,

        /// <summary>
        /// B out A. Display the destination image wherever the destination image is opaque
        /// and the source image is transparent. Display transparency elsewhere.
        /// </summary>
        [ScriptName(""destination-out"")]
        DestinationOut = 3,

        /// <summary>
        /// B over A. Display the destination image wherever the destination image is opaque.
        /// Display the source image elsewhere. 
        /// </summary>
        [ScriptName(""destination-over"")]
        DestinationOver = 4,

        /// <summary>
        /// A plus B. Display the sum of the source image and destination image, with color
        /// values approaching 1 as a limit.
        /// </summary>
        Lighter = 5,

        /// <summary>
        /// A atop B. Display the source image wherever both images are opaque. Display the
        /// destination image wherever the destination image is opaque but the source image
        /// is transparent. Display transparency elsewhere.
        /// </summary>
        [ScriptName(""source-atop"")]
        SourceAtop = 6,

        /// <summary>
        /// A in B. Display the source image wherever both the source image and destination
        /// image are opaque. Display transparency elsewhere.
        /// </summary>
        [ScriptName(""source-in"")]
        SourceIn = 7,

        /// <summary>
        /// A out B. Display the source image wherever the source image is opaque and the
        /// destination image is transparent. Display transparency elsewhere.
        /// </summary>
        [ScriptName(""source-out"")]
        SourceOut = 8,

        /// <summary>
        /// A over B. Display the source image wherever the source image is opaque. Display the
        /// destination image elsewhere. This is the default option.
        /// </summary>
        [ScriptName(""source-over"")]
        SourceOver = 9,

        /// <summary>
        /// A xor B. Exclusive OR of the source image and destination image.
        /// </summary>
        Xor = 10
    }
}
// Gradient.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Gradient {

        private Gradient() {
        }

        public void AddColorStop(double offset, string color) {
        }
    }
}
// ImageData.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class ImageData {

        private ImageData() {
        }

        [ScriptField]
        public PixelArray Data {
            get {
                return null;
            }
        }

        [ScriptField]
        public int Height {
            get {
                return 0;
            }
        }

        [ScriptField]
        public int Width {
            get {
                return 0;
            }
        }
    }
}
// LineCap.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum LineCap {

        Butt = 0,

        Round = 1,

        Square = 2
    }
}
// LineJoin.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum LineJoin {

        Miter = 0,

        Round = 1,

        Bevel = 2
    }
}
// Pattern.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Pattern {

        private Pattern() {
        }
    }
}
// PixelArray.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class PixelArray {

        private PixelArray() {
        }

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
    }
}
// Rendering.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum Rendering {

        [ScriptName(""2d"")]
        Render2D,

        [ScriptName(""3d"")]
        Render3D
    }
}
// TextAlign.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum TextAlign {

        Start = 0,

        End = 1,

        Left = 2,

        Right = 3,

        Center = 4
    }
}
// TextBaseline.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants(UseNames = true)]
    public enum TextBaseline {

        Top = 0,

        Hanging = 1,

        Middle = 2,

        Bottom = 3,

        Alphabetic = 4,

        Ideographic = 5
    }
}
// TextMetrics.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Media.Graphics {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class TextMetrics {

        private TextMetrics() {
        }

        [ScriptField]
        public double Width {
            get {
                return 0f;
            }
        }
    }
}
// GeoCoordinates.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Services {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class GeoCoordinates {

        private GeoCoordinates() {
        }

        [ScriptField]
        public double Accuracy {
            get {
                return 0.0;
            }
        }

        [ScriptField]
        public double Altitude {
            get {
                return 0.0;
            }
        }

        [ScriptField]
        public double AltitudeAccuracy {
            get {
                return 0.0;
            }
        }

        [ScriptField]
        public double Heading {
            get {
                return 0.0;
            }
        }

        [ScriptField]
        public double Latitude {
            get {
                return 0.0;
            }
        }

        [ScriptField]
        public double Longitude {
            get {
                return 0.0;
            }
        }

        [ScriptField]
        public double Speed {
            get {
                return 0.0;
            }
        }
    }
}
// Geolocation.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Services {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class Geolocation {

        private Geolocation() {
        }

        [ScriptField]
        [ScriptName(""coords"")]
        public GeoCoordinates Coordinates {
            get {
                return null;
            }
        }

        [ScriptField]
        public int Timestamp {
            get {
                return 0;
            }
        }
    }
}
// GeolocationError.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Services {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class GeolocationError {

        private GeolocationError() {
        }

        [ScriptField]
        public GeolocationErrorCode Code {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string Message {
            get {
                return null;
            }
        }
    }
}
// GeolocationErrorCode.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Services {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum GeolocationErrorCode {

        PermissionDenied = 1,

        PositionUnavailable = 2,

        Timeout = 3
    }
}
// GeolocationOptions.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Services {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptName(""Object"")]
    public sealed class GeolocationOptions {

        public GeolocationOptions(params object[] nameValuePairs) {
        }

        [ScriptField]
        public bool EnableHighAccuracy {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public int MaximumAge {
            get {
                return 0;
            }
            set {
            }
        }

        [ScriptField]
        public int Timeout {
            get {
                return 0;
            }
            set {
            }
        }
    }
}
// GeolocationService.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.Services {

    public delegate void GeolocationSuccessCallback(Geolocation location);

    public delegate void GeolocationErrorCallback(GeolocationError error);

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class GeolocationService {

        private GeolocationService() {
        }

        public void ClearWatch(object watchCookie) {
        }

        public void GetCurrentPosition(GeolocationSuccessCallback successCallback) {
        }

        public void GetCurrentPosition(GeolocationSuccessCallback successCallback, GeolocationErrorCallback errorCallback) {
        }

        public void GetCurrentPosition(GeolocationSuccessCallback successCallback, GeolocationErrorCallback errorCallback, GeolocationOptions options) {
        }

        public object WatchPosition(GeolocationSuccessCallback successCallback) {
            return null;
        }

        public object WatchPosition(GeolocationSuccessCallback successCallback, GeolocationErrorCallback errorCallback) {
            return null;
        }

        public object WatchPosition(GeolocationSuccessCallback successCallback, GeolocationErrorCallback errorCallback, GeolocationOptions options) {
            return null;
        }
    }
}
// StyleCharsetRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleCharsetRule : StyleRule {

        private StyleCharsetRule() {
        }

        [ScriptField]
        public string Encoding {
            get {
                return null;
            }
            set {
            }
        }
    }
}
// StyleCSSRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleCSSRule : StyleRule {

        private StyleCSSRule() {
        }

        [ScriptField]
        public string SelectorText {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public Style Style {
            get {
                return null;
            }
        }
    }
}
// StyleFontFaceRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleFontFaceRule : StyleRule {

        private StyleFontFaceRule() {
        }

        [ScriptField]
        public Style Style {
            get {
                return null;
            }
        }
    }
}
// StyleImportRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleImportRule : StyleRule {

        private StyleImportRule() {
        }

        [ScriptField]
        public string Href {
            get {
                return null;
            }
        }

        [ScriptField]
        public StyleMediaList Media {
            get {
                return null;
            }
        }

        [ScriptField]
        public StyleSheet StyleSheet {
            get {
                return null;
            }
        }
    }
}
// StyleMediaList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleMediaList : IEnumerable {

        private StyleMediaList() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public string MediaText {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string this[int index] {
            get {
                return null;
            }
        }

        public void AppendMedium(string newMedium) {
        }

        public void DeleteMedium(string oldMedium) {
        }

        public static implicit operator string[](StyleMediaList list) {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return null;
        }
    }
}
// StyleMediaRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleMediaRule : StyleRule {

        private StyleMediaRule() {
        }

        [ScriptField]
        public StyleMediaList Media {
            get {
                return null;
            }
        }

        [ScriptField]
        [ScriptName(""cssRules"")]
        public StyleRuleList StyleRules {
            get {
                return null;
            }
        }

        public void DeleteRule(int index) {
        }

        public int InsertRule(string rule, int index) {
            return 0;
        }
    }
}
// StylePageRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StylePageRule : StyleRule {

        private StylePageRule() {
        }

        [ScriptField]
        public string SelectorText {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public Style Style {
            get {
                return null;
            }
        }
    }
}
// StyleRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public abstract class StyleRule {

        internal StyleRule() {
        }

        [ScriptField]
        [ScriptName(""cssText"")]
        public string CSSText {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public StyleRule ParentRule {
            get {
                return null;
            }
        }

        [ScriptField]
        public StyleSheet ParentStyleSheet {
            get {
                return null;
            }
        }

        [ScriptField]
        public StyleRuleType Type {
            get {
                return StyleRuleType.Unknown;
            }
        }
    }
}
// StyleRuleList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleRuleList : IEnumerable {

        private StyleRuleList() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public StyleRule this[int index] {
            get {
                return null;
            }
        }

        public static implicit operator StyleRule[](StyleRuleList list) {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return null;
        }
    }
}
// StyleRule.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    [ScriptConstants]
    public enum StyleRuleType {

        Unknown = 0,

        Style = 1,

        Charset = 2,

        Import = 3,

        Media = 4,

        FontFace = 5,

        Page = 6,

    }
}
// StyleSheet.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleSheet {

        private StyleSheet() {
        }

        [ScriptField]
        [ScriptName(""cssRules"")]
        public StyleRuleList StyleRules {
            get {
                return null;
            }
        }

        [ScriptField]
        public bool Disabled {
            get {
                return false;
            }
            set {
            }
        }

        [ScriptField]
        public string Href {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public string Id {
            get {
                return null;
            }
            set {
            }
        }

        [ScriptField]
        public StyleMediaList Media {
            get {
                return null;
            }
        }

        [ScriptField]
        public Element OwnerNode {
            get {
                return null;
            }
        }

        [ScriptField]
        public StyleRule OwnerRule {
            get {
                return null;
            }
        }

        [ScriptField]
        public StyleSheet ParentStyleSheet {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Title {
            get {
                return null;
            }
        }

        [ScriptField]
        public string Type {
            get {
                return null;
            }
        }

        public void DeleteRule(int index) {
        }

        public int InsertRule(string rule, int index) {
            return 0;
        }
    }
}
// StyleSheetList.cs
// Script#/Libraries/Web
// This source code is subject to terms and conditions of the Apache License, Version 2.0.
//

using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Html.StyleSheets {

    [ScriptIgnoreNamespace]
    [ScriptImport]
    public sealed class StyleSheetList : IEnumerable {

        private StyleSheetList() {
        }

        [ScriptField]
        public int Length {
            get {
                return 0;
            }
        }

        [ScriptField]
        public StyleSheet this[int index] {
            get {
                return null;
            }
        }

        public static implicit operator StyleSheet[](StyleSheetList list) {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return null;
        }
    }
}




        ";


    }
}
