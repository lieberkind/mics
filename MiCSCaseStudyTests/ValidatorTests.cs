using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiCSCaseStudy;

namespace MiCSCaseStudyTests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void CaseStudyValidator_ValidNameTest()
        {
            var validator = new Validator();
            Assert.IsTrue(validator.IsNameValid("Tomas LieberKind"));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidNameTest()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsNameValid("To Li"));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidNameTest2()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsNameValid("Tomas"));
        }

        [TestMethod]
        public void CaseStudyValidator_ValidAddressTest()
        {
            var validator = new Validator();
            Assert.IsTrue(validator.IsAddressValid("Torvegade 18, 5 TH", "1400"));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidAddressTest()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsAddressValid("Torvegade", "1400"));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidAddressTest2()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsAddressValid("Torvegade 18, 5", "1400"));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidAddressZipTest()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsAddressValid("Torvegade 18, 5 TH", "14000"));
        }

        [TestMethod]
        public void CaseStudyValidator_ValidPhoneTest()
        {
            var validator = new Validator();
            Assert.IsTrue(validator.IsPhoneValid("12341234"));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidPhoneTest()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsPhoneValid("1234512345"));
        }

        [TestMethod]
        public void CaseStudyValidator_ValidDeliveryMethodTest()
        {
            var validator = new Validator();
            Assert.IsTrue(validator.IsDeliveryMethodsValid(new[] { true, true }, "Torvegade 18, 5 TH", "1400", "wokdeo@iejdei.com"));
        }

        [TestMethod]
        public void CaseStudyValidator_ValidDeliveryMethodTest2()
        {
            var validator = new Validator();
            Assert.IsTrue(validator.IsDeliveryMethodsValid(new[] { true, false }, "Torvegade 18, 5 TH", "1400", ""));
        }

        [TestMethod]
        public void CaseStudyValidator_InvalidDeliveryMethodTest()
        {
            var validator = new Validator();
            Assert.IsFalse(validator.IsDeliveryMethodsValid(new[] { false, false }, "Torvegade 18, 5 TH", "1400", "wokdeo@iejdei.com"));
        }
    }
}
