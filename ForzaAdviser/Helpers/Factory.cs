using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForzaAdviser.Helpers
{
    public class Factory<T> : IFactory<T>
    {
        private readonly Func<T> _factory;

        public Factory(Func<T> factory)
        {
            _factory = factory;
        }

        public T Create
        {
            get 
            {
                return _factory();
            }
        }
    }
}
