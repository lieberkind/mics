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
    public class ClassBuilder : SyntaxWalker
    {
        SS.NamespaceSymbol ssParentNamespace;
        public readonly List<SS.ClassSymbol> ssClasses = new List<SS.ClassSymbol>();

        public ClassBuilder(SS.NamespaceSymbol ssParentNamespace)
        {
            this.ssParentNamespace = ssParentNamespace;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax @class)
        {
            // Ignore JavaScript/DOM built in types.
            if (@class.IsUserType())
            {
                var ssClass = @class.Map(ssParentNamespace);

                ssClass.Members.AddRange(MethodBuilder.BuildList(@class, ssClass, ssParentNamespace));

                ssClasses.Add(ssClass);
            }
            //base.VisitClassDeclaration(node);
        }

        public static SS.ClassSymbol Build(ClassDeclarationSyntax @class, SS.NamespaceSymbol ssNamespace)
        {
            var classBuilder = new ClassBuilder(ssNamespace);
            classBuilder.Visit(@class);

            return classBuilder.ssClasses.First();
        }

    }

}
