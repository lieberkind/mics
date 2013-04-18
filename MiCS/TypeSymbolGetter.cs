using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    public class TypeSymbolGetter : SyntaxWalker
    {

        public TypeSymbol TypeSymbol
        {
            get;
            private set;
        }

        public override void DefaultVisit(SyntaxNode node)
        {
            throw new NotSupportedException("Unable to get type from Syntax Node");
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax objectCreationExpression)
        {
            var type = MiCSManager.ScriptTypeSemanticModel.GetTypeInfo(objectCreationExpression.Type).Type;

            if (type is ErrorTypeSymbol)
                type = TypeSymbolGetter.GetTypeSymbol(objectCreationExpression.Type);

            TypeSymbol = type;
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            var type = MiCSManager.ScriptTypeSemanticModel.GetTypeInfo(node).Type;

            if (type is ErrorTypeSymbol)
                type = CoreTypeManager.GetTypeByName(node.Identifier.ValueText);

            TypeSymbol = type;
        }

        public static TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            // Todo: Look in CoreTypeManager if not found
            return MiCSManager.ScriptTypeSemanticModel.GetTypeInfo(expression).Type;
        }

        public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            var typeSymbolGetter = new TypeSymbolGetter();
            typeSymbolGetter.Visit(node);
            return typeSymbolGetter.TypeSymbol;
        }

        public static TypeSymbol GetReturnType(SimpleNameSyntax node)
        {
            var method = MiCSManager.ScriptTypeSemanticModel.GetSymbolInfo(node).Symbol;

            var validMethodDeclaration =
                method.DeclaringSyntaxNodes.Count == 1 &&
                (method.DeclaringSyntaxNodes[0] is MethodDeclarationSyntax);

            if(!validMethodDeclaration)
                throw new NotSupportedException();

            var methodDeclaration = (MethodDeclarationSyntax)method.DeclaringSyntaxNodes[0];
            return TypeSymbolGetter.GetTypeSymbol(methodDeclaration.ReturnType);
        }
    }
}
