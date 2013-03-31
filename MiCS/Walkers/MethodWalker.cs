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

            //base.VisitMethodDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            foreach (var roslynMethod in node.Members)
            {
                scriptSharpMethods.Add(MethodWalker.Map(roslynMethod, parentClass, parentNamespace));
            }

            //base.VisitClassDeclaration(node);
        }

        public static List<ScriptSharp.ScriptModel.MethodSymbol> Maps(SyntaxNode node,
            ScriptSharp.ScriptModel.ClassSymbol requiredClassReference,
            ScriptSharp.ScriptModel.NamespaceSymbol requiredNamespaceReference)
        {
            var methodWalker = new MethodWalker(requiredClassReference, requiredNamespaceReference);
            methodWalker.Visit(node);
            return methodWalker.scriptSharpMethods;
        }

        public static ScriptSharp.ScriptModel.MethodSymbol Map(SyntaxNode node,
            ScriptSharp.ScriptModel.ClassSymbol requiredClassReference,
            ScriptSharp.ScriptModel.NamespaceSymbol requiredNamespaceReference)
        {
            var scriptSharpMethods = MethodWalker.Maps(node, requiredClassReference, requiredNamespaceReference);
            if (scriptSharpMethods.Count != 1)
                throw new Exception("There are not exactly one method.");

            return scriptSharpMethods.First();
        }
    }
}
