using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chinookapi1
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }
        public string SalesRep { get; set; }
        public string CustomerFullName { get; set; }
    }
}
