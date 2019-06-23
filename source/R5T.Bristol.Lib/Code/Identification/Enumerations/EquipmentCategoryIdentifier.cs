using System;


namespace R5T.Bristol.Lib.Identification
{
    /// <summary>
    /// Currently all BIC container codes end in "U"
    /// </summary>
    public enum EquipmentCategoryIdentifier
    {
        /// <summary>
        /// For all freight containers.
        /// </summary>
        U,
        /// <summary>
        /// For detachable freight container-related equipment.
        /// </summary>
        J,
        /// <summary>
        /// For trailers and chassis.
        /// </summary>
        Z
    }
}
