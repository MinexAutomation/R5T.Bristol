using System;


namespace R5T.Bristol.Lib.Identification.Extensions
{
    public static class IntExtensions
    {
        public static UnvalidatedCheckDigit AsUnvalidatedCheckDigit(this int value)
        {
            var unvalidatedCheckDigit = new UnvalidatedCheckDigit(value);
            return unvalidatedCheckDigit;
        }

        public static CheckDigit AsCheckDigit(this int value)
        {
            var checkDigit = new CheckDigit(value);
            return checkDigit;
        }

        public static CheckDigit ToCheckDigit(this int value)
        {
            var unvalidatedCheckDigit = value.AsUnvalidatedCheckDigit();

            var checkDigit = Utilities.Validate(unvalidatedCheckDigit);
            return checkDigit;
        }
    }
}
