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

        public Collector(CompilationUnitSyntax compilationUnit, string attributeName = null)
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
                var namespaceName = node.GetFullName();

                if (!Members.ContainsKey(namespaceName))
                {
                    Members.Add(namespaceName, new Dictionary<string, List<string>>(currentNamespaceMembers));
                }
                else
                {
                    foreach (var key in currentNamespaceMembers.Keys)
                        Members[namespaceName].Add(key, currentNamespaceMembers[key]);
                }
            }
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            currentMethods.Clear();

            base.VisitClassDeclaration(node);

            if (currentMethods.Count > 0 && !currentNamespaceMembers.ContainsKey(node.Identifier.ValueText))
                currentNamespaceMembers.Add(node.Identifier.ValueText, currentMethods.ToList());
        }



        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (String.IsNullOrEmpty(attributeName) || node.HasAttribute(attributeName))
                currentMethods.Add(node.Identifier.ValueText);

            base.VisitMethodDeclaration(node);
        }

        #region Helper Methods
        private string GetFullName(NameSyntax name)
        {
            if (name is IdentifierNameSyntax)
            {
                return ((IdentifierNameSyntax)name).Identifier.ValueText;
            }
            else if (name is QualifiedNameSyntax)
            {
                var qualifiedName = (QualifiedNameSyntax)name;

                return GetFullName(qualifiedName.Left) + qualifiedName.DotToken.ValueText + GetFullName(qualifiedName.Right);
            }
            else
            {
                throw new NotSupportedException("The namesyntax on the provided namespace is not supported");
            }
        }
        #endregion
    }
}
