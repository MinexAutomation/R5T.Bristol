using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class OwnerCodeExtensions
    {
        public static DescribedResult<bool> IsValid(this OwnerCode ownerCode)
        {
            var output = Utilities.IsValid(ownerCode);
            return output;
        }

        public static void Validate(this OwnerCode ownerCode)
        {
            var isValid = ownerCode.IsValid();
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(ownerCode));
            }
        }
    }
}
