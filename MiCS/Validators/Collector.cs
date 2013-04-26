using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    /// <summary>
    /// Collects namespaces, classes and methods to be used in validation
    /// </summary>
    public class Collector : SyntaxWalker
    {
        /// <summary>
        /// List of attribute names. Only methods with 
        /// attributes contained in this list is collected.
        /// </summary>
        List<string> attributeNames;

        /// <summary>
        /// The current namespace members
        /// </summary>
        Dictionary<string, List<string>> currentNamespaceMembers;

        /// <summary>
        /// The current methods
        /// </summary>
        List<string> currentMethods;

        /// <summary>
        /// The compilation unit to collect members from
        /// </summary>
        CompilationUnitSyntax compilationUnit;

        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public Dictionary<string, Dictionary<string, List<string>>> Members
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collector"/> class.
        /// </summary>
        /// <param name="compilationUnit">The compilation unit to collect from</param>
        /// <param name="attributeNames">The attribute names</param>
        public Collector(CompilationUnitSyntax compilationUnit, List<string> attributeNames = null)
        {
            this.compilationUnit = compilationUnit;

            if (attributeNames == null)
                this.attributeNames = new List<string>();
            else
                this.attributeNames = attributeNames;

            currentMethods = new List<string>();
            currentNamespaceMembers = new Dictionary<string, List<string>>();
            Members = new Dictionary<string, Dictionary<string, List<string>>>();
        }

        /// <summary>
        /// Collects members
        /// </summary>
        public void Collect()
        {
            Visit(compilationUnit);
        }

        /// <summary>
        /// Determines wether the current namespace should be collected
        /// </summary>
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

        /// <summary>
        /// Detemines whether the current class should be collected
        /// </summary>
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            currentMethods.Clear();

            base.VisitClassDeclaration(node);

            // Todo: Prettify this
            if (currentMethods.Count > 0 && !currentNamespaceMembers.ContainsKey(node.Identifier.ValueText))
                currentNamespaceMembers.Add(node.Identifier.ValueText, currentMethods.ToList());
            else if (attributeNames.Count == 0 && node.IsDOMType())
            {
                if(!currentNamespaceMembers.ContainsKey(node.Identifier.ValueText))
                    currentNamespaceMembers.Add(node.Identifier.ValueText, currentMethods.ToList());
            }
                
        }


        /// <summary>
        /// Makes sure that properties are added to members when no attribute
        /// names have been set. This is to ensure that DOM types' properties
        /// can be used.
        /// </summary>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (attributeNames.Count == 0)
                currentMethods.Add(node.Identifier.ValueText);
        }

        /// <summary>
        /// Determines whether the current method should be collected
        /// </summary>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var hasAttributeName = false;
            foreach (var attributeName in attributeNames)
            {
                if (node.HasAttribute(attributeName))
                {
                    hasAttributeName = true;
                    break;
                }
            }

            if ((attributeNames.Count == 0) || hasAttributeName)
                currentMethods.Add(node.Identifier.ValueText);

            base.VisitMethodDeclaration(node);
        }

        #region Helper Methods
        /// <summary>
        /// Gets the full name of a name syntax
        /// </summary>
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
