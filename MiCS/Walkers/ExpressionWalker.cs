using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Walkers
{
    class ExpressionWalker : SyntaxWalker
    {
        ScriptSharp.ScriptModel.TypeSymbol associatedType;
        public readonly List<ScriptSharp.ScriptModel.Expression> scriptSharpExpressions = new List<Expression>();

        public ExpressionWalker()
        {

        }
        public ExpressionWalker(ScriptSharp.ScriptModel.TypeSymbol associatedType)
	    {
            this.associatedType = associatedType;
	    }

        public static List<ScriptSharp.ScriptModel.Expression> Maps(SyntaxNode node, ScriptSharp.ScriptModel.TypeSymbol associatedType)
        {
            var expressionWalker = new ExpressionWalker(associatedType);
            expressionWalker.Visit(node);
            return expressionWalker.scriptSharpExpressions;
        }
        public static List<ScriptSharp.ScriptModel.Expression> Maps(SyntaxNode node)
        {
            var expressionWalker = new ExpressionWalker();
            expressionWalker.Visit(node);
            return expressionWalker.scriptSharpExpressions;
        }
        public static ScriptSharp.ScriptModel.Expression Map(SyntaxNode node, ScriptSharp.ScriptModel.TypeSymbol associatedType)
        {
            var expressions = ExpressionWalker.Maps(node, associatedType);
            if (expressions.Count != 1)
                throw new Exception("There are not exactly one expression!");

            return expressions.First();
        }
        public static ScriptSharp.ScriptModel.Expression Map(SyntaxNode node)
        {
            var expressions = ExpressionWalker.Maps(node);
            if (expressions.Count != 1)
                throw new Exception("There are not exactly one expression!");

            return expressions.First();
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            scriptSharpExpressions.Add(node.Map());
            
            //base.VisitIdentifierName(node);
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            scriptSharpExpressions.Add(node.Map());

            //base.VisitLiteralExpression(node);
        }

        public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
        {
            scriptSharpExpressions.Add(node.Map());
            
            //base.VisitPrefixUnaryExpression(node);
        }

        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            scriptSharpExpressions.Add(node.Map());

            //base.VisitBinaryExpression(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            scriptSharpExpressions.Add(node.Map(associatedType));
            
            //base.VisitInvocationExpression(node);
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            scriptSharpExpressions.Add(node.Map(associatedType));
            
            //base.VisitObjectCreationExpression(node);
        }

        public override void VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            scriptSharpExpressions.Add(node.Map());
            
            //base.VisitConditionalExpression(node);
        }
    }
}
