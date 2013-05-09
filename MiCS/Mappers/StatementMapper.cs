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
    /// <summary>
    /// Class with extension mapping methods that are used to map
    /// from Roslyn AST nodes to ScriptSharp statements.
    /// </summary>
    internal static class StatementMapper
    {
        /// <summary>
        /// Returns mapped ScriptSharp IfElseStatement with the specified condition, ifStatement and elseStatement.
        /// </summary>
        static internal SS.IfElseStatement Map(this IfStatementSyntax stmt,
            SS.Expression ssCondition,
            SS.Statement ssIfStatement,
            SS.Statement ssElseStatement
        )
        {
            return new SS.IfElseStatement(ssCondition, ssIfStatement, ssElseStatement);
        }

        /// <summary>
        /// Returns mapped ScriptSharp block statement.
        /// </summary>
        static internal SS.BlockStatement Map(this BlockSyntax block)
        {
            return new SS.BlockStatement();
        }

        /// <summary>
        /// Returns mapped ScriptSharp ReturnStatement with specified expression.
        /// </summary>
        static internal SS.ReturnStatement Map(this ReturnStatementSyntax returnStatement, 
            SS.Expression ssExpression
        )
        {
            return new SS.ReturnStatement(ssExpression);
        }

        /// <summary>
        /// Returns mapped ScriptSharp ExpressionStatement with specified expression.
        /// </summary>
        static internal SS.ExpressionStatement Map(this ExpressionStatementSyntax expressionStatement, SS.Expression ssExpression)
        {
            return new SS.ExpressionStatement(ssExpression);
        }

        /// <summary>
        /// Returns mapped ScriptSharp VariableDeclarationStatement.
        /// </summary>
        static internal SS.VariableDeclarationStatement Map(this VariableDeclarationSyntax variableDeclaration)
        {
            return new SS.VariableDeclarationStatement();
        }

        // Todo: What is goin on here :-) are we not missing a LocalDeclarationStatement mapping? seems not...
        //static internal SS.VariableDeclarationStatement Map(this LocalDeclarationStatementSyntax localDeclarationStatement)
        //{
        //    return new SS.VariableDeclarationStatement();
        //}

        /// <summary>
        /// Returns mapped ScriptSharp ForStatement.
        /// </summary>
        static internal SS.ForStatement Map(this ForStatementSyntax forStatement)
        {
            return new SS.ForStatement();
        }

    }
}
