using System;

namespace MusicStore.Api
{
    public class EmployeeApiResponse<Employee>
    {
        public EmployeeApiResponse() {

        }

        public Exception Exception { get; set; }

        public bool HasException {
            get
            {
                return Exception != null;
            }
        }

        public Employee Result { get; set; }
    }
}
