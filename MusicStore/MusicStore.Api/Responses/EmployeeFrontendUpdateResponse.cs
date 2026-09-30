using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class EmployeeFrontendUpdateResponse
    {
        public EmployeeUpdateRequest EmployeeUpdateRequest { get; set; }
        public EmployeeResponse EmployeeResponse { get; set; }
    }
}
