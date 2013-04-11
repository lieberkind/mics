using Roslyn.Compilers.CSharp;
using SS = ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiCS.Builders;

// Todo: DOM representation!
// Todo: Check how is C# built in complex types (e.g. String & DateTime) supported?
// Todo: Consider the return type issue related to MethodSymbol nodes (see mapping method).

namespace MiCS.Mappers
{
    public static class Expressions
    {
        //static internal Expression Map(this ExpressionSyntax expr, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        //{
        //    if (expr is IdentifierNameSyntax)
        //        return ((IdentifierNameSyntax)expr).Map();
        //    else if (expr is LiteralExpressionSyntax)
        //        return ((LiteralExpressionSyntax)expr).Map();
        //    else if (expr is PrefixUnaryExpressionSyntax)
        //        return ((PrefixUnaryExpressionSyntax)expr).Map();
        //    else if (expr is BinaryExpressionSyntax)
        //        return ((BinaryExpressionSyntax)expr).Map();
        //    else if (expr is InvocationExpressionSyntax)
        //        return ((InvocationExpressionSyntax)expr).Map(parent);
        //    else if (expr is ObjectCreationExpressionSyntax)
        //        return ((ObjectCreationExpressionSyntax)expr).Map(parent); 
        //    else if (expr is ConditionalExpressionSyntax)
        //        return ((ConditionalExpressionSyntax)expr).Map();
        //    else
        //        throw new NotSupportedException("This type of expression is currently not supported!");
        //}

        static internal SS.UnaryExpression Map(this PrefixUnaryExpressionSyntax prefixUnaryExpression, SS.Expression ssOperandExpression)
        {
            if (prefixUnaryExpression.OperatorToken.Kind == SyntaxKind.MinusToken)
                return new SS.UnaryExpression(SS.Operator.Minus, ssOperandExpression);
            else
                throw new NotSupportedException("Prefix unary operator is currently not supported.");
        }

        static internal SS.NewExpression Map(this ObjectCreationExpressionSyntax objectCreationExpression, SS.TypeSymbol associatedType)
        {
            if (associatedType == null)
                throw new Exception("AssociatedType cannot be null");
            if (!(associatedType is SS.ClassSymbol))
                throw new Exception("Only ClassSymbols as associated type is currently supported.");

            return new SS.NewExpression(associatedType);
        }

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
                    if (expr.Left is IdentifierNameSyntax)
                    {
                        if (expr.Right is LiteralExpressionSyntax)
                            return new SS.BinaryExpression(SS.Operator.Equals, ssLeftExpression, ssRightExpression);
                        else if (expr.Right is IdentifierNameSyntax)
                            return new SS.BinaryExpression(SS.Operator.Equals, ssLeftExpression, ssRightExpression);
                        else
                            throw new NotSupportedException("The right side of this binary is not supported with a IdentifierNameSyntax right side.");
                    }
                    else
                        throw new NotSupportedException("Left operator of binary expression is not supported!");
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

                // Todo: consider use of strict operators such as "===".
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

                // Logical expressions
                // C# "conditional and" and "conditional or" 
                case SyntaxKind.AmpersandAmpersandToken:
                    return new SS.BinaryExpression(SS.Operator.LogicalAnd, ssLeftExpression, ssRightExpression);
                case SyntaxKind.BarBarToken:
                    return new SS.BinaryExpression(SS.Operator.LogicalOr, ssLeftExpression, ssRightExpression);

                default:
                    throw new NotSupportedException("Binary expression operator not supported!");
            }

