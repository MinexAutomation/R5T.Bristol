using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class OwnerCodeExtensions
    {
        public static DescribedResult<bool> IsValid(this UnvalidatedOwnerCode unvalidatedOwnerCode)
        {
            var output = Utilities.IsValid(unvalidatedOwnerCode);
            return output;
        }

        public static DescribedResult<bool> IsValid(this OwnerCode ownerCode)
        {
            var output = Utilities.IsValid(ownerCode);
            return output;
        }

        public static DescribedResult<bool> TryValidate(this UnvalidatedOwnerCode unvalidatedOwnerCode, out OwnerCode ownerCode)
        {
            var isValid = Utilities.TryValidate(unvalidatedOwnerCode, out ownerCode);
            return isValid;
        }

        public static OwnerCode Validate(this UnvalidatedOwnerCode unvalidatedOwnerCode)
        {
            var ownerCode = Utilities.Validate(unvalidatedOwnerCode);
            return ownerCode;
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
