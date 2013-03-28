using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using MiCS;

namespace MiCSTests
{
    [TestClass]
    public class CollectorTests
    {
        private SyntaxTree treeWithMixedSideAttribute = SyntaxTree.ParseText(@"
            using System;
 
            namespace HelloWorld
            {
                class Program
                {
                    [MixedSide]
                    static void Main(string[] args)
                    {
                        Console.WriteLine(""Hello, World!"");
                    }
                }
            }
        ");

        private SyntaxTree treeWithoutMixedSideAttribute = SyntaxTree.ParseText(@"
            using System;
 
            namespace HelloWorld
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        Console.WriteLine(""Hello, World!"");
                    }
                }
            }
        ");

        [TestMethod]
        public void ClassCollectorReturnsClassesIfHasMixedSideMethods()
        {
            var classCollector = new ClassCollector();
            classCollector.Visit(treeWithMixedSideAttribute.GetRoot());

            Assert.IsTrue(classCollector.Classes.Count > 0);
        }

        [TestMethod]
        public void ClassCollectorIsEmptyIfNotHasMixedSideMethods()
        {
            var classCollector = new ClassCollector();
            classCollector.Visit(treeWithoutMixedSideAttribute.GetRoot());

            Assert.IsTrue(classCollector.Classes.Count == 0);
        }

        
    }
}
