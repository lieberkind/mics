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
    public class CoreTypeTests
    {

        [TestMethod]
        public void CoreTypeTestRegexCanBeMapped()
        {
            var source = @"
                using System.Text.RegularExpressions;
                
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                            Regex rx = new Regex(""imapattern""); 
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void CoreTypeTestRegexMethodCanBeMapped()
        {
            var source = @"
                using System.Text.RegularExpressions;
                
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                            Regex rx = new Regex(""regexpattern""); 
                            var b = rx.IsMatch(""targetstring"");
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void CoreTypeTestStringCanBeMapped()
        {
            var source = @"
                using System;
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                            String s = ""foo"";
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void CoreTypeTestStringPropertyCanBeMapped()
        {
            var source = @"
                using System;
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                            String s = ""foo"";
                            int i = s.Length;
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void CoreTypeTestStringMethodCanBeMapped()
        {
            var source = @"
                using System;
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void f() {
                            String s = ""foo"";
                            int j = s.IndexOf('o');
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CoreTypeTestDateCannotBeMapped()
        {
            var source = @"
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                           System.Date d = new Date(); 
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void CoreTypeTestVoid()
        {
            var source = @"
                namespace TestNamespace {
                    class TestClass {
                
                        [MixedSide]
                        public void ImARegEx() {
                           int i = 0; 
                        }
                    }
                }

            ";

            var @namespace = (NamespaceDeclarationSyntax)Parse.Namespaces(source).First();
            var ssNamespace = NamespaceBuilder.Build(@namespace);
        }

        [TestMethod]
        public void CoreTypeTestBool()
        {
            var source = @"bool b = true;";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.Boolean");
        }

        [TestMethod]
        [ExpectedException(typeof(MemberNotMappedException))]
        public void CoreTypeTestBooleanFail()
        {
            var source = @"bool b = true; b.GetType();";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;
        }

        [TestMethod]
        public void CoreTypeTestBoolean()
        {
            var source = @"Boolean b = true;";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.Boolean");
        }

        [TestMethod]
        public void CoreTypeTestBooleanNew()
        {
            var source = @"bool b = new Boolean();";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.Boolean");
        }

        [TestMethod]
        [ExpectedException(typeof(MemberSignatureArgumentTypeNotMappedException))]
        public void CoreTypeTestStringMemberFail()
        {
            var source = @"string s = ""foo""; s.IndexOf(""foo"")";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        [TestMethod]
        [ExpectedException(typeof(MemberSignatureNotMappedException))]
        public void CoreTypeTestStringMemberFailArgumentCount()
        {
            var source = @"string s = ""foo""; s.IndexOf('f', 2)";
            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;
        }

        [TestMethod]
        public void CoreTypeTestStringMember()
        {
            var source = @"string s = ""foo""; s.IndexOf('f')";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        [TestMethod]
        public void CoreTypeTestString()
        {
            var source = @"string s = ""foo"";";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }

        [TestMethod]
        public void CoreTypeTestStringNew()
        {
            var source = @"var s = new String(""foo"");";

            var ssStatement = (SS.VariableDeclarationStatement)Parse.StatementToSS(source);
            var ssVariableType = (SS.ClassSymbol)ssStatement.Variables.ElementAt(0).ValueType;

            Assert.AreEqual(ssVariableType.FullName, "System.String");
        }
    }
}
