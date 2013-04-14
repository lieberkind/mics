using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    public class ClientSideValidator : SyntaxWalker
    {
        Dictionary<string, Dictionary<string, List<string>>> mixedSideMembers;
        Dictionary<string, Dictionary<string, List<string>>> clientSideMembers;
        CompilationUnitSyntax root;

        public bool IsValid
        {
            get;
            private set;
        }

        public ClientSideValidator(CompilationUnitSyntax root)
        {
            this.root = root;
            IsValid = false;

            var mixedSideCollector = new Collector(root, "MixedSide");
            var clientSideCollector = new Collector(root, "ClientSide");

            mixedSideCollector.Collect();
            clientSideCollector.Collect();

            mixedSideMembers = mixedSideCollector.Members;
            clientSideMembers = clientSideCollector.Members;
        }

        public void Validate()
        {
            Visit(root);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var model = MiCSManager.SemanticModel;
            var info = model.GetTypeInfo(node.Expression);

            var @namespace = info.Type.ParentNamespace();
            var namespaceName = ((IdentifierNameSyntax)@namespace.Name).Identifier.ValueText;

            var typeName = info.Type.Name;
            var methodName = node.Name.Identifier.ValueText;

            var isValidMixedSideMember =
                mixedSideMembers.ContainsKey(namespaceName) &&
                mixedSideMembers[namespaceName].ContainsKey(methodName) &&
                mixedSideMembers[namespaceName][typeName].Contains(methodName);

            var isValidClientSideMember =
                clientSideMembers.ContainsKey(namespaceName) &&
                clientSideMembers[namespaceName].ContainsKey(typeName) &&
                clientSideMembers[namespaceName][typeName].Contains(methodName);

            IsValid = isValidMixedSideMember || isValidClientSideMember;

            if (IsValid)
                base.VisitMemberAccessExpression(node);
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            if (!(node.Type is IdentifierNameSyntax))
                throw new Exception("Only IdentifierNameSyntax is supported at this time");

            var typeName = ((IdentifierNameSyntax)node.Type).Identifier.ValueText;

            var @namespace = node.Type.ParentNamespace();
            var namespaceName = ((IdentifierNameSyntax)@namespace.Name).Identifier.ValueText;

            var isValidMixedSideMember =
                mixedSideMembers.ContainsKey(namespaceName) &&
                mixedSideMembers[namespaceName].ContainsKey(typeName);

            var isValidClientSideMember =
                clientSideMembers.ContainsKey(namespaceName) &&
                clientSideMembers[namespaceName].ContainsKey(typeName);

            IsValid = isValidMixedSideMember || isValidMixedSideMember;

            if (IsValid)
                base.VisitObjectCreationExpression(node);
        }
    }


}
