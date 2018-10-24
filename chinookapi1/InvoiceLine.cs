using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chinookapi1
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal Unitprice { get; set; }
        public int Quantity { get; set; }
    }
}
