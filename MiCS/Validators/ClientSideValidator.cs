using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    internal class ClientSideValidator : SyntaxWalker
    {
        Dictionary<string, List<string>> mixedSideMembers;
        Dictionary<string, List<string>> clientSideMembers;
        CompilationUnitSyntax root;

        public bool IsValid
        {
            get;
            private set;
        }

        public ClientSideValidator(CompilationUnitSyntax root)
        {
            this.root = root;
            IsValid = true;

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

            var typeName = info.Type.Name;
            var methodName = node.Name.Identifier.ValueText;

            var isValidMixedSideMember = mixedSideMembers.ContainsKey(typeName) && mixedSideMembers[typeName].Contains(methodName);
            var isValidClientSideMember = clientSideMembers.ContainsKey(typeName) && clientSideMembers[typeName].Contains(methodName);

            var valid = isValidMixedSideMember || isValidClientSideMember;

            if (!valid)
                IsValid = false;
        }
    }


}
