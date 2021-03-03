using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;

namespace Assessment.Domains.Users
{
    public class AppUserRole : AppUser
    {
        public List<SelectListItem> Roles { get; set; }

        public void MapFrom(AppUser u) 
        {
            LockoutEnd = u.LockoutEnd;
            TwoFactorEnabled = u.TwoFactorEnabled;
            PhoneNumberConfirmed = u.PhoneNumberConfirmed;
            PhoneNumber = u.PhoneNumber;
            ConcurrencyStamp = u.ConcurrencyStamp;
            SecurityStamp = u.SecurityStamp;
            PasswordHash = u.PasswordHash;
            EmailConfirmed = u.EmailConfirmed;
            NormalizedEmail = u.NormalizedEmail;
            Email = u.Email;
            NormalizedUserName = u.NormalizedUserName;
            UserName = u.UserName;
            Id = u.Id;
            LockoutEnabled = u.LockoutEnabled;
            AccessFailedCount = u.AccessFailedCount;
            NSIN = u.NSIN;
            NRG = u.NRG;
        }
    }
}
