using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core
{
    public interface IObjectStorage
    {
        Task Write<T>(string key, T value);
        Task<T> Read<T>(string key);
        Task Delete(string key);
    }
}
