using System;


namespace R5T.Bristol.Lib.Extensions
{
    public static class StringExtensions
    {
        public static EquipmentCategory AsEquipmentCategory(this string value)
        {
            var equipmentCategory = new EquipmentCategory(value);
            return equipmentCategory;
        }
    }
}
