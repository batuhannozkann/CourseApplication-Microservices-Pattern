using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Course.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? XAddress { get; set; }
        public string? LinkedInAddress { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Picture { get; set; }
    }
}
