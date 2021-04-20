using System.ComponentModel.DataAnnotations;

namespace Phonebook{
    // an phone number class as there can be many numbers
    public class Phone : BaseEntity{
        // Id (key) of Person goes here
         public int PersonId  { get; set; }

        [Required(ErrorMessage="Phone number is required")]
        // an typical expression for phone numbers
        // https://habr.com/en/post/110731/
        // https://stackoverflow.com/questions/16699007/regular-expression-to-match-standard-10-digit-phone-number
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage="Wrong phone number format")]
        public string Number  { get; set; }
    }
}