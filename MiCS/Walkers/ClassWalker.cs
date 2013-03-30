using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Walkers
{
    public class ClassWalker : SyntaxWalker
    {
        ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace;
        public readonly List<ScriptSharp.ScriptModel.ClassSymbol> ssClasses = new List<ScriptSharp.ScriptModel.ClassSymbol>();

        public ClassWalker(ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace)
        {
            this.parentNamespace = parentNamespace;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var ssClass = Map(node);
            MapChildren(ssClass, node);
            ssClasses.Add(ssClass);
            base.VisitClassDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.ClassSymbol> Visit(ClassDeclarationSyntax node, ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace)
        {
            var classWalker = new ClassWalker(parentNamespace);
            classWalker.VisitClassDeclaration(node);
            return classWalker.ssClasses;
        }
        
        public ScriptSharp.ScriptModel.ClassSymbol Map(ClassDeclarationSyntax roslynClass)
        {
            return new ScriptSharp.ScriptModel.ClassSymbol(roslynClass.Identifier.ValueText, parentNamespace);
        }

        public void MapChildren(ScriptSharp.ScriptModel.ClassSymbol scriptSharpClass, ClassDeclarationSyntax roslynClass)
        {
            var methodMapper = new MethodWalker(scriptSharpClass);
            methodMapper.Visit(roslynClass);

            foreach (var ssMethod in methodMapper.ssMethods)
            {
                scriptSharpClass.Members.Add(ssMethod);
            }
        }
    }
}
