using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Account
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}