using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class SerialNumberExtensions
    {
        public static DescribedResult<bool> IsValid(this SerialNumber serialNumber)
        {
            var output = Utilities.IsValid(serialNumber);
            return output;
        }

        public static void Validate(this SerialNumber serialNumber)
        {
            var isValid = serialNumber.IsValid();
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(serialNumber));
            }
        }
    }
}
