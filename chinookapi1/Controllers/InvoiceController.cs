using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace chinookapi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly string ConnectionString;


        public InvoiceController(IConfiguration config)
        {
            ConnectionString = config.GetSection("ConnectionString").Value;
        }

        [HttpGet]
        public List<Invoice> Get()
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<Invoice> Invoices = new List<Invoice>();

               

                var result = connection.Query<Invoice>(@"select 
	                                                             i.*,
	                                                            [SalesRep] = e.FirstName + ' ' + e.LastName
                                                            from employee e
                                                            join customer c
	                                                            on e.EmployeeId = c.SupportRepId
                                                            join Invoice i
	                                                            on i.CustomerId = c.CustomerId");
                

                return result.ToList();


                //var command = connection.CreateCommand();
                //command.CommandText = @"select 
	               //          i.*,
	               //         [saleRep] = e.FirstName + ' ' + e.LastName
                //        from employee e
                //        join customer c
	               //         on e.EmployeeId = c.SupportRepId
                //        join Invoice i
	               //         on i.CustomerId = c.CustomerId";

                //var read = command.ExecuteReader();

                //while (read.Read())
                //{

                //    var invoice = new Invoice();

                //    invoice.InvoiceId = (int)read["invoiceId"];
                //    invoice.CustomerId = (int)read["customerId"];
                //    invoice.InvoiceDate = (int)read["invoiceId"];
                //    invoice.BillingAddress = read["billingAddress"].ToString();
                //    invoice.BillingCity = read["billingCity"].ToString();
                //    invoice.BillingState = read["billingState"].ToString();
                //    invoice.BillingCountry = read["billingCountry"].ToString();
                //    invoice.BillingPostalCode = read["billingPostalCode"].ToString();
                //    invoice.Total = (decimal)read["total"];
                //    invoice.SalesRep = read["saleRep"].ToString();

                //    Invoices.Add(invoice);
                //}

            }
        }

        [HttpGet("invoiceTwo")]
        public List<InvoiceTwo> GetCustomer()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var result = connection.Query<InvoiceTwo>(@"select 
	                                                         InvoiceTotal = i.Total,
	                                                         CustomerName = c.FirstName + ' ' + c.LastName,
	                                                         Country = i.BillingCountry,
	                                                         SalesRep = e.FirstName + ' ' + e.LastName
                                                        from employee e
                                                        join customer c
	                                                        on e.EmployeeId = c.SupportRepId
                                                        join Invoice i
	                                                        on i.CustomerId = c.CustomerId");
            return result.ToList();

                //var command = connection.CreateCommand();
                //command.CommandText = @"select 
	               //                          [Invoice Total] = i.Total,
	               //                          [Customer Name] = c.FirstName + ' ' + c.LastName,
	               //                          Country = i.BillingCountry,
	               //                         [sale rep] = e.FirstName + ' ' + e.LastName
                //                        from employee e
                //                        join customer c
	               //                         on e.EmployeeId = c.SupportRepId
                //                        join Invoice i
	               //                         on i.CustomerId = c.CustomerId";

                //var read = command.ExecuteReader();

                //List<Invoice> Invoices2 = new List<Invoice>();

                //while (read.Read())
                //{
                //    var invoice = new Invoice();

                //    invoice.Total = (decimal)read["Invoice Total"];
                //    invoice.CustomerFullName = read["Customer Name"].ToString();
                //    invoice.BillingCountry = read["Country"].ToString();
                //    invoice.SalesRep = read["sale rep"].ToString();

                //    Invoices2.Add(invoice);

                //}
                //return Invoices2;
            }
        }

        [HttpPost]
        public void addNewInvoice( Invoice invoice)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                connection.Execute(@"insert
	                                into invoice (CustomerId, BillingAddress,Total,InvoiceDate)
	                                values (@customerId, @billingAddress, 2.00, '2018/10/23')", invoice );



                //var command = connection.CreateCommand();
                //command.CommandText = @"insert
	               //                    into invoice (CustomerId, BillingAddress,Total,InvoiceDate)
	               //                    values (1,'maken', 2.00, '2018/10/23')";

            }
        }

        [HttpPut]
        public void updateEmployeeName()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                connection.Execute();
            }
        }
    }
}