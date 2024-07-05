using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jmeza44.EtherBlog.IdentityServer.Pages.UsersManagement.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IList<UserViewModel> Users { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            const int pageSize = 10;
            Users = new List<UserViewModel>();

            IQueryable<ApplicationUser> usersQuery = _userManager.Users;

            TotalPages = (int)Math.Ceiling(await usersQuery.CountAsync() / (double)pageSize);

            PageIndex = pageIndex ?? 1;

            var users = await usersQuery
                .Skip((PageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                Users.Add(new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role ?? "",
                    
                });
            }

            HasPreviousPage = (PageIndex > 1);
            HasNextPage = (PageIndex < TotalPages);

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRole = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRole);
            await _userManager.AddToRoleAsync(user, role);

            return new RedirectToPageResult("/UsersManagement/Users/Index");
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                // Handle errors
            }

            return new RedirectToPageResult("/UsersManagement/Users/Index");
        }

        public class UserViewModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
    }
}
