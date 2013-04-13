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
            var syntaxTree = MiCSUtilities.GetSyntaxTree(@"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCS.sln", "MiCSUtilitiesTests.cs");
            Assert.IsInstanceOfType(syntaxTree, typeof(SyntaxTree));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ExceptionIsThrownWhenFileDoesNotExistInSolution() 
        {
            var syntaxTree = MiCSUtilities.GetSyntaxTree(@"C:\Users\L520\Documents\Visual Studio 2012\Projects\mics\MiCS.sln", "AFileThatNeverExists.random");
        }
    }
}
