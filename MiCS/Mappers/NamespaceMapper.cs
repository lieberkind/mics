using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiCS.Extensions;
using System.Threading.Tasks;

namespace MiCS.Mappers
{
    public class NamespaceMapper : SyntaxWalker
    {
        public readonly List<ScriptSharp.ScriptModel.NamespaceSymbol> ssNamespaces = new List<ScriptSharp.ScriptModel.NamespaceSymbol>();

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            //var ssNamespace = Map(node);

            

            //MapChildren(ssNamespace, node);

            //ssNamespace.MapChildren(node);
            
            ssNamespaces.Add(node.Map());

            base.VisitNamespaceDeclaration(node);
        }

        public ScriptSharp.ScriptModel.NamespaceSymbol Map(NamespaceDeclarationSyntax roslynNamespace)
        { 
            return new ScriptSharp.ScriptModel.NamespaceSymbol(((IdentifierNameSyntax)roslynNamespace.Name).Identifier.ValueText, null);
        }

        public void MapChildren(ScriptSharp.ScriptModel.NamespaceSymbol scriptSharpNamespace, NamespaceDeclarationSyntax roslynNamespace) 
        {
            var classMapper = new ClassMapper(scriptSharpNamespace);
            classMapper.Visit(roslynNamespace);

            foreach (var ssClass in classMapper.ssClasses)
            {
                scriptSharpNamespace.Types.Add(ssClass);
            }
        }
    }
}
