using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using MiCS;
using MiCSTests.TestUtils;

namespace MiCSTests
{
    /// <summary>
    /// Summary description for MapTests
    /// </summary>
    [TestClass]
    public class MapTests
    {


        [TestMethod]
        public void NamespaceNameTest()
        {
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(@"namespace TestNamespace{ }").First();
            var SSNamespace = RosNamespace.Map();

            Assert.AreEqual(RosNamespace.Name.ToString(), SSNamespace.Name);
        }

        [TestMethod]
        public void NamespaceMemberTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { }
                } 
            }";
            var RosNamespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var SSNamespace = RosNamespace.Map();

            var RosMember = (ClassDeclarationSyntax)RosNamespace.Members.First();
            var SSMember = (ClassSymbol)SSNamespace.Types.First();
            Assert.AreEqual(RosMember.Identifier.ValueText, SSMember.Name);
        }
    }
}
