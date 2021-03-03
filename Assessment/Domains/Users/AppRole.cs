using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Models
{
    public class AppRole : IdentityRole<long> 
    {
        public override bool Equals(object obj)
        {
            if (obj is AppRole appRole) 
            {
                return Name.Equals(appRole.Name, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
