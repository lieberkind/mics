using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiCS.Validators;
using Roslyn.Compilers.CSharp;
using MiCS;

namespace MiCSTests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void CollectorCollectsWhenThereAreMethodsToCollect()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        [MixedSide]
                        public void MethodWithoutMixedSideAttribute()
                        {
                        }

                        [MixedSide]
                        public void TestFunction(string name, string name2, string name3)
                        {
                        }
                    }

                    public class Animal
                    {
                        [MixedSide]
                        public void Bark()
                        {
                        }

                        public void IShouldNotBeAddedToTheDictionary()
                        {
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);
            var collector = new Collector(st.GetRoot(), "MixedSide");
            collector.Collect();

            Assert.AreEqual(collector.Members["Person"].Count, 2);
            Assert.AreEqual(collector.Members["Animal"].Count, 1);
        }

        [TestMethod]
        public void MixedSideValidationPassesWhenTreeIsValid()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        public Person(string name)
                        {

                        }

                        [MixedSide]
                        public void MethodWithMixedSideAttribute()
                        {
                        }

                        [MixedSide]
                        public void TestFunction(string name, string name2, string name3)
                        {
                            Person p = new Person(""Tomas"");
                            p.MethodWithMixedSideAttribute();
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            var mixedSideValidator = new MixedSideValidator(MiCSManager.CompilationUnit);
            mixedSideValidator.Validate();

            Assert.IsTrue(mixedSideValidator.IsValid);
        }

        [TestMethod]
        public void MixedSideValidationFailWhenMixedSideCallsNonMixedSide()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        public Person(string name)
                        {

                        }

                        public void MethodWithMixedSideAttribute()
                        {
                        }

                        [MixedSide]
                        public void TestFunction(string name, string name2, string name3)
                        {
                            Person p = new Person(""Tomas"");
                            p.MethodWithMixedSideAttribute();
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            var mixedSideValidator = new MixedSideValidator(MiCSManager.CompilationUnit);
            mixedSideValidator.Validate();

            Assert.IsFalse(mixedSideValidator.IsValid);
        }

        [TestMethod]
        public void ClientSideValidationPassesWhenTreeIsValid()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        public Person(string name)
                        {

                        }

                        [MixedSide]
                        public void IDontDoAnythingButAtLeastImMixedSide()
                        {
                        }

                        [ClientSide]
                        public void MethodWithMixedSideAttribute()
                        {
                        }

                        [ClientSide]
                        public void TestFunction(string name, string name2, string name3)
                        {
                            Person p = new Person(""Tomas"");
                            p.MethodWithMixedSideAttribute();
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            var clientSideValidator = new ClientSideValidator(MiCSManager.CompilationUnit);
            clientSideValidator.Validate();

            Assert.IsTrue(clientSideValidator.IsValid);
        }

        [TestMethod]
        public void ClientSideValidationFailWhenMixedSideCallsNonMixedOrClientSide()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        public Person(string name)
                        {

                        }

                        [MixedSide]
                        public void IDontDoAnythingButAtLeastImMixedSide()
                        {
                        }

                        public void MethodWithMixedSideAttribute()
                        {
                        }

                        [ClientSide]
                        public void TestFunction(string name, string name2, string name3)
                        {
                            Person p = new Person(""Tomas"");
                            p.MethodWithMixedSideAttribute();
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            var clientSideValidator = new ClientSideValidator(MiCSManager.CompilationUnit);
            clientSideValidator.Validate();

            Assert.IsFalse(clientSideValidator.IsValid);
        }
    }
}
