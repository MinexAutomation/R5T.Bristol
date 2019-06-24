using System;


namespace R5T.Bristol.Lib.Identification
{
    public static class Constants
    {
        public const string EquipmentCategoryJStandardString = "J";
        public const string EquipmentCategoryUStandardString = "U";
        public const string EquipmentCategoryZStandardString = "Z";
        public static readonly string[] EquipmentCategoryStandardStrings = new string[]
        {
            Constants.EquipmentCategoryJStandardString,
            Constants.EquipmentCategoryUStandardString,
            Constants.EquipmentCategoryZStandardString,
        };

        /// <summary>
        /// Must be three (3) upper-case letters A-Z.
        /// </summary>
        public const string OwnerCodeRegexPattern = @"[A-Z]{3}";
        /// <summary>
        /// Must be six (6) numbers 0-9.
        /// </summary>
        public const string SerialNumberRegexPattern = @"[0-9]{6}";
        /// <summary>
        /// Must be a number 0-9.
        /// </summary>
        public const string CheckDigitRegexPattern = @"[0-9]";
        /// <summary>
        /// Must be a number 0-9 or an upper-case letter A-Z.
        /// </summary>
        public const string ValidCharacterRegexPattern = @"[0-9]|[A-Z]";
        /// <summary>
        /// Must be 3 upper-case letters, an upper-case letter, 6 numbers, and a number (11 characters).
        /// </summary>
        public const string CheckedContainerIdentificationRegexPattern = @"[A-Z]{3}[A-Z][0-9]{6}[0-9]";
        /// <summary>
        /// Must be 3 upper-case letters, an upper-case letter, 6 numbers (10 characters), without the final check digit.
        /// </summary>
        public const string ContainerIdentificationRegexPattern = @"[A-Z]{3}[A-Z][0-9]{6}";
    }
}
