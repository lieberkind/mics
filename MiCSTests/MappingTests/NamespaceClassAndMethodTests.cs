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

namespace MiCSTests.MappingTests
{
    [TestClass]
    public class NamespaceClassAndMethodTests
    {
        [TestMethod]
        public void Namespace_MemberTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { }
                } 
            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            Assert.AreEqual(@namespace.Members.Count, ssNamespace.Types.Count);
            Assert.AreEqual(member.Identifier.ValueText, ssMember.Name);
        }

        [TestMethod]
        public void Namespace_MultipleTest()
        {
            var source = @"
            using System.Html;
            namespace TestNamespace1 { 
                class TestClass1 { 
                    [ClientSide]
                    void g() { Document.HasFocus(); }
                }
            }
            namespace TestNamespace2 { 
                class TestClass2 { 
                    [ClientSide]
                    void f() { Document.HasFocus(); }
                }
            }
            ";
            var ssSymbolSet = Parse.NamespacesToSymbolSet(source);

        }

        [TestMethod]
        public void Class_EmptyMemberTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { }
                } 
            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            Assert.AreEqual(method.Identifier.ValueText, ssMethod.Name);
            Assert.AreEqual(method.Body.Statements.Count, ssMethod.Implementation.Statements.Count);
        }

        [TestMethod]
        public void Class_MemberTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { int i = 0; }
                } 
            }";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            var statement = method.Body.Statements.First();
            var ssStatement = ssMethod.Implementation.Statements.First();

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);
        }

        [TestMethod]
        public void Class_MemberDeclarationPredefinedReturnTypeTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    void f() { int i; }
                } 
            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            var returnTypeName = ((PredefinedTypeSyntax)method.ReturnType).Keyword.ValueText;
            Assert.AreEqual(returnTypeName, ssMethod.AssociatedType.Name.ToLower());

            var statement = method.Body.Statements.First();
            var ssStatement = ssMethod.Implementation.Statements.First();

            Assert.IsTrue(statement is LocalDeclarationStatementSyntax);
            Assert.IsTrue(ssStatement is SS.VariableDeclarationStatement);
        }

        [TestMethod]
        public void Class_MemberDeclarationCustomReturnTypeTest()
        {
            var source = @"
            namespace TestNamespace { 
                class TestClass { 
                    [MixedSide]
                    MyType f() { return new MyType(); }
                } 

                class MyType { 
                    [MixedSide]
                    void f() { int i; }
                } 
            }";
            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);

            var member = (ClassDeclarationSyntax)@namespace.Members.First();
            var ssMember = (SS.ClassSymbol)ssNamespace.Types.First();

            var method = (MethodDeclarationSyntax)member.Members.First();
            var ssMethod = (ScriptSharp.ScriptModel.MethodSymbol)ssMember.Members.First();

            var returnTypeName = ((IdentifierNameSyntax)method.ReturnType).Identifier.ValueText;
            Assert.AreEqual(returnTypeName, ssMethod.AssociatedType.Name);

        }

        // Todo: Test nested namespaces
            //        namespace TestNameSpace.Nested.NestedAgain.Andagain {
            //    class Lol {
            //        [MixedSide]
            //        void lol() { }
            //    }
            //}
    }
}
