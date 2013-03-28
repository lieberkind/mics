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

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var methodCollector = new MethodCollector();
            methodCollector.Visit(node);

            if (methodCollector.Methods.Count > 0)
            {
                Classes.Add(node);
            }

            base.VisitClassDeclaration(node);
        }
    }


    class MethodCollector : SyntaxWalker
    {
        public readonly List<MethodDeclarationSyntax> Methods = new List<MethodDeclarationSyntax>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var attributeCollector = new AttributeCollector();
            attributeCollector.Visit(node);

            if (attributeCollector.Attributes.Count > 0) 
            {
                Methods.Add(node);
            }

            base.VisitMethodDeclaration(node);
        }

    }

    class AttributeCollector : SyntaxWalker
    {
        public readonly List<AttributeSyntax> Attributes = new List<AttributeSyntax>();

        public override void VisitAttribute(AttributeSyntax node)
        {
            if (((IdentifierNameSyntax)node.Name).Identifier.ValueText == "MixedSide") 
            {
                Attributes.Add(node);
            }   
            base.VisitAttribute(node);
        }
    }
}
