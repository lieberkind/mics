﻿using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCSTests.TestUtils
{
    public static class Parse
    {

        public static CompilationUnitSyntax CompilationUnit(string source)
        {
            var syntaxTree = SyntaxTree.ParseText(source);
            if (!Syntax.IsCompleteSubmission(syntaxTree))
                throw new Exception("Source submission failed!");
            return syntaxTree.GetRoot();
        }

        public static IEnumerable<SyntaxNode> Namespaces(string source)
        {
            var cUnit = CompilationUnit(source);
            var namespaces = cUnit.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration);
            return namespaces;
        }

        public static IEnumerable<SyntaxNode> Classes(string classSource)
        {
            var cUnit = CompilationUnit(@"namespace TestNameSpace{ " + classSource + @" }");
            var namespaces = cUnit.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration);
            return namespaces.First().DescendantNodes().Where(m => m.Kind == SyntaxKind.ClassDeclaration);
        }

        public static IEnumerable<SyntaxNode> Methods(string methodsSource)
        {
            var classes = Classes(@"class TestClass{ " + methodsSource + @" }");
            var methods = classes.First().DescendantNodes().Where(m => m.Kind == SyntaxKind.MethodDeclaration);
            return methods;
        }
    }
}
