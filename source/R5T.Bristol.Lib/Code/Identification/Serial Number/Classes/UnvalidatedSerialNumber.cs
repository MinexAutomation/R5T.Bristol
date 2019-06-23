using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class UnvalidatedSerialNumber : TypedString
    {
        public UnvalidatedSerialNumber(string value)
            : base(value)
        {
        }
    }
}
