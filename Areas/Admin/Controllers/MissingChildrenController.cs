using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MissingChildrenController : Controller
    {
        ILostChildren _oClsLostChildren;
        private readonly CloudinaryService _cloudinaryService;
        UserManager<ApplicationUser> _userManager;
        public MissingChildrenController(ILostChildren LostChildren, CloudinaryService cloudinaryService,
            UserManager<ApplicationUser> userManager)
        {
            _oClsLostChildren = LostChildren;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
        }
        public IActionResult List()
        {
            
            return View(_oClsLostChildren.GetAll());
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(new LostChild());
            }
            else
            {
                var lostChild = _oClsLostChildren.GetLostChild(Convert.ToInt32(id));
                if (lostChild == null)
                    // To Error 404 or not found
                    return RedirectToAction("List");
                return View(lostChild);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(LostChild model , IFormFile File)
        {
            if (File != null && File.FileName != "")
            {
                var arr = File.FileName.ToLower().Split(".");
                if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
                {
                    ModelState.Remove("LostChildrenImage");
                }
                else
                {
                    ModelState.AddModelError("LostChildrenImage", "Only PNG, JPG, and JPEG images are allowed");
                }
            }
            
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            model.LC_Embedding = "Test";
            model.LostChildrenImage = await _cloudinaryService.UploadImageAsync(File, "LostChildren");
            var user = await _userManager.GetUserAsync(User);
            _oClsLostChildren.Save(model,user.Id);  
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsLostChildren.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
