using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class RawCheckedContainerIdentification : TypedString
    {
        public const RawCheckedContainerIdentification Invalid = null;


        public RawCheckedContainerIdentification(string value)
            : base(value)
        {
        }
    }
}
