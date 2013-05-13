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
    /// <summary>
    /// Builds ScriptSharp expressions from Roslyn Expressions
    /// </summary>
    class ExpressionBuilder : SyntaxWalker
    {
        /// <summary>
        /// The ScriptSharp type that holds the expression
        /// </summary>
        SS.TypeSymbol associatedType;

        /// <summary>
        /// The ScriptSharp member that holds the expression
        /// </summary>
        SS.MemberSymbol associatedParent;

        /// <summary>
        /// List of generated ScriptSharp expressions
        /// </summary>
        public readonly List<SS.Expression> ssExpressions = new List<SS.Expression>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionBuilder"/> class.
        /// </summary>
        /// <param name="associatedType">The ScriptSharp type that holds the expression.</param>
        /// <param name="associatedParent">The ScriptSharp member that holds the expression.</param>
        public ExpressionBuilder(SS.TypeSymbol associatedType, SS.MemberSymbol associatedParent)
	    {
            this.associatedType = associatedType;
            this.associatedParent = associatedParent;
	    }

        /// <summary>
        /// Determines that an expression cannot be mapped
        /// </summary>
        /// <param name="node">The node.</param>
        /// <exception cref="System.NotSupportedException">
        /// Throws NotSupportedException as this kind of expression is not currently supported
        /// </exception>
        public override void DefaultVisit(SyntaxNode node)
        {
            throw new NotSupportedException("The Expression of kind " + node.Kind.ToString() + " cannot be mapped");
        }

        /// <summary>
        /// Builds ScriptSharp expression where the parenthesized property 
        /// is set to true (if parenthesizes are not redundant).
        /// </summary>
        public override void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            var ssExpression = ExpressionBuilder.BuildExpression(node.Expression, associatedType, associatedParent);
            ssExpression.AddParenthesisHint();
            ssExpressions.Add(ssExpression);
        }

        /// <summary>
        /// Builds an array creation expression
        /// </summary>
        /// <param name="arrayCreationExpression">The array creation expression.</param>
        public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax arrayCreationExpression)
        {
            var arrayType = TypeManager.GetTypeSymbol(arrayCreationExpression);
            var ssArrayType = arrayType.Map();

            var count = arrayCreationExpression.Initializer == null ? 0 : arrayCreationExpression.Initializer.Expressions.Count;

            SS.Expression[] exprs = new SS.Expression[count];
            if (count > 0)
            {
                int i = 0;
                foreach (var expr in arrayCreationExpression.Initializer.Expressions)
                {
                    exprs[i] = ExpressionBuilder.BuildExpression(expr, associatedType, associatedParent);
                    i++;
                }
            }

            ssExpressions.Add(arrayCreationExpression.Map(ssArrayType, associatedParent, exprs));
        }

        /// <summary>
        /// Builds an element access expression.
        /// </summary>
        /// <param name="elementAccessExpression">The element access expression.</param>
        /// <exception cref="System.NotSupportedException">
        /// Throws NotSupportedExceptions if the elementAccessExpressions expression is not ArrayTypeSymbol or if
        /// the array is associative
        /// </exception>
        public override void VisitElementAccessExpression(ElementAccessExpressionSyntax elementAccessExpression)
        {
            var parentType = TypeManager.GetTypeSymbol(elementAccessExpression.Expression);
            if (!(parentType is ArrayTypeSymbol))
                throw new NotSupportedException("Only array elements access is supported.");

            var ssType = TypeManager.GetTypeSymbol(elementAccessExpression).Map();
            var ssParentType = TypeManager.GetTypeSymbol(elementAccessExpression.Expression).Map();
            var ssIndexerSymbol = elementAccessExpression.ArgumentList.Map(ssParentType, ssType);

            var ssObjectReference = ExpressionBuilder.BuildExpression(elementAccessExpression.Expression, ssParentType, associatedParent);
            var ssIndexerExpression = elementAccessExpression.Map(ssObjectReference, ssIndexerSymbol);

             // This ensures that ScritpSharp doesn't use the ScriptSharp get_item
             // JavaScript method but the built in bracketed indexer ([1]) instead.
            ssIndexerExpression.Indexer.SetScriptIndexer();

            if (elementAccessExpression.ArgumentList.Arguments.Count > 1)
                throw new NotSupportedException("Only single dimension arrays are supported");

            var argumentExpression = elementAccessExpression.ArgumentList.Arguments[0].Expression;
            var ssArgumentType = TypeManager.GetTypeSymbol(argumentExpression).Map();
            var ssArgument = ExpressionBuilder.BuildExpression(argumentExpression, ssArgumentType, associatedParent);

            ssIndexerExpression.AddIndexParameterValue(ssArgument);

            ssExpressions.Add(ssIndexerExpression);
        }

        /// <summary>
        /// Builds an identifier name
        /// </summary>
        /// <param name="identifierName">The identifierName</param>
        public override void VisitIdentifierName(IdentifierNameSyntax identifierName)
        {
            ssExpressions.Add(identifierName.Map(associatedType, associatedParent));
        }

        /// <summary>
        /// Builds a literal expression.
        /// </summary>
        /// <param name="literalExpression">The literal expression.</param>
        public override void VisitLiteralExpression(LiteralExpressionSyntax literalExpression)
        {
            ssExpressions.Add(literalExpression.Map());
        }

        /// <summary>
        /// Builds a prefix unary expression.
        /// </summary>
        /// <remarks>Currently only minus and exclamation mark is supported</remarks>
        /// <param name="prefixUnaryExpression">The prefix unary expression.</param>
        public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax prefixUnaryExpression)
        {
            var ssOperand = ExpressionBuilder.BuildExpression(prefixUnaryExpression.Operand, associatedType, associatedParent);
            ssExpressions.Add(prefixUnaryExpression.Map(ssOperand));
        }

        /// <summary>
        /// Builds a binary expression.
        /// </summary>
        /// <param name="binaryExpression">The binary expression.</param>
        public override void VisitBinaryExpression(BinaryExpressionSyntax binaryExpression)
        {
            var ssLeftExpression = ExpressionBuilder.BuildExpression(binaryExpression.Left, associatedType, associatedParent);
            var ssRightExpression = ExpressionBuilder.BuildExpression(binaryExpression.Right, associatedType, associatedParent);
            
            ssExpressions.Add(binaryExpression.Map(ssLeftExpression, ssRightExpression));
        }

        /// <summary>
        /// Builds an invocation expression.
        /// </summary>
        /// <param name="invocationExpression">The invocation expression.</param>
        public override void VisitInvocationExpression(InvocationExpressionSyntax invocationExpression)
        {
            var ssParameters = new Collection<SS.Expression>();

            foreach (var arg in invocationExpression.ArgumentList.Arguments)
                ssParameters.Add(ExpressionBuilder.BuildExpression(arg.Expression, associatedType, associatedParent));

            var ssInvocation = invocationExpression.Map((SS.ClassSymbol)associatedType, (SS.MethodSymbol)associatedParent, ssParameters);

            ssExpressions.Add(ssInvocation);
        }

        /// <summary>
        /// Builds a cast expression.
        /// </summary>
        /// <remarks>As neither ScriptSharp nor JavaScript supports casts, the cast is ignored and only the casts expression is built</remarks>
        /// <param name="castExpression">The cast expression.</param>
        public override void VisitCastExpression(CastExpressionSyntax castExpression)
        {
            Visit(castExpression.Expression);
        }

        /// <summary>
        /// Builds an object creation expression.
        /// </summary>
        /// <param name="objectCreationExpression">The object creation expression.</param>
        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax objectCreationExpression)
        {
            var type = TypeManager.GetTypeSymbol(objectCreationExpression);

            var ssTargetType = type.Map();
            var ssNewExpression = objectCreationExpression.Map(ssTargetType);

            foreach (var argument in objectCreationExpression.ArgumentList.Arguments)
                ssNewExpression.AddParameterValue(ExpressionBuilder.BuildExpression(argument.Expression, associatedType, associatedParent));

            ssExpressions.Add(ssNewExpression);
        }

        /// <summary>
        /// Builds a conditional expression.
        /// </summary>
        /// <param name="conditionalExpression">The conditional expression.</param>
        public override void VisitConditionalExpression(ConditionalExpressionSyntax conditionalExpression)
        {
            var ssCondition = ExpressionBuilder.BuildExpression(conditionalExpression.Condition, associatedType, associatedParent);
            var ssTrueExpression = ExpressionBuilder.BuildExpression(conditionalExpression.WhenTrue, associatedType, associatedParent);
            var ssFalseExpression = ExpressionBuilder.BuildExpression(conditionalExpression.WhenFalse, associatedType, associatedParent);

            ssExpressions.Add(conditionalExpression.Map(ssCondition, ssTrueExpression, ssFalseExpression));
        }

        /// <summary>
        /// Builds a member access expression.
        /// </summary>
        /// <param name="memberAccess">The member access.</param>
        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax memberAccess)
        {
            var memberParentType = TypeManager.GetTypeSymbol(memberAccess.Expression);

            var ssObjectReference = ExpressionBuilder.BuildExpression(memberAccess.Expression, associatedType, associatedParent);
            var ssMemberParentType = memberParentType.Map();
            var ssType = TypeManager.GetTypeSymbol(memberAccess.Name).Map();
            var ssFieldName = memberAccess.Name.GetScriptName();

            var ssField = new SS.FieldSymbol(ssFieldName, ssMemberParentType, ssType); 
            var ssFieldExpression = new SS.FieldExpression(ssObjectReference, ssField);

            ssExpressions.Add(ssFieldExpression);
        }

        /// <summary>
        /// Builds a single expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <remarks>WARNING! Usage of this method may cause unexpected behavior and should only be used in tests. Use the overload version that takes associated type and parent instead.</remarks>
        /// <returns></returns>
        public static SS.Expression BuildExpression(ExpressionSyntax expression)
        {
            return BuildExpression(expression, null, null);
        }

        /// <summary>
        /// Builds a single expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <remarks>WARNING! Usage of this method may cause unexpected behavior and should only be used in tests. Use the overload version that takes associated type and parent instead.</remarks>
        /// <returns></returns>
        public static SS.Expression BuildExpression(ExpressionSyntax expression, SS.TypeSymbol associatedType)
        {
            return BuildExpression(expression, associatedType, null);
        }

        /// <summary>
        /// Builds a single ScriptSharp expression from a Roslyn expression
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="associatedType">The associated ScriptSharp type.</param>
        /// <param name="associatedParent">The associated ScriptSharp parent.</param>
        public static SS.Expression BuildExpression(ExpressionSyntax expression, SS.TypeSymbol associatedType, SS.MemberSymbol associatedParent)
        {
            var expressionBuilder = new ExpressionBuilder(associatedType, associatedParent);
            expressionBuilder.Visit(expression);
            return expressionBuilder.ssExpressions.First();
        }
    }
}
