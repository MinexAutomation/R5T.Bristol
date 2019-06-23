using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public static class EquipmentCategoryExtensions
    {
        public static DescribedResult<bool> IsValid(this EquipmentCategory equipmentCategory)
        {
            var output = Utilities.IsValid(equipmentCategory);
            return output;
        }

        public static void Validate(this EquipmentCategory equipmentCategory)
        {
            var isValid = equipmentCategory.IsValid();
            if(!isValid.Value)
            {
                throw new ArgumentException(isValid.Message, nameof(equipmentCategory));
            }
        }
    }
}
