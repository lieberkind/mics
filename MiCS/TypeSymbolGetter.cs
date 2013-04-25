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
            TypeSymbol = TypeSymbolGetter.GetTypeSymbol(objectCreationExpression.Type);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            TypeSymbol = TypeSymbolGetter.GetTypeSymbol(node);
        }

        public override void VisitArgument(ArgumentSyntax node)
        {
            TypeSymbol = TypeSymbolGetter.GetTypeSymbol(node.Expression);
        }

        public static TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            var type = ScriptTypeManager.Instance.SemanticModel.GetTypeInfo(expression).Type;
            if (type == null)
            {
                // Todo: When does this happen?
                /*
                 * Happens on static and none static references
                 * to DOM types.
                 */
                if (expression is IdentifierNameSyntax)
                {
                    var symbol = ScriptTypeManager.Instance.SemanticModel.GetSymbolInfo((IdentifierNameSyntax)expression).Symbol;
                    if (symbol is MethodSymbol)
                    {
                        var method = (MethodSymbol)symbol;
                        type = method.ReturnType;
                    }
                }
                else
                    throw new NotImplementedException();
            }

            if (type is ErrorTypeSymbol)
                throw new NotSupportedException("Specified expression type was not found in ScriptTypes or CoreTypes. Remember to import required namespaces (e.g. 'using System.Html;').");

            return type;
        }

        public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            var typeSymbolGetter = new TypeSymbolGetter();
            typeSymbolGetter.Visit(node);
            return typeSymbolGetter.TypeSymbol;
        }

        public static TypeSymbol GetReturnType(SimpleNameSyntax node)
        {
            var symbol = MiCSManager.ScriptTypeSemanticModel.GetSymbolInfo(node).Symbol;

            if (symbol == null)
                throw new Exception("Symbol is null. Can be caused by invalid C# syntax.");
            else if(!(symbol is MethodSymbol))
                throw new NotSupportedException();

            return ((MethodSymbol)symbol).ReturnType;
        }


    }
}
