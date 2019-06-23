using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using IdentificationUtilities = R5T.Bristol.Lib.Identification.Utilities;


namespace R5T.Bristol.Testing
{
    [TestClass]
    public class Identification
    {
        [TestMethod]
        public void BasicCheckDigit()
        {
            var rawUnvalidatedUncheckedContainerIdentificationValue = "AMFU304936";
            var expectedCheckDigitValue = 9;

            var checkDigitValue = IdentificationUtilities.ComputeCheckDigit(rawUnvalidatedUncheckedContainerIdentificationValue);

            Assert.AreEqual(expectedCheckDigitValue, checkDigitValue);
        }
    }
}
