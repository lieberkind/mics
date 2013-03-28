using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiCS;
using System.IO;
using Roslyn.Compilers.CSharp;

namespace MiCSTests
{
    [TestClass]
    public class MiCSUtilitiesTests
    {
        [TestMethod]
        public void SyntaxTreeIsReturnedWhenFileExistsInSolution()
        {
            var syntaxTree = MiCSUtilities.GetSyntaxTree("MiCSUtilitiesTests.cs");
            Assert.IsInstanceOfType(syntaxTree, typeof(SyntaxTree));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ExceptionIsThrownWhenFileDoesNotExistInSolution() 
        {
            var syntaxTree = MiCSUtilities.GetSyntaxTree("AFileThatNeverExists.random");
        }
    }
}
