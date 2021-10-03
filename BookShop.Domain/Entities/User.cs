using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }     
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }             
        public string City { get; set; }
        public string Country { get; set; }   
        public ICollection<UserRole> UserRoles { get; set; }    
    }
}
