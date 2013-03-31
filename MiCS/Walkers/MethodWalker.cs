using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Walkers
{
    class MethodWalker : SyntaxWalker
    {
        ScriptSharp.ScriptModel.ClassSymbol parentClass;
        ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace;
        public readonly List<ScriptSharp.ScriptModel.MethodSymbol> scriptSharpMethods = new List<ScriptSharp.ScriptModel.MethodSymbol>();

        public MethodWalker(ScriptSharp.ScriptModel.ClassSymbol parentClass,
            ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace)
        {
            this.parentClass = parentClass;
            this.parentNamespace = parentNamespace;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            scriptSharpMethods.Add(node.Map(parentClass, parentNamespace));

            base.VisitMethodDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.MethodSymbol> GetMethodsIn(ClassDeclarationSyntax roslynClass, 
            ScriptSharp.ScriptModel.ClassSymbol requiredClassReference, 
            ScriptSharp.ScriptModel.NamespaceSymbol requiredNamespaceReference)
        {
            var methodWalker = new MethodWalker(requiredClassReference, requiredNamespaceReference);
            methodWalker.Visit(roslynClass);
            return methodWalker.scriptSharpMethods;
        }
    }
}