            // OLD VERSION WITHOUT WALKERS
            //var op = expr.OperatorToken.Kind;
            ///*
            // * C# operators
            // * http://msdn.microsoft.com/en-us/library/6a71f45d(v=vs.80).aspx
            // */
            //switch (op)
            //{
            //    case SyntaxKind.EqualsToken:
            //        if (expr.Left is IdentifierNameSyntax)
            //        {
            //            if (expr.Right is LiteralExpressionSyntax)
            //                return new BinaryExpression(Operator.Equals, expr.Left.Map(), expr.Right.Map());
            //            else if (expr.Right is IdentifierNameSyntax)
            //                return new BinaryExpression(Operator.Equals, expr.Left.Map(), expr.Right.Map());
            //            else
            //                throw new NotSupportedException("The right side of this binary is not supported with a IdentifierNameSyntax right side.");
            //        }
            //        else
            //            throw new NotSupportedException("Left operator of binary expression is not supported!");
            //    case SyntaxKind.PlusToken:
            //        return new BinaryExpression(Operator.Plus, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.MinusToken:
            //        return new BinaryExpression(Operator.Minus, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.AsteriskToken:
            //        return new BinaryExpression(Operator.Multiply, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.SlashToken:
            //        return new BinaryExpression(Operator.Divide, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.PercentToken:
            //        return new BinaryExpression(Operator.Mod, expr.Left.Map(), expr.Right.Map());

            //        // Todo: consider use of strict operators such as "===".
            //        // Relational expressions (C# "is" and "as" operators are not currently supported).
            //    case SyntaxKind.EqualsEqualsToken:
            //        return new BinaryExpression(Operator.EqualEqual, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.ExclamationEqualsToken:
            //        return new BinaryExpression(Operator.NotEqual, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.GreaterThanToken:
            //        return new BinaryExpression(Operator.Greater, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.LessThanToken:
            //        return new BinaryExpression(Operator.Less, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.GreaterThanEqualsToken:
            //        return new BinaryExpression(Operator.GreaterEqual, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.LessThanEqualsToken:
            //        return new BinaryExpression(Operator.LessEqual, expr.Left.Map(), expr.Right.Map());

            //        // Logical expressions
            //        // C# "conditional and" and "conditional or" 
            //    case SyntaxKind.AmpersandAmpersandToken:
            //        return new BinaryExpression(Operator.LogicalAnd, expr.Left.Map(), expr.Right.Map());
            //    case SyntaxKind.BarBarToken:
            //        return new BinaryExpression(Operator.LogicalOr, expr.Left.Map(), expr.Right.Map());

