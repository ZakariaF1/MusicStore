using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStoreWebApp.Models.Requests
{
    public class InvoiceDeleteRequest
    {
        public int[] InvoiceIdsForDeletion { get; set; }
        public int? CustomerId { get; set; }
        public bool IsFromCustomer { get; set; }
    }
}
