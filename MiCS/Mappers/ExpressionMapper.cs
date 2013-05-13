 using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Builders;
using ScriptSharp.CodeModel;



namespace MiCS.Mappers
{
    /// <summary>
    /// Class with extension mapping methods that are used to map
    /// from Roslyn AST nodes to ScriptSharp expressions.
    /// </summary>
    internal static class ExpressionMapper
    {
        /// <summary>
        /// Returns mapped ScriptSharp UnaryExpression with specified operand expression.
        /// </summary>
        /// <remarks>Currently only minus and exclamationmark unary operators are supported.</remarks>
        static internal SS.UnaryExpression Map(this PrefixUnaryExpressionSyntax prefixUnaryExpression, SS.Expression ssOperandExpression)
        {
            if (prefixUnaryExpression.OperatorToken.Kind == SyntaxKind.MinusToken)
                return new SS.UnaryExpression(SS.Operator.Minus, ssOperandExpression);
            if (prefixUnaryExpression.OperatorToken.Kind == SyntaxKind.ExclamationToken)
                return new SS.UnaryExpression(SS.Operator.LogicalNot, ssOperandExpression);
            else
                throw new NotSupportedException("Prefix unary operator is currently not supported.");
        }

        /// <summary>
        /// Return ScriptSharp LiteralExpression (when initializer syntax is used) or NewExpression representing new Array creation.
        /// </summary>
        /// <param name="arrayCreationExpression">Roslyn ArrayCreationExpression AST node.</param>
        /// <param name="associatedType"></param>
        /// <param name="associatedParent"></param>
        /// <returns></returns>
        static internal SS.Expression Map(this ArrayCreationExpressionSyntax arrayCreationExpression, SS.TypeSymbol associatedType, SS.MemberSymbol associatedParent)
        {
            SS.TypeSymbol arrayTypeSymbol = new SS.ClassSymbol("Array", new SS.NamespaceSymbol("System", null));
            arrayTypeSymbol.SetIgnoreNamespace();
            arrayTypeSymbol.SetArray();

            // If the array has any initializers, a LiteralExpression and not a NewExpression is returned.
            // This is ScriptSharps quirky way of building array syntax and is expected behavior.
            #region Region: Add initializer if any is used.

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

            if (exprs.Length > 0)
                return new SS.LiteralExpression(arrayTypeSymbol, exprs);

            #endregion

            SS.NewExpression newExpr = new SS.NewExpression(arrayTypeSymbol);
            return newExpr;
        }

        /// <summary>
        /// Returns mapped ScriptSharp NewExpression with the specified type.
        /// </summary>
        static internal SS.NewExpression Map(this ObjectCreationExpressionSyntax objectCreationExpression, SS.TypeSymbol associatedType)
        {
            if (associatedType == null)
                throw new Exception("AssociatedType cannot be null");
            if (!(associatedType is SS.ClassSymbol))
                throw new Exception("Only ClassSymbols as associated type is currently supported.");

            return new SS.NewExpression(associatedType);
        }

        /// <summary>
        /// Returns mapped ScriptSharp BinaryExpression with specified left and right expressions.
        /// </summary>
        static internal SS.BinaryExpression Map(this BinaryExpressionSyntax expr, SS.Expression ssLeftExpression, SS.Expression ssRightExpression)
        {
            var op = expr.OperatorToken.Kind;
            /*
             * C# operators
             * http://msdn.microsoft.com/en-us/library/6a71f45d(v=vs.80).aspx
             */
            switch (op)
            {
                case SyntaxKind.EqualsToken:
                    return new SS.BinaryExpression(SS.Operator.Equals, ssLeftExpression, ssRightExpression);

                // Arithmetic expressions
                case SyntaxKind.PlusToken:
                    return new SS.BinaryExpression(SS.Operator.Plus, ssLeftExpression, ssRightExpression);
                case SyntaxKind.MinusToken:
                    return new SS.BinaryExpression(SS.Operator.Minus, ssLeftExpression, ssRightExpression);
                case SyntaxKind.AsteriskToken:
                    return new SS.BinaryExpression(SS.Operator.Multiply, ssLeftExpression, ssRightExpression);
                case SyntaxKind.SlashToken:
                    return new SS.BinaryExpression(SS.Operator.Divide, ssLeftExpression, ssRightExpression);
                case SyntaxKind.PercentToken:
                    return new SS.BinaryExpression(SS.Operator.Mod, ssLeftExpression, ssRightExpression);

                // Relational expressions (C# "is" and "as" operators are not currently supported).
                case SyntaxKind.EqualsEqualsToken:
                    return new SS.BinaryExpression(SS.Operator.EqualEqual, ssLeftExpression, ssRightExpression);
                case SyntaxKind.ExclamationEqualsToken:
                    return new SS.BinaryExpression(SS.Operator.NotEqual, ssLeftExpression, ssRightExpression);
                case SyntaxKind.GreaterThanToken:
                    return new SS.BinaryExpression(SS.Operator.Greater, ssLeftExpression, ssRightExpression);
                case SyntaxKind.LessThanToken:
                    return new SS.BinaryExpression(SS.Operator.Less, ssLeftExpression, ssRightExpression);
                case SyntaxKind.GreaterThanEqualsToken:
                    return new SS.BinaryExpression(SS.Operator.GreaterEqual, ssLeftExpression, ssRightExpression);
                case SyntaxKind.LessThanEqualsToken:
                    return new SS.BinaryExpression(SS.Operator.LessEqual, ssLeftExpression, ssRightExpression);

                // Logical expressions (C# "conditional and" and "conditional or") 
                case SyntaxKind.AmpersandAmpersandToken:
                    return new SS.BinaryExpression(SS.Operator.LogicalAnd, ssLeftExpression, ssRightExpression);
                case SyntaxKind.BarBarToken:
                    return new SS.BinaryExpression(SS.Operator.LogicalOr, ssLeftExpression, ssRightExpression);

                default:
                    throw new NotSupportedException("Binary expression operator not supported!");
            }

        }