            //    default:
            //        throw new NotSupportedException("Binary expression operator not supported!");
            //}


        }

        static internal SS.LocalExpression Map(this IdentifierNameSyntax identifierName)
        {
            // Todo: Set parent and value type as done in ScriptSharp
            return new SS.LocalExpression(new SS.VariableSymbol(identifierName.Identifier.ValueText, null, null));
        }

        static internal SS.LiteralExpression Map(this LiteralExpressionSyntax literalExpression)
        {
            var @value = literalExpression.Token.Value;

            // Todo: Set valueType parameter as done in ScriptSharp
            switch (literalExpression.Kind)
            {
                case SyntaxKind.StringLiteralExpression:
                    return new SS.LiteralExpression(null, (string)@value);
                case SyntaxKind.NumericLiteralExpression:
                    if (@value is int)
                        return new SS.LiteralExpression(null, (int)@value);
                    if (@value is double)
                        return new SS.LiteralExpression(null, (double)@value);
                    if (@value is float)
                        return new SS.LiteralExpression(null, (float)@value);
                    if (@value is decimal)
                        return new SS.LiteralExpression(null, (decimal)@value);
                    if (@value is uint)
                        return new SS.LiteralExpression(null, (uint)@value);
                    if (@value is long)
                        return new SS.LiteralExpression(null, (long)@value);
                    if (@value is ulong)
                        return new SS.LiteralExpression(null, (ulong)@value);
                    if (@value is short)
                        return new SS.LiteralExpression(null, (short)@value);
                    if (@value is ushort)
                        return new SS.LiteralExpression(null, (ushort)@value);
                    throw new NotSupportedException("Literal type is not supported!");
                case SyntaxKind.FalseLiteralExpression:
                    return new SS.LiteralExpression(null, (bool)@value);
                case SyntaxKind.TrueLiteralExpression:
                    return new SS.LiteralExpression(null, (bool)@value);
                case SyntaxKind.CharacterLiteralExpression:
                    return new SS.LiteralExpression(null, (char)@value);
                case SyntaxKind.NullLiteralExpression:
                    return new SS.LiteralExpression(null, null);
                default:
                    throw new NotSupportedException("Literal type is not supported!");
            }
        }

        static internal SS.MethodExpression Map(this InvocationExpressionSyntax expr, SS.TypeSymbol ssParent, Collection<SS.Expression> ssParameters)
        {
            if (!(expr.Expression is IdentifierNameSyntax) && !(expr.Expression is MemberAccessExpressionSyntax))
                throw new NotSupportedException("Currently only this/local invocations is supported!");

            if (!(expr.Parent is ExpressionStatementSyntax) &&
                !(expr.Parent is ReturnStatementSyntax))
                throw new NotSupportedException("Method invocation is being performed in a context that is not currently supported.");

            if (!(ssParent is SS.ClassSymbol)) 
                throw new NotSupportedException("The parent class symbol (of the method that is the invocation target) is required.");

            var parentClass = (SS.ClassSymbol)ssParent;
            if (!(parentClass.Parent is SS.NamespaceSymbol)) 
                throw new Exception("The parent class namespace is currently required.");

            var parentNamespace = (SS.NamespaceSymbol)parentClass.Parent;

            // Todo: This check is probably unneeded. It has already been checked.
            if (expr.Expression is IdentifierNameSyntax)
            {
                var iNS = (IdentifierNameSyntax)expr.Expression;

                // Todo: Not sure if the return type is important at all? as the static return types doesn't really exist in JavaScript.
                var voidReturnType = new SS.ClassSymbol("void", parentNamespace);
                var methodSymbol = new SS.MethodSymbol(iNS.Identifier.ValueText, parentClass, voidReturnType);

                // Todo: Add support for non-local invocations
                var thisExpr = new SS.ThisExpression(parentClass, true);
                return new SS.MethodExpression(SS.ExpressionType.MethodInvoke, thisExpr, methodSymbol, ssParameters);

                // Todo: This constructor also seems to work. Not sure what the difference is.
                //return new MethodExpression(thisExpr, methodSymbol);
            }
            else if (expr.Expression is MemberAccessExpressionSyntax)
            {
                var iNS = (MemberAccessExpressionSyntax)expr.Expression;

                // Todo: Not sure if the return type is important at all? as the static return types doesn't really exist in JavaScript.
                var voidReturnType = new SS.ClassSymbol("void", parentNamespace);
                var methodSymbol = new SS.MethodSymbol(iNS.Name.Identifier.ValueText, parentClass, voidReturnType);

                // Todo: Add support for non-local invocations
                //var thisExpr = new SS.LocalExpression(ExpressionBuilder.Build(iNS.Expression));
                return new SS.MethodExpression(SS.ExpressionType.MethodInvoke, thisExpr, methodSymbol, ssParameters);

                // Todo: This constructor also seems to work. Not sure what the difference is.
                //return new MethodExpression(thisExpr, methodSymbol);   
            }
            else
            {
                throw new NotImplementedException("Member access invocation is currently not supported.");
            }
        }

        static internal SS.FieldExpression Map(this MemberAccessExpressionSyntax memberAccess, SS.Expression ssObjectReference, SS.FieldSymbol ssField)
        {
            return new SS.FieldExpression(ssObjectReference, ssField);
        }

        static internal SS.ConditionalExpression Map(this ConditionalExpressionSyntax conditionalExpression, 
            SS.Expression ssCondition, 
            SS.Expression ssTrueExpression, 
            SS.Expression ssFalseExpression
        )
        {
            return new SS.ConditionalExpression(ssCondition, ssTrueExpression, ssFalseExpression);
        }
    }
}
