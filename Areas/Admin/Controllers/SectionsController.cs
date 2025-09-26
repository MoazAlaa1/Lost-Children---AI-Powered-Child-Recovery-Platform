using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SectionsController : Controller
    {
        ISections _oClsSections;
        private readonly CloudinaryService _cloudinaryService;
        public SectionsController(ISections section, CloudinaryService cloudinaryService)
        {
            _oClsSections = section;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult List()
        {
            
            return View(_oClsSections.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(new AboutSections());
            }
            else
            {
                var section = _oClsSections.GetSection(Convert.ToInt32(id));
                if (section == null)
                    // To Error 404 or not found
                    return RedirectToAction("List");
                return View(section);
            }
        }
        public async Task<IActionResult> Save(AboutSections model, IFormFile File)
        {
            if (File != null && File.FileName != "")
            {
                var arr = File.FileName.ToLower().Split(".");
                if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
                {
                    ModelState.Remove("SectionImage");
                }
                else
                {
                    ModelState.AddModelError("SectionImage", "Only PNG, JPG, and JPEG images are allowed");
                }
                
            }
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            model.SectionImage = await _cloudinaryService.UploadImageAsync(File, "LostChildrenAssets");
            _oClsSections.Save(model);
            return RedirectToAction("List");
        } 
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsSections.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
