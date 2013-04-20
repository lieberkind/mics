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
        public void CollectorCollectsAllMixedSideClassesInNamespace()
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
                        public void SomeOtherMethod()
                        {
                            var p = new Person(""Person Name"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            var collector = new Collector(st.GetRoot(), new List<string>() { "MixedSide" });
            collector.Collect();

            Assert.IsTrue(collector.Members["ScriptLibrary1"].Count == 2);
            Assert.IsTrue(collector.Members["ScriptLibrary1"]["Person"].Contains("SomeMethod"));
            Assert.IsTrue(collector.Members["ScriptLibrary1"]["SomeClass"].Contains("SomeOtherMethod"));
        }

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

            MiCSManager.Initiate(st);

            Assert.IsTrue(MiCSManager.UserTreeIsValid);
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

            Assert.IsFalse(MiCSManager.UserTreeIsValid);
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
                        public void MethodWithClientSideAttribute()
                        {
                        }

                        [ClientSide]
                        public void TestFunction(string name, string name2, string name3)
                        {
                            Person p = new Person(""Tomas"");
                            p.MethodWithClientSideAttribute();
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            Assert.IsTrue(MiCSManager.UserTreeIsValid);
        }

        [TestMethod]
        public void ValidatorAllowsCreationOfRegex()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Text.RegularExpressions;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        [MixedSide]
                        public bool CallToRegex()
                        {
                            Regex regEx = new Regex(""somepattern"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            Assert.IsTrue(MiCSManager.UserTreeIsValid);
        }

        [TestMethod]
        public void ValidatorAllowsRegexsIsMatchMethodToBeCalled()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Text.RegularExpressions;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class Person
                    {
                        [MixedSide]
                        public bool isValid()
                        {
                            Regex regEx = new Regex(""somepattern"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            Assert.IsTrue(MiCSManager.UserTreeIsValid);
        }

        [TestMethod]
        public void ClientSideValidationFailsWhenMixedSideCallsNonMixedOrClientSide()
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

            Assert.IsFalse(MiCSManager.UserTreeIsValid);
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

            MiCSManager.Initiate(st);

            Assert.IsFalse(MiCSManager.UserTreeIsValid);
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
                        public void SomeOtherMethod()
                        {
                            var p = new Person(""Person Name"");
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            Assert.IsTrue(MiCSManager.UserTreeIsValid);
        }

        [TestMethod]
        public void MixedSideCodeCantCallClientSideCode()
        {
            string treeText = @"
                using MiCS;
                using System;
                using System.Collections.Generic;
                using System.Runtime.CompilerServices;

                namespace ScriptLibrary1
                {
                    public class ClientSideClass
                    {
                        public ClientSideClass()
                        {
                        }

                        [ClientSide]
                        public void SomeClientSideMethod() 
                        {
                        }
                    }

                    public class SomeClass
                    {
                        [MixedSide]
                        public void SomeMethod()
                        {
                            var clientSideClass = new ClientSideClass();
                            clientSideClass.SomeClientSideMethod();
                        }
                    }
                }";

            var st = SyntaxTree.ParseText(treeText);

            MiCSManager.Initiate(st);

            Assert.IsFalse(MiCSManager.UserTreeIsValid);
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

            MiCSManager.Initiate(st);

            Assert.IsTrue(MiCSManager.UserTreeIsValid);
        }
    }
}
