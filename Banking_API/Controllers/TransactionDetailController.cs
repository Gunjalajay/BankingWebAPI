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
    public class TransactionDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionDetailController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TransactionDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDetail>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        [HttpGet("{id}")]
        //[Route("AccStatement/{id}")]
        public async Task<ActionResult<IEnumerable<TransactionDetail>>> GetAccStatement(int id)
        {
            //return await _context.Transactions.ToListAsync();
            var AccStatement = (from row in _context.Transactions
                                where row.AccountId == id
                                select row).ToListAsync();

            return await AccStatement;
        }

        // GET: api/TransactionDetail/5
       // [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetail>> GetTransactionDetail(int id)
        {
            var transactionDetail = await _context.Transactions.FindAsync(id);

            if (transactionDetail == null)
            {
                return NotFound();
            }

            return transactionDetail;
        }

        // PUT: api/TransactionDetail/5
     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionDetail(int id, TransactionDetail transactionDetail)
        {
            if (id != transactionDetail.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transactionDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionDetailExists(id))
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

        // POST: api/TransactionDetail
        
        [HttpPost]
        public async Task<ActionResult<TransactionDetail>> PostTransactionDetail(TransactionDetail transactionDetail)
        {
            _context.Transactions.Add(transactionDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionDetail", new { id = transactionDetail.TransactionId }, transactionDetail);
        }

        // DELETE: api/TransactionDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TransactionDetail>> DeleteTransactionDetail(int id)
        {
            var transactionDetail = await _context.Transactions.FindAsync(id);
            if (transactionDetail == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactionDetail);
            await _context.SaveChangesAsync();

            return transactionDetail;
        }

        private bool TransactionDetailExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
