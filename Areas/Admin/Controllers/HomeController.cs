using FoundChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        IFoundChildren _oClsFoundedChildren;
        public HomeController(IFoundChildren FoundedChildren)
        {
            _oClsFoundedChildren = FoundedChildren;
        }
        public async Task<IActionResult> Index()
        
        
        
        {
            var result = await _oClsFoundedChildren.GetDashboardDataAsync();
            return View(result);
        }
        

    }
}
