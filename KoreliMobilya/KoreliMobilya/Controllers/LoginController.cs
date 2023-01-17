using KoreliMobilyaDeneme.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KoreliMobilyaDeneme.Controllers
{
	public class LoginController : Controller
    {

		private readonly AppDbContext _context;

		public LoginController(AppDbContext context)
		{
			_context = context;
		}

		[AllowAnonymous]
		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Index(Admin p)
		{
				var adminuserinfo= _context.Admins.FirstOrDefault(x=> x.AdminUserName==p.AdminUserName && x.AdminPassword==p.AdminPassword);
			if (adminuserinfo!=null)
			{
				var claims = new List<Claim>
				{

					new Claim(ClaimTypes.Name,p.AdminUserName)
				};

				var useridentity=new ClaimsIdentity(claims,"Login");
				ClaimsPrincipal principal= new ClaimsPrincipal(useridentity);

				await HttpContext.SignInAsync(principal);

				return RedirectToAction("index","products");

			}
			 
			
				return View();  
		
				
		}


		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		
	}
}
