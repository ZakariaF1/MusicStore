using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Domain
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public DateTime? InvoiceDate { get; set; }
        [MaxLength(70)]
        public string BillingAddress { get; set; }
        [MaxLength(40)]
        public string BillingCity { get; set; }
        [MaxLength(40)]
        public string BillingState { get; set; }
        [MaxLength(40)]
        public string BillingCountry { get; set; }
        [MaxLength(10)]
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }
    }
}
