using System;


namespace R5T.Bristol.Lib.Identification
{
    /// <summary>
    /// Currently all BIC container codes end in "U"
    /// </summary>
    public enum EquipmentCategory
    {
        /// <summary>
        /// Default value for unknown.
        /// </summary>
        Unknown = 0,

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
