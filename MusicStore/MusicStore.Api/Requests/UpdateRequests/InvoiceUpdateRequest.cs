using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicStore.Api.Requests.UpdateRequests
{
    public class InvoiceUpdateRequest
    {
        [Required(ErrorMessage = "The customer Id field is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The invoice date field is required.")]
        public DateTime? InvoiceDate { get; set; }

        [RegularExpression(@"^(?=.*?[a-zA-Z])[0-9a-zA-Z /.'-]+$", ErrorMessage = "Please provide a proper address. Allowed characters are ( 0-9, a-z, A-Z, /, ', ., - ) with at least one letter Ex: 11120 Jasper Ave NW or New York Ave. 75")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "Between 5 and 70 characters is required")]
        public string BillingAddress { get; set; }

        [RegularExpression(@"^[a-zA-Z' .-]+$", ErrorMessage = "Please provide a proper city.Allowed characters are (a-z, A-Z, ', ., - ) Ex: Budapest or Warsaw")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Between 4 and 40 characters is required")]
        public string BillingCity { get; set; }

        [RegularExpression(@"^[a-zA-Z' .-]+$", ErrorMessage = "Please provide a proper state.Allowed characters are (a-z, A-Z, ', ., - ) Ex: Ilfov or Nevada ")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Between 2 and 40 characters is required")]
        public string BillingState { get; set; }

        [MaxLength(40)]
        public string BillingCountry { get; set; }

        [RegularExpression(@"^[0-9A-Z -]+$", ErrorMessage = "Please provide a proper postal code. Allowed characters are ( 0-9, A-Z, - ) Ex: T5K 2N1 or 12345-6789")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Between 4 and 10 characters is required")]
        public string BillingPostalCode { get; set; }
    }
}
