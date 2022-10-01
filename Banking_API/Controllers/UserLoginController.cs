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
    public class UserLoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserLoginController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLogin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLoginDetail>>> GetUserLogIn()
        {
            return await _context.UserLogIn.ToListAsync();
        }

        // GET: api/UserLogin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLoginDetail>> GetUserLogin(int? id)
        {
            var userLogin = await _context.UserLogIn.FindAsync(id);

            if (userLogin == null)
            {
                return NotFound();
            }

            return userLogin;
        }

        [HttpGet]
        [Route("accountid/{id}")]
        public ActionResult<UserLoginDetail> GetLog(int? id)
        {
            var data = _context.UserLogIn.FirstOrDefault(u => u.AccountId == id);
            if (data == null)
                return BadRequest();
            else
             return data;

        }

        // PUT: api/UserLogin/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLogin(int? id, UserLoginDetail userLogin)
        {
            if (id != userLogin.LogId)
            {
                return BadRequest();
            }

            _context.Entry(userLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(id))
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

        // POST: api/UserLogin
        
        [HttpPost]
        public async Task<ActionResult<UserLoginDetail>> PostUserLogin(UserLoginDetail userLogin)
        {
            _context.UserLogIn.Add(userLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLogin", new { id = userLogin.LogId }, userLogin);
        }

        // DELETE: api/UserLogin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserLoginDetail>> DeleteUserLogin(int? id)
        {
            var userLogin = await _context.UserLogIn.FindAsync(id);
            if (userLogin == null)
            {
                return NotFound();
            }

            _context.UserLogIn.Remove(userLogin);
            await _context.SaveChangesAsync();

            return userLogin;
        }

        private bool UserLoginExists(int? id)
        {
            return _context.UserLogIn.Any(e => e.LogId == id);
        }
    }
}
