using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Walkers;

namespace MiCS.Mappers
{
    public static class Statements
    {
        //static internal Statement Map(this StatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        //{
        //    if (stmt is ReturnStatementSyntax)
        //        return ((ReturnStatementSyntax)stmt).Map(parent);
        //    else if (stmt is BlockSyntax)
        //        return ((BlockSyntax)stmt).Map(parent);
        //    else if (stmt is LocalDeclarationStatementSyntax)
        //        return ((LocalDeclarationStatementSyntax)stmt).Map(parent);
        //    else if (stmt is ExpressionStatementSyntax)
        //        return ((ExpressionStatementSyntax)stmt).Map(parent);
        //    else if (stmt is IfStatementSyntax)
        //        return ((IfStatementSyntax)stmt).Map(parent);
        //    else
        //        throw new NotSupportedException("Statement type is not currently supported!");
        //}

        static internal IfElseStatement Map(this IfStatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent)
        {
            var condition = ExpressionWalker.Map(stmt.Condition);
            var ifStatement = StatementWalker.Map(stmt.Statement);
            var elseStatement = stmt.Else == null ? null : StatementWalker.Map(stmt.Else.Statement);
            return new IfElseStatement(condition, ifStatement, elseStatement);

            // Todo: Remove
            // OLD VERSION WITHOUT WALKERS
            //var elseStmt = stmt.Else == null ? null : stmt.Else.Statement.Map(parent);
            //return new IfElseStatement(stmt.Condition.Map(parent), stmt.Statement.Map(parent), elseStmt);
        }

        static internal BlockStatement Map(this BlockSyntax roslynBlock, ScriptSharp.ScriptModel.ClassSymbol typeReference)
        {
            var scriptSharpBlock = new BlockStatement();
            foreach (var roslynStatement in roslynBlock.Statements)
            {
                scriptSharpBlock.Statements.Add(StatementWalker.Map(roslynStatement, typeReference));
            }
            return scriptSharpBlock;
        }

        static internal ReturnStatement Map(this ReturnStatementSyntax roslynResturnStatement, ScriptSharp.ScriptModel.TypeSymbol associatedType)
        {
            var scriptSharpExpression = ExpressionWalker.Map(roslynResturnStatement.Expression, associatedType);
            return new ReturnStatement(scriptSharpExpression);
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
            var roslynVariable = stmt.Declaration.Variables[0];

            if (!(stmt.Declaration is VariableDeclarationSyntax))
            {
                throw new NotSupportedException("LocalDeclarationStatement has not supported declaration");
            }

            var identifier = roslynVariable.Identifier;
            if (identifier.Kind != SyntaxKind.IdentifierToken)
                throw new NotSupportedException(); // Todo: Maybe not a necesary check...

            var scriptSharpVariable = new VariableSymbol(identifier.ValueText, null, null);
            
            var initializer = roslynVariable.Initializer;
            if (initializer != null)
            {
                if (!(initializer is EqualsValueClauseSyntax))
                    throw new NotSupportedException("Unsupported initializer");

                var val = roslynVariable.Initializer.Value;

                scriptSharpVariable.SetValue(ExpressionWalker.Map(val));

                // OLD VERSION WITHOUT WALKERS
                //if (val is LiteralExpressionSyntax)
                //    scriptSharpVariable.SetValue(val.Map());
                //else if (val is BinaryExpressionSyntax)
                //    scriptSharpVariable.SetValue(val.Map());
                //else if (val is InvocationExpressionSyntax)
                //    scriptSharpVariable.SetValue(val.Map(parent));
                //else if (val is ObjectCreationExpressionSyntax)
                //    scriptSharpVariable.SetValue(val.Map(parent));
                //else
                //    throw new NotSupportedException("Declaration initializer value is not currently supported.");
            }

            var vDS = new VariableDeclarationStatement();
            vDS.Variables.Add(scriptSharpVariable);
            return vDS;
        }

    }
}
