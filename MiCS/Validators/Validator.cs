using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Validators
{
    /// <summary>
    /// Validates a SyntaxTree given its root, a Dictionary of members and an attribute name.
    /// The Validator will look through all Classes and check to see if any of its methods has
    /// an attribute with the given attribute name. If so, it will validate that class and methods.
    /// Properties and fields are not supported and thus not validated.
    /// </summary>
    public class Validator : SyntaxWalker
    {
        /// <summary>
        /// Construct of members
        /// </summary>
        Dictionary<string, Dictionary<string, List<string>>> members;

        /// <summary>
        /// The CompilationUnitSyntax to validate
        /// </summary>
        CompilationUnitSyntax root;

        /// <summary>
        /// The attribute name
        /// </summary>
        string attributeName;

        /// <summary>
        /// Indicates whether the validation passed or failed
        /// </summary>
        public bool IsValid
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Validator"/> class.
        /// </summary>
        /// <param name="root">The CompilationUnit to validate.</param>
        /// <param name="members">The Members to validate against.</param>
        /// <param name="attributeName">The attribute name.</param>
        public Validator(CompilationUnitSyntax root, Dictionary<string, Dictionary<string, List<string>>> members, string attributeName)
        {
            this.root = root;
            this.members = members;
            this.attributeName = attributeName;
            IsValid = true;
        }

        /// <summary>
        /// Validates
        /// </summary>
        public void Validate()
        {
            Visit(root);
        }

        /// <summary>
        /// Visits the ClassDeclaration and determines wether its members should be validated
        /// </summary>
        public override void VisitClassDeclaration(ClassDeclarationSyntax @class)
        {
            var methods = @class.DescendantNodes().Where(a => a.Kind == SyntaxKind.MethodDeclaration);
            var visit = false;

            foreach (var method in methods)
            {
                visit = ((MethodDeclarationSyntax)method).HasAttribute(attributeName);

                if (visit)
                    VisitMethodDeclaration((MethodDeclarationSyntax)method);
            }
        }

        /// <summary>
        /// Determines whether a method should be validated 
        /// </summary>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.HasAttribute(attributeName))
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

        /// <summary>
        /// Visits the invocation expression.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public override void VisitInvocationExpression(InvocationExpressionSyntax invocation)
        {
            try
            {
                if (invocation.Expression is MemberAccessExpressionSyntax)
                {
                    var memberAccess = (MemberAccessExpressionSyntax)invocation.Expression;
                    if (memberAccess.Expression is IdentifierNameSyntax)
                        TypeManager.VerifyCorrectUseOfSupportedCoreType(invocation);
                }
            }
            catch (Exception e)
            {
                throw new IllegalInvocationException("Illegal invocation: " + invocation.Expression.ToFullString() + ". Check that argument is valid for this invocation", e);
            }

            base.VisitInvocationExpression(invocation);
        }

        /// <summary>
        /// Validates member access expressions such as method calls.
        /// </summary>
        /// <param name="node"></param>
        /// <exception cref="MixedSidePrincipleViolatedException">
        /// Throws MixedSidePrincipleViolatedException if member access is illegal
        /// </exception>
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
                throw new MixedSidePrincipleViolatedException(attributeName + " code made illegal call");
        }

        /// <summary>
        /// Validates object creation expressions. 
        /// </summary>
        /// <exception cref="MixedSidePrincipleViolatedException">
        /// Throns MixedSidePrincipleViolatedException if the object creation is illegal
        /// </exception>
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
                throw new MixedSidePrincipleViolatedException(attributeName + " code created illegal object");
        }

        /// <summary>
        /// Add members to the current members
        /// </summary>
        /// <param name="newMembers">The new members.</param>
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

        /// <summary>
        /// Determines whether the specified node is core type.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if the specified node is a core type; otherwise, <c>false</c>.
        /// </returns>
        private bool IsCoreType(TypeSyntax node)
        {
            var typeSymbol = TypeManager.GetTypeSymbol(node);

            var typeName = typeSymbol.Name;
            var namespaceName = typeSymbol.OriginalDefinition.ContainingNamespace.ToString();

            var coreTypes = TypeManager.CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName));
            var isCoreType = coreTypes.Count() > 0;

            return isCoreType;
        }

        /// <summary>
        /// Determines whether the specified method is a core member.
        /// </summary>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///   <c>true</c> if the member is a core member; otherwise, <c>false</c>.
        /// </returns>
        private bool IsCoreMember(string namespaceName, string typeName, string methodName)
        {
            var coreMembers = TypeManager.CoreMapping.Where(t => t.NamespaceName.Equals(namespaceName) && t.Name.Equals(typeName) && (t.Members.Where(m => m.Name.Equals(methodName)).Count() > 0));
            var isCoreMember = coreMembers.Count() > 0;

            return isCoreMember;
        }
    }
}
