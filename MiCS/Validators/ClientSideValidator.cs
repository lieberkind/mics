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
        Dictionary<string, TypeDeclarationSyntax> mixedSideTypes;
        Dictionary<string, TypeDeclarationSyntax> clientSideTypes;

        Dictionary<string, MethodDeclarationSyntax> mixedSideMethods;
        Dictionary<string, MethodDeclarationSyntax> clientSideMethods;

        CompilationUnitSyntax root;

        public ClientSideValidator(CompilationUnitSyntax root)
        {
            this.root = root;
        }

        public void Validate()
        {
            Visit(root);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var name = node.Name.Identifier.ValueText;
            var valid = mixedSideMethods.ContainsKey(name) || clientSideMethods.ContainsKey(name);

            if (!valid)
                throw new Exception("Client side method access to non-client-side or non-mix-side code");
        }
    }


}
