using LostChildrenGP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Controllers
{
    public class UserController : Controller
    {
         UserManager<ApplicationUser> _userManager;
         SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = user;
            _signInManager = signInManager;
        }
        public IActionResult LoginForm(string returnUrl)
        {
            var model = new Users()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        public IActionResult RegisterForm(string returnUrl)
        {
            var model = new Users()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users userModel)
        {
            ModelState.Remove("ReturnUrl");
            if (!ModelState.IsValid)
            {
                return View("RegisterForm", userModel);
            }
            ApplicationUser user = new ApplicationUser()
            {
               FirstName = userModel.FirstName,
               LastName = userModel.LastName,
               Email = userModel.Email,
               UserName = userModel.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    var MyUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(MyUser, "Customer");
                    var loginResult = await _signInManager.PasswordSignInAsync(user.Email, userModel.Password, true, true);
                    if (string.IsNullOrEmpty(userModel.ReturnUrl))
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        return Redirect(userModel.ReturnUrl);
                    }
                }
                else
                {
                    return Redirect("/User/RegisterForm");
                }
            }
            catch (Exception)
            {
                return Redirect("/Error/E500");
            }
            
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users userModel )
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("ReturnUrl");
            if (!ModelState.IsValid)
            {
                return View("LoginForm", userModel);
            }
            ApplicationUser user = new ApplicationUser()
            {
               
               Email = userModel.Email,
               UserName = userModel.Email
            };
            try
            {
              
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, userModel.Password, true, true);
                if (loginResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(userModel.ReturnUrl))
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        return Redirect(userModel.ReturnUrl);
                    }
                }
                else
                {
                    return Redirect("/User/LoginForm");
                }
                
            }
            catch (Exception)
            {
                return Redirect("/Error/E500");
            }
            
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
