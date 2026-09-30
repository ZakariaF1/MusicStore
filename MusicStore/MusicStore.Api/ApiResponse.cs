using System;

namespace MusicStore.Api
{
    public class ApiResponse<T> //generics to read about it
    {
        public T Result { get; set; }
        public bool HasException
        {
            get
            {
                return Exception != null;
            }
        }
        public Exception Exception { get; set; }
    }
}
