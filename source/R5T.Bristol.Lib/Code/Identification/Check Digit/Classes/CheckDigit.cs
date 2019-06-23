using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class CheckDigit : TypedInt
    {
        public const CheckDigit Invalid = null;


        public CheckDigit(int value)
            : base(value)
        {
        }
    }
}
