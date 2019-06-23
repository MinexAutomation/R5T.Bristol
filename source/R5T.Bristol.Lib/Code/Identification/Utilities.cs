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


        public static string GetStandardEquipmentCateogoryString(EquipmentCategoryIdentifier equipmentCategoryIdentifier)
        {
            switch (equipmentCategoryIdentifier)
            {
                case EquipmentCategoryIdentifier.J:
                    return Constants.EquipmentCategoryIdentifierJValue;

                case EquipmentCategoryIdentifier.U:
                    return Constants.EquipmentCategoryIdentifierUValue;

                case EquipmentCategoryIdentifier.Z:
                    return Constants.EquipmentCategoryIdentifierZValue;

                default:
                    throw new ArgumentException(EnumHelper.UnexpectedEnumerationValueMessage(equipmentCategoryIdentifier), nameof(equipmentCategoryIdentifier));
            }
        }

        public static EquipmentCategoryIdentifier GetEquipmentCategoryIdentifier(string equipmentCategoryValue)
        {
            switch (equipmentCategoryValue)
            {
                case Constants.EquipmentCategoryIdentifierJValue:
                    return EquipmentCategoryIdentifier.J;

                case Constants.EquipmentCategoryIdentifierUValue:
                    return EquipmentCategoryIdentifier.U;

                case Constants.EquipmentCategoryIdentifierZValue:
                    return EquipmentCategoryIdentifier.Z;

                default:
                    throw new ArgumentException(EnumHelper.UnrecognizedEnumerationValueMessage<EquipmentCategoryIdentifier>(equipmentCategoryValue), nameof(equipmentCategoryValue));
            }
        }

        public static EquipmentCategoryIdentifier GetEquipmentCategoryIdentifier(EquipmentCategory equipmentCategory)
        {
            var equipmentCategoryIdentifier = Utilities.GetEquipmentCategoryIdentifier(equipmentCategory.Value);
            return equipmentCategoryIdentifier;
        }

        public static DescribedResult<bool> IsValidEquipmentCategoryIdentifier(string equipmentCategoryValue)
        {
            switch (equipmentCategoryValue)
            {
                case Constants.EquipmentCategoryIdentifierJValue:
                case Constants.EquipmentCategoryIdentifierUValue:
                case Constants.EquipmentCategoryIdentifierZValue:
                    return DescribedResult.FromValue(true);

                default:
                    return DescribedResult.FromValue(false, MessageHelper.AllowedValues(Constants.EquipmentCategoryIdentifiers));
            }
        }

        public static DescribedResult<bool> IsValid(EquipmentCategory equipmentCategory)
        {
            var output = Utilities.IsValidEquipmentCategoryIdentifier(equipmentCategory.Value);
            return output;
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

        public static DescribedResult<bool> IsValid(OwnerCode ownerCode)
        {
            var output = Utilities.IsValidOwnerCode(ownerCode.Value);
            return output;
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

        public static DescribedResult<bool> IsValid(SerialNumber serialNumber)
        {
            var output = Utilities.IsValidSerialNumber(serialNumber.Value);
            return output;
        }

        public static DescribedResult<bool> IsValidCheckDigit(string checkDigitValue)
        {
            var isValid = Utilities.CheckDigitRegex.IsMatch(checkDigitValue);
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Check digit must be a single (1) number (not a letter). Examples: 1, 2, 3.\nFound: {checkDigitValue}.");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(CheckDigit checkDigit)
        {
            var output = Utilities.IsValidCheckDigit(checkDigit.Value);
            return output;
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

        public static int ValueToLetterOrNumber(int value)
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
    }
}
