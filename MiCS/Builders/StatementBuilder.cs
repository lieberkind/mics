using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Mappers;

namespace MiCS.Builders
{
    /// <summary>
    /// Builds ScriptSharp statements from Roslyn statements
    /// </summary>
    class StatementBuilder : SyntaxWalker
    {
        /// <summary>
        /// The ScriptSharp class in which the statements' parent method belong.
        /// </summary>
        SS.ClassSymbol ssTypeReference;

        /// <summary>
        /// The ScriptSharp member in which the statements belong.
        /// </summary>
        SS.MemberSymbol ssParentMember;

        /// <summary>
        /// List of generated ScriptSharp statements
        /// </summary>
        public readonly List<SS.Statement> ssStatements = new List<SS.Statement>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementBuilder"/> class.
        /// </summary>
        /// <param name="typeReference">The ScriptSharp class in which the statements' parent method belong.</param>
        /// <param name="ssParentMember">The ScriptSharp member in which the statements belong.</param>
        public StatementBuilder(SS.ClassSymbol typeReference, SS.MemberSymbol ssParentMember)
        {
            this.ssTypeReference = typeReference;
            this.ssParentMember = ssParentMember;
        }

        /// <summary>
        /// Builds an if statement along with its condition and branches
        /// </summary>
        /// <param name="ifStatement">If statement.</param>
        public override void VisitIfStatement(IfStatementSyntax ifStatement)
        {
            var ssCondition = ExpressionBuilder.BuildExpression(ifStatement.Condition, ssTypeReference, ssParentMember);
            var ssIfStatement = StatementBuilder.BuildStatement(ifStatement.Statement, ssTypeReference, ssParentMember);
            var ssElseStatement = ifStatement.Else == null ? null : StatementBuilder.BuildStatement(ifStatement.Else.Statement, ssTypeReference, ssParentMember);

            ssStatements.Add(ifStatement.Map(ssCondition, ssIfStatement, ssElseStatement));            
        }

        /// <summary>
        /// Builds a for statement along with its initializers, codition, increments and body
        /// </summary>
        /// <param name="forStatement">For statement.</param>
        /// <remarks>Currently has a problem with initializers not declared outside the for statement. Mapping the VaribleDeclarationSyntax should solve this.</remarks>
        /// <exception cref="System.NotSupportedException">Initializers cannot be null. Hint: declare intcrementors outside for statement.</exception>
        public override void VisitForStatement(ForStatementSyntax forStatement)
        {
            var ssForStatement = forStatement.Map();

            // Todo: Should forStatement.Declaration be checked and possibly mapped? Has todo with VariableDeclarationSyntax
            // Todo: Implement mapping of VariablDeclarationSyntax
            if (forStatement.Initializers == null)
                throw new NotSupportedException("Initializers cannot be null. Hint: declare intcrementors outside for statement.");

            foreach (var initializer in forStatement.Initializers)
                ssForStatement.AddInitializer(ExpressionBuilder.BuildExpression(initializer, ssTypeReference, ssParentMember));

            ssForStatement.AddCondition(ExpressionBuilder.BuildExpression(forStatement.Condition, ssTypeReference, ssParentMember));

            foreach (var incrementor in forStatement.Incrementors)
                ssForStatement.AddIncrement(ExpressionBuilder.BuildExpression(incrementor, ssTypeReference, ssParentMember));

            ssForStatement.AddBody(StatementBuilder.BuildStatement(forStatement.Statement, ssTypeReference, ssParentMember));

            ssStatements.Add(ssForStatement);
        }

        /// <summary>
        /// Builds a block statement along with its child statements
        /// </summary>
        /// <param name="block">The block.</param>
        public override void VisitBlock(BlockSyntax block)
        {
            var ssBlock = block.Map();

            var ssChildStatements = new List<SS.Statement>();

            foreach (var statement in block.Statements)
            {
                ssChildStatements.Add(StatementBuilder.BuildStatement(statement, ssTypeReference, ssParentMember));
            }

            ssBlock.Statements.AddRange(ssChildStatements);
            ssStatements.Add(ssBlock);
        }

        /// <summary>
        /// Builds a return statement along with its expression
        /// </summary>
        /// <param name="returnStatement">The return statement.</param>
        public override void VisitReturnStatement(ReturnStatementSyntax returnStatement)
        {
            var ssExpression = ExpressionBuilder.BuildExpression(returnStatement.Expression, ssTypeReference, ssParentMember);

            ssStatements.Add(returnStatement.Map(ssExpression));
        }

        /// <summary>
        /// Builds an expression statement along with its expression
        /// </summary>
        /// <param name="expressionStatement">The expression statement.</param>
        public override void VisitExpressionStatement(ExpressionStatementSyntax expressionStatement)
        {
            var ssExpression = ExpressionBuilder.BuildExpression(expressionStatement.Expression, ssTypeReference, ssParentMember);
            
            ssStatements.Add(expressionStatement.Map(ssExpression));

        }

        /// <summary>
        /// Builds a ScriptSharp VariableSymbol from a local declaration statement, along with its initializer
        /// </summary>
        /// <param name="localDeclarationStatement">The local declaration statement.</param>
        /// <exception cref="System.NotSupportedException">
        /// LocalDeclarationStatement has unsupported declaration
        /// or
        /// Unsupported initializer
        /// </exception>
        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax localDeclarationStatement)
        {
            var variable = localDeclarationStatement.Declaration.Variables[0];

            if (!(localDeclarationStatement.Declaration is VariableDeclarationSyntax))
                throw new NotSupportedException("LocalDeclarationStatement has unsupported declaration");
 
            var type = TypeManager.GetTypeSymbol(localDeclarationStatement.Declaration.Type);
            var ssVariable = variable.Map(ssParentMember, type.Map());

            var initializer = variable.Initializer;
            if (initializer != null)
            {
                if (!(initializer is EqualsValueClauseSyntax))
                    throw new NotSupportedException("Unsupported initializer");

                var val = initializer.Value;

                ssVariable.SetValue(ExpressionBuilder.BuildExpression(val, ssTypeReference, ssParentMember));
            }

            var ssVariableDecalarationStatement = new SS.VariableDeclarationStatement();
            ssVariableDecalarationStatement.Variables.Add(ssVariable);

            ssStatements.Add(ssVariableDecalarationStatement);
        }

        /// <summary>
        /// Builds a single statement
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="ssTypeReference">The ScriptSharp class in which the statements' parent method belong.</param>
        /// <param name="ssParentMember">The ScriptSharp member in which the statements belong.</param>
        /// <returns></returns>
        public static SS.Statement BuildStatement(StatementSyntax statement, SS.ClassSymbol ssTypeReference, SS.MemberSymbol ssParentMember)
        {
            return StatementBuilder.BuildStatements(statement, ssTypeReference, ssParentMember).First();
        }

        /// <summary>
        /// Builds a list of ScriptSharp statements from a single statement
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="ssTypeReference">The ScriptSharp class in which the statements' parent method belong.</param>
        /// <param name="ssParentMember">The ScriptSharp member in which the statements belong.</param>
        /// <returns></returns>
        public static List<SS.Statement> BuildStatements(StatementSyntax statement, SS.ClassSymbol ssTypeReference, SS.MemberSymbol ssParentMember)
        {
            var statementBuilder = new StatementBuilder(ssTypeReference, ssParentMember);
            statementBuilder.Visit(statement);

            return statementBuilder.ssStatements;
        }
    }
}
