using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Builders;

namespace MiCS.Mappers
{
    public static class StatementMapper
    {

        static internal SS.IfElseStatement Map(this IfStatementSyntax stmt,
            SS.Expression ssCondition,
            SS.Statement ssIfStatement,
            SS.Statement ssElseStatement
        )
        {
            return new SS.IfElseStatement(ssCondition, ssIfStatement, ssElseStatement);
        }

        static internal SS.BlockStatement Map(this BlockSyntax block)
        {
            return new SS.BlockStatement();
        }

        static internal SS.ReturnStatement Map(this ReturnStatementSyntax returnStatement, SS.Expression ssExpression)
        {
            return new SS.ReturnStatement(ssExpression);
        }

        static internal SS.ExpressionStatement Map(this ExpressionStatementSyntax expressionStatement, SS.Expression ssExpression)
        {
            return new SS.ExpressionStatement(ssExpression);
        }

        static internal SS.VariableDeclarationStatement Map(this VariableDeclarationSyntax variableDeclaration)
        {
            return new SS.VariableDeclarationStatement();
        }

        // Todo: What is goin on here :-) are we not missing a LocalDeclarationStatement mapping? seems not...
        //static internal SS.VariableDeclarationStatement Map(this LocalDeclarationStatementSyntax localDeclarationStatement)
        //{
        //    return new SS.VariableDeclarationStatement();
        //}

        static internal SS.ForStatement Map(this ForStatementSyntax forStatement)
        {
            return new SS.ForStatement();
        }

    }
}
