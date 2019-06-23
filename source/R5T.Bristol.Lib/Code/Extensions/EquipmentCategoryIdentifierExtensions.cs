using System;

using R5T.Bristol.Lib.Extensions;


namespace R5T.Bristol.Lib
{
    public static class EquipmentCategoryIdentifierExtensions
    {
        public static string ToStringStandard(this EquipmentCategoryIdentifier equipmentCategoryIdentifier)
        {
            var standardString = Utilities.GetStandardEquipmentCateogoryString(equipmentCategoryIdentifier);
            return standardString;
        }

        public static EquipmentCategory ToEquipmentCategory(this EquipmentCategoryIdentifier equipmentCategoryIdentifier)
        {
            var standardString = equipmentCategoryIdentifier.ToStringStandard();

            var equipmentCategory = standardString.AsEquipmentCategory();
            return equipmentCategory;
        }
    }
}
