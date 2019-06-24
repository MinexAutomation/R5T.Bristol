using System;


namespace R5T.Bristol.Lib.Identification.Extensions
{
    public static class StringExtensions
    {
        public static UnvalidatedEquipmentCategory AsUnvalidatedEquipmentCategory(this string value)
        {
            var unvalidatedEquipmentCategory = new UnvalidatedEquipmentCategory(value);
            return unvalidatedEquipmentCategory;
        }

        public static EquipmentCategory ToEquipmentCategory(this string value)
        {
            var unvalidatedEquipmentCategory = value.AsUnvalidatedEquipmentCategory();

            var equipmentCategory = Utilities.Validate(unvalidatedEquipmentCategory);
            return equipmentCategory;
        }

        public static UnvalidatedOwnerCode AsUnvalidatedOwnerCode(this string value)
        {
            var unvalidatedOwnerCode = new UnvalidatedOwnerCode(value);
            return unvalidatedOwnerCode;
        }

        public static OwnerCode AsOwnerCode(this string value)
        {
            var ownerCode = new OwnerCode(value);
            return ownerCode;
        }

        public static OwnerCode ToOwnerCode(this string value)
        {
            var unvalidatedOwnerCode = value.AsUnvalidatedOwnerCode();

            var ownerCode = Utilities.Validate(unvalidatedOwnerCode);
            return ownerCode;
        }

        public static UnvalidatedSerialNumber AsUnvalidatedSerialNumber(this string value)
        {
            var unvalidatedSerialNumber = new UnvalidatedSerialNumber(value);
            return unvalidatedSerialNumber;
        }

        public static SerialNumber AsSerialNumber(this string value)
        {
            var serialNumber = new SerialNumber(value);
            return serialNumber;
        }

        public static SerialNumber ToSerialNumber(this string value)
        {
            var unvalidatedSerialNumber = value.AsUnvalidatedSerialNumber();

            var serialNumber = Utilities.Validate(unvalidatedSerialNumber);
            return serialNumber;
        }

        public static UnvalidatedCheckDigitString AsUnvalidatedCheckDigitString(this string value)
        {
            var unvalidatedCheckDigitString = new UnvalidatedCheckDigitString(value);
            return unvalidatedCheckDigitString;
        }

        public static CheckDigitString AsCheckDigitString(this string value)
        {
            var checkDigitString = new CheckDigitString(value);
            return checkDigitString;
        }

        public static CheckDigitString ToCheckDigitString(this string value)
        {
            var unvalidatedCheckDigitString = value.AsUnvalidatedCheckDigitString();

            var checkDigitString = Utilities.Validate(unvalidatedCheckDigitString);
            return checkDigitString;
        }

        public static CheckDigit AsCheckDigit(this string value)
        {
            var checkDigitValue = Utilities.DefaultCheckDigitToNumericConverter(value);

            var checkDigit = checkDigitValue.AsCheckDigit();
            return checkDigit;
        }

        public static CheckDigit ToCheckDigit(this string value)
        {
            var checkDigitString = value.ToCheckDigitString();

            var checkDigit = Utilities.ToNumeric(checkDigitString);
            return checkDigit;
        }
    }
}
