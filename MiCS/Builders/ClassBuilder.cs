using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;
using SS = ScriptSharp.ScriptModel;

namespace MiCS.Builders
{

    /// <summary>
    /// Builds ScriptSharp classes from Roslyn classes
    /// </summary>
    public class ClassBuilder : SyntaxWalker
    {
        SS.NamespaceSymbol ssParentNamespace;
        public readonly List<SS.ClassSymbol> ssClasses = new List<SS.ClassSymbol>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassBuilder"/> class.
        /// </summary>
        /// <param name="ssParentNamespace">The parent ScriptSharp namespace.</param>
        public ClassBuilder(SS.NamespaceSymbol ssParentNamespace)
        {
            this.ssParentNamespace = ssParentNamespace;
        }

        /// <summary>
        /// Builds the specified class and all its descendant nodes.
        /// </summary>
        /// <remarks>Ignores JavaScript/DOM built in types.</remarks>
        /// <param name="class">The class.</param>
        public override void VisitClassDeclaration(ClassDeclarationSyntax @class)
        {
            if (@class.IsUserType())
            {
                var ssClass = @class.Map(ssParentNamespace);

                ssClass.Members.AddRange(MethodBuilder.BuildMethods(@class, ssClass, ssParentNamespace));

                ssClasses.Add(ssClass);
            }
        }

        /// <summary>
        /// Builds the specified class and all its descendant nodes.
        /// </summary>
        /// <param name="class">The class.</param>
        /// <param name="ssParentNamespace">The parent ScriptSharp namespace.</param>
        public static SS.ClassSymbol Build(ClassDeclarationSyntax @class, SS.NamespaceSymbol ssParentNamespace)
        {
            var classBuilder = new ClassBuilder(ssParentNamespace);
            classBuilder.Visit(@class);

            return classBuilder.ssClasses.First();
        }

    }

}
