namespace Veros.Framework.Servicos
{
    using System;
    using System.Collections.Generic;
    using Veros.Framework;

    /// <summary>
    /// TODO: testes automatizados
    /// </summary>
    public sealed class ObjectPool<T> : IDisposable where T : IPooledObject
    {
        private readonly object locker = new object();
        private readonly Func<ObjectPool<T>, T> factory;
        private readonly Queue<T> queue;
        private readonly List<T> liveObjects = new List<T>();

        public ObjectPool(int size, Func<ObjectPool<T>, T> factory)
        {
            if (size <= 0)
            {
                const string Message = "The size of the pool must be greater than zero.";
                throw new ArgumentOutOfRangeException("size", size, Message);
            }

            this.Size = size;
            this.factory = factory;
            this.queue = new Queue<T>();

            Log.Application.DebugFormat("Pool de {0} criado com tamanho {1}", typeof(T).Name, size);
        }

        public int Size
        {
            get;
            set;
        }

        public T Get()
        {
            lock (this.locker)
            {
                T item;

                if (this.queue.Count > 0)
                {
                    item = this.queue.Dequeue();
                    return item;
                }

                item = this.factory(this);
                this.liveObjects.Add(item);
                return item;
            }
        }

        public void Put(T item)
        {
            lock (this.locker)
            {
                if (this.Size > this.queue.Count)
                {
                    this.queue.Enqueue(item);
                }
                else
                {
                    this.liveObjects.Remove(item);
                    item.Release();
                }
            }
        }

        /// <summary>
        /// Disposes of items in the pool that implement IDisposable.
        /// </summary>
        public void Dispose()
        {
            lock (this.locker)
            {
                while (this.queue.Count > 0)
                {
                    var item = this.queue.Dequeue();
                    item.Release();
                }
            }
        }
    }
}
