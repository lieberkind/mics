using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Mappers
{
    class ExpressionMapper : SyntaxWalker
    {
        public readonly List<ScriptSharp.ScriptModel.Expression> ssExpressions = new List<Expression>();

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            var ssLiteralExpression = MapLiteralExpression(node);
            ssExpressions.Add(ssLiteralExpression);
            base.VisitLiteralExpression(node);
        }

        public ScriptSharp.ScriptModel.LiteralExpression MapLiteralExpression(LiteralExpressionSyntax roslynLiteralExpression) 
        {
            var val = roslynLiteralExpression.Token.Value;
            switch (roslynLiteralExpression.Kind)
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
    }
}
