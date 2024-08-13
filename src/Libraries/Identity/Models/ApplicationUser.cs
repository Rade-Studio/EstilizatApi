using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Status { get; set; }
        public List<Preference> Preferences { get; set; } = [];
        
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

}