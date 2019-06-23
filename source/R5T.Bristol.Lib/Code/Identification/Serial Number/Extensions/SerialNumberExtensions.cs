using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class SerialNumberExtensions
    {
        public static DescribedResult<bool> IsValid(this UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var output = Utilities.IsValid(unvalidatedSerialNumber);
            return output;
        }

        public static DescribedResult<bool> IsValid(this SerialNumber serialNumber)
        {
            var output = Utilities.IsValid(serialNumber);
            return output;
        }

        public static DescribedResult<bool> TryValidate(this UnvalidatedSerialNumber unvalidatedSerialNumber, out SerialNumber serialNumber)
        {
            var isValid = Utilities.TryValidate(unvalidatedSerialNumber, out serialNumber);
            return isValid;
        }

        public static SerialNumber Validate(this UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var serialNumber = Utilities.Validate(unvalidatedSerialNumber);
            return serialNumber;
        }

        public static void Validate(this SerialNumber serialNumber)
        {
            var isValid = serialNumber.IsValid();
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(serialNumber));
            }
        }

        public static DescribedResult<bool> IsValid(this UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var output = Utilities.IsValid(unvalidatedSerialNumberNumeric);
            return output;
        }

        public static DescribedResult<bool> IsValid(this SerialNumberNumeric serialNumberNumeric)
        {
            var output = Utilities.IsValid(serialNumberNumeric);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric, out SerialNumberNumeric serialNumberNumeric)
        {
            var isValid = Utilities.TryValidate(unvalidatedSerialNumberNumeric, out serialNumberNumeric);
            return isValid;
        }

        public static SerialNumberNumeric Validate(this UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var serialNumberNumeric = Utilities.Validate(unvalidatedSerialNumberNumeric);
            return serialNumberNumeric;
        }

        public static void Validate(this SerialNumberNumeric serialNumberNumeric)
        {
            var isValid = serialNumberNumeric.IsValid();
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(serialNumberNumeric));
            }
        }
    }
}
