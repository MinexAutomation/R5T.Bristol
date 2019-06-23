//using System;

//using R5T.Bristol.Lib.Identification.Extensions;


//namespace R5T.Bristol.Lib.Identification
//{
//    public static class UnvalidatedEquipmentCategoryExtensions
//    {
//        public static string ToStringStandard(this EquipmentCategory equipmentCategory)
//        {
//            var standardString = Utilities.GetEquipmentCategoryStandardString(equipmentCategory);
//            return standardString;
//        }

//        public static EquipmentCategory ToEquipmentCategory(this EquipmentCategory equipmentCategory)
//        {
//            var standardString = equipmentCategory.ToStringStandard();

//            var equipmentCategory = standardString.AsEquipmentCategory();
//            return equipmentCategory;
//        }
//    }
//}
