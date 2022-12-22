
using System.ComponentModel.DataAnnotations;

namespace FamilyFinance.Persistence.Models.Identity
{
    public class UserModel : BaseTraceModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 25 chars")]
        public string Password { get; set; }
        public string? Token { get; set; }
    }
}
