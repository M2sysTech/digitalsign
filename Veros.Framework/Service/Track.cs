namespace Veros.Framework.Service
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Veros.Framework.Modelo;

    public class Track : ITrack
    {
        private readonly Dictionary<string, IEntidade> clones = new Dictionary<string, IEntidade>();

        public void Snapshot<T>(T entidade) where T : IEntidade
        {
            this.clones[entidade.ToString()] = entidade.SerializedClone();
        }

        public bool HasChanged<T>(T entidade) where T : IEntidade
        {
            var clone = this.clones[entidade.ToString()];
            return this.AreObjectsEqual(clone, entidade) == false;
        }
        
        private bool AreObjectsEqual(
            object objectA, 
            object objectB,
            bool includeEnumerables = false,
            bool includeReferences = false)
        {
            bool result;

            if (objectA != null && objectB != null)
            {
                var objectType = objectA.GetType();
                result = true;

                foreach (PropertyInfo propertyInfo in objectType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead))
                {
                    var valueA = propertyInfo.GetValue(objectA, null);
                    var valueB = propertyInfo.GetValue(objectB, null);

                    if (this.CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (this.AreValuesEqual(valueA, valueB) == false)
                        {
                            result = false;
                        }
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        if (includeEnumerables == false)
                        {
                            continue;
                        }

                        if ((valueA == null && valueB != null) || (valueA != null && valueB == null))
                        {
                            result = false;
                        }
                        else if (valueA != null)
                        {
                            IEnumerable<object> collectionItems1 = ((IEnumerable)valueA).Cast<object>();
                            IEnumerable<object> collectionItems2 = ((IEnumerable)valueB).Cast<object>();
                            int collectionItemsCount1 = collectionItems1.Count();
                            int collectionItemsCount2 = collectionItems2.Count();

                            if (collectionItemsCount1 != collectionItemsCount2)
                            {
                                result = false;
                            }
                            else
                            {
                                for (int i = 0; i < collectionItemsCount1; i++)
                                {
                                    object collectionItem1 = collectionItems1.ElementAt(i);
                                    object collectionItem2 = collectionItems2.ElementAt(i);
                                    Type collectionItemType = collectionItem1.GetType();

                                    if (this.CanDirectlyCompare(collectionItemType))
                                    {
                                        if (this.AreValuesEqual(
                                            collectionItem1,
                                            collectionItem2) == false)
                                        {
                                            result = false;
                                        }
                                    }
                                    else if (AreObjectsEqual(
                                        collectionItem1,
                                        collectionItem2) == false)
                                    {
                                        result = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (includeReferences == false)
                        {
                            continue;
                        }

                        if (AreObjectsEqual(
                            propertyInfo.GetValue(objectA, null),
                            propertyInfo.GetValue(objectB, null)) == false)
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
            {
                result = Equals(objectA, objectB);
            }

            return result;
        }

        private bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        private bool AreValuesEqual(object valueA, object valueB)
        {
            bool result;
            var selfValueComparer = valueA as IComparable;

            if ((valueA == null && valueB != null) || (valueA != null && valueB == null))
            {
                result = false; // one of the values is null
            }
            else if (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
            {
                result = false; // the comparison using IComparable failed
            }
            else if (!Equals(valueA, valueB))
            {
                result = false; // the comparison using Equals failed
            }
            else
            {
                result = true; // match
            }

            return result;
        }
    }
}