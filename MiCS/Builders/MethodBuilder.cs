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
    /// <summary>
    /// Builds ScriptSharp methods from Roslyn methods
    /// </summary>
    class MethodBuilder : SyntaxWalker
    {
        /// <summary>
        /// The ScriptSharp class in which the method is defined
        /// </summary>
        SS.ClassSymbol ssParentClass;

        /// <summary>
        /// The ScriptSharp namespace in which the method is defined
        /// </summary>
        SS.NamespaceSymbol ssParentNamespace;

        /// <summary>
        /// The ScriptSharp methods of the parent class
        /// </summary>
        public readonly List<SS.MethodSymbol> ssMethods = new List<SS.MethodSymbol>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBuilder"/> class.
        /// </summary>
        /// <param name="ssParentClass">The parent ScriptSharp class.</param>
        /// <param name="ssParentNamespace">The parent ScriptSharp namespace.</param>
        public MethodBuilder(SS.ClassSymbol ssParentClass, SS.NamespaceSymbol ssParentNamespace)
        {
            this.ssParentClass = ssParentClass;
            this.ssParentNamespace = ssParentNamespace;
        }

        /// <summary>
        /// Builds the specified method and all its descendant nodes
        /// </summary>
        /// <param name="method">The method.</param>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax method)
        {
            var isClientSide = TypeManager.IsClientSideMethod(ssParentNamespace.Name, ssParentClass.Name, method.Identifier.ValueText);
            var isMixedSide = TypeManager.IsMixedSideMethod(ssParentNamespace.Name, ssParentClass.Name, method.Identifier.ValueText);

            if (isClientSide || isMixedSide)
            {
                var ssMethod = method.Map(ssParentClass, ssParentNamespace);
                ssMethods.Add(ssMethod);
            }

        }


        /// <summary>
        /// Builds the methods in the specified Roslyn class and all of their descendant nodes.
        /// </summary>
        /// <param name="roslynClass">The Roslyn class which methods should be built</param>
        /// <param name="ssParentClass">The parent ScriptSharp class.</param>
        /// <param name="ssParentNamespace">The parent ScriptSharp namespace.</param>
        /// <returns></returns>
        public static List<SS.MethodSymbol> BuildMethods(ClassDeclarationSyntax roslynClass, SS.ClassSymbol ssParentClass, SS.NamespaceSymbol ssParentNamespace)
        {
            var methodBuilder = new MethodBuilder(ssParentClass, ssParentNamespace);
            methodBuilder.Visit(roslynClass);

            return methodBuilder.ssMethods;
        }
    }
}
