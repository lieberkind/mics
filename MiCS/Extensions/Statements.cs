using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Extensions
{
    public static class Statements
    {
        static internal Statement Map(this StatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        {
            if (stmt is ReturnStatementSyntax)
                return ((ReturnStatementSyntax)stmt).Map();
            else if (stmt is BlockSyntax)
                return ((BlockSyntax)stmt).Map(parent);
            else if (stmt is LocalDeclarationStatementSyntax)
                return ((LocalDeclarationStatementSyntax)stmt).Map(parent);
            else if (stmt is ExpressionStatementSyntax)
                return ((ExpressionStatementSyntax)stmt).Map(parent);
            else if (stmt is IfStatementSyntax)
                return ((IfStatementSyntax)stmt).Map(parent);
            else
                throw new NotSupportedException("Statement type is not currently supported!");
        }

        static internal IfElseStatement Map(this IfStatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent)
        {
            return new IfElseStatement(stmt.Condition.Map(parent), stmt.Statement.Map(parent), stmt.Else.Statement.Map(parent));
        }

        static internal BlockStatement Map(this BlockSyntax block, ScriptSharp.ScriptModel.TypeSymbol parent)
        {
            var blockStmt = new BlockStatement();
            foreach (var stmt in block.Statements)
            {
                blockStmt.AddStatement(stmt.Map(parent));
            }
            return blockStmt;
        }

        static internal ReturnStatement Map(this ReturnStatementSyntax stmt)
        {
            var expr = stmt.Expression;
            if (expr is LiteralExpressionSyntax)
                return new ReturnStatement(((LiteralExpressionSyntax)expr).Map());
            throw new NotSupportedException("Expression type is not supported!");
        }

        static internal ExpressionStatement Map(this ExpressionStatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        {
            var expr = stmt.Expression;
            if (expr is BinaryExpressionSyntax)
                return new ExpressionStatement(((BinaryExpressionSyntax)expr).Map());
            else if (expr is InvocationExpressionSyntax)
                return new ExpressionStatement(((InvocationExpressionSyntax)expr).Map(parent));
            else
                throw new NotSupportedException();
        }

        static internal VariableDeclarationStatement Map(this LocalDeclarationStatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent)
        {
            var variable = stmt.Declaration.Variables[0];

            if (!(stmt.Declaration is VariableDeclarationSyntax))
            {
                throw new NotSupportedException("LocalDeclarationStatement has not supported declaration");
            }

            var identifier = variable.Identifier;
            if (identifier.Kind != SyntaxKind.IdentifierToken)
                throw new NotSupportedException(); // Maybe not a necesary check...


            var vS = new VariableSymbol(identifier.ValueText, null, null);
            
            var initializer = variable.Initializer;
            
            if (initializer != null)
            {
                if (!(initializer is EqualsValueClauseSyntax))
                    throw new NotSupportedException("Unsupported initializer");

                var initValue = variable.Initializer.Value;

                if (initValue is LiteralExpressionSyntax)
                    vS.SetValue(initValue.Map());
                else if (initValue is BinaryExpressionSyntax)
                    vS.SetValue(initValue.Map());
                else if (initValue is InvocationExpressionSyntax)
                    vS.SetValue(initValue.Map(parent));
                else if (initValue is ObjectCreationExpressionSyntax)
                    vS.SetValue(initValue.Map(parent));
                else
                    throw new NotSupportedException("Declaration initializer value is not currently supported.");
            }

            var vDS = new VariableDeclarationStatement();
            vDS.Variables.Add(vS);
            return vDS;
        }

    }
}
