using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Services
{
    public class StoredList<T> : ICollection<T>
    {
        private readonly IObjectStorage objectStorage;
        private readonly string storageKey;

        public StoredList(IObjectStorage objectStorage, string storageKey)
        {
            this.objectStorage = objectStorage;
            this.storageKey = storageKey;
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            this.Items = new List<T>();
        }

        public bool Contains(T item)
        {
            return this.Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Items.ToArray().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.Items.Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            List<T> items = this.Items.ToList();
            bool result = items.Remove(item);
            this.Items = items;
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Items
        {
            get
            {
                return this.ReadFromStorage().Result;
            }
            set
            {
                this.WriteToStorage(value.ToList()).Wait();
            }
        }

        private async Task WriteToStorage(List<T> items)
        {
            await this.objectStorage.Write(this.storageKey, items);
        }

        private async Task<List<T>> ReadFromStorage()
        {
            try
            {
                return await this.objectStorage.Read<List<T>>(this.storageKey);
            }
            catch (FileNotFoundException exception)
            {
                this.objectStorage.Write(this.storageKey, new List<T>()).Wait();
                return new List<T>();
            }
        }
    }
}
