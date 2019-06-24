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
        public static readonly Regex OwnerCodeRegex = new Regex(Constants.OwnerCodeRegexPattern);
        /// <summary>
        /// Must be six (6) numbers 0-9 (see <see cref="Constants.SerialNumberRegexPattern"/>).
        /// </summary>
        public static readonly Regex SerialNumberRegex = new Regex(Constants.SerialNumberRegexPattern);
        /// <summary>
        /// Must be a number 0-9 (see <see cref="Constants.CheckDigitRegexPattern"/>).
        /// </summary>
        public static readonly Regex CheckDigitRegex = new Regex(Constants.CheckDigitRegexPattern);
        /// <summary>
        /// Must be a number 0-9 or an upper-case letter A-Z (see <see cref="Constants.ValidCharacterRegexPattern"/>).
        /// </summary>
        public static readonly Regex ValidCharacterRegex = new Regex(Constants.ValidCharacterRegexPattern);
        /// <summary>
        /// Must be 3 upper-case letters, an upper-case letter, 6 numbers, and a number (11 characters) (see <see cref="Constants.CheckedContainerIdentificationRegexPattern"/>).
        /// </summary>
        public static readonly Regex CheckedContainerIdentificationRegex = new Regex(Constants.CheckedContainerIdentificationRegexPattern);
        /// <summary>
        /// Must be 3 upper-case letters, an upper-case letter, 6 numbers (10 characters), without the final check digit (see <see cref="Constants.ContainerIdentificationRegexPattern"/>).
        /// </summary>
        public static readonly Regex ContainerIdentificationRegex = new Regex(Constants.ContainerIdentificationRegexPattern);


        public static int DefaultToIntegerConverter(string alphabetic)
        {
            var numeric = Convert.ToInt32(alphabetic);
            return numeric;
        }

        public static string DefaultToStringConverter(int numeric)
        {
            var alphabetic = Convert.ToString(numeric);
            return alphabetic;
        }

        #region Equipment Category

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

        #endregion

        #region Owner Code

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

        #endregion

        #region Serial Number

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

        public static int DefaultSerialNumberToNumericConverter(string serialNumberString)
        {
            var serialNumberNumeric = Utilities.DefaultToIntegerConverter(serialNumberString);
            return serialNumberNumeric;
        }

        public static UnvalidatedSerialNumberNumeric ToUnvalidatedNumeric(UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var unvalidatedSerialNumberNumericValue = Utilities.DefaultSerialNumberToNumericConverter(unvalidatedSerialNumber.Value);

            var unvalidatedSerialNumberNumeric = new UnvalidatedSerialNumberNumeric(unvalidatedSerialNumberNumericValue);
            return unvalidatedSerialNumberNumeric;
        }

        public static UnvalidatedSerialNumberNumeric ToUnvalidatedNumeric(SerialNumber serialNumber)
        {
            var unvalidatedSerialNumberNumericValue = Utilities.DefaultSerialNumberToNumericConverter(serialNumber.Value);

            var unvalidatedSerialNumberNumeric = new UnvalidatedSerialNumberNumeric(unvalidatedSerialNumberNumericValue);
            return unvalidatedSerialNumberNumeric;
        }

        public static SerialNumberNumeric ToNumeric(UnvalidatedSerialNumber unvalidatedSerialNumber)
        {
            var unvalidatedSerialNumberNumeric = Utilities.ToUnvalidatedNumeric(unvalidatedSerialNumber);

            var serialNumberNumeric = Utilities.Validate(unvalidatedSerialNumberNumeric);
            return serialNumberNumeric;
        }

        public static SerialNumberNumeric ToNumeric(SerialNumber serialNumber)
        {
            var unvalidatedSerialNumberNumeric = Utilities.ToUnvalidatedNumeric(serialNumber);

            var serialNumberNumeric = Utilities.Validate(unvalidatedSerialNumberNumeric);
            return serialNumberNumeric;
        }

        public static string DefaultSerialNumberToStringConverter(int serialNumberNumericValue)
        {
            var serialNumberString = Utilities.DefaultToStringConverter(serialNumberNumericValue);
            return serialNumberString;
        }

        public static UnvalidatedSerialNumber ToUnvalidatedString(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var valueString = Utilities.DefaultSerialNumberToStringConverter(unvalidatedSerialNumberNumeric.Value);

            var unvalidatedSerialNumber = new UnvalidatedSerialNumber(valueString);
            return unvalidatedSerialNumber;
        }

        public static UnvalidatedSerialNumber ToUnvalidatedString(SerialNumberNumeric serialNumberNumeric)
        {
            var valueString = Utilities.DefaultSerialNumberToStringConverter(serialNumberNumeric.Value);

            var unvalidatedSerialNumber = new UnvalidatedSerialNumber(valueString);
            return unvalidatedSerialNumber;
        }

        public static SerialNumber ToString(UnvalidatedSerialNumberNumeric unvalidatedSerialNumberNumeric)
        {
            var unvalidatedSerialNumber = Utilities.ToUnvalidatedString(unvalidatedSerialNumberNumeric);

            var serialNumber = Utilities.Validate(unvalidatedSerialNumber);
            return serialNumber;
        }

        public static SerialNumber ToString(SerialNumberNumeric serialNumberNumeric)
        {
            var unvalidatedSerialNumber = Utilities.ToUnvalidatedString(serialNumberNumeric);

            var serialNumber = Utilities.Validate(unvalidatedSerialNumber);
            return serialNumber;
        }

        #endregion

        #region Check Digit

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

        public static int DefaultCheckDigitToNumericConverter(string checkDigitStringValue)
        {
            var checkDigitValue = Utilities.DefaultToIntegerConverter(checkDigitStringValue);
            return checkDigitValue;
        }

        public static UnvalidatedCheckDigit ToUnvalidatedNumeric(UnvalidatedCheckDigitString unvalidatedCheckDigitString)
        {
            var unvalidatedCheckDigitValue = Utilities.DefaultCheckDigitToNumericConverter(unvalidatedCheckDigitString.Value);

            var unvalidatedCheckDigit = new UnvalidatedCheckDigit(unvalidatedCheckDigitValue);
            return unvalidatedCheckDigit;
        }

        public static UnvalidatedCheckDigit ToUnvalidatedNumeric(CheckDigitString checkDigitString)
        {
            var unvalidatedCheckDigitValue = Utilities.DefaultCheckDigitToNumericConverter(checkDigitString.Value);

            var unvalidatedCheckDigit = new UnvalidatedCheckDigit(unvalidatedCheckDigitValue);
            return unvalidatedCheckDigit;
        }

        public static CheckDigit ToNumeric(UnvalidatedCheckDigitString unvalidatedCheckDigitString)
        {
            var unvalidatedCheckDigit = Utilities.ToUnvalidatedNumeric(unvalidatedCheckDigitString);

            var checkDigit = Utilities.Validate(unvalidatedCheckDigit);
            return checkDigit;
        }

        public static CheckDigit ToNumeric(CheckDigitString checkDigitString)
        {
            var unvalidatedCheckDigit = Utilities.ToUnvalidatedNumeric(checkDigitString);

            var checkDigit = Utilities.Validate(unvalidatedCheckDigit);
            return checkDigit;
        }

        public static string DefaultCheckDigitToStringConverter(int checkDigitValue)
        {
            var checkDigitStringValue = Utilities.DefaultToStringConverter(checkDigitValue);
            return checkDigitStringValue;
        }

        public static UnvalidatedCheckDigitString ToUnvalidatedString(UnvalidatedCheckDigit unvalidatedCheckDigit)
        {
            var unvalidatedCheckDigitStringValue = Utilities.DefaultCheckDigitToStringConverter(unvalidatedCheckDigit.Value);

            var unvalidatedCheckDigitString = new UnvalidatedCheckDigitString(unvalidatedCheckDigitStringValue);
            return unvalidatedCheckDigitString;
        }

        public static UnvalidatedCheckDigitString ToUnvalidatedString(CheckDigit checkDigit)
        {
            var unvalidatedCheckDigitStringValue = Utilities.DefaultCheckDigitToStringConverter(checkDigit.Value);

            var unvalidatedCheckDigitString = new UnvalidatedCheckDigitString(unvalidatedCheckDigitStringValue);
            return unvalidatedCheckDigitString;
        }

        public static CheckDigitString ToString(UnvalidatedCheckDigit unvalidatedCheckDigit)
        {
            var unvalidatedCheckDigitString = Utilities.ToUnvalidatedString(unvalidatedCheckDigit);

            var checkDigitString = Utilities.Validate(unvalidatedCheckDigitString);
            return checkDigitString;
        }

        public static CheckDigitString ToString(CheckDigit checkDigit)
        {
            var unvalidatedCheckDigitString = Utilities.ToUnvalidatedString(checkDigit);

            var checkDigitString = Utilities.Validate(unvalidatedCheckDigitString);
            return checkDigitString;
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

        #endregion

        #region Raw Container Identification

        public static DescribedResult<bool> IsValidRawContainerIdentification(string rawContainerIdentificationValue)
        {
            var isValid = Utilities.CheckedContainerIdentificationRegex.IsMatch(rawContainerIdentificationValue);
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Container identification must be 3 upper-case letters, an upper-case letter, and 6 numbers (10 characters). Example: ZEPU003725.\nFound: {rawContainerIdentificationValue}");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedRawContainerIdentification unvalidatedRawContainerIdentification)
        {
            var output = Utilities.IsValidRawContainerIdentification(unvalidatedRawContainerIdentification.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(RawContainerIdentification rawContainerIdentification)
        {
            var output = Utilities.IsValidRawContainerIdentification(rawContainerIdentification.Value);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedRawContainerIdentification unvalidatedRawContainerIdentification, out RawContainerIdentification rawContainerIdentification)
        {
            var isValid = Utilities.IsValid(unvalidatedRawContainerIdentification);
            if(isValid.Value)
            {
                rawContainerIdentification = new RawContainerIdentification(unvalidatedRawContainerIdentification.Value);
            }
            else
            {
                rawContainerIdentification = RawContainerIdentification.Invalid;
            }

            return isValid;
        }

        public static RawContainerIdentification Validate(UnvalidatedRawContainerIdentification unvalidatedRawContainerIdentification)
        {
            var isValid = Utilities.TryValidate(unvalidatedRawContainerIdentification, out var rawContainerIdentification);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedRawContainerIdentification));
            }

            return rawContainerIdentification;
        }

        #endregion

        #region Raw Checked Container Identification

        public static DescribedResult<bool> IsValidRawCheckedContainerIdentification(string rawCheckedContainerIdentificationValue)
        {
            var isValid = Utilities.ContainerIdentificationRegex.IsMatch(rawCheckedContainerIdentificationValue);
            if(!isValid)
            {
                return DescribedResult.FromValue(false, $"Container identification must be 3 upper-case letters, an upper-case letter, 6 numbers, and a number (11 characters). Example: ZEPU0037255.\nFound: {rawCheckedContainerIdentificationValue}");
            }

            return DescribedResult.FromValue(true);
        }

        public static DescribedResult<bool> IsValid(UnvalidatedRawCheckedContainerIdentification unvalidatedRawCheckedContainerIdentification)
        {
            var output = Utilities.IsValidRawCheckedContainerIdentification(unvalidatedRawCheckedContainerIdentification.Value);
            return output;
        }

        public static DescribedResult<bool> IsValid(RawCheckedContainerIdentification rawCheckedContainerIdentification)
        {
            var output = Utilities.IsValidRawCheckedContainerIdentification(rawCheckedContainerIdentification.Value);
            return output;
        }

        public static DescribedResult<bool> TryValidate(UnvalidatedRawCheckedContainerIdentification unvalidatedRawCheckedContainerIdentification, out RawCheckedContainerIdentification rawCheckedContainerIdentification)
        {
            var isValid = Utilities.IsValid(unvalidatedRawCheckedContainerIdentification);
            if(isValid.Value)
            {
                rawCheckedContainerIdentification = new RawCheckedContainerIdentification(unvalidatedRawCheckedContainerIdentification.Value);
            }
            else
            {
                rawCheckedContainerIdentification = RawCheckedContainerIdentification.Invalid;
            }

            return isValid;
        }

        public static RawCheckedContainerIdentification Validate(UnvalidatedRawCheckedContainerIdentification unvalidatedRawCheckedContainerIdentification)
        {
            var isValid = Utilities.TryValidate(unvalidatedRawCheckedContainerIdentification, out var rawCheckedContainerIdentification);
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(unvalidatedRawCheckedContainerIdentification));
            }

            return rawCheckedContainerIdentification;
        }

        #endregion

        #region Container Identification

        public static string GetOwnerCodeValue(string containerIdentificationValue)
        {
            var ownerCodeValue = containerIdentificationValue.Substring(0, 3);
            return ownerCodeValue;
        }

        public static string GetEquipmentCategoryString(string containerIdentificationValue)
        {
            var equipmentCategoryValue = containerIdentificationValue.Substring(3, 1);
            return equipmentCategoryValue;
        }

        public static string GetSerialNumberValue(string containerIdentificationValue)
        {
            var serialNumberValue = containerIdentificationValue.Substring(4, 6);
            return serialNumberValue;
        }

        public static string GetCheckDigitStringValue(string checkedContainerIdentificationValue)
        {
            var checkDigitStringValue = checkedContainerIdentificationValue.Substring(10, 1);
            return checkDigitStringValue;
        }

        public static void Parse(string containerIdentificationValue, out string ownerCodeValue, out string equipmentCategoryString, out string serialNumberValue)
        {
            ownerCodeValue = Utilities.GetOwnerCodeValue(containerIdentificationValue);
            equipmentCategoryString = Utilities.GetEquipmentCategoryString(containerIdentificationValue);
            serialNumberValue = Utilities.GetSerialNumberValue(containerIdentificationValue);
        }

        public static void Parse(string checkedContainerIdentificationValue, out string ownerCodeValue, out string equipmentCategoryString, out string serialNumberValue, out string checkDigitStringValue)
        {
            Utilities.Parse(checkedContainerIdentificationValue, out ownerCodeValue, out equipmentCategoryString, out serialNumberValue);

            checkDigitStringValue = Utilities.GetCheckDigitStringValue(checkedContainerIdentificationValue);
        }

        public static ContainerIdentification Parse(RawContainerIdentification rawContainerIdentification)
        {
            Utilities.Parse(rawContainerIdentification.Value, out var ownerCodeValue, out var equipmentCategoryString, out var serialNumberValue);

            var containerIdentification = ContainerIdentification.NewFrom(ownerCodeValue, equipmentCategoryString, serialNumberValue);
            return containerIdentification;
        }

        public static CheckedContainerIdentification Parse(RawCheckedContainerIdentification rawContainerIdentification)
        {
            Utilities.Parse(rawContainerIdentification.Value, out var ownerCodeValue, out var equipmentCategoryString, out var serialNumberValue, out var checkDigitStringValue);

            var containerIdentification = CheckedContainerIdentification.NewFrom(ownerCodeValue, equipmentCategoryString, serialNumberValue, checkDigitStringValue);
            return containerIdentification;
        }

        #endregion

        #region Letter to Number

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

        #endregion
    }
}
