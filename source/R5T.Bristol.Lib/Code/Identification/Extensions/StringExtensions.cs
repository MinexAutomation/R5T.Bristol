using System;


namespace R5T.Bristol.Lib.Identification.Extensions
{
    public static class StringExtensions
    {
        public static UnvalidatedEquipmentCategory AsUnvalidatedEquipmentCategory(this string value)
        {
            var unvalidatedEquipmentCategory = new UnvalidatedEquipmentCategory(value);
            return unvalidatedEquipmentCategory;
        }

        public static EquipmentCategory ToEquipmentCategory(this string value)
        {
            var unvalidatedEquipmentCategory = value.AsUnvalidatedEquipmentCategory();

            var equipmentCategory = Utilities.Validate(unvalidatedEquipmentCategory);
            return equipmentCategory;
        }

        public static UnvalidatedOwnerCode AsUnvalidatedOwnerCode(this string value)
        {
            var unvalidatedOwnerCode = new UnvalidatedOwnerCode(value);
            return unvalidatedOwnerCode;
        }

        public static OwnerCode ToOwnerCode(this string value)
        {
            var unvalidatedOwnerCode = value.AsUnvalidatedOwnerCode();

            var ownerCode = Utilities.Validate(unvalidatedOwnerCode);
            return ownerCode;
        }
    }
}
