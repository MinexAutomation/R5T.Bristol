using System;

using R5T.Bristol.Lib.Identification;


namespace R5T.Bristol.Lib
{
    public class ContainerIdentification
    {
        #region Static

        public static ContainerIdentification NewFrom(OwnerCode ownerCode, EquipmentCategory equipmentCategory, SerialNumber serialNumber, CheckDigit checkDigit)
        {
            var containerIdentification = new ContainerIdentification().From(ownerCode, equipmentCategory, serialNumber, checkDigit);

            return containerIdentification;
        }

        #endregion


        public OwnerCode OwnerCode { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }
        public SerialNumber SerialNumber { get; set; }
        public CheckDigit CheckDigit { get; set; }


        public ContainerIdentification()
        {
        }
    }
}
