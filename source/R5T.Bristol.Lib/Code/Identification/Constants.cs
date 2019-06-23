using System;


namespace R5T.Bristol.Lib.Identification
{
    public static class Constants
    {
        public const string EquipmentCategoryIdentifierJValue = "J";
        public const string EquipmentCategoryIdentifierUValue = "U";
        public const string EquipmentCategoryIdentifierZValue = "Z";
        public static readonly string[] EquipmentCategoryIdentifiers = new string[]
        {
            Constants.EquipmentCategoryIdentifierJValue,
            Constants.EquipmentCategoryIdentifierUValue,
            Constants.EquipmentCategoryIdentifierZValue,
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
    }
}
