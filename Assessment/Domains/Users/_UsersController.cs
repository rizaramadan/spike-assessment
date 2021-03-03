using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Auth;
using Assessment.Domains.Data;
using Assessment.Models;

namespace Assessment.Domains.Users
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(AppDbContext context, UserManager<AppUser> m, RoleManager<AppRole> r)
        {
            _context = context;
            _userManager = m;
            _roleManager = r;
        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.OrderBy(x => x.Id).ToListAsync());
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = appUser.Email, Email = appUser.Email };
                var result = await _userManager.CreateAsync(user, appUser.PasswordHash);
                if (result.Succeeded) 
                {
                    await _userManager.SetPhoneNumberAsync(user, appUser.PhoneNumber);
                    await _userManager.SetUserNameAsync(user, appUser.UserName);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _userManager.ConfirmEmailAsync(user, token);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            var allRoles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _context.UserRoles
                .AsNoTracking()
                .Where(x => x.UserId == id).Select(x => new AppRole { Id = x.RoleId })
                .ToListAsync();
            userRoles.ForEach(x => x.Name = allRoles.FirstOrDefault(y => y.Id == x.Id)?.Name);
            var notInRoles = allRoles.Except(userRoles);
            var appUserRole = new AppUserRole();
            appUserRole.MapFrom(appUser);
            
            var resultRoles = userRoles.Select(x => new SelectListItem { Value = $"{x.Id}", Text = x.Name, Selected = true }).ToList();
            resultRoles.AddRange(notInRoles.Select(x => new SelectListItem { Value = $"{x.Id}", Text = x.Name, Selected = false }).ToList());
            appUserRole.Roles = resultRoles.OrderBy(x => x.Text).ToList();
            return View(appUserRole);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, AppUserRole appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync($"{id}");
                    if(user.Email != appUser.Email)
                        await _userManager.SetEmailAsync(user, appUser.Email);
                    if (user.PhoneNumber != appUser.PhoneNumber)
                        await _userManager.SetPhoneNumberAsync(user, appUser.PhoneNumber);
                    if(user.UserName != appUser.UserName)
                        await _userManager.SetUserNameAsync(user, appUser.UserName);

                    if (user.NRG != appUser.NRG)
                    {
                        var aUser = await _context.Users.FindAsync(id);
                        aUser.NRG = appUser.NRG;
                        await _context.SaveChangesAsync();
                    }

                    if (user.NSIN != appUser.NSIN)
                    {
                        var aUser = await _context.Users.FindAsync(id);
                        aUser.NSIN = appUser.NSIN;
                        await _context.SaveChangesAsync();
                    }

                    foreach (var each in appUser.Roles) 
                    {
                        var isInRole = await _userManager.IsInRoleAsync(user, each.Text);
                        if (each.Selected && !isInRole)
                        {
                            await _userManager.AddToRoleAsync(user, each.Text);
                        }
                        else if (!each.Selected && isInRole) 
                        {
                            await _userManager.RemoveFromRoleAsync(user, each.Text);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
