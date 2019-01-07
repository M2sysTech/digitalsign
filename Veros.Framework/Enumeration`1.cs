namespace Veros.Framework
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration<TEnumeration, TValue> : IComparable<TEnumeration>,
        IEquatable<TEnumeration> where TEnumeration : Enumeration<TEnumeration, TValue> where TValue : IComparable
    {
        private static readonly Lazy<TEnumeration[]> enumerations = new Lazy<TEnumeration[]>(GetEnumerations);

        private readonly string counterName;
        private readonly TValue value;

        protected Enumeration(TValue value,
            string counterName)
        {
            this.value = value;
            this.counterName = counterName;
        }

        public TValue Value
        {
            get
            {
                return this.value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.counterName;
            }
        }

        public static TEnumeration[] GetAll()
        {
            return enumerations.Value;
        }

        public static bool operator ==(Enumeration<TEnumeration, TValue> left,
            Enumeration<TEnumeration, TValue> right)
        {
            return Equals(left,
                right);
        }

        public static bool operator !=(Enumeration<TEnumeration, TValue> left,
            Enumeration<TEnumeration, TValue> right)
        {
            return !Equals(left,
                right);
        }

        public static TEnumeration FromValue(TValue value)
        {
            return Parse(value,
                "value",
                item => item.Value.Equals(value));
        }

        public static TEnumeration Parse(string displayName)
        {
            return Parse(displayName,
                "display name",
                item => item.DisplayName == displayName);
        }

        public static bool TryParse(TValue value,
            out TEnumeration result)
        {
            return TryParse(e => e.Value.Equals(value),
                out result);
        }

        public static bool TryParse(string displayName,
            out TEnumeration result)
        {
            return TryParse(e => e.DisplayName == displayName,
                out result);
        }

        public int CompareTo(TEnumeration other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public override sealed string ToString()
        {
            return this.DisplayName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TEnumeration);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        private static bool TryParse(Func<TEnumeration, bool> predicate,
            out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }

        private static TEnumeration[] GetEnumerations()
        {
            Type enumerationType = typeof(TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null)).Cast<TEnumeration>().ToArray();
        }

        private static TEnumeration Parse(object value,
            string description,
            Func<TEnumeration, bool> predicate)
        {
            TEnumeration result;

            if (!TryParse(predicate,
                out result))
            {
                string message = string.Format("'{0}' is not a valid {1} in {2}",
                    value,
                    description,
                    typeof(TEnumeration));
                throw new ArgumentException(message,
                    "value");
            }

            return result;
        }
    }
}
