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
    class StatementWalker : SyntaxWalker
    {
        ScriptSharp.ScriptModel.ClassSymbol typeReference;
        public readonly List<Statement> scriptSharpStatements = new List<Statement>();

        public StatementWalker(ScriptSharp.ScriptModel.ClassSymbol typeReference)
        {
            this.typeReference = typeReference;
        }
        public StatementWalker()
        {

        }

        public static List<Statement> Maps(SyntaxNode roslynNode)
        {
            var statementWalker = new StatementWalker();
            statementWalker.Visit(roslynNode);
            return statementWalker.scriptSharpStatements;
        }
        public static List<Statement> Maps(SyntaxNode roslynNode, ScriptSharp.ScriptModel.ClassSymbol requiredTypeReference)
        {
            var statementWalker = new StatementWalker(requiredTypeReference);
            statementWalker.Visit(roslynNode);
            return statementWalker.scriptSharpStatements;
        }
        public static Statement Map(SyntaxNode roslynNode)
        {
            return StatementWalker.Maps(roslynNode).First();
        }
        public static Statement Map(SyntaxNode roslynNode, ScriptSharp.ScriptModel.ClassSymbol requiredTypeReference)
        {
            return StatementWalker.Maps(roslynNode, requiredTypeReference).First();
        }

        public override void DefaultVisit(SyntaxNode node)
        {
            // Todo: Consider how to throw exception on unsupported statement types!
            //throw new NotSupportedException("Statement type is not currently supported.");

            base.DefaultVisit(node);
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            scriptSharpStatements.Add(node.Map(typeReference));
            
            base.VisitIfStatement(node);
        }

        public override void VisitBlock(BlockSyntax node)
        {
            foreach (var roslynStatement in node.Statements)
            {
                scriptSharpStatements.Add(StatementWalker.Map(roslynStatement, typeReference));
            }
            
            base.VisitBlock(node);
        }

        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            scriptSharpStatements.Add(node.Map(typeReference));

            base.VisitReturnStatement(node);
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            scriptSharpStatements.Add(node.Map(typeReference));

            base.VisitExpressionStatement(node);
        }

        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            scriptSharpStatements.Add(node.Map(typeReference));

            base.VisitLocalDeclarationStatement(node);
        }

        //public override void VisitIdentifierName(IdentifierNameSyntax node)
        //{
        //    base.VisitIdentifierName(node);
        //}

        //public ScriptSharp.ScriptModel.ReturnStatement MapReturnStatement(ReturnStatementSyntax roslynReturnStatement)
        //{
        //    var expressionMapper = new ExpressionWalker();
        //    expressionMapper.Visit(roslynReturnStatement.Expression);

        //    var ssExpression = expressionMapper.ssExpressions.First();
            
        //    return new ReturnStatement(ssExpression);
        //}
    }
}
