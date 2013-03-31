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
        public readonly List<ScriptSharp.ScriptModel.NamespaceSymbol> ssNamespaces = new List<ScriptSharp.ScriptModel.NamespaceSymbol>();

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {          
            ssNamespaces.Add(node.Map());

            base.VisitNamespaceDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.NamespaceSymbol> Visit(NamespaceDeclarationSyntax node)
        {
            var namespaceWalker = new NamespaceWalker();
            namespaceWalker.VisitNamespaceDeclaration(node);
            return namespaceWalker.ssNamespaces;
        }
    }
}
