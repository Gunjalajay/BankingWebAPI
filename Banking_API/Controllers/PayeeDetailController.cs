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
    public class PayeeDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PayeeDetailController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PayeeDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayeeDetail>>> GetPayeeDetails()
        {
            return await _context.PayeeDetails.ToListAsync();
        }

        // GET: api/PayeeDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayeeDetail>> GetPayeeDetail(int? id)
        {
            var payeeDetail = await _context.PayeeDetails.FindAsync(id);

            if (payeeDetail == null)
            {
                return NotFound();
            }

            return payeeDetail;
        }

        [HttpGet]
        [Route("accountid/{id}")]

        public ActionResult<PayeeDetail> GetPayee(int? id)
        {
            var data = _context.PayeeDetails.FirstOrDefault(p => p.AccountId == id);
            if (data == null)
            {
                return BadRequest();
            }
            else
                 return data;
        }

        // PUT: api/PayeeDetail/5
     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayeeDetail(int? id, PayeeDetail payeeDetail)
        {
            if (id != payeeDetail.PayeeId)
            {
                return BadRequest();
            }

            _context.Entry(payeeDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayeeDetailExists(id))
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

        // POST: api/PayeeDetail
     
        [HttpPost]
        public async Task<ActionResult<PayeeDetail>> PostPayeeDetail(PayeeDetail payeeDetail)
        {
            _context.PayeeDetails.Add(payeeDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayeeDetail", new { id = payeeDetail.PayeeId }, payeeDetail);
        }

        // DELETE: api/PayeeDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PayeeDetail>> DeletePayeeDetail(int? id)
        {
            var payeeDetail = await _context.PayeeDetails.FindAsync(id);
            if (payeeDetail == null)
            {
                return NotFound();
            }

            _context.PayeeDetails.Remove(payeeDetail);
            await _context.SaveChangesAsync();

            return payeeDetail;
        }

        private bool PayeeDetailExists(int? id)
        {
            return _context.PayeeDetails.Any(e => e.PayeeId == id);
        }
    }
}
