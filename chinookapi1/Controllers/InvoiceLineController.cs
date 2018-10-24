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
    public class InvoiceLineController : ControllerBase
    {
        [HttpGet("{id}")]
        public int GetById(int id)
        {
            using ( var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True;"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"select 
	                                        count(InvoiceId)
                                        from InvoiceLine 
                                        where InvoiceId = @id";

                command.Parameters.AddWithValue("@id",id);

                var scaler = (int)command.ExecuteScalar();

                return scaler;
            }
        }
    }
}