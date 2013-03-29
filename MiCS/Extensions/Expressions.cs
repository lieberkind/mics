﻿using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Todo: DOM representation!
// Todo: Check how is C# built in complex types (e.g. String & DateTime) supported?
// Todo: Consider the return type issue related to MethodSymbol nodes (see mapping method).

namespace MiCS.Extensions
{
    public static class Expressions
    {
        static internal Expression Map(this ExpressionSyntax expr, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        {
            if (expr is IdentifierNameSyntax)
                return ((IdentifierNameSyntax)expr).Map();
            else if (expr is LiteralExpressionSyntax)
                return ((LiteralExpressionSyntax)expr).Map();
            else if (expr is PrefixUnaryExpressionSyntax)
                return ((PrefixUnaryExpressionSyntax)expr).Map();
            else if (expr is BinaryExpressionSyntax)
                return ((BinaryExpressionSyntax)expr).Map();
            else if (expr is InvocationExpressionSyntax)
                return ((InvocationExpressionSyntax)expr).Map(parent);
            else if (expr is ObjectCreationExpressionSyntax)
                return ((ObjectCreationExpressionSyntax)expr).Map(parent); 
            else if (expr is ConditionalExpressionSyntax)
                return ((ConditionalExpressionSyntax)expr).Map();
            else
                throw new NotSupportedException("This type of expression is currently not supported!");
        }

        static internal UnaryExpression Map(this PrefixUnaryExpressionSyntax expr)
        {
            if (expr.OperatorToken.Kind == SyntaxKind.MinusToken)
                return new UnaryExpression(Operator.Minus, expr.Operand.Map());
            else
                throw new NotSupportedException("Prefix unary operator is currently not supported.");
        }

        static internal NewExpression Map(this ObjectCreationExpressionSyntax expr, ScriptSharp.ScriptModel.TypeSymbol associatedType)
        {
            if (associatedType == null)
                throw new Exception("AssociatedType cannot be null");
            if (!(associatedType is ClassSymbol))
                throw new Exception("Only ClassSymbols as associated type is currently supported.");

            //Todo: Handle arguments/parameters...
            if (expr.ArgumentList.Arguments.Count > 0)
                throw new NotSupportedException("Arguments are currently not supported.");
            return new NewExpression(associatedType);
        }

        static internal BinaryExpression Map(this BinaryExpressionSyntax expr)
        {
            var op = expr.OperatorToken.Kind;
            /*
             * C# operators
             * http://msdn.microsoft.com/en-us/library/6a71f45d(v=vs.80).aspx
             */
            switch (op)
            {
                case SyntaxKind.EqualsToken:
                    if (expr.Left is IdentifierNameSyntax)
                    {
                        if (expr.Right is LiteralExpressionSyntax)
                            return new BinaryExpression(Operator.Equals, expr.Left.Map(), expr.Right.Map());
                        else if (expr.Right is IdentifierNameSyntax)
                            return new BinaryExpression(Operator.Equals, expr.Left.Map(), expr.Right.Map());
                        else
                            throw new NotSupportedException("The right side of this binary is not supported with a IdentifierNameSyntax right side.");
                    }
                    else
                        throw new NotSupportedException("Left operator of binary expression is not supported!");
                case SyntaxKind.PlusToken:
                    return new BinaryExpression(Operator.Plus, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.MinusToken:
                    return new BinaryExpression(Operator.Minus, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.AsteriskToken:
                    return new BinaryExpression(Operator.Multiply, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.SlashToken:
                    return new BinaryExpression(Operator.Divide, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.PercentToken:
                    return new BinaryExpression(Operator.Mod, expr.Left.Map(), expr.Right.Map());

                    // Todo: consider use of strict operators such as "===".
                    // Relational expressions (C# "is" and "as" operators are not currently supported).
                case SyntaxKind.EqualsEqualsToken:
                    return new BinaryExpression(Operator.EqualEqual, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.ExclamationEqualsToken:
                    return new BinaryExpression(Operator.NotEqual, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.GreaterThanToken:
                    return new BinaryExpression(Operator.Greater, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.LessThanToken:
                    return new BinaryExpression(Operator.Less, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.GreaterThanEqualsToken:
                    return new BinaryExpression(Operator.GreaterEqual, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.LessThanEqualsToken:
                    return new BinaryExpression(Operator.LessEqual, expr.Left.Map(), expr.Right.Map());

                    // Logical expressions
                    // C# "conditional and" and "conditional or" 
                case SyntaxKind.AmpersandAmpersandToken:
                    return new BinaryExpression(Operator.LogicalAnd, expr.Left.Map(), expr.Right.Map());
                case SyntaxKind.BarBarToken:
                    return new BinaryExpression(Operator.LogicalOr, expr.Left.Map(), expr.Right.Map());

                default:
                    throw new NotSupportedException("Binary expression operator not supported!");
            }


        }

        static internal LocalExpression Map(this IdentifierNameSyntax expr)
        {
            return new LocalExpression(new VariableSymbol(expr.Identifier.ValueText, null, null));
        }

        static internal LiteralExpression Map(this LiteralExpressionSyntax expr)
        {
            var val = expr.Token.Value;
            switch (expr.Kind)
            {
                case SyntaxKind.StringLiteralExpression:
                    return new LiteralExpression(null, (string)val);
                case SyntaxKind.NumericLiteralExpression:
                    if (val is int)
                        return new LiteralExpression(null, (int)val);
                    if (val is double)
                        return new LiteralExpression(null, (double)val);
                    if (val is float)
                        return new LiteralExpression(null, (float)val);
                    if (val is decimal)
                        return new LiteralExpression(null, (decimal)val);
                    if (val is uint)
                        return new LiteralExpression(null, (uint)val);
                    if (val is long)
                        return new LiteralExpression(null, (long)val);
                    if (val is ulong)
                        return new LiteralExpression(null, (ulong)val);
                    if (val is short)
                        return new LiteralExpression(null, (short)val);
                    if (val is ushort)
                        return new LiteralExpression(null, (ushort)val);
                    throw new NotSupportedException("Literal type is not supported!");
                case SyntaxKind.FalseLiteralExpression:
                    return new LiteralExpression(null, (bool)val);
                case SyntaxKind.TrueLiteralExpression:
                    return new LiteralExpression(null, (bool)val);
                case SyntaxKind.CharacterLiteralExpression:
                    return new LiteralExpression(null, (char)val);
                case SyntaxKind.NullLiteralExpression:
                    return new LiteralExpression(null, null);
                default:
                    throw new NotSupportedException("Literal type is not supported!");
            }
        }

        static internal MethodExpression Map(this InvocationExpressionSyntax expr, ScriptSharp.ScriptModel.TypeSymbol parent)
        {
            if (!(expr.Expression is IdentifierNameSyntax))
                throw new NotSupportedException("Currently only this/local invocations is supported!");

            if (!(expr.Parent is ExpressionStatementSyntax))
                throw new NotSupportedException("Method invocation is being performed in a context that is not currently supported.");

            if (!(parent is ClassSymbol)) 
                throw new NotSupportedException("The parent class symbol (of the method that is the invocation target) is required.");

            var parentClass = (ClassSymbol)parent;
            if (!(parentClass.Parent is ScriptSharp.ScriptModel.NamespaceSymbol)) 
                throw new Exception("The parent class namespace is currently required.");

            var parentNamespace = (ScriptSharp.ScriptModel.NamespaceSymbol)parentClass.Parent;

            if (expr.Expression is IdentifierNameSyntax)
            {
                var iNS = (IdentifierNameSyntax)expr.Expression;

                // Todo: Not sure if the return type is important at all?
                //var voidReturnType = new ClassSymbol("void", parentNamespace);
                //var methodSymbol = new ScriptSharp.ScriptModel.MethodSymbol(iNS.Identifier.ValueText, parentClass, voidReturnType);

                var methodSymbols = parentClass.Members.Where(m => m.Type == SymbolType.Method && m.Name.Equals(iNS.Identifier.ValueText));
                var methodSymbol = (ScriptSharp.ScriptModel.MethodSymbol)methodSymbols.First();
                var parameters = new Collection<Expression>();
                foreach (var arg in expr.ArgumentList.Arguments)
                {
                    parameters.Add(arg.Expression.Map());
                }

                var thisExpr = new ThisExpression(parentClass, true);
                return new MethodExpression(ExpressionType.MethodInvoke, thisExpr, methodSymbol, null);
            }
            else
            {
                // Member access invocation is currently not supported.
                throw new NotImplementedException();
            }
        }

        static internal ConditionalExpression Map(this ConditionalExpressionSyntax expr)
        {
            return new ConditionalExpression(expr.Condition.Map(), expr.WhenTrue.Map(), expr.WhenFalse.Map());
        }
    }
}
