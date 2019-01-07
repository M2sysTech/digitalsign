//-----------------------------------------------------------------------
// <copyright file="LocalData.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Threads
{
    using System;
    using System.Collections;

    public class LocalData : ILocalData
    {
        private static readonly object localDataHashtableKey = new object();

        [ThreadStatic]
        private static Hashtable localData = new Hashtable();

        public static bool RunningInWeb
        {
            get { return false; }
        }

        public int Count
        {
            get { return LocalData.localData.Count; }
        }

        private static Hashtable LocalHashtable
        {
            get
            {
                return InstanceFromHashTable();
            }
        }

        public object this[string key]
        {
            get { return LocalData.LocalHashtable[key]; }
            set { LocalData.LocalHashtable[key] = value; }
        }

        public void Clear()
        {
            LocalData.LocalHashtable.Clear();
        }

        public bool ContainsKey(string key)
        {
            return LocalData.LocalHashtable.ContainsKey(key);
        }
        
        private static Hashtable InstanceFromHashTable()
        {
            if (LocalData.localData == null)
            {
                LocalData.localData = new Hashtable();
            }

            return LocalData.localData;
        }
    }
}