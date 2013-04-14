﻿using Roslyn.Compilers.CSharp;
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
                if (node.Name is IdentifierNameSyntax)
                {
                    // Custom namespaces
                    var namespaceName = ((IdentifierNameSyntax)node.Name).Identifier.ValueText;
                    if (!Members.ContainsKey(namespaceName))
                        Members.Add(namespaceName, new Dictionary<string, List<string>>(currentNamespaceMembers));
                }
                else if (node.Name is QualifiedNameSyntax)
                {
                    // Built in namespaces
                    var namespaceName = ((QualifiedNameSyntax)node.Name).ToString();
                    if (!Members.ContainsKey(namespaceName))
                        Members.Add(namespaceName, new Dictionary<string, List<string>>(currentNamespaceMembers));
                    else
                    {
                        // Todo: do the same for custom namespaces.
                        // Namespace has already been added but more classes from different .cs
                        // file (same namespace) needs to be added.
                        foreach (var key in currentNamespaceMembers.Keys)
                        {
                            Members[namespaceName].Add(key, currentNamespaceMembers[key]);
                        }
                    }


                }
                else
                    throw new NotSupportedException();
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
