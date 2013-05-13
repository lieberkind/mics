using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiCSCaseStudy;

namespace MiCSCaseStudyTests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void Validator_ValidNameTest()
        {
            var validator = new Validator();
            Assert.IsTrue(validator.IsNameValid("Tomas LieberKind"));
        }
    }
}
