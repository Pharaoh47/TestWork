using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phonebook{
    // an class for human, holding his name and birthday
    // name of person holded by parent abstract class NamedEntity, key is holdede by parent BaseEntity
    public class Person : NamedEntity{
        // some validation on birthday
        [Required(ErrorMessage="Birthday is required")]
        public DateTime BirthDay  { get; set; }

        // a list of numbers, its needed for ef, we dont use it and use FK PersonId in Phone
        public List<Phone> Phones { get; } = new List<Phone>();
    }
}