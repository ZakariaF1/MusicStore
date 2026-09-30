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
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceRepository invoiceRepository, IInvoiceService invoiceService)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceService = invoiceService;
        }

        [HttpGet("api/v1/invoices")]
        public async Task<IActionResult> ListInvoices()
        {
            IQueryable<Invoice> invoicesQuery = _invoiceRepository.InvoiceBaseQuery();

            invoicesQuery = invoicesQuery.OrderBy(invoice => invoice.InvoiceId);

            IEnumerable<Invoice> invoices = await _invoiceRepository.ListInvoicesAsync(invoicesQuery, true);

            IEnumerable<InvoiceResponse> response = invoices.Select(p => p.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/invoices")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool customerExists = await _invoiceRepository.CustomerExistsAsync(request.CustomerId);

            if (!customerExists)
            {
                return NotFound("Customer wasn't found");
            }

            Invoice invoice = request.ToEntity();

            await _invoiceRepository.CreateInvoiceAsync(invoice);

            await _invoiceRepository.SaveAsync();

            InvoiceLiteResponse response = invoice.ToLiteResponseDto();

            return CreatedAtRoute("GetInvoiceById", new { invoiceId = invoice.InvoiceId }, response);
        }

        [HttpGet("api/v1/invoices/{invoiceId}", Name = "GetInvoiceById")]
        public async Task<IActionResult> GetInvoice([FromRoute] int invoiceId)
        {
            Invoice invoice = await _invoiceRepository.GetInvoiceAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound("Invoice wasn't found");
            }
            else
            {

                InvoiceResponse response = invoice.ToResponseDto();

                return Ok(response);
            }
        }

        [HttpDelete("api/v1/invoices/{invoiceId}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int invoiceId)
        {
            Invoice invoice = await _invoiceRepository.GetInvoiceAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound("Invoice wasn't found");
            }
            else
            {
                await _invoiceService.DeleteInvoiceCascadeAsync(invoice);
                //_invoiceRepository.DeleteInvoice(invoice);
                await _invoiceRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/invoices/{invoiceId}")]
        public async Task<IActionResult> UpdateInvoice([FromRoute] int invoiceId, [FromBody] InvoiceUpdateRequest request)
        {
            Invoice invoice = await _invoiceRepository.GetInvoiceAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound("Invoice wasn't found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool customerExists = await _invoiceRepository.CustomerExistsAsync(request.CustomerId);

            if (!customerExists)
            {
                return NotFound("Customer wasn't found");
            }

            invoice.UpdateInvoice(request);
            await _invoiceRepository.SaveAsync();

            InvoiceLiteResponse response = invoice.ToLiteResponseDto();

            return Ok(invoice);
        }

        [HttpGet("api/v1/invoices/{invoiceId}/invoiceItems")]
        public async Task<IActionResult> ListInvoiceItems([FromRoute] int invoiceId)
        {
            bool invoiceExists = await _invoiceRepository.InvoiceExistsAsync(invoiceId);

            if (!invoiceExists)
            {
                return NotFound("Invoice wasn't found");
            }
            else
            {
                IQueryable<InvoiceItem> invoiceItemsQuery = _invoiceRepository.InvoiceItemBaseQuery();

                invoiceItemsQuery = invoiceItemsQuery.Where(invoiceItem => invoiceItem.InvoiceId == invoiceId);

                IEnumerable<InvoiceItem> invoiceItems = await _invoiceRepository.ListInvoiceItemsAsync(invoiceItemsQuery, true);

                IEnumerable<InvoiceItemResponse> response = invoiceItems.Select(p => p.ToResponseDto());

                return Ok(response);
            }
        }

        [HttpPost("api/v1/invoices/{invoiceId}/invoiceItems")]
        public async Task<IActionResult> CreateInvoiceItem([FromRoute] int invoiceId, [FromBody] InvoiceItemCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool invoiceExists = await _invoiceRepository.InvoiceExistsAsync(invoiceId);

            if (!invoiceExists)
            {
                return NotFound("Invoice wasn't found");
            }

            bool trackExists = await _invoiceRepository.TrackExistsAsync(request.TrackId);

            if (!trackExists)
            {
                return NotFound("Track wasn't found");
            }

            InvoiceItem invoiceItem = request.ToEntity();
            invoiceItem.InvoiceId = invoiceId;

            await _invoiceRepository.CreateInvoiceItemAsync(invoiceItem);

            await _invoiceRepository.SaveAsync();

            InvoiceItemLiteResponse response = invoiceItem.ToLiteResponseDto();

            return CreatedAtRoute("GetInvoiceInvoiceItemById", new { invoiceLineId = invoiceItem.InvoiceLineId }, response);
        }

        [HttpGet("api/v1/invoices/{invoiceId}/invoiceItems/{invoiceLineId}", Name = "GetInvoiceInvoiceItemById")]
        public async Task<IActionResult> GetInvoiceItem([FromRoute] int invoiceId, [FromRoute] int invoiceLineId)
        {
            bool invoiceExists = await _invoiceRepository.InvoiceExistsAsync(invoiceId);

            if (!invoiceExists)
            {
                return NotFound("Invoice wasn't found");
            }

            InvoiceItem invoiceItem = await _invoiceRepository.GetInvoiceItemAsync(invoiceId, invoiceLineId);
            if (invoiceItem == null)
            {
                return NotFound("The invoice has no invoice item with this ID");
            }
            else
            {
                InvoiceItemResponse response = invoiceItem.ToResponseDto();

                return Ok(response);
            }
        }

        [HttpDelete("api/v1/invoices/{invoiceId}/invoiceItems/{invoiceLineId}")]
        public async Task<IActionResult> DeleteInvoiceItem([FromRoute]int invoiceId, [FromRoute] int invoiceLineId)
        {
            bool invoiceExists = await _invoiceRepository.InvoiceExistsAsync(invoiceId);

            if (!invoiceExists)
            {
                return NotFound("Invoice wasn't found");
            }

            InvoiceItem invoiceItem = await _invoiceRepository.GetInvoiceItemAsync(invoiceId, invoiceLineId);
            if (invoiceItem == null)
            {
                return NotFound("The invoice has no invoice item with this ID");
            }
            else
            {
                await _invoiceService.DeleteInvoiceItemCascadeAsync(invoiceItem);
                //_invoiceRepository.DeleteInvoiceItem(invoiceItem);
                await _invoiceRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/invoices/{invoiceId}/invoiceItems/{invoiceLineId}")]
        public async Task<IActionResult> UpdateInvoiceItem([FromRoute] int invoiceId, [FromRoute] int invoiceLineId, [FromBody] InvoiceItemUpdateRequest request)
        {
            Invoice invoice = await _invoiceRepository.GetInvoiceAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound("Invoice wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InvoiceItem invoiceItem = await _invoiceRepository.GetInvoiceItemAsync(invoiceId, invoiceLineId);
            if (invoiceItem == null)
            {
                return NotFound("The invoice has no invoice item with this ID");
            }

            Track track = await _invoiceRepository.TrackBaseQuery().Where(p => p.TrackId == request.TrackId).FirstOrDefaultAsync();
            if (track == null)
            {
                return NotFound("Track wasn't found");
            }
            else
            {
                if (request.UnitPrice == 0)
                {
                    request.UnitPrice = track.UnitPrice;
                }
                invoice.Total -= invoiceItem.UnitPrice * invoiceItem.Quantity;

                invoiceItem.UpdateInvoiceItem(request);

                invoice.Total += invoiceItem.UnitPrice * invoiceItem.Quantity;

                await _invoiceRepository.SaveAsync();

                InvoiceItemLiteResponse response = invoiceItem.ToLiteResponseDto();

                return Ok(response);
            }
        }
    }
}
