﻿using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Builders
{
    class StatementBuilder : SyntaxWalker
    {

        SS.ClassSymbol ssTypeReference;
        SS.MemberSymbol ssParentMember;
        public readonly List<SS.Statement> ssStatements = new List<SS.Statement>();

        //public StatementBuilder(SS.ClassSymbol typeReference)
        //{
        //    this.ssTypeReference = typeReference;
        //}

        public StatementBuilder(SS.ClassSymbol typeReference, SS.MemberSymbol ssParentMember)
        {
            this.ssTypeReference = typeReference;
            this.ssParentMember = ssParentMember;
        }

        public override void DefaultVisit(SyntaxNode node)
        {
            base.DefaultVisit(node);
        }

        public override void VisitIfStatement(IfStatementSyntax ifStatement)
        {
            var ssCondition = ExpressionBuilder.Build(ifStatement.Condition);
            var ssIfStatement = StatementBuilder.Build(ifStatement.Statement, ssTypeReference, ssParentMember);
            var ssElseStatement = ifStatement.Else == null ? null : StatementBuilder.Build(ifStatement.Else.Statement, ssTypeReference, ssParentMember);

            ssStatements.Add(ifStatement.Map(ssCondition, ssIfStatement, ssElseStatement));            
        }

        public override void VisitForStatement(ForStatementSyntax forStatement)
        {
            var ssForStatement = forStatement.Map();

            // Todo: Implement mapping of VariablDeclarationSyntax and VariableDeclaratorSyntax
            if (forStatement.Initializers == null)
                throw new NotSupportedException("Initializers cannot be null. Hint: declare incrementors outside for statement.");

            foreach (var initializer in forStatement.Initializers)
                ssForStatement.AddInitializer(ExpressionBuilder.Build(initializer));

            ssForStatement.AddCondition(ExpressionBuilder.Build(forStatement.Condition));

            foreach (var incrementor in forStatement.Incrementors)
                ssForStatement.AddIncrement(ExpressionBuilder.Build(incrementor));

            ssForStatement.AddBody(StatementBuilder.Build(forStatement.Statement, ssTypeReference, ssParentMember));

            ssStatements.Add(ssForStatement);
            //base.VisitForStatement(node);
        }

        public override void VisitBlock(BlockSyntax block)
        {
            var ssBlock = block.Map();

            var ssChildStatements = new List<SS.Statement>();

            foreach (var statement in block.Statements)
            {
                ssChildStatements.Add(StatementBuilder.Build(statement, ssTypeReference, ssParentMember));
            }

            ssBlock.Statements.AddRange(ssChildStatements);
            ssStatements.Add(ssBlock);

            //base.VisitBlock(block);
        }

        public override void VisitReturnStatement(ReturnStatementSyntax returnStatement)
        {

            var ssExpression = ExpressionBuilder.Build(returnStatement.Expression, ssTypeReference);

            ssStatements.Add(returnStatement.Map(ssExpression));

            // base.VisitReturnStatement(returnStatement);
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax expressionStatement)
        {
            var ssExpression = ExpressionBuilder.Build(expressionStatement.Expression, ssTypeReference);
            
            ssStatements.Add(expressionStatement.Map(ssExpression));

            // base.VisitExpressionStatement(expressionStatement);
        }

        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax localDeclarationStatement)
        {
            var variable = localDeclarationStatement.Declaration.Variables[0];

            if (!(localDeclarationStatement.Declaration is VariableDeclarationSyntax))
            {
                throw new NotSupportedException("LocalDeclarationStatement has not supported declaration");
            }

            var identifier = variable.Identifier;
            if (identifier.Kind != SyntaxKind.IdentifierToken)
                throw new NotSupportedException(); // Todo: Maybe not a necesary check...
 

            var type = TypeSymbolGetter.GetTypeSymbol(localDeclarationStatement.Declaration.Type);

            // Todo: Delete this
            //if (type is ErrorTypeSymbol)
            //{
            //    var coreTypeName = ((IdentifierNameSyntax)localDeclarationStatement.Declaration.Type).Identifier.ValueText;
            //    type  = CoreTypeManager.GetTypeByName(coreTypeName);
            //}

            var ssVariable = variable.Map(ssParentMember, type.Map());

            var initializer = variable.Initializer;
            if (initializer != null)
            {
                if (!(initializer is EqualsValueClauseSyntax))
                    throw new NotSupportedException("Unsupported initializer");

                var val = variable.Initializer.Value;

                ssVariable.SetValue(ExpressionBuilder.Build(val, ssTypeReference));

            }

            var ssVariableDecalarationStatement = new SS.VariableDeclarationStatement();
            ssVariableDecalarationStatement.Variables.Add(ssVariable);

            ssStatements.Add(ssVariableDecalarationStatement);

            //base.VisitLocalDeclarationStatement(localDeclarationStatement);
        }

        public static SS.Statement Build(StatementSyntax statement, SS.ClassSymbol ssTypeReference, SS.MemberSymbol ssParentMember)
        {
            return StatementBuilder.BuildList(statement, ssTypeReference, ssParentMember).First();
        }

        public static List<SS.Statement> BuildList(StatementSyntax statement, SS.ClassSymbol ssTypeReference, SS.MemberSymbol ssParentMember)
        {
            var statementBuilder = new StatementBuilder(ssTypeReference, ssParentMember);
            statementBuilder.Visit(statement);

            return statementBuilder.ssStatements;
        }

        public static List<SS.Statement> BuildList(SyntaxNode node, SS.ClassSymbol ssTypeReference, SS.MemberSymbol ssParentMember)
        {
            var statementBuilder = new StatementBuilder(ssTypeReference, ssParentMember);
            statementBuilder.Visit(node);

            return statementBuilder.ssStatements;
        }
    }
}
