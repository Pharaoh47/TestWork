using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook{
    [Route("api/phone")]
    [ApiController]
    // Phone controller holds only methods for listing phone numbers
    // all CRUD at EntityController generic class
    public class PhoneController : EntityController<Phone>
    {
        public PhoneController(PhonebookContext context) : base(context){}

        // list all numbers
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Phone>>> List()
        {
            // async task for sorted list
            return await _context.Phones
                .OrderBy(p=>p.Number)
                .ToListAsync();
        }
        
        // list numbers by person
        [HttpGet("list/{personId}")]
        public async Task<ActionResult<IEnumerable<Phone>>> List(int personId)
        {
            // async task for sorted list for one person
            return await _context.Phones
                .Where(p=>p.PersonId == personId)
                .OrderBy(p=>p.Number)
                .ToListAsync();
        }
    } 
}