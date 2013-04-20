using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    public class Validator : SyntaxWalker
    {
        Dictionary<string, Dictionary<string, List<string>>> members;
        CompilationUnitSyntax root;

        public bool IsValid
        {
            get;
            private set;
        }

        public Validator(CompilationUnitSyntax root, Dictionary<string, Dictionary<string, List<string>>> members)
        {
            this.root = root;
            this.members = members;
        }

        public void Validate()
        {
            Visit(root);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var methods = node.DescendantNodes().Where(a => a.Kind == SyntaxKind.MethodDeclaration);

            var visit = false;

            foreach (var method in methods)
            {
                var m = ((MethodDeclarationSyntax)method);
                visit = m.HasAttribute("MixedSide") || m.HasAttribute("ClientSide");

                if (visit)
                    break;
            }

            if(visit)
                base.VisitClassDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.HasAttribute("MixedSide") || node.HasAttribute("ClientSide"))
                base.VisitMethodDeclaration(node);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var type = TypeSymbolGetter.GetTypeSymbol(node.Expression);

            var @namespace = type.ParentNamespace();
            var namespaceName = @namespace.GetFullName();

            var typeName = type.Name;
            var methodName = node.Name.Identifier.ValueText;

            IsValid =
                members.ContainsKey(namespaceName) &&
                members[namespaceName].ContainsKey(typeName) &&
                members[namespaceName][typeName].Contains(methodName);

            if (IsValid)
                base.VisitMemberAccessExpression(node);
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            if (!(node.Type is IdentifierNameSyntax))
                throw new Exception("Only IdentifierNameSyntax is supported at this time");

            var typeName = ((IdentifierNameSyntax)node.Type).Identifier.ValueText;

            var @namespace = node.Type.ParentNamespace();
            var namespaceName = @namespace.GetFullName();

            IsValid =
                members.ContainsKey(namespaceName) &&
                members[namespaceName].ContainsKey(typeName);

            if (IsValid)
                base.VisitObjectCreationExpression(node);
        }
    }
}
