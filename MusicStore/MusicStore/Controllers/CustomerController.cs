using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using MusicStore.Repository;
using MusicStore.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Services;

namespace MusicStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerRepository customerRepository,
                                    IInvoiceRepository invoiceRepository,
                                    IEmployeeRepository employeeRepository,
                                    ITrackRepository trackRepository,
                                    ICustomerService customerService)
        {
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;
            _employeeRepository = employeeRepository;
            _trackRepository = trackRepository;
            _customerService = customerService;
        }

        [HttpGet("api/v1/customers")]
        public async Task<IActionResult> ListCustomers()
        {
            IQueryable<Customer> customersQuery = _customerRepository.CustomerBaseQuery();

            customersQuery = customersQuery.OrderBy(customer => customer.CustomerId);

            IEnumerable<Customer> customers = await _customerRepository.ListCustomersAsync(customersQuery, true);

            IEnumerable<CustomerResponse> response = customers.Select(p => p.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/customers")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool employeeExists = await _employeeRepository.EmployeeExistsAsync(request.SupportRepId);

            if (employeeExists || request.SupportRepId.HasValue == false)
            {
                Customer customer = request.ToEntity();

                await _customerRepository.CreateCustomerAsync(customer);

                await _customerRepository.SaveAsync();

                CustomerLiteResponse response = customer.ToLiteResponseDto();

                return CreatedAtRoute("GetCustomerById", new { customerId = customer.CustomerId }, response);
            }
            else
            {
                return BadRequest("The Support Rep doesnt exist, please change SupportRepId property");
            }
        }

        [HttpGet("api/v1/customers/{customerId}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetCustomer([FromRoute] int customerId)
        {
            Customer customer = await _customerRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                return NotFound("Customer wasn't found");
            }
            else
            {
                CustomerResponse response = customer.ToResponseDto();

                return Ok(response);
            }
        }

        [HttpDelete("api/v1/customers/{customerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int customerId)
        {
            Customer customer = await _customerRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                return NotFound("Customer wasn't found");
            }
            else
            {
                //_customerRepository.DeleteCustomer(customer);
                await _customerService.DeleteCustomerCascadeAsync(customer);
                await _customerRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/customers/{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int customerId, [FromBody] CustomerUpdateRequest request)
        {
            Customer customer = await _customerRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                return NotFound("Customer wasn't found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool employeeExists = await _employeeRepository.EmployeeExistsAsync(request.SupportRepId);

            if (employeeExists || request.SupportRepId.HasValue == false)
            {
                customer.UpdateCustomer(request);
                await _customerRepository.SaveAsync();

                CustomerLiteResponse response = customer.ToLiteResponseDto();

                return Ok(response);
            }
            else
            {
                return BadRequest("The Support Rep doesnt exist, please change SupportRepId property");
            }
        }

        [HttpGet("api/v1/customers/{customerId}/invoices")]
        public async Task<IActionResult> ListInvoices([FromRoute] int customerId)
        {
            bool customerExists = await _customerRepository.CustomerExistsAsync(customerId);

            if (!customerExists)
            {
                return NotFound("Customer wasn't found");
            }
            else
            {
                IQueryable<Invoice> invoicesQuery = _invoiceRepository.InvoiceBaseQuery();

                invoicesQuery = invoicesQuery.Where(invoice => invoice.CustomerId == customerId);

                IEnumerable<Invoice> invoices = await _invoiceRepository.ListInvoicesAsync(invoicesQuery, true);

                IEnumerable<InvoiceResponse> response = invoices.Select(p => p.ToResponseDto());

                return Ok(response);
            }
        }

        [HttpGet("api/v1/customers/{customerId}/tracks")]
        public async Task<IActionResult> ListTracks([FromRoute] int customerId)
        {
            bool customerExists = await _customerRepository.CustomerExistsAsync(customerId);

            if (!customerExists)
            {
                return NotFound("Customer wasn't found");
            }
            else
            {
                List<Track> trackList = new List<Track>();

                // Customer invoices
                IQueryable<Invoice> invoicesQuery = _invoiceRepository.InvoiceBaseQuery();

                invoicesQuery = invoicesQuery.Where(invoice => invoice.CustomerId == customerId);

                IEnumerable<Invoice> invoices = await _invoiceRepository.ListInvoicesAsync(invoicesQuery, true);

                // Iterate the customer invoices
                foreach (Invoice invoice in invoices)
                {
                    // Inovice invoiceItems
                    IQueryable<InvoiceItem> invoiceItemsQuery = _invoiceRepository.InvoiceItemBaseQuery();

                    invoiceItemsQuery = invoiceItemsQuery.Where(invoiceItem => invoiceItem.InvoiceId == invoice.InvoiceId);

                    IEnumerable<InvoiceItem> invoiceItems = await _invoiceRepository.ListInvoiceItemsAsync(invoiceItemsQuery, true);

                    // Iterate the inovice invoiceItems
                    foreach (InvoiceItem invoiceItem in invoiceItems)
                    {
                        Track invoiceItemTrack = await _invoiceRepository.TrackBaseQuery().Where(track => track.TrackId == invoiceItem.TrackId).FirstOrDefaultAsync();

                        trackList.Add(invoiceItemTrack);
                    }
                }
                IQueryable<Track> tracksQuery = _invoiceRepository.TrackBaseQuery();

                tracksQuery = tracksQuery.Where(track => trackList.Contains(track));

                IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery, true);

                IEnumerable<TrackResponse> response = tracks.Select(p => p.ToResponseDto());

                return Ok(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ValidateEmailAddress([FromBody] string email)
        {
            bool emailExists = await _customerRepository.EmailAddressExistsAsync(email);

            if (emailExists)
            {
                return Json("This email is used by an another customer");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
