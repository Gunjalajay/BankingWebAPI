using Banking_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public AppDbContext _context { get; }

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(_context.Customers.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var data = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            return Ok(data);

        }
        [HttpPost]
        public ActionResult Post(Customer customers)
        {
            _context.Customers.Add(customers);
            _context.SaveChanges();
            //return Ok();
            return CreatedAtAction("Get", new { id = customers.CustomerId }, customers);

        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, Customer modifiedcust)
        {
            var data = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (data == null) return BadRequest();
            else
            {
                data.Title = modifiedcust.Title;
                _context.SaveChanges();
                return Ok();
            }

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id, Customer modifieddet)
        {
            var data = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (data == null) return BadRequest();
            else
            {
                _context.Remove(data);
                _context.SaveChanges();
                return Ok();

            }

        }
    }
}
