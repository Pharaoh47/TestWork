using System.ComponentModel.DataAnnotations;

namespace Phonebook{
    // Entity with name is typical in development
    public abstract class NamedEntity : BaseEntity{
        // some validation what name exist
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }
    }
}