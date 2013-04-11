﻿using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Builders
{
    class MethodBuilder : SyntaxWalker
    {
        SS.ClassSymbol ssParentClass;
        SS.NamespaceSymbol ssParentNamespace;
        public readonly List<SS.MethodSymbol> ssMethods = new List<SS.MethodSymbol>();

        public MethodBuilder(SS.ClassSymbol ssParentClass, SS.NamespaceSymbol ssParentNamespace)
        {
            this.ssParentClass = ssParentClass;
            this.ssParentNamespace = ssParentNamespace;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax method)
        {
            var ssMethod = method.Map(ssParentClass, ssParentNamespace);

            //var ssMethodImplementation = new SS.SymbolImplementation(
            //ssMethod.AddImplementation(
            //var statementBuilder = new StatementBuilder(

            ssMethods.Add(ssMethod);

            //base.VisitMethodDeclaration(node);
        }

        public static SS.MethodSymbol Build(SyntaxNode node, SS.ClassSymbol ssClass, SS.NamespaceSymbol ssNamespace)
        {
            var methodBuilder = new MethodBuilder(ssClass, ssNamespace);
            methodBuilder.Visit(node);

            if (methodBuilder.ssMethods.Count != 1)
                throw new Exception("Trying to build a single method but there are multiple!");

            return methodBuilder.ssMethods.First();
        }

        public static List<SS.MethodSymbol> BuildList(SyntaxNode node, SS.ClassSymbol ssClass, SS.NamespaceSymbol ssNamespace)
        {
            var methodBuilder = new MethodBuilder(ssClass, ssNamespace);
            methodBuilder.Visit(node);

            return methodBuilder.ssMethods;
        }
    }
}