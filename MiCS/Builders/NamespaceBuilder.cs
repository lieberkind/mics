using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiCS.Mappers;
using System.Threading.Tasks;
using SS = ScriptSharp.ScriptModel;

namespace MiCS.Builders
{
    public class NamespaceBuilder : SyntaxWalker
    {
        public readonly List<SS.NamespaceSymbol> ssNamespaces = new List<SS.NamespaceSymbol>();

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax @namespace)
        {
            var ssNamespace = @namespace.Map();

            var classBuilder = new ClassBuilder(ssNamespace);
            classBuilder.Visit(@namespace);

            ssNamespace.Types.AddRange(classBuilder.ssClasses);

            ssNamespaces.Add(ssNamespace);

            //base.VisitNamespaceDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.NamespaceSymbol> Maps(SyntaxNode node)
        {
            var namespaceWalker = new NamespaceBuilder();
            namespaceWalker.Visit(node);
            return namespaceWalker.ssNamespaces;
        }

        public static ScriptSharp.ScriptModel.NamespaceSymbol Map(SyntaxNode node)
        {
            var namespaces = NamespaceBuilder.Maps(node);
            if (namespaces.Count != 1)
                throw new Exception("There are not exactly one namespace!");

            return namespaces.First();
        }
    }
}
