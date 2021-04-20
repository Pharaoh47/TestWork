using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Phonebook{

    // Primary class for CRUD, it is generic on the type of BaseEntity
    [ApiController]
    public class EntityController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        // constructor accept data context and we save it
        protected readonly PhonebookContext _context;
        public EntityController(PhonebookContext context)
        {
            _context = context;
        }

        // Read action, by entity id
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            // awaits our entity
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            // return it or throw an 404
            if (entity == null) return NotFound(); 
            return entity;
        }

        // Create action
        [HttpPost]
        public async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            // add our entity
            _context.Set<TEntity>().Add(entity);
            // and awaits its save
            await _context.SaveChangesAsync();
            // return 201 and entity
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        // Update action
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Update(int id, TEntity updatedEntity)
        {
            // ensure an foreign key is correct
            updatedEntity.Id = id;
            // attach and mark entity for save
            _context.Attach(updatedEntity);
            _context.Entry(updatedEntity).State = EntityState.Modified;
            // awaits saving entity
            await _context.SaveChangesAsync();
            // return 200
            return Ok();
        }

        // Delete action
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            // check if entity for deletion exist
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            // return 404 if not
            if (entity == null)
                return NotFound(); 
            // delete entity
            _context.Set<TEntity>().Remove(entity);
            // awaits its removal
            await _context.SaveChangesAsync();
            // return 200
            return Ok();
        }    
    }
}