using System;

using R5T.Bristol.Lib.Identification;


namespace R5T.Bristol.Lib
{
    public static class ContainerIdentificationExtensions
    {
        public static ContainerIdentification From(this ContainerIdentification containerIdentification,
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
