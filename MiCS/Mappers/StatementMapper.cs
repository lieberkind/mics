using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Mappers
{
    class StatementMapper : SyntaxWalker
    {
        public readonly List<Statement> ssStatements = new List<Statement>();

        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            var ssReturnStatement = MapReturnStatement(node);
            ssStatements.Add(ssReturnStatement);
            base.VisitReturnStatement(node);
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            base.VisitExpressionStatement(node);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            base.VisitIdentifierName(node);
        }

        public ScriptSharp.ScriptModel.ReturnStatement MapReturnStatement(ReturnStatementSyntax roslynReturnStatement)
        {
            var expressionMapper = new ExpressionMapper();
            expressionMapper.Visit(roslynReturnStatement.Expression);

            var ssExpression = expressionMapper.ssExpressions.First();
            
            return new ReturnStatement(ssExpression);
        }
    }
}
