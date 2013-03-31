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

        public static List<ScriptSharp.ScriptModel.ClassSymbol> Maps(SyntaxNode node, ScriptSharp.ScriptModel.NamespaceSymbol requiredNamespaceReference)
        {
            var classWalker = new ClassWalker(requiredNamespaceReference);
            classWalker.Visit(node);
            return classWalker.scriptSharpClasses;
        }
 
        public static ScriptSharp.ScriptModel.ClassSymbol Map(SyntaxNode node, ScriptSharp.ScriptModel.NamespaceSymbol requiredNamespaceReference)
        {
            var classes = ClassWalker.Maps(node, requiredNamespaceReference);
            if (classes.Count != 1)
                throw new Exception("There are not exactly one class!");

            return classes.First();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            scriptSharpClasses.Add(node.Map(parentNamespace));

            //base.VisitClassDeclaration(node);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            foreach (var roslynClass in node.Members)
            {
                scriptSharpClasses.Add(ClassWalker.Map(roslynClass, parentNamespace));
            }
            
            //base.VisitNamespaceDeclaration(node);
        }


    }

}
