using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class SerialNumberNumeric : TypedInt
    {
        public const SerialNumberNumeric Invalid = null;


        public SerialNumberNumeric(int value)
            : base(value)
        {
        }
    }
}
