using System;


namespace R5T.Bristol.Lib.Identification
{
    public static class CheckedContainerIdentificationExtensions
    {
        public static CheckedContainerIdentification From(this CheckedContainerIdentification containerIdentification,
            OwnerCode ownerCode,
            EquipmentCategory equipmentCategory,
            SerialNumber serialNumber,
            CheckDigit checkDigit)
        {
            containerIdentification.OwnerCode = ownerCode;
            containerIdentification.EquipmentCategory = equipmentCategory;
            containerIdentification.SerialNumber = serialNumber;
            containerIdentification.CheckDigit = checkDigit;

            return containerIdentification;
        }
    }
}
