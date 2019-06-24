using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class RawContainerIdentification : TypedString
    {
        public const RawContainerIdentification Invalid = null;


        public RawContainerIdentification(string value)
            : base(value)
        {
        }
    }
}
