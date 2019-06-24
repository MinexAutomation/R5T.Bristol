using System;

using R5T.Bristol.Lib.Identification.Extensions;


namespace R5T.Bristol.Lib.Identification
{
    public class CheckedContainerIdentification
    {
        #region Static

        public static CheckedContainerIdentification NewFrom(OwnerCode ownerCode, EquipmentCategory equipmentCategory, SerialNumber serialNumber, CheckDigit checkDigit)
        {
            var containerIdentification = new CheckedContainerIdentification().From(ownerCode, equipmentCategory, serialNumber, checkDigit);

            return containerIdentification;
        }

        public static CheckedContainerIdentification NewFromUnvalidated(string unvalidatedOwnerCodeValue, string unvalidatedEquipmentCategoryValue, string unvalidatedSerialNumberValue, string unvalidatedCheckDigitStringValue)
        {
            var ownerCode = unvalidatedOwnerCodeValue.ToOwnerCode();
            var equipmentCategory = unvalidatedEquipmentCategoryValue.ToEquipmentCategory();
            var serialNumber = unvalidatedSerialNumberValue.ToSerialNumber();
            var checkDigit = unvalidatedCheckDigitStringValue.ToCheckDigit();

            var checkedContainerIdentification = CheckedContainerIdentification.NewFrom(ownerCode, equipmentCategory, serialNumber, checkDigit);
            return checkedContainerIdentification;
        }

        public static CheckedContainerIdentification NewFrom(string ownerCodeValue, string equipmentCategoryString, string serialNumberValue, int checkDigitValue)
        {
            var ownerCode = ownerCodeValue.AsOwnerCode();
            var equipmentCategory = Utilities.GetEquipmentCategory(equipmentCategoryString); // Assumes standard string.
            var serialNumber = serialNumberValue.AsSerialNumber();
            var checkDigit = checkDigitValue.AsCheckDigit();

            var containerIdentification = CheckedContainerIdentification.NewFrom(ownerCode, equipmentCategory, serialNumber, checkDigit);
            return containerIdentification;
        }

        public static CheckedContainerIdentification NewFrom(string ownerCodeValue, string equipmentCategoryString, string serialNumberValue, string checkDigitStringValue)
        {
            var ownerCode = ownerCodeValue.AsOwnerCode();
            var equipmentCategory = Utilities.GetEquipmentCategory(equipmentCategoryString); // Assumes standard string.
            var serialNumber = serialNumberValue.AsSerialNumber();
            var checkDigit = checkDigitStringValue.AsCheckDigit();

            var containerIdentification = CheckedContainerIdentification.NewFrom(ownerCode, equipmentCategory, serialNumber, checkDigit);
            return containerIdentification;
        }

        #endregion


        public OwnerCode OwnerCode { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }
        public SerialNumber SerialNumber { get; set; }
        public CheckDigit CheckDigit { get; set; }


        public CheckedContainerIdentification()
        {
        }
    }
}
