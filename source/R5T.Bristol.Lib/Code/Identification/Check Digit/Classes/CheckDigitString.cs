using System;

using R5T.NetStandard;


namespace R5T.Bristol.Lib.Identification
{
    public class CheckDigitString : TypedString
    {
        public const CheckDigitString Invalid = null;


        public CheckDigitString(string value)
            : base(value)
        {
        }
    }
}
