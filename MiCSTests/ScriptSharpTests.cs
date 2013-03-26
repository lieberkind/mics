using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptSharp.ScriptModel;
using System.IO;
using ScriptSharp.Generator;
using ScriptSharp;

namespace MiCSTests
{
    /// <summary>
    /// Summary description for ScriptSharpTests
    /// </summary>
    [TestClass]
    public class ScriptSharpTests
    {
        private string GenerateExpressionScript(Expression expr)
        {
            var stringWriter = new StringWriter();
            var writer = new ScriptTextWriter(stringWriter);
            var options = new CompilerOptions();
            var generator = new ScriptGenerator(writer, options, null);
            ExpressionGenerator.GenerateExpression(generator, null, expr);
            return stringWriter.ToString();
        }

        [TestMethod]
        public void ExpressionGeneratorSimpleTest()
        {
            var l = new LiteralExpression(null, true);
            Assert.IsTrue(Boolean.Parse(GenerateExpressionScript(l)));
        }
    }
}
