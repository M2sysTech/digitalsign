namespace Veros.Framework
{
    using System;
    using System.Diagnostics;

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class EnumerationInt<TEnumeration> : Enumeration<TEnumeration, int>
        where TEnumeration : EnumerationInt<TEnumeration>
    {
        protected EnumerationInt(int value, string counterName)
            : base(value, counterName)
        {
        }

        public static TEnumeration FromInt(int value)
        {
            return FromValue(value);
        }

        public static bool TryFromInt(int listItemValue, out TEnumeration result)
        {
            return TryParse(listItemValue, out result);
        }
    }
}
