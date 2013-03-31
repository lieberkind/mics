using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiCS.Mappers;
using System.Threading.Tasks;

namespace MiCS.Walkers
{
    public class NamespaceWalker : SyntaxWalker
    {
        public readonly List<ScriptSharp.ScriptModel.NamespaceSymbol> scriptSharpNamespaces = new List<ScriptSharp.ScriptModel.NamespaceSymbol>();

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {          
            scriptSharpNamespaces.Add(node.Map());

            base.VisitNamespaceDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.NamespaceSymbol> Maps(SyntaxNode node)
        {
            var namespaceWalker = new NamespaceWalker();
            namespaceWalker.Visit(node);
            return namespaceWalker.scriptSharpNamespaces;
        }

        public static ScriptSharp.ScriptModel.NamespaceSymbol Map(SyntaxNode node)
        {
            var namespaces = NamespaceWalker.Maps(node);
            if (namespaces.Count != 1)
                throw new Exception("There are not exactly one namespace!");

            return namespaces.First();
        }
    }
}
