using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;
using System.Collections.ObjectModel;

namespace MiCS.Builders
{
    class ExpressionBuilder : SyntaxWalker
    {
        SS.TypeSymbol associatedType;
        SS.MemberSymbol associatedParent;
        public readonly List<SS.Expression> ssExpressions = new List<SS.Expression>();

        public ExpressionBuilder(SS.TypeSymbol associatedType, SS.MemberSymbol associatedParent)
	    {
            this.associatedType = associatedType;
            this.associatedParent = associatedParent;
	    }

        public override void DefaultVisit(SyntaxNode node)
        {
            throw new NotSupportedException("The Expression of kind " + node.Kind.ToString() + " cannot be mapped");
        }

        public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax arrayCreationExpression)
        {
            ssExpressions.Add(arrayCreationExpression.Map(associatedType));
        }

        public override void VisitIdentifierName(IdentifierNameSyntax identifierName)
        {
            ssExpressions.Add(identifierName.Map());
            
            //base.VisitIdentifierName(node);
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax literalExpression)
        {
            ssExpressions.Add(literalExpression.Map());

            //base.VisitLiteralExpression(node);
        }

        public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax prefixUnaryExpression)
        {
            var ssOperand = ExpressionBuilder.Build(prefixUnaryExpression.Operand);
            ssExpressions.Add(prefixUnaryExpression.Map(ssOperand));
            
            //base.VisitPrefixUnaryExpression(node);
        }

        public override void VisitBinaryExpression(BinaryExpressionSyntax binaryExpression)
        {
            var ssLeftExpression = ExpressionBuilder.Build(binaryExpression.Left, associatedType, associatedParent);
            var ssRightExpression = ExpressionBuilder.Build(binaryExpression.Right, associatedType, associatedParent);
            
            ssExpressions.Add(binaryExpression.Map(ssLeftExpression, ssRightExpression));

            //base.VisitBinaryExpression(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax invocationExpression)
        {
            var ssParameters = new Collection<SS.Expression>();
            foreach (var arg in invocationExpression.ArgumentList.Arguments)
            {
                ssParameters.Add(ExpressionBuilder.Build(arg.Expression));
            }

            ssExpressions.Add(invocationExpression.Map((SS.ClassSymbol)associatedType, (SS.MethodSymbol)associatedParent, ssParameters));
            
            //base.VisitInvocationExpression(node);
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax objectCreationExpression)
        {
            // Todo: This type might have been mapped already...
            var type = TypeSymbolGetter.GetTypeSymbol(objectCreationExpression);

            var ssTargetType = type.Map();
            var ssNewExpression = objectCreationExpression.Map(ssTargetType);

            foreach (var argument in objectCreationExpression.ArgumentList.Arguments)
            {
                ssNewExpression.AddParameterValue(ExpressionBuilder.Build(argument.Expression));
            }

            ssExpressions.Add(ssNewExpression);
            
            //base.VisitObjectCreationExpression(node);
        }

        public override void VisitConditionalExpression(ConditionalExpressionSyntax conditionalExpression)
        {
            var ssCondition = ExpressionBuilder.Build(conditionalExpression.Condition);
            var ssTrueExpression = ExpressionBuilder.Build(conditionalExpression.WhenTrue);
            var ssFalseExpression = ExpressionBuilder.Build(conditionalExpression.WhenFalse);

            ssExpressions.Add(conditionalExpression.Map(ssCondition, ssTrueExpression, ssFalseExpression));

            //base.VisitConditionalExpression(node);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax memberAccess)
        {
            var memberParentType = TypeSymbolGetter.GetTypeSymbol(memberAccess.Expression);

            var ssObjectReference = ExpressionBuilder.Build(memberAccess.Expression);
            var ssMemberParentType = memberParentType.Map();
            var ssType = TypeSymbolGetter.GetTypeSymbol(memberAccess.Name).Map();
            var ssFieldName = memberAccess.Name.ScriptName();

            var ssField = new SS.FieldSymbol(ssFieldName, ssMemberParentType, ssType); 
            var ssFieldExpression = new SS.FieldExpression(ssObjectReference, ssField);

            ssExpressions.Add(ssFieldExpression);

            //base.VisitConditionalExpression(node);
        }

        public static SS.Expression Build(ExpressionSyntax expression)
        {
            return Build(expression, null, null);
        }
        public static SS.Expression Build(ExpressionSyntax expression, SS.TypeSymbol associatedType)
        {
            return Build(expression, associatedType, null);
        }
        public static SS.Expression Build(ExpressionSyntax expression, SS.TypeSymbol associatedType, SS.MemberSymbol associatedParent)
        {
            var expressionBuilder = new ExpressionBuilder(associatedType, associatedParent);
            expressionBuilder.Visit(expression);
            return expressionBuilder.ssExpressions.First();
        }
    }
}
