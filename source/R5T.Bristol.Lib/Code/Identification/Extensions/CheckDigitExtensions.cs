using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class CheckDigitExtensions
    {
        public static DescribedResult<bool> IsValid(this CheckDigit checkDigit)
        {
            var output = Utilities.IsValid(checkDigit);
            return output;
        }

        public static void Validate(this CheckDigit checkDigit)
        {
            var isValid = checkDigit.IsValid();
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(checkDigit));
            }
        }
    }
}
