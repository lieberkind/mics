using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    class Collector : SyntaxWalker
    {
        string attributeName;
        List<string> currentMethods;
        CompilationUnitSyntax compilationUnit;

        public Dictionary<string, List<string>> Members
        {
            get;
            private set;
        }

        public Collector(CompilationUnitSyntax compilationUnit, string attributeName)
        {
            this.compilationUnit = compilationUnit;
            this.attributeName = attributeName;
            currentMethods = new List<string>();
            Members = new Dictionary<string, List<string>>();
        }

        public void Collect()
        {
            Visit(compilationUnit);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            currentMethods.Clear();

            base.VisitClassDeclaration(node);

            if (currentMethods.Count > 0)
                Members.Add(node.Identifier.ValueText, currentMethods.ToList());
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.hasAttribute(attributeName))
                currentMethods.Add(node.Identifier.ValueText);

            base.VisitMethodDeclaration(node);
        }
    }
}
