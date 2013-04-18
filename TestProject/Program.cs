using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;
using MiCS;
using System.IO;
using MiCS.Validators;
using System.Diagnostics;
using MiCS.Builders;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = MiCSUtilities.GetSyntaxTree(@"C:\Users\Tomas Lieberkind\Documents\Visual Studio 2012\Projects\MiCS\MiCS.sln", "Person.cs");

            MiCSManager.Initiate(st);

            //MiCSValidator validator = new MiCSValidator(MiCSManager.CompilationUnit);

            //validator.ValidateTree();

            //var namespaceWalker = new NamespaceBuilder();
            //namespaceWalker.Visit(st.GetRoot());

            //var root = MiCSManager.CompilationUnit;

            //Collector c = new Collector(root, "MixedSide");

            //var namespaceBuilder = new NamespaceBuilder();
            //namespaceBuilder.Visit(root);

            //var re = new RegExp("hello");
            

            //Console.Read();
        }
    }
}
