using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptSharp.Generator;
using System.IO;
using ScriptSharp;
using ScriptSharp.ScriptModel;
namespace MiCSTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleScriptSharpNamespaceTest()
        {
            var stringWriter = new StringWriter();
            var writer = new ScriptTextWriter(stringWriter);
            var options = new CompilerOptions();
            var generator = new ScriptGenerator(writer, options, null);

            var l = new LiteralExpression(null, true);
            ExpressionGenerator.GenerateExpression(generator, null, l);

            Assert.IsTrue(Boolean.Parse(stringWriter.ToString()));
        }
    }
}
