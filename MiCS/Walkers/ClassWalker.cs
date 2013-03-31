using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Walkers
{
    public class ClassWalker : SyntaxWalker
    {
        ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace;
        public readonly List<ScriptSharp.ScriptModel.ClassSymbol> scriptSharpClasses = new List<ScriptSharp.ScriptModel.ClassSymbol>();

        public ClassWalker(ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace)
        {
            this.parentNamespace = parentNamespace;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            scriptSharpClasses.Add(node.Map(parentNamespace));

            base.VisitClassDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.ClassSymbol> GetClassesIn(NamespaceDeclarationSyntax roslynNamespace, ScriptSharp.ScriptModel.NamespaceSymbol requiredNamespaceReference)
        {
            var classWalker = new ClassWalker(requiredNamespaceReference);
            classWalker.Visit(roslynNamespace);
            return classWalker.scriptSharpClasses;
        }

    }

}
