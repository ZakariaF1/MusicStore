using System;

namespace MusicStore.Api
{
    public class CustomerApiResponse<Customer>
    {
        public CustomerApiResponse() {

        }

        public Exception Exception { get; set; }

        public bool HasException {
            get
            {
                return Exception != null;
            }
        }

        public Customer Result { get; set; }
    }
}
