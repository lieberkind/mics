using Roslyn.Compilers.CSharp;
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

            //var statementBuilder = new StatementBuilder(

            ssMethods.Add(ssMethod);

            //base.VisitMethodDeclaration(node);
        }
    }
}
