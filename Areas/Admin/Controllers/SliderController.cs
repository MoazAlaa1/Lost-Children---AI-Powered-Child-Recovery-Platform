using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        ISlider _oClsSlider;
        private readonly CloudinaryService _cloudinaryService;
        public SliderController(ISlider ClsSlider, CloudinaryService cloudinaryService)
        {
            _oClsSlider = ClsSlider;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult Edit()
        {
            var model = _oClsSlider.GetSlider() ?? new Slider();
            return View(model);
        }
        public async Task<IActionResult> Save(Slider model, IFormFile File)
        {
            if (File != null && File.FileName != "")
            {
                var arr = File.FileName.ToLower().Split(".");
                if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
                {
                    ModelState.Remove("SliderImage");
                }
                else
                {
                    ModelState.AddModelError("SliderImage", "Only PNG, JPG, and JPEG images are allowed");
                }

            }
            ModelState.Remove("File");
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            model.SliderImage = await _cloudinaryService.UploadImageAsync(File, "LostChildrenAssets");
            _oClsSlider.Save(model);
            return Redirect("/Admin/Home/Index");
        }
    }
}
