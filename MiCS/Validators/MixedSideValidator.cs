using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    internal class MixedSideValidator : SyntaxWalker
    {
        Dictionary<string, List<string>> mixedSideMembers;
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
            IsValid = true;
            mixedSideCollector.Collect();
            mixedSideMembers = mixedSideCollector.Members;
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

            var valid = mixedSideMembers.ContainsKey(typeName) && mixedSideMembers[typeName].Contains(methodName);

            if (!valid)
                IsValid = false;
        }
    }
}
