using mAxCommerce.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone
{
    public class WindowPhoneObjectStorage : IObjectStorage
    {
        private readonly IsolatedStorageFile isolatedStorage;

        public WindowPhoneObjectStorage()
        {
            this.isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
        }

        public Task Write<T>(string key, T value)
        {
            return Task.Run(() =>
            {
                using (var stream = new IsolatedStorageFileStream(key, FileMode.OpenOrCreate, this.isolatedStorage))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(stream, value);
                }
            });
        }

        public Task<T> Read<T>(string key)
        {
            return Task.Run(() =>
            {
                if (!this.isolatedStorage.FileExists(key))
                {
                    throw new FileNotFoundException("Could not find a file for key: " + key);
                }

                using (var stream = new IsolatedStorageFileStream(key, FileMode.Open, this.isolatedStorage))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    return (T)serializer.ReadObject(stream);
                }
            });
        }

        public Task Delete(string key)
        {
            return Task.Run(() => 
            {
                this.isolatedStorage.DeleteFile(key);
            });
        }
    }
}
