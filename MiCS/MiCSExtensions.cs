using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    public static class MiCSExtensions
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

        static internal VariableDeclarationStatement Map(this LocalDeclarationStatementSyntax stmt, ScriptSharp.ScriptModel.TypeSymbol parent)
        {
            var v = stmt.Declaration.Variables[0];
            var identifier = v.Identifier;
            if (identifier.Kind != SyntaxKind.IdentifierToken)
                throw new NotSupportedException(); // Maybe not a necesary check...


            var vS = new VariableSymbol(identifier.ValueText, null, null);

            if (v.Initializer != null) {
                if (v.Initializer.Value is LiteralExpressionSyntax)
                    vS.SetValue(v.Initializer.Value.Map());
                else if (v.Initializer.Value is InvocationExpressionSyntax)
                    vS.SetValue(v.Initializer.Value.Map(parent));
                else
                    throw new NotSupportedException();
            }

            var vDS = new VariableDeclarationStatement();
            vDS.Variables.Add(vS);
            return vDS;
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


        //static internal Expression Map(this ExpressionSyntax expr)
        //{
        //    return expr.Map(null);
        //}

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
            else
                throw new NotSupportedException("This type of expression is currently not supported!");
        }

        static internal BinaryExpression Map(this BinaryExpressionSyntax expr)
        {
            var op = expr.OperatorToken.Kind;
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
                var parentNamespace = iNS.ParentNamespace().Map();

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
            else{
                throw new NotSupportedException();
            }
        }

        #region Region: Parent Nodes

        static internal NamespaceDeclarationSyntax ParentNamespace(this ExpressionSyntax expr)
        {
            if (expr.Parent is NamespaceDeclarationSyntax)
            {
                return (NamespaceDeclarationSyntax)expr.Parent;
            }
            else
            {
                return expr.Parent.ParentNamespace();
            }
        }

        static internal NamespaceDeclarationSyntax ParentNamespace(this SyntaxNode expr)
        {
            if (expr.Parent is NamespaceDeclarationSyntax)
            {
                return (NamespaceDeclarationSyntax)expr.Parent;
            }
            else
            {
                return expr.Parent.ParentNamespace();
            }
        }

        //static internal ClassDeclarationSyntax ParentClass(this ExpressionSyntax expr)
        //{
        //    if (expr.Parent is ClassDeclarationSyntax)
        //    {
        //        return (ClassDeclarationSyntax)expr.Parent;
        //    }
        //    else
        //    {
        //        return expr.Parent.ParentClass();
        //    }
        //}

        //static internal ClassDeclarationSyntax ParentClass(this SyntaxNode expr)
        //{
        //    if (expr.Parent is ClassDeclarationSyntax)
        //    {
        //        return (ClassDeclarationSyntax)expr.Parent;
        //    }
        //    else
        //    {
        //        return expr.Parent.ParentClass();
        //    }
        //}

        #endregion




        static internal ScriptSharp.ScriptModel.ParameterSymbol Map(this ParameterSyntax p)
        {
            return new ScriptSharp.ScriptModel.ParameterSymbol(p.Identifier.ValueText, null, null, ParameterMode.InOut);
        }

        static internal ScriptSharp.ScriptModel.MethodSymbol Map(this MethodDeclarationSyntax mD, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        {
            //var parentNamespace = new ScriptSharp.ScriptModel.NamespaceSymbol(parent.Namespace, null);
            //var returnType = new ResourcesSymbol("ResourcesSymbolName", parentNamespace);
            var method = new ScriptSharp.ScriptModel.MethodSymbol(mD.Identifier.ValueText, parent, null);

            var i = new List<Statement>();
            foreach (var item in mD.Body.Statements)
            {
                i.Add(item.Map(parent));
            }
            var sI = new SymbolImplementation(i, null, "symbolImplementationThisIdentifier_" + method.GeneratedName);
            method.AddImplementation(sI);
            return method;

        }

        static internal ScriptSharp.ScriptModel.ClassSymbol Map(this ClassDeclarationSyntax cD)
        {
            var parentNamespace = cD.ParentNamespace().Map();
            var cl = new ClassSymbol(cD.Identifier.ValueText, parentNamespace);

            foreach (var methodMember in cD.DescendantNodes().Where(m => m.Kind == SyntaxKind.MethodDeclaration))
            {
                var mD = (MethodDeclarationSyntax)methodMember;
                var mapMethod = false;
                if (mD.AttributeLists.Any())
                {
                    foreach (var attList in mD.AttributeLists)
                    {
                        foreach (AttributeSyntax att in attList.Attributes)
                        {
                            if (((IdentifierNameSyntax)att.Name).Identifier.ValueText.Equals("MixedSide"))
                            {
                                mapMethod = true;
                            }
                        }
                    }
                }
                if (mapMethod)
                {
                    cl.AddMember(mD.Map(cl));
                }
            }

            return cl;
        }

        static internal ScriptSharp.ScriptModel.NamespaceSymbol Map(this NamespaceDeclarationSyntax ns)
        {
            // Todo: Implement so that members are mapped as well!
            return new ScriptSharp.ScriptModel.NamespaceSymbol(((IdentifierNameSyntax)ns.Name).Identifier.ValueText, null);
        }

    }
}
