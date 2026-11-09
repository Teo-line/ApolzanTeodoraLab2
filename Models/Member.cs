using System.ComponentModel.DataAnnotations;

namespace ApolzanTeodoraLab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$",
            ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau Ana-Maria)")]
        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]
        public string? LastName { get; set; }

        [StringLength(70)]
        public string? Address { get; set; }

        public string Email { get; set; } = string.Empty;

        // Task 3: first digit must be 0
        [RegularExpression(@"^\(?(0[0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
            ErrorMessage = "Telefonul trebuie sa fie de forma '0722-123-123' sau '0722.123.123' sau '0722 123 123'")]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName        // note the "?": it was "string" in your file
        {
            get { return FirstName + " " + LastName; }
        }

        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}