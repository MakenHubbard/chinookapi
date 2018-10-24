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
        public List<Invoice> Get()
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
	                        on i.CustomerId = c.CustomerId";

                var read = command.ExecuteReader();


                List<Invoice> Invoices = new List<Invoice>();

                while (read.Read())
                {

                    var invoice = new Invoice();
                    
                    invoice.InvoiceId = (int)read["invoiceId"];
                    invoice.CustomerId = (int)read["customerId"];
                    invoice.InvoiceDate = (int)read["invoiceId"];
                    invoice.BillingAddress = read["billingAddress"].ToString();
                    invoice.BillingCity = read["billingCity"].ToString();
                    invoice.BillingState = read["billingState"].ToString();
                    invoice.BillingCountry = read["billingCountry"].ToString();
                    invoice.BillingPostalCode = read["billingPostalCode"].ToString();
                    invoice.Total = (decimal)read["total"];
                    invoice.SalesRep = read["saleRep"].ToString();

                    Invoices.Add(invoice);
                }
                return Invoices;

            }
        }

        [HttpGet("invoiceTwo")]
        public List<Invoice> GetCustomer()
        {
            using (var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"select 
	                                         [Invoice Total] = i.Total,
	                                         [Customer Name] = c.FirstName + ' ' + c.LastName,
	                                         Country = i.BillingCountry,
	                                        [sale rep] = e.FirstName + ' ' + e.LastName
                                        from employee e
                                        join customer c
	                                        on e.EmployeeId = c.SupportRepId
                                        join Invoice i
	                                        on i.CustomerId = c.CustomerId";

                var read = command.ExecuteReader();

                List<Invoice> Invoices2 = new List<Invoice>();

                while (read.Read())
                {
                    var invoice = new Invoice();
                    var customer = new Customer();
                    var customerFullName = customer.FirstName + " " + customer.LastName;

                    invoice.Total = (decimal)read["Invoice Total"];
                    customerFullName = read["Customer Name"].ToString();
                    invoice.BillingCountry = read["Country"].ToString();
                    invoice.SalesRep = read["sale rep"].ToString();

                    Invoices2.Add(invoice);

                }
                return Invoices2;
            }
        }
    }
}