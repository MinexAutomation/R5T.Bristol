using System;
using System.Text.RegularExpressions;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class Utilities
    {
        /// <summary>
        /// Must be three (3) upper-case letters A-Z (see <see cref="Constants.OwnerCodeRegexPattern"/>).
        /// </summary>
        private static readonly Regex OwnerCodeRegex = new Regex(Constants.OwnerCodeRegexPattern);
        /// <summary>
        /// Must be six (6) numbers 0-9 (see <see cref="Constants.SerialNumberRegexPattern"/>).
        /// </summary>
        private static readonly Regex SerialNumberRegex = new Regex(Constants.SerialNumberRegexPattern);
        /// <summary>
        /// Must be a number 0-9 (see <see cref="Constants.CheckDigitRegexPattern"/>).
        /// </summary>
        private static readonly Regex CheckDigitRegex = new Regex(Constants.CheckDigitRegexPattern);
        /// <summary>
        /// Must be a number 0-9 or an upper-case letter A-Z (see <see cref="Constants.ValidCharacterRegexPattern"/>).
        /// </summary>
        private static readonly Regex ValidCharacterRegex = new Regex(Constants.ValidCharacterRegexPattern);


        public static int DefaultSerialNumberToNumericConverter(string serialNumberString)
        {
            var serialNumberNumeric = Convert.ToInt32(serialNumberString);
            return serialNumberNumeric;
        }

        public static string DefaultSerialNumberToStringConverter(int serialNumberNumeric)
        {
            var serialNumberString = serialNumberNumeric.ToString();
            return serialNumberString;
        }

        public static string GetEquipmentCategoryStandardString(EquipmentCategory equipmentCategory)
        {
            switch (equipmentCategory)
            {
                case EquipmentCategory.J:
                    return Constants.EquipmentCategoryJStandardString;

                case EquipmentCategory.U:
                    return Constants.EquipmentCategoryUStandardString;

                case EquipmentCategory.Z:
                    return Constants.EquipmentCategoryZStandardString;

                default:
                    throw new ArgumentException(EnumHelper.UnexpectedEnumerationValueMessage(equipmentCategory), nameof(equipmentCategory));
            }
        }

        public static EquipmentCategory GetEquipmentCategory(string equipmentCategoryStandardString)
        {
            switch (equipmentCategoryStandardString)
            {
                case Constants.EquipmentCategoryJStandardString:
                    return EquipmentCategory.J;

                case Constants.EquipmentCategoryUStandardString:
                    return EquipmentCategory.U;

                case Constants.EquipmentCategoryZStandardString:
                    return EquipmentCategory.Z;

                default:
                    throw new ArgumentException(EnumHelper.UnrecognizedEnumerationValueMessage<EquipmentCategory>(equipmentCategoryStandardString), nameof(equipmentCategoryStandardString));
            }
        }

        public static DescribedResult<bool> IsValidEquipmentCategory(string equipmentCategoryString)
        {
            switch (equipmentCategoryString)
            {
                case Constants.EquipmentCategoryJStandardString:
                case Constants.EquipmentCategoryUStandardString:
                case Constants.EquipmentCategoryZStandardString:
                    return DescribedResult.FromValue(true);

                default:
                    return DescribedResult.FromValue(false, MessageHelper.AllowedValues(Constants.EquipmentCategoryStandardStrings));
            }
        }

        public static DescribedResult<bool> IsValid(UnvalidatedEquipmentCategory unvalidatedEquipmentCategory)
        {
            var output = Utilities.IsValidEquipmentCategory(unvalidatedEquipmentCategory.Value);
            return output;
        }

        /// <summary>
        /// Returns <see cref="EquipmentCategory.Unknown"/> if invalid.
        /// </summary>
        public static DescribedResult<bool> TryValidate(UnvalidatedEquipmentCategory unvalidatedEquipmentCategory, out EquipmentCategory equipmentCategory)
        {
            var isValid = Utilities.IsValid(unvalidatedEquipmentCategory);
            if(isValid.Value)
            {
                equipmentCategory = Utilities.GetEquipmentCategory(unvalidatedEquipmentCategory.Value);
            }
            else
            {
                equipmentCategory = EquipmentCategory.Unknown;
            }

            return isValid;
        }

        public static EquipmentCategory Validate(UnvalidatedEquipmentCategory unvalidatedEquipmentCategory)
        {
            var isValid = Utilities.TryValidate(unvalidatedEquipmentCategory, out var equipmentCategory);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedEquipmentCategory));
            }

            return equipmentCategory;
        }

        /// <summary>
        /// Per 3.1.1 - Owner code must be three (3) capital letters (not numbers).
        /// </summary>
        public static DescribedResult<bool> IsValidOwnerCode(string ownerCodeValue)
        {
            var isValid = Utilities.OwnerCodeRegex.IsMatch(ownerCodeValue);
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Owner code must be exactly three (3) upper-case letters (not numbers). Examples: XYZ, ABC, or TUV.\nFound: {ownerCodeValue}.");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedOwnerCode unvalidatedOwnerCode)
        {
            var output = Utilities.IsValidOwnerCode(unvalidatedOwnerCode.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(OwnerCode ownerCode)
        {
            var output = Utilities.IsValidOwnerCode(ownerCode.Value);
            return output;
        }

        /// <summary>
        /// Returns <see cref="OwnerCode.Invalid"/> if invalid.
        /// </summary>
        public static DescribedResult<bool> TryValidate(UnvalidatedOwnerCode unvalidatedOwnerCode, out OwnerCode ownerCode)
        {
            var isValid = Utilities.IsValid(unvalidatedOwnerCode);
            if(isValid.Value)
            {
                ownerCode = new OwnerCode(unvalidatedOwnerCode.Value);
            }
            else
            {
                ownerCode = OwnerCode.Invalid;
            }

            return isValid;
        }

        public static OwnerCode Validate(UnvalidatedOwnerCode unvalidatedOwnerCode)
        {
            var isValid = Utilities.TryValidate(unvalidatedOwnerCode, out var ownerCode);
            if (!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedOwnerCode));
            }

            return ownerCode;
        }

        public static DescribedResult<bool> IsValidSerialNumber(string serialNumberValue)
        {
            var isValid = Utilities.SerialNumberRegex.IsMatch(serialNumberValue);
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Serial number must be exactly six (6) numbers (no letters). Examples: 123456, 789012, 555555.\nFound: {serialNumberValue}.");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var output = Utilities.IsValidSerialNumber(unvalidatedSerialNumber.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(SerialNumber serialNumber)
        {
            var output = Utilities.IsValidSerialNumber(serialNumber.Value);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedSerialNumber unvalidatedSerialNumber, out SerialNumber serialNumber)
        {
            var isValid = Utilities.IsValid(unvalidatedSerialNumber);
            if(isValid.Value)
            {
                serialNumber = new SerialNumber(unvalidatedSerialNumber.Value);
            }
            else
            {
                serialNumber = SerialNumber.Invalid;
            }

            return isValid;
        }

        public static SerialNumber Validate(UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var isValid = Utilities.TryValidate(unvalidatedSerialNumber, out var serialNumber);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedSerialNumber));
            }

            return serialNumber;
        }

        public static DescribedResult<bool> IsValidSerialNumber(int serialNumberNumericValue)
        {
            var isWithinRange = serialNumberNumericValue > -1 && serialNumberNumericValue < 1_000_000;
            if(!isWithinRange)
            {
                return DescribedResult.FromValue(false, MessageHelper.AllowedRange(0, 999_999));
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var output = Utilities.IsValidSerialNumber(unvalidatedSerialNumberNumeric.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(SerialNumberNumeric serialNumberNumeric)
        {
            var output = Utilities.IsValidSerialNumber(serialNumberNumeric.Value);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric, out SerialNumberNumeric serialNumberNumeric)
        {
            var isValid = Utilities.IsValid(unvalidatedSerialNumberNumeric);
            if (isValid.Value)
            {
                serialNumberNumeric = new SerialNumberNumeric(unvalidatedSerialNumberNumeric.Value);
            }
            else
            {
                serialNumberNumeric = SerialNumberNumeric.Invalid;
            }

            return isValid;
        }

        public static SerialNumberNumeric Validate(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var isValid = Utilities.TryValidate(unvalidatedSerialNumberNumeric, out var serialNumberNumeric);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedSerialNumberNumeric));
            }

            return serialNumberNumeric;
        }

        public static UnvalidatedSerialNumberNumeric ToUnvalidatedNumeric(UnvalidatedSerialNumber unvalidatedSerialNumber, Func<string, int> converter)
        {
            var valueNumeric = converter(unvalidatedSerialNumber.Value);

            var unvalidatedSerialNumberNumeric = new UnvalidatedSerialNumberNumeric(valueNumeric);
            return unvalidatedSerialNumberNumeric;
        }

        public static UnvalidatedSerialNumberNumeric ToUnvalidatedNumeric(UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var unvalidatedSerialNumberNumeric = Utilities.ToUnvalidatedNumeric(unvalidatedSerialNumber, Utilities.DefaultSerialNumberToNumericConverter);
            return unvalidatedSerialNumberNumeric;
        }

        public static UnvalidatedSerialNumberNumeric ToUnvalidatedNumeric(SerialNumber serialNumber, Func<string, int> converter)
        {
            var valueNumeric = converter(serialNumber.Value);

            var unvalidatedSerialNumberNumeric = new UnvalidatedSerialNumberNumeric(valueNumeric);
            return unvalidatedSerialNumberNumeric;
        }

        public static UnvalidatedSerialNumberNumeric ToUnvalidatedNumeric(SerialNumber serialNumber)
        {
            var unvalidatedSerialNumberNumeric = Utilities.ToUnvalidatedNumeric(serialNumber, Utilities.DefaultSerialNumberToNumericConverter);
            return unvalidatedSerialNumberNumeric;
        }

        public static SerialNumberNumeric ToNumeric(UnvalidatedSerialNumber unvalidatedSerialNumber, Func<string, int> converter)
        {
            var unvalidatedSerialNumberNumeric = Utilities.ToUnvalidatedNumeric(unvalidatedSerialNumber, converter);

            var serialNumberNumeric = Utilities.Validate(unvalidatedSerialNumberNumeric);
            return serialNumberNumeric;
        }

        public static SerialNumberNumeric ToNumeric(UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var serialNumberNumeric = Utilities.ToNumeric(unvalidatedSerialNumber, Utilities.DefaultSerialNumberToNumericConverter);
            return serialNumberNumeric;
        }

        public static SerialNumberNumeric ToNumeric(SerialNumber serialNumber, Func<string, int> converter)
        {
            var unvalidatedSerialNumberNumeric = Utilities.ToUnvalidatedNumeric(serialNumber, converter);

            var serialNumberNumeric = Utilities.Validate(unvalidatedSerialNumberNumeric);
            return serialNumberNumeric;
        }

        public static SerialNumberNumeric ToNumeric(SerialNumber serialNumber)
        {
            var serialNumberNumeric = Utilities.ToNumeric(serialNumber, Utilities.DefaultSerialNumberToNumericConverter);
            return serialNumberNumeric;
        }

        public static UnvalidatedSerialNumber ToUnvalidatedString(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric, Func<int, string> converter)
        {
            var valueString = converter(unvalidatedSerialNumberNumeric.Value);

            var unvalidatedSerialNumber = new UnvalidatedSerialNumber(valueString);
            return unvalidatedSerialNumber;
        }

        public static UnvalidatedSerialNumber ToUnvalidatedString(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var unvalidatedSerialNumber = Utilities.ToUnvalidatedString(unvalidatedSerialNumberNumeric, Utilities.DefaultSerialNumberToStringConverter);
            return unvalidatedSerialNumber;
        }

        public static UnvalidatedSerialNumber ToUnvalidatedString(SerialNumberNumeric serialNumberNumeric, Func<int, string> converter)
        {
            var valueString = converter(serialNumberNumeric.Value);

            var unvalidatedSerialNumber = new UnvalidatedSerialNumber(valueString);
            return unvalidatedSerialNumber;
        }

        public static UnvalidatedSerialNumber ToUnvalidatedString(SerialNumberNumeric serialNumberNumeric)
        {
            var unvalidatedSerialNumber = Utilities.ToUnvalidatedString(serialNumberNumeric, Utilities.DefaultSerialNumberToStringConverter);
            return unvalidatedSerialNumber;
        }

        public static SerialNumber ToString(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric, Func<int, string> converter)
        {
            var unvalidatedSerialNumber = Utilities.ToUnvalidatedString(unvalidatedSerialNumberNumeric, converter);

            var serialNumber = Utilities.Validate(unvalidatedSerialNumber);
            return serialNumber;
        }

        public static SerialNumber ToString(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var serialNumber = Utilities.ToString(unvalidatedSerialNumberNumeric, Utilities.DefaultSerialNumberToStringConverter);
            return serialNumber;
        }

        public static SerialNumber ToString(SerialNumberNumeric serialNumberNumeric, Func<int, string> converter)
        {
            var unvalidatedSerialNumber = Utilities.ToUnvalidatedString(serialNumberNumeric, converter);

            var serialNumber = Utilities.Validate(unvalidatedSerialNumber);
            return serialNumber;
        }

        public static SerialNumber ToString(SerialNumberNumeric serialNumberNumeric)
        {
            var serialNumber = Utilities.ToString(serialNumberNumeric, Utilities.DefaultSerialNumberToStringConverter);
            return serialNumber;
        }

        public static DescribedResult<bool> IsValidCheckDigit(int checkDigitValue)
        {
            var isValid = checkDigitValue > -1 && checkDigitValue < 10;
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Check digit must be a single (1) number (not a letter) between 0 and 9. Examples: 1, 2, 3.\nFound: {checkDigitValue}.");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedCheckDigit unvalidatedCheckDigit)
        {
            var output = Utilities.IsValidCheckDigit(unvalidatedCheckDigit.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(CheckDigit checkDigit)
        {
            var output = Utilities.IsValidCheckDigit(checkDigit.Value);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedCheckDigit unvalidatedCheckDigit, out CheckDigit checkDigit)
        {
            var isValid = Utilities.IsValid(unvalidatedCheckDigit);
            if(isValid.Value)
            {
                checkDigit = new CheckDigit(unvalidatedCheckDigit.Value);
            }
            else
            {
                checkDigit = CheckDigit.Invalid;
            }

            return isValid;
        }

        public static CheckDigit Validate(UnvalidatedCheckDigit unvalidatedCheckDigit)
        {
            var isValid = Utilities.TryValidate(unvalidatedCheckDigit, out var checkDigit);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedCheckDigit));
            }

            return checkDigit;
        }

        public static DescribedResult<bool> IsValidCheckDigit(string checkDigitStringValue)
        {
            var isValid = Utilities.CheckDigitRegex.IsMatch(checkDigitStringValue);
            if (!isValid)
            {
                return DescribedResult.FromValue(false, $"Check digit must be a single (1) number (not a letter). Examples: 1, 2, 3.\nFound: {checkDigitStringValue}.");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedCheckDigitString unvalidatedCheckDigitString)
        {
            var output = Utilities.IsValidCheckDigit(unvalidatedCheckDigitString.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(CheckDigitString checkDigitString)
        {
            var output = Utilities.IsValidCheckDigit(checkDigitString.Value);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedCheckDigitString unvalidatedCheckDigitString, out CheckDigitString checkDigitString)
        {
            var isValid = Utilities.IsValid(unvalidatedCheckDigitString);
            if(isValid.Value)
            {
                checkDigitString = new CheckDigitString(unvalidatedCheckDigitString.Value);
            }
            else
            {
                checkDigitString = CheckDigitString.Invalid;
            }

            return isValid;
        }

        public static CheckDigitString Validate(UnvalidatedCheckDigitString unvalidatedCheckDigitString)
        {
            var isValid = Utilities.TryValidate(unvalidatedCheckDigitString, out var checkDigitString);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedCheckDigitString));
            }

            return checkDigitString;
        }


        /// <summary>
        /// Determines whether the input character is a valid container identification marker character.
        /// (Must be a number 0-9, or an upper-case letter, A-Z.)
        /// </summary>
        public static DescribedResult<bool> IsValidCharacter(char character)
        {
            var characterString = character.ToString();

            var isValid = Utilities.ValidCharacterRegex.IsMatch(characterString);
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Character must be a number 0-9, or an upper-case letter A-Z.\nFound: {character}.");
            }

            return DescribedResult.FromValue(true);
        }

        public static int LetterOrNumberToValue(char letterOrNumber)
        {
            // Using a simple, reliable, expressive switch-statement instead of a numerical computation for clarity.
            switch(letterOrNumber)
            {
                case '0':
                    return 0;

                case '1':
                    return 1;

                case '2':
                    return 2;

                case '3':
                    return 3;

                case '4':
                    return 4;

                case '5':
                    return 5;

                case '6':
                    return 6;

                case '7':
                    return 7;

                case '8':
                    return 8;

                case '9':
                    return 9;

                case 'A':
                    return 10;

                case 'B':
                    return 12; // No multiples of 11 are returned.

                case 'C':
                    return 13;

                case 'D':
                    return 14;

                case 'E':
                    return 15;

                case 'F':
                    return 16;

                case 'G':
                    return 17;

                case 'H':
                    return 18;

                case 'I':
                    return 19;

                case 'J':
                    return 20;

                case 'K':
                    return 21;

                case 'L':
                    return 23; // No multiples of 11.

                case 'M':
                    return 24;

                case 'N':
                    return 25;

                case 'O':
                    return 26;

                case 'P':
                    return 27;

                case 'Q':
                    return 28;

                case 'R':
                    return 29;

                case 'S':
                    return 30;

                case 'T':
                    return 31;

                case 'U':
                    return 32;

                case 'V':
                    return 34; // No multiples of 11.

                case 'W':
                    return 35;

                case 'X':
                    return 36;

                case 'Y':
                    return 37;

                case 'Z':
                    return 38;

                default:
                    throw new ArgumentException($"Invalid letter or number. Must be 0-9 or (uppercase) A-Z.\nFound: {letterOrNumber}.");
            }
        }

        public static int GetValue(char letterOrNumber)
        {
            var value = Utilities.LetterOrNumberToValue(letterOrNumber);
            return value;
        }

        public static char ValueToLetterOrNumber(int value)
        {

            // Using a simple, reliable, expressive switch-statement instead of a numerical computation for clarity.
            switch (value)
            {
                case 0:
                    return '0';

                case 1:
                    return '1';

                case 2:
                    return '2';

                case 3:
                    return '3';

                case 4:
                    return '4';

                case 5:
                    return '5';

                case 6:
                    return '6';

                case 7:
                    return '7';

                case 8:
                    return '8';

                case 9:
                    return '9';

                case 10:
                    return 'A';

                //case 11: // No multiples of 11.

                case 12:
                    return 'B';

                case 13:
                    return 'C';

                case 14:
                    return 'D';

                case 15:
                    return 'E';

                case 16:
                    return 'F';

                case 17:
                    return 'G';

                case 18:
                    return 'H';

                case 19:
                    return 'I';

                case 20:
                    return 'J';

                case 21:
                    return 'K';

                //case 22: // No multiples of 11.

                case 23:
                    return 'L';

                case 24:
                    return 'M';

                case 25:
                    return 'N';

                case 26:
                    return 'O';

                case 27:
                    return 'P';

                case 28:
                    return 'Q';

                case 29:
                    return 'R';

                case 30:
                    return 'S';

                case 31:
                    return 'T';

                case 32:
                    return 'U';

                //case 33: // No multiples of 11.

                case 34:
                    return 'V';

                case 35:
                    return 'W';

                case 36:
                    return 'X';

                case 37:
                    return 'Y';

                case 38:
                    return 'Z';

                default:
                    throw new ArgumentException($"Invalid letter or number value. Must be 0-9 (numbers) or 10-38, skipping 11, 22, and 33 (multiples of 11), (upper-case letters).\nFound: {value}.");
            }
        }

        public static char GetLetterOrNumber(int value)
        {
            var letterOrNumber = Utilities.ValueToLetterOrNumber(value);
            return letterOrNumber;
        }

        public static int ComputeCheckDigit(string rawUncheckedContainerIdentificationValue)
        {
            var sum = 0.0; // Double.
            for (int i = 0; i < 10; i++)
            {
                var character = rawUncheckedContainerIdentificationValue[i];
                var value = Utilities.GetValue(character);
                var summand = value * Math.Pow(2, i);
                sum += summand;
            }

            var intSum = Convert.ToInt32(sum);

            var modulus11 = intSum % 11;
            var checkDigit = modulus11 % 10;
            return checkDigit;
        }
    }
}
