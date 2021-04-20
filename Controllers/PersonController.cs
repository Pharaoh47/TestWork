using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Phonebook{
    [Route("api/person")]
    [ApiController]
    // Person controller holds only method for listing persons
    // all CRUD at EntityController generic class
    public class PersonController : EntityController<Person>
    {
        public PersonController(PhonebookContext context) : base(context){}
        
        // list all persons sorted by name
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Person>>> List()
        {
            // async task for sorted list
            return await _context.Persons
                .OrderBy(p=>p.Name)
                .ToListAsync();
        }
    } 
}