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
    public class UserAccountDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserAccountDetailController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAccountDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccountDetail>>> GetUserAccountDetails()
        {
            return await _context.UserAccountDetails.ToListAsync();
        }

        // GET: api/UserAccountDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountDetail>> GetUserAccountDetail(int? id)
        {
            var userAccountDetail = await _context.UserAccountDetails.FindAsync(id);

            if (userAccountDetail == null)
            {
                return NotFound();
            }

            return userAccountDetail;
        }

        [HttpGet]
        [Route("customerid/{id}")]

        public ActionResult<UserAccountDetail> GetAccount(int? id)
        {
            var data = _context.UserAccountDetails.FirstOrDefault(a => a.CustomerId == id);
            if (data == null)
                return BadRequest();
            else
                return data;
        }


        // PUT: api/UserAccountDetail/5
      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccountDetail(int? id, UserAccountDetail userAccountDetail)
        {
            if (id != userAccountDetail.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(userAccountDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountDetailExists(id))
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

        [HttpPatch()]
        [Route ("updatebalance/{id}")]
        public ActionResult Patch(int id, UserAccountDetail userAccountDetail)
        {
            var data = _context.UserAccountDetails.FirstOrDefault(c => c.AccountId == id);
            if (data == null) return BadRequest();
            else
            {
                data.Balance = userAccountDetail.Balance;
                _context.SaveChanges();
                return Ok();

            }

        }

        [HttpPatch]
        [Route("approval/{id}")]
        public ActionResult AcceptCustomer(int? id)
        {
            var data = _context.UserAccountDetails.FirstOrDefault(u => u.CustomerId == id);
            if (data == null)
                return BadRequest();
            else
            {
                data.AccountStatus = true;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPatch]
        [Route("reject/{id}")]
        public ActionResult RejectCustomer(int? id)
        {
            var data = _context.UserAccountDetails.FirstOrDefault(u => u.CustomerId == id);
            if (data == null)
                return BadRequest();
            else
            {
                data.AccountStatus = false;
                _context.SaveChanges();
                return Ok();
            }
        }


        // POST: api/UserAccountDetail

        [HttpPost]
        public async Task<ActionResult<UserAccountDetail>> PostUserAccountDetail(UserAccountDetail userAccountDetail)
        {
            _context.UserAccountDetails.Add(userAccountDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAccountDetail", new { id = userAccountDetail.AccountId }, userAccountDetail);
        }

        // DELETE: api/UserAccountDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAccountDetail>> DeleteUserAccountDetail(int? id)
        {
            var userAccountDetail = await _context.UserAccountDetails.FindAsync(id);
            if (userAccountDetail == null)
            {
                return NotFound();
            }

            _context.UserAccountDetails.Remove(userAccountDetail);
            await _context.SaveChangesAsync();

            return userAccountDetail;
        }

        private bool UserAccountDetailExists(int? id)
        {
            return _context.UserAccountDetails.Any(e => e.AccountId == id);
        }
    }
}
