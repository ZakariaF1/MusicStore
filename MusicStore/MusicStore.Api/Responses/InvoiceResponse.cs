using System;


namespace MusicStore.Api.Responses
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }
        public CustomerResponse Customer { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }
    }
}
