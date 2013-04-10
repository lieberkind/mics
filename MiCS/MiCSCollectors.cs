using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    public class ClassCollector : SyntaxWalker
    {
        public readonly List<ClassDeclarationSyntax> Classes = new List<ClassDeclarationSyntax>();
        string attributeName;
        
        public MethodCollector MethodCollector
        {
            get; private set;
        }

        public ClassCollector(string attributeName)
        {
            this.attributeName = attributeName;
            MethodCollector = new MethodCollector(attributeName);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            MethodCollector.Visit(node);

            if (MethodCollector.Methods.Count > 0)
            {
                Classes.Add(node);
            }

            base.VisitClassDeclaration(node);
        }
    }


    public class MethodCollector : SyntaxWalker
    {
        public readonly List<MethodDeclarationSyntax> Methods = new List<MethodDeclarationSyntax>();
        string attributeName;

        public MethodCollector(string attributeName)
        {
            this.attributeName = attributeName;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var attributeCollector = new AttributeCollector(attributeName);
            attributeCollector.Visit(node);

            if (attributeCollector.Attributes.Count > 0) 
            {
                Methods.Add(node);
            }

            base.VisitMethodDeclaration(node);
        }

    }

    public class AttributeCollector : SyntaxWalker
    {
        public readonly List<AttributeSyntax> Attributes = new List<AttributeSyntax>();
        string attributeName;

        public AttributeCollector(string attributeName)
        {
            this.attributeName = attributeName;
        }

        public override void VisitAttribute(AttributeSyntax node)
        {
            if (((IdentifierNameSyntax)node.Name).Identifier.ValueText == attributeName) 
            {
                Attributes.Add(node);
            }   
            base.VisitAttribute(node);
        }
    }
}
