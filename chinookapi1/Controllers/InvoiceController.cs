using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace chinookapi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public Invoice Get()
        {
            using (var connection = new SqlConnection("Server=(Local);Database=Chinook;Trusted_Connection=True"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"select 
	                         i.*,
	                        [saleRep] = e.FirstName + ' ' + e.LastName
                        from employee e
                        join customer c
	                        on e.EmployeeId = c.SupportRepId
                        join Invoice i
	                        on i.CustomerId = c.CustomerId ";

                var read = command.ExecuteReader();

                if (read.Read())
                {
                    var invoice = new Invoice();

                    invoice.InvoiceId = (int)read["invoiceId"];
                    invoice.CustomerId = (int)read["customerId"];
                    invoice.InvoiceDate = (int)read["invoiceId"];
                    invoice.BillingAddress = (string)read["billingAddress"];
                    invoice.BillingCity = (string)read["billingCity"];
                    invoice.BillingState = read["billingState"].ToString();
                    invoice.BillingCountry = (string)read["billingCountry"];
                    invoice.BillingPostalCode = (string)read["billingPostalCode"];
                    invoice.Total = (decimal)read["total"];
                    invoice.SalesRep = read["saleRep"].ToString();

                    return invoice;
                }

                return null;
            }
        }
    }
}