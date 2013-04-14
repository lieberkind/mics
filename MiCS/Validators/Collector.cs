using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    public class Collector : SyntaxWalker
    {
        string attributeName;
        Dictionary<string, List<string>> currentNamespaceMembers;
        List<string> currentMethods;
        CompilationUnitSyntax compilationUnit;

        public Dictionary<string, Dictionary<string, List<string>>> Members
        {
            get;
            private set;
        }

        public Collector(CompilationUnitSyntax compilationUnit, string attributeName)
        {
            this.compilationUnit = compilationUnit;
            this.attributeName = attributeName;

            currentMethods = new List<string>();
            currentNamespaceMembers = new Dictionary<string, List<string>>();
            Members = new Dictionary<string, Dictionary<string, List<string>>>();
        }

        public void Collect()
        {
            Visit(compilationUnit);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            currentNamespaceMembers.Clear();

            base.VisitNamespaceDeclaration(node);

            if (currentNamespaceMembers.Count > 0)
            {
                var namespaceName = ((IdentifierNameSyntax)node.Name).Identifier.ValueText;
                Members.Add(namespaceName, new Dictionary<string, List<string>>(currentNamespaceMembers));
            }
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            currentMethods.Clear();

            base.VisitClassDeclaration(node);

            if (currentMethods.Count > 0)
                currentNamespaceMembers.Add(node.Identifier.ValueText, currentMethods.ToList());
        }



        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.HasAttribute(attributeName))
                currentMethods.Add(node.Identifier.ValueText);

            base.VisitMethodDeclaration(node);
        }
    }
}
