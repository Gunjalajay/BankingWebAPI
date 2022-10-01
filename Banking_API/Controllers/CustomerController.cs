using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banking_API.Models;

namespace Banking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int? id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int? id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPatch]
        [Route("approval/{id}")]
        public ActionResult ApproveCustomer(int? id)
        {
            var data = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (data == null)
                return BadRequest();
            else
            {
                data.ApproveStatus =true;
                _context.SaveChanges();
                return Ok();
            }
        }
        [HttpPatch]
        [Route("reject/{id}")]
        public ActionResult RejectCustomer(int? id)
        {
            var data = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (data == null)
                return BadRequest();
            else
            {
                data.ApproveStatus = false;
                _context.SaveChanges();
                return Ok();
            }
        }

        // POST: api/Customer
      
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int? id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        [HttpGet()]
        [Route("pendingcustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetPendingApprovalCustomerDetails()
        {
            var UserList = (from row in _context.Customers
                            where row.ApproveStatus == false || row.ApproveStatus == null
                            select row).ToListAsync();

            return await UserList;
        }

        private bool CustomerExists(int? id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
