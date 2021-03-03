using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class AppUser : IdentityUser<long>
    {
        public string NRG { get; set; }
        public string NSIN { get; set; }
    }
}
