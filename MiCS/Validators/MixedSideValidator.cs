using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    public class MixedSideValidator : SyntaxWalker
    {
        Dictionary<string, Dictionary<string, List<string>>> mixedSideMembers;
        CompilationUnitSyntax root;

        public bool IsValid
        {
            get;
            private set;
        }
        
        public MixedSideValidator(CompilationUnitSyntax root)
        {
            this.root = root;
            var mixedSideCollector = new Collector(root, "MixedSide");
            IsValid = false;
            mixedSideCollector.Collect();
            mixedSideMembers = mixedSideCollector.Members;
        }

        public void Validate()
        {
            Visit(root);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var model = MiCSManager.MixedSideSemanticModel;
            var info = model.GetTypeInfo(node.Expression);

            var @namespace = info.Type.ParentNamespace();
            var namespaceName = ((IdentifierNameSyntax)@namespace.Name).Identifier.ValueText;

            var typeName = info.Type.Name;            
            var methodName = node.Name.Identifier.ValueText;

            IsValid =
                mixedSideMembers.ContainsKey(namespaceName) &&
                mixedSideMembers[namespaceName].ContainsKey(typeName) &&
                mixedSideMembers[namespaceName][typeName].Contains(methodName);

            if (IsValid)
                base.VisitMemberAccessExpression(node);
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            if(!(node.Type is IdentifierNameSyntax))
                throw new Exception("Only IdentifierNameSyntax is supported at this time");
            
            var typeName = ((IdentifierNameSyntax)node.Type).Identifier.ValueText;

            var @namespace = node.Type.ParentNamespace();
            var namespaceName = ((IdentifierNameSyntax)@namespace.Name).Identifier.ValueText;

            IsValid =
                mixedSideMembers.ContainsKey(namespaceName) &&
                mixedSideMembers[namespaceName].ContainsKey(typeName);

            if(IsValid)
                base.VisitObjectCreationExpression(node);
        }
    }
}
