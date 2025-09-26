using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorKStepsController : Controller
    {
        IWorkSteps _oClsWorkStep;
        private readonly CloudinaryService _cloudinaryService;
        public WorKStepsController(IWorkSteps ClsWorkStep, CloudinaryService cloudinaryService)
        {
            _oClsWorkStep = ClsWorkStep;
            _cloudinaryService = cloudinaryService;
        }
        public IActionResult List()
        {

            return View(_oClsWorkStep.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(new WorkSteps());
            }
            else
            {
                var faq = _oClsWorkStep.GetWorkStep(Convert.ToInt32(id));
                if (faq == null)
                    // To Error 404 or not found
                    return RedirectToAction("List");
                return View(faq);
            }
        }
        public async Task<IActionResult> Save(WorkSteps model, IFormFile File)
        {
            if (File != null && File.FileName != "")
            {
                var arr = File.FileName.ToLower().Split(".");
                if (arr[arr.Length - 1] == "png" || arr[arr.Length - 1] == "jpg" || arr[arr.Length - 1] == "jpeg")
                {
                    ModelState.Remove("StepIcon");
                }
                else
                {
                    ModelState.AddModelError("StepIcon", "Only PNG, JPG, and JPEG images are allowed");
                }
            }
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            model.StepIcon = await _cloudinaryService.UploadImageAsync(File, "LostChildrenAssets");
            _oClsWorkStep.Save(model);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsWorkStep.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
