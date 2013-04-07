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
            var ssClass = @class.Map(ssParentNamespace);

            var methodBuilder = new MethodBuilder(ssClass, ssParentNamespace);
            methodBuilder.Visit(@class);

            ssClass.Members.AddRange(methodBuilder.ssMethods);

            ssClasses.Add(ssClass);

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
