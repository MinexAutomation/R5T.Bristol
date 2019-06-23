using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class UnvalidatedOwnerCode : TypedString
    {
        public UnvalidatedOwnerCode(string value)
            : base(value)
        {
        }
    }
}