        /// <summary>
        /// Returns mapped ScriptSharp LocalExpression with VariableSymbol (mapped from Roslyn Identifier name).
        /// </summary>
        /// <param name="identifierName">Roslyn identifier name AST node</param>
        /// <param name="valueType">The type of the mapped variable symbol.</param>
        /// <param name="parent">The parent member symbol (MethodSymbol) that contains the mapped variable symbol</param>
        static internal SS.LocalExpression Map(this IdentifierNameSyntax identifierName, SS.TypeSymbol valueType, SS.MemberSymbol parent)
        {
            return new SS.LocalExpression(new SS.VariableSymbol(identifierName.Identifier.ValueText, parent, valueType));
        }

        /// <summary>
        /// Returns mapped ScriptSharp LiteralExpression.
        /// </summary>
        static internal SS.LiteralExpression Map(this LiteralExpressionSyntax literalExpression)
        {
            var @value = literalExpression.Token.Value;
            var ssValue = TypeManager.GetTypeSymbol(literalExpression).Map();

            switch (literalExpression.Kind)
            {
                case SyntaxKind.StringLiteralExpression:
                    return new SS.LiteralExpression(ssValue, (string)@value);
                case SyntaxKind.NumericLiteralExpression:
                    if (@value is int)
                        return new SS.LiteralExpression(ssValue, (int)@value);
                    if (@value is double)
                        return new SS.LiteralExpression(ssValue, (double)@value);
                    if (@value is float)
                        return new SS.LiteralExpression(ssValue, (float)@value);
                    if (@value is decimal)
                        return new SS.LiteralExpression(ssValue, (decimal)@value);
                    if (@value is uint)
                        return new SS.LiteralExpression(ssValue, (uint)@value);
                    if (@value is long)
                        return new SS.LiteralExpression(ssValue, (long)@value);
                    if (@value is ulong)
                        return new SS.LiteralExpression(ssValue, (ulong)@value);
                    if (@value is short)
                        return new SS.LiteralExpression(ssValue, (short)@value);
                    if (@value is ushort)
                        return new SS.LiteralExpression(ssValue, (ushort)@value);
                    throw new NotSupportedException("Literal type is not supported!");
                case SyntaxKind.FalseLiteralExpression:
                    return new SS.LiteralExpression(ssValue, (bool)@value);
                case SyntaxKind.TrueLiteralExpression:
                    return new SS.LiteralExpression(ssValue, (bool)@value);
                case SyntaxKind.CharacterLiteralExpression:
                    return new SS.LiteralExpression(ssValue, (char)@value);
                case SyntaxKind.NullLiteralExpression:
                    return new SS.LiteralExpression(ssValue, null);
                default:
                    throw new NotSupportedException("Literal type is not supported!");
            }
        }
        // Todo: Maybe try and clean up more...
        /// <summary>
        /// Return mapped ScriptSharp MethodExpression representing method invocation.
        /// </summary>
        /// <param name="invocation">Roslyn InvocationExpressionSyntax AST node.</param>
        /// <param name="ssParent">Parent ScriptSharp class containing the method being called.</param>
        /// <param name="ssParentMethod">Parent ScriptSharp method containing the invocation.</param>
        /// <param name="ssParameters">ScriptSharp invocation parameters.</param>
        static internal SS.MethodExpression Map(this InvocationExpressionSyntax invocation, SS.ClassSymbol ssParent, SS.MethodSymbol ssParentMethod, Collection<SS.Expression> ssParameters)
        {
            if (!(invocation.Expression is IdentifierNameSyntax) && !(invocation.Expression is MemberAccessExpressionSyntax))
                throw new NotSupportedException();

            // Todo: Unneeded check
            if (!(ssParent is SS.ClassSymbol)) 
                throw new NotSupportedException("The parent class symbol (of the method that is the invocation target) is required.");

            var ssParentClass = (SS.ClassSymbol)ssParent;
            if (!(ssParentClass.Parent is SS.NamespaceSymbol)) 
                throw new Exception("The parent class namespace is currently required.");

            var ssParentNamespace = (SS.NamespaceSymbol)ssParentClass.Parent; // Todo: Make parentNamespace method.

            if (invocation.Expression is IdentifierNameSyntax)
            {
                var identifierName = (IdentifierNameSyntax)invocation.Expression;

                var ssReturnType = TypeManager.GetReturnType(identifierName).Map();
                var ssMethodName = identifierName.GetScriptName();

                var ssMethodSymbol = new SS.MethodSymbol(ssMethodName, ssParentClass, ssReturnType);

                var ssThisExpr = new SS.ThisExpression(ssParentClass, false);
                return new SS.MethodExpression(SS.ExpressionType.MethodInvoke, ssThisExpr, ssMethodSymbol, ssParameters);

            }
            else if (invocation.Expression is MemberAccessExpressionSyntax)
            {
                var memberAccess = (MemberAccessExpressionSyntax)invocation.Expression;
                if (memberAccess.Expression is IdentifierNameSyntax)
                {
                    var objectReference = (IdentifierNameSyntax)memberAccess.Expression;

                    /*
                     * Verify correct use of supported core type (if
                     * this member access is on a supported core type).
                     */
                    //TypeManager.VerifyCorrectUseOfSupportedCoreType(invocation);
                    // Todo: Consider writing for report issues with only verify correct use of core
                    // types on 'foreign' member access. If you can inherit from a core type this
                    // could maybe be problematic, but as inheritance is not within the scope of our
                    // project it might not be a problem anyways?

                    var ssObjectReferenceName = objectReference.GetScriptName();
                    var ssMethodName = memberAccess.Name.GetScriptName();

                    var ssReturnType = TypeManager.GetReturnType(memberAccess.Name).Map();
                    var ssMethodSymbol = new SS.MethodSymbol(ssMethodName, ssParentClass, ssReturnType);

                    var ssVariableType = TypeManager.GetTypeSymbol(objectReference).Map();
                    var ssObjectReference = new SS.VariableSymbol(ssObjectReferenceName, ssParentMethod, ssVariableType);

                    var ssLocalExpression = new SS.LocalExpression(ssObjectReference);
                    var ssMethodExpression = new SS.MethodExpression(SS.ExpressionType.MethodInvoke, ssLocalExpression, ssMethodSymbol, ssParameters);

                    return ssMethodExpression;
                }
                else if (memberAccess.Expression is ThisExpressionSyntax)
                {
                    // Todo: Is a bit redundant... maybe clean.
                    var ssMethodName = memberAccess.Name.GetScriptName();
                    var ssReturnType = TypeManager.GetReturnType(memberAccess.Name).Map();
                    var ssMethodSymbol = new SS.MethodSymbol(ssMethodName, ssParentClass, ssReturnType);

                    var ssThisExpression = new SS.ThisExpression(ssParentClass, true);
                    var ssMethodExpression = new SS.MethodExpression(SS.ExpressionType.MethodInvoke, ssThisExpression, ssMethodSymbol, ssParameters);

                    return ssMethodExpression;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Returns mapped ScriptSharp FieldExpression with the specified object reference and field symbol.
        /// </summary>
        static internal SS.FieldExpression Map(this MemberAccessExpressionSyntax memberAccess, SS.Expression ssObjectReference, SS.FieldSymbol ssField)
        {
            // Todo: Do we have any code that come in here? no tests it seems...
            return new SS.FieldExpression(ssObjectReference, ssField);
        }

        /// <summary>
        /// Returns mapped ScriptSharp ConditionalExpression with the specified condition, true expression and false expression.
        /// </summary>
        static internal SS.ConditionalExpression Map(this ConditionalExpressionSyntax conditionalExpression, 
            SS.Expression ssCondition, 
            SS.Expression ssTrueExpression, 
            SS.Expression ssFalseExpression
        )
        {
            return new SS.ConditionalExpression(ssCondition, ssTrueExpression, ssFalseExpression);
        }

        /// <summary>
        /// Returns mapped ScriptSharp IndexerExpression with the specified object 
        /// reference and indexer symbol (e.g. used for accessing Array elements).
        /// </summary>
        static internal SS.IndexerExpression Map(this ElementAccessExpressionSyntax elementAccessExpression, SS.Expression ssObjectReference, SS.IndexerSymbol ssIndexerSymbol)
        {
            return new SS.IndexerExpression(ssObjectReference, ssIndexerSymbol);
        }
    }
}
