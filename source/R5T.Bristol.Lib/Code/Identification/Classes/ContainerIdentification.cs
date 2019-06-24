using System;

using R5T.Bristol.Lib.Identification.Extensions;


namespace R5T.Bristol.Lib.Identification
{
    public class ContainerIdentification
    {
        #region Static

        public static ContainerIdentification NewFrom(OwnerCode ownerCode, EquipmentCategory equipmentCategory, SerialNumber serialNumber)
        {
            var containerIdentification = new ContainerIdentification().From(ownerCode, equipmentCategory, serialNumber);
            return containerIdentification;
        }

        public static ContainerIdentification NewFromUnvalidated(string unvalidatedOwnerCodeValue, string unvalidatedEquipmentCategoryValue, string unvalidatedSerialNumberValue)
        {
            var ownerCode = unvalidatedOwnerCodeValue.ToOwnerCode();
            var equipmentCategory = unvalidatedEquipmentCategoryValue.ToEquipmentCategory();
            var serialNumber = unvalidatedSerialNumberValue.ToSerialNumber();

            var containerIdentification = ContainerIdentification.NewFrom(ownerCode, equipmentCategory, serialNumber);
            return containerIdentification;
        }

        public static ContainerIdentification NewFrom(string ownerCodeValue, string equipmentCategoryString, string serialNumberValue)
        {
            var ownerCode = ownerCodeValue.AsOwnerCode();
            var equipmentCategory = Utilities.GetEquipmentCategory(equipmentCategoryString); // Assumes standard string.
            var serialNumber = serialNumberValue.AsSerialNumber();

            var containerIdentification = ContainerIdentification.NewFrom(ownerCode, equipmentCategory, serialNumber);
            return containerIdentification;
        }

        #endregion


        public OwnerCode OwnerCode { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }
        public SerialNumber SerialNumber { get; set; }


        public ContainerIdentification()
        {
        }
    }
}
