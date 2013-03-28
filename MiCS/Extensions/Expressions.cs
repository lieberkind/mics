using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else if (expr is BinaryExpressionSyntax)
                return ((BinaryExpressionSyntax)expr).Map();
            else if (expr is InvocationExpressionSyntax)
                return ((InvocationExpressionSyntax)expr).Map(parent);
            else if (expr is ObjectCreationExpressionSyntax)
                return ((ObjectCreationExpressionSyntax)expr).Map(parent);
            else
                throw new NotSupportedException("This type of expression is currently not supported!");
        }

        static internal NewExpression Map(this ObjectCreationExpressionSyntax expr, ClassSymbol associatedType)
        {
            if (associatedType == null)
                throw new Exception("AssociatedType cannot be null");

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
            if (!(parent is ClassSymbol)) throw new NotSupportedException();
            var parentClass = (ClassSymbol)parent;

            if (expr.Expression is IdentifierNameSyntax)
            {
                var iNS = (IdentifierNameSyntax)expr.Expression;
                var parentNamespace = (ScriptSharp.ScriptModel.NamespaceSymbol)parentClass.Parent;

                // Todo: Not sure if the return type is important at all?
                var voidReturnType = new ClassSymbol("Void", parentNamespace);

                var mS = new ScriptSharp.ScriptModel.MethodSymbol(iNS.Identifier.ValueText, parentClass, voidReturnType);
                var ps = new Collection<Expression>();
                foreach (var arg in expr.ArgumentList.Arguments)
                {
                    ps.Add(arg.Expression.Map());
                }
                var vS = new VariableSymbol("name", null, null);
                var lE = new LocalExpression(vS);
                var mE = new MethodExpression(ExpressionType.MethodInvoke, lE, mS, null);

                return mE;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

    }
}
