using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiCS.Validators;
using Roslyn.Compilers.CSharp;
using MiCS;
using System.Collections.Generic;

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
            var collector = new Collector(st.GetRoot(), new List<string>() { "MixedSide" });
            collector.Collect();

            Assert.AreEqual(collector.Members["ScriptLibrary1"]["Person"].Count, 2);
            Assert.AreEqual(collector.Members["ScriptLibrary1"]["Animal"].Count, 1);
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
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string>() { "MixedSide" });
            collector.Collect();

            MiCSManager.Initiate(st);

            var mixedSideValidator = new Validator(root, collector.Members);
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
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string>() { "MixedSide"});
            collector.Collect();

            MiCSManager.Initiate(st);

            var mixedSideValidator = new Validator(root, collector.Members);
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
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string>() { "ClientSide" });
            collector.Collect();

            MiCSManager.Initiate(st);

            var clientSideValidator = new Validator(root, collector.Members);
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
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string>() { "ClientSide" });
            collector.Collect();

            MiCSManager.Initiate(st);

            var clientSideValidator = new Validator(root, collector.Members);
            clientSideValidator.Validate();

            Assert.IsFalse(clientSideValidator.IsValid);
        }


        [TestMethod]
        public void InstanceCreationOfNonMixedOrClientSideClassFails()
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
                    }

                    public class SomeClass
                    {
                        [MixedSide]
                        public void someMethod()
                        {
                            var p = new Person(""Person Name"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string> { "MixedSide" });
            collector.Collect();

            MiCSManager.Initiate(st);

            var mixedSideValidator = new Validator(root, collector.Members);
            mixedSideValidator.Validate();

            Assert.IsFalse(mixedSideValidator.IsValid);
        }

        [TestMethod]
        public void ObjectCreationIsPossibleIfObjectIsMixedSide()
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
                        public void SomeMethod() 
                        {
                        }
                    }

                    public class SomeClass
                    {
                        [MixedSide]
                        public void SomeMethod()
                        {
                            var p = new Person(""Person Name"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string> { "MixedSide" });
            collector.Collect();

            MiCSManager.Initiate(st);

            var mixedSideValidator = new Validator(root, collector.Members);
            mixedSideValidator.Validate();

            Assert.IsTrue(mixedSideValidator.IsValid);
        }

        [TestMethod]
        public void ClientSideCanCreateMixedSideObjects()
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
                        public void SomeMethod() 
                        {
                        }
                    }

                    public class SomeClass
                    {
                        [ClientSide]
                        public void SomeMethod()
                        {
                            var p = new Person(""Person Name"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);
            var root = st.GetRoot();

            var collector = new Collector(root, new List<string>() { "MixedSide", "ClientSide"} );
            collector.Collect();

            MiCSManager.Initiate(st);

            var clientSideValidator = new Validator(root, collector.Members);
            clientSideValidator.Validate();

            Assert.IsTrue(clientSideValidator.IsValid);
        }
    }
}
