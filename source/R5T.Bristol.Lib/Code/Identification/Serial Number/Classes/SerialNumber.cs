using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class SerialNumber : TypedString
    {
        public const SerialNumber Invalid = null;


        public SerialNumber(string value)
            : base(value)
        {
        }
    }
}
