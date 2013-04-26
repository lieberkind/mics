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
        string attribute;

        public bool IsValid
        {
            get;
            private set;
        }

        public Validator(CompilationUnitSyntax root, Dictionary<string, Dictionary<string, List<string>>> members, string attribute)
        {
            this.root = root;
            this.members = members;
            this.attribute = attribute;
            IsValid = true;
        }

        public void Validate()
        {
            Visit(root);
        }

        // Todo: Maybe this should handle if a node should be validated or not
        public override void DefaultVisit(SyntaxNode node)
        {
            base.DefaultVisit(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var methods = node.DescendantNodes().Where(a => a.Kind == SyntaxKind.MethodDeclaration);
            var visit = false;

            foreach (var method in methods)
            {
                visit = ((MethodDeclarationSyntax)method).HasAttribute(attribute);

                if (visit)
                    break;
            }

            if (visit)
                base.VisitClassDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.HasAttribute(attribute))
                base.VisitMethodDeclaration(node);
        }

        // Ignore fields - not supported
        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            
        }

        // Ignore properties - not supported
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax invocation)
        {
            if (invocation.Expression is MemberAccessExpressionSyntax)
            {
                var memberAccess = (MemberAccessExpressionSyntax)invocation.Expression;
                if (memberAccess.Expression is IdentifierNameSyntax)
                    TypeManager.VerifyCorrectUseOfSupportedCoreType(invocation);
            }

            base.VisitInvocationExpression(invocation);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var type = TypeManager.GetTypeSymbol(node.Expression);

            var namespaceName = type.OriginalDefinition.ContainingNamespace.ToString();

            var typeName = type.Name;
            var methodName = node.Name.Identifier.ValueText;

            var isUserMember =
                members.ContainsKey(namespaceName) &&
                members[namespaceName].ContainsKey(typeName) &&
                members[namespaceName][typeName].Contains(methodName);

            var isCoreMember = IsCoreMember(namespaceName, typeName, methodName);

            IsValid = isUserMember || isCoreMember;

            if (IsValid)
                base.VisitMemberAccessExpression(node);
            else
                throw new MixedSidePrincipleViolatedException(attribute + " code made illegal call");
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            if (!(node.Type is IdentifierNameSyntax))
                throw new Exception("Only IdentifierNameSyntax is supported at this time");

            var typeName = ((IdentifierNameSyntax)node.Type).Identifier.ValueText;

            var namespaceName = TypeManager.GetTypeSymbol(node).ContainingNamespace.GetFullName();
            
            
            var isUserType =
                members.ContainsKey(namespaceName) &&
                members[namespaceName].ContainsKey(typeName);

            var isCoreType = IsCoreType(node.Type);

            IsValid = isUserType || isCoreType;

            if (IsValid)
                base.VisitObjectCreationExpression(node);
            else
                throw new MixedSidePrincipleViolatedException(attribute + " code created illegal object");
        }

        public void AddToMembers(Dictionary<string, Dictionary<string, List<string>>> newMembers)
        {
            foreach (var @namespace in newMembers)
            {
                var namespaceName = @namespace.Key;
                var namespaceClasses = @namespace.Value;
                if (!members.ContainsKey(namespaceName))
                {
                    members.Add(namespaceName, new Dictionary<string, List<string>>(namespaceClasses));
                }
                else
                {
                    foreach (var @class in namespaceClasses.Keys)
                    {
                        if (!members[namespaceName].ContainsKey(@class))
                        {
                            members[namespaceName].Add(@class, namespaceClasses[@class]);
                        }
                        else
                        {
                            var methods = @namespace.Value[@class];
                            foreach (var method in methods)
                            {
                                if (!members[namespaceName][@class].Contains(method))
                                    members[namespaceName][@class].Add(method);
                            }
                        }
                    }
                }
            }
        }

        private bool IsCoreType(TypeSyntax node)
        {
            var typeSymbol = TypeManager.GetTypeSymbol(node);

            var typeName = typeSymbol.Name;
            var namespaceName = typeSymbol.OriginalDefinition.ContainingNamespace.ToString();

            var coreTypes = TypeManager.CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName));
            var isCoreType = coreTypes.Count() > 0;

            return isCoreType;
        }

        private bool IsCoreMember(string namespaceName, string typeName, string methodName)
        {
            var coreMembers = TypeManager.CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName) && (t.Members.Where(m => m.Name.Equals(methodName)).Count() > 0));
            var isCoreMember = coreMembers.Count() > 0;

            return isCoreMember;
        }
    }
}
