using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityDemo.Pages.Account;

public class Login : PageModel
{
    [BindProperty]
    public Credential Credential { get; set; }

    public void OnGet()
    {
       
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();
        
        // Verify credential
        if (Credential.UserName == "admin" && Credential.Password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@admin.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("Department", "HR"),
                new Claim("EmploymentDate", "2021-03-30"),
                new Claim("Manager", "True"),
            };

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = Credential.RememberMe
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

            return RedirectToPage("/Index");
        }

        return Page();
    }
}

public class Credential
{
    [Required]
    [Display(Name = "User Name")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Display(Name = "Remember Me")] 
    public bool RememberMe { get; set; }
}