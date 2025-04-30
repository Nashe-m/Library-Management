using LibraryManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs
{
    public class MemberDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name is too short")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "LastName is too short")]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public IList<Borrowing> Borrowing { get; set; }
    }
    public class AddMemberDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name is too short")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "LastName is too short")]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
    public class UpdateMemberDto : AddMemberDto
    {

    }
}
