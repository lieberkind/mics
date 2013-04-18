using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    class Validator : SyntaxWalker
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

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var type = TypeSymbolGetter.GetTypeSymbol(node.Expression);

            var @namespace = type.ParentNamespace();
            var namespaceName = @namespace.GetFullName();

            var typeName = type.Name;
            var methodName = node.Name.Identifier.ValueText;

            IsValid =
                members.ContainsKey(namespaceName) &&
                members[namespaceName].ContainsKey(methodName) &&
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
