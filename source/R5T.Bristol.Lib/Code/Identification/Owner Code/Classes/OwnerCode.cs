using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class OwnerCode : TypedString
    {
        public const OwnerCode Invalid = null;


        public OwnerCode(string value)
            : base(value)
        {
        }
    }
}
