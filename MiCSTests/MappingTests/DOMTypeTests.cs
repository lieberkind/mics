using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using MiCS;
using MiCS.Mappers;
using MiCSTests.TestUtils;
using MiCS.Builders;
using MiCS.Validators;

namespace MiCSTests
{
    [TestClass]
    public class DOMTypeTests
    {

        [TestMethod]
        public void DOMType_InvocationTest()
        {
            var source = @"
            using System.Html;
            namespace TestNamespace { 
                class TestClass { 
                    [ClientSide]
                    bool f() { Element e = new Element(); e.Blur(); }
                } 

            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
            var ssClass = ssNamespace.Types.First();
            var ssMethod = (SS.MethodSymbol)ssClass.Members.First();
            var ssStatement = (SS.ExpressionStatement)ssMethod.Implementation.Statements.ElementAt(1);
            var ssInvocation = (SS.MethodExpression)ssStatement.Expression;
            var ssMethodSymbol = (SS.MethodSymbol)ssInvocation.Method;
            var ssLocalExpression = (SS.LocalExpression)ssInvocation.ObjectReference;

            Assert.AreEqual(ssMethodSymbol.AssociatedType.Name, "Void");
            Assert.AreEqual(ssLocalExpression.Symbol.Name, "e");
            Assert.AreEqual(ssMethodSymbol.Name, "Blur");
        }

        [TestMethod]
        public void DOMType_StaticInvocationTest()
        {
            var source = @"
            using System.Html;
            namespace TestNamespace { 
                class TestClass { 
                    [ClientSide]
                    bool f() { return Document.HasFocus(); }
                } 

            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
            var ssClass = ssNamespace.Types.First();
            var ssMethod = (SS.MethodSymbol)ssClass.Members.First();
            var ssReturnStatement = (SS.ReturnStatement)ssMethod.Implementation.Statements.First();
            var ssInvocation = (SS.MethodExpression)ssReturnStatement.Value;
            var ssMethodSymbol = (SS.MethodSymbol)ssInvocation.Method;
            var ssLocalExpression = (SS.LocalExpression)ssInvocation.ObjectReference;

            Assert.AreEqual(ssMethodSymbol.AssociatedType.Name, "Boolean");
            Assert.AreEqual(ssMethodSymbol.Name, "HasFocus");
            Assert.AreEqual(ssLocalExpression.Symbol.Name, "document");
            /*
             * The last assert is expecting the Document script name
             * (document) as static references are translated to 
             * script names before ScriptSharp script generation. This
             * is not the case for non static references.
             */


        }

        [TestMethod]
        public void DOMType_Test()
        {
            var source = @"
                [ClientSide]
                public void TestMethod() 
                {
                    Element e = new Element(); 
                    var e2 = Document.GetElementById(""ewjde"");
                }";
            var ssStatement = Parse.MethodsToSS(source);
        }
    }
}
