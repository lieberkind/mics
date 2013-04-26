using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiCS
{
    /// <summary>
    /// Helper class to get a type symbol
    /// </summary>
    class TypeSymbolWalker : SyntaxWalker
    {
        /// <summary>
        /// The semantic model in which to look for the type
        /// </summary>
        private SemanticModel semanticModel;

        /// <summary>
        /// The returned type symbol
        /// </summary>
        public TypeSymbol TypeSymbol
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSymbolWalker"/> class.
        /// </summary>
        /// <param name="semanticModel">The semantic in which to look for type symbols</param>
        public TypeSymbolWalker(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Default visit. If this is visited, TypeSymbol walker 
        /// was unable to get the type get the type symbol from
        /// the syntax node
        /// </summary>
        public override void DefaultVisit(SyntaxNode node)
        {
            throw new NotSupportedException("Unable to get type from Syntax Node");
        }

        /// <summary>
        /// Get the type symbol of an object creation
        /// </summary>
        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax objectCreationExpression)
        {
            TypeSymbol = GetTypeSymbol(objectCreationExpression.Type);
        }

        /// <summary>
        /// Get the type symbol of an identifier name
        /// </summary>
        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            TypeSymbol = GetTypeSymbol(node);
        }

        /// <summary>
        /// Get the type symbol of an arguments expression
        /// </summary>
        public override void VisitArgument(ArgumentSyntax node)
        {
            TypeSymbol = GetTypeSymbol(node.Expression);
        }

        /// <summary>
        /// Gets a type symbol from the specified expression
        /// </summary>
        /// <param name="expression">The expression.</param>
        public TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            var type = semanticModel.GetTypeInfo(expression).Type;
            if (type == null)
            {
                // Happens on static and none static references to DOM types.
                if (expression is IdentifierNameSyntax)
                {
                    var symbol = semanticModel.GetSymbolInfo((IdentifierNameSyntax)expression).Symbol;
                    if (symbol is MethodSymbol)
                        type = ((MethodSymbol)symbol).ReturnType;
                }
                else
                {
                    // Not sure how Roslyn identifies null type so instead null is used for null literals only.
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
