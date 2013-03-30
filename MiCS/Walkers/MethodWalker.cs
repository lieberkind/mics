using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Walkers
{
    class MethodWalker : SyntaxWalker
    {
        ScriptSharp.ScriptModel.ClassSymbol parentClass;
        public readonly List<ScriptSharp.ScriptModel.MethodSymbol> ssMethods = new List<ScriptSharp.ScriptModel.MethodSymbol>();

        public MethodWalker(ScriptSharp.ScriptModel.ClassSymbol parentClass)
        {
            this.parentClass = parentClass;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var ssMethod = Map(node);
            
            ssMethods.Add(ssMethod);
            MapChildren(ssMethod, node);

            base.VisitMethodDeclaration(node);
        }

        public ScriptSharp.ScriptModel.MethodSymbol Map(MethodDeclarationSyntax roslynMethod)
        {
            return new ScriptSharp.ScriptModel.MethodSymbol(roslynMethod.Identifier.ValueText, parentClass, null);
        }

        public void MapChildren(ScriptSharp.ScriptModel.MethodSymbol scriptSharpMethod, MethodDeclarationSyntax roslynMethod)
        {
            var statementMapper = new StatementWalker();
            statementMapper.Visit(roslynMethod);

            var symbolImplementation = new SymbolImplementation(statementMapper.ssStatements, null, scriptSharpMethod.GeneratedName);
            scriptSharpMethod.AddImplementation(symbolImplementation);
        }
    }
}
