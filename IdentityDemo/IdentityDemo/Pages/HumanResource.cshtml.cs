using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityDemo.Pages;

[Authorize(Policy = "MustBeHR")]
public class HumanResource : PageModel
{
    public void OnGet()
    {
        
    }
}