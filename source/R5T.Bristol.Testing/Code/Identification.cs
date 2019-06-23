using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using IdentificationUtilities = R5T.Bristol.Lib.Identification.Utilities;


namespace R5T.Bristol.Testing
{
    [TestClass]
    public class Identification
    {
        /// <summary>
        /// From ISO 6346:1995 documentation - Appendix A.
        /// </summary>
        [TestMethod]
        public void BasicCheckDigit_00()
        {
            var rawUnvalidatedUncheckedContainerIdentificationValue = "ZEPU003725";
            var expectedCheckDigitValue = 5;

            var checkDigitValue = IdentificationUtilities.ComputeCheckDigit(rawUnvalidatedUncheckedContainerIdentificationValue);

            Assert.AreEqual(expectedCheckDigitValue, checkDigitValue);
        }

        [TestMethod]
        public void BasicCheckDigit_01()
        {
            var rawUnvalidatedUncheckedContainerIdentificationValue = "AMFU304936";
            var expectedCheckDigitValue = 9;

            var checkDigitValue = IdentificationUtilities.ComputeCheckDigit(rawUnvalidatedUncheckedContainerIdentificationValue);

            Assert.AreEqual(expectedCheckDigitValue, checkDigitValue);
        }
    }
}
