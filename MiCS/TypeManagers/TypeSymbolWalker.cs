using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiCS
{
    class TypeSymbolWalker : SyntaxWalker
    {
        private SemanticModel semanticModel;

        public TypeSymbol TypeSymbol
        {
            get;
            private set;
        }

        public TypeSymbolWalker(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override void DefaultVisit(SyntaxNode node)
        {
            throw new NotSupportedException("Unable to get type from Syntax Node");
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax objectCreationExpression)
        {
            TypeSymbol = GetTypeSymbol(objectCreationExpression.Type);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            TypeSymbol = GetTypeSymbol(node);
        }

        public override void VisitArgument(ArgumentSyntax node)
        {
            TypeSymbol = GetTypeSymbol(node.Expression);
        }

        public TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            var type = semanticModel.GetTypeInfo(expression).Type;
            if (type == null)
            {
                // Todo: When does this happen?
                /*
                 * Happens on static and none static references
                 * to DOM types.
                 */
                if (expression is IdentifierNameSyntax)
                {
                    var symbol = semanticModel.GetSymbolInfo((IdentifierNameSyntax)expression).Symbol;
                    if (symbol is MethodSymbol)
                    {
                        var method = (MethodSymbol)symbol;
                        type = method.ReturnType;
                    }
                }
                else
                {
                    /*
                     * Not sure how Roslyn identifies null type so 
                     * instead null is used for null literals only.
                     */
                    if (expression.Kind == SyntaxKind.NullLiteralExpression)
                        return null;

                    throw new NotImplementedException();
                }
            }

            if (type is ErrorTypeSymbol)
                throw new NotSupportedException("Specified expression type was not found in ScriptTypes or CoreTypes. Remember to import required namespaces (e.g. 'using System.Html;').");

            if (type == null)
                throw new Exception("Only allowed to return null on NullLiteralExpressions.");

            return type;
        }
    }
}
