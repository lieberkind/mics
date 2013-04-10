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
        public void ValidationPassesWhenTreeIsValid()
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
    }
}
