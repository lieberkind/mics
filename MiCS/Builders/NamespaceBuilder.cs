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

    /// <summary>
    /// Builds ScriptSharp namespaces from Roslyn namespaces
    /// </summary>
    public class NamespaceBuilder : SyntaxWalker
    {
        public readonly List<SS.NamespaceSymbol> ssNamespaces = new List<SS.NamespaceSymbol>();

        /// <summary>
        /// Builds the specified namespace and its descendant nodes.
        /// </summary>
        /// <param name="namespace">The namespace.</param>
        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax @namespace)
        {
            var ssNamespace = @namespace.Map();

            var classBuilder = new ClassBuilder(ssNamespace);
            classBuilder.Visit(@namespace);

            ssNamespace.Types.AddRange(classBuilder.ssClasses);

            ssNamespaces.Add(ssNamespace);
        }

        /// <summary>
        /// Builds the specified namespace and its descendant nodes.
        /// </summary>
        /// <param name="namespace">The namespace.</param>
        public static SS.NamespaceSymbol Build(NamespaceDeclarationSyntax @namespace)
        {
            var namespaceBuilder = new NamespaceBuilder();
            namespaceBuilder.Visit(@namespace);

            return namespaceBuilder.ssNamespaces.First();
        }

    }
}
