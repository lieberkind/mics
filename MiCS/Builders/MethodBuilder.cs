using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Builders
{
    class MethodBuilder : SyntaxWalker
    {
        SS.ClassSymbol ssParentClass;
        SS.NamespaceSymbol ssParentNamespace;
        public readonly List<SS.MethodSymbol> ssMethods = new List<SS.MethodSymbol>();

        public MethodBuilder(SS.ClassSymbol ssParentClass, SS.NamespaceSymbol ssParentNamespace)
        {
            this.ssParentClass = ssParentClass;
            this.ssParentNamespace = ssParentNamespace;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax method)
        {
            var isClientSide = TypeManager.IsClientSideMethod(ssParentNamespace.Name, ssParentClass.Name, method.Identifier.ValueText);
                //MiCSManager.ClientSideMembers.ContainsKey(ssParentNamespace.Name) &&
                //MiCSManager.ClientSideMembers[ssParentNamespace.Name].ContainsKey(ssParentClass.Name) &&
                //MiCSManager.ClientSideMembers[ssParentNamespace.Name][ssParentClass.Name].Contains(method.Identifier.ValueText);

            var isMixedSide = TypeManager.IsMixedSideMethod(ssParentNamespace.Name, ssParentClass.Name, method.Identifier.ValueText);
                //MiCSManager.MixedSideMembers.ContainsKey(ssParentNamespace.Name) &&
                //MiCSManager.MixedSideMembers[ssParentNamespace.Name].ContainsKey(ssParentClass.Name) &&
                //MiCSManager.MixedSideMembers[ssParentNamespace.Name][ssParentClass.Name].Contains(method.Identifier.ValueText);

            if (isClientSide || isMixedSide)
            {
                var ssMethod = method.Map(ssParentClass, ssParentNamespace);
                ssMethods.Add(ssMethod);
            }

        }

        public static SS.MethodSymbol Build(SyntaxNode node, SS.ClassSymbol ssClass, SS.NamespaceSymbol ssNamespace)
        {
            var methodBuilder = new MethodBuilder(ssClass, ssNamespace);
            methodBuilder.Visit(node);

            if (methodBuilder.ssMethods.Count != 1)
                throw new Exception("Trying to build a single method but there are multiple!");

            return methodBuilder.ssMethods.First();
        }

        public static List<SS.MethodSymbol> BuildList(SyntaxNode node, SS.ClassSymbol ssClass, SS.NamespaceSymbol ssNamespace)
        {
            var methodBuilder = new MethodBuilder(ssClass, ssNamespace);
            methodBuilder.Visit(node);

            return methodBuilder.ssMethods;
        }
    }
}
