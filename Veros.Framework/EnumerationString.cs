namespace Veros.Framework
{
    using System;
    using System.Diagnostics;

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class EnumerationString<TEnumeration> : Enumeration<TEnumeration, string>
        where TEnumeration : EnumerationString<TEnumeration>
    {
        protected EnumerationString(string value, string counterName)
            : base(value, counterName)
        {
        }

        public static TEnumeration FromString(string value)
        {
            return FromValue(value);
        }

        public static bool TryFromString(string listItemValue, out TEnumeration result)
        {
            return TryParse(listItemValue, out result);
        }
    }
}
