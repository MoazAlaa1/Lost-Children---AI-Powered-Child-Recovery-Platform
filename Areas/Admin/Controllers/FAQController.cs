using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class FAQController : Controller
    {
        IFAQ _oClsFAQ;
        public FAQController(IFAQ ClsFAQ)
        {
            _oClsFAQ = ClsFAQ;
        }
        public IActionResult List()
        {

            return View(_oClsFAQ.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(new FAQ());
            }
            else
            {
                var faq = _oClsFAQ.GetFAQ(Convert.ToInt32(id));
                if (faq == null)
                    // To Error 404 or not found
                    return Redirect("/Error/E404?type=Admin");
                return View(faq);
            }
        }
        public IActionResult Save(FAQ model)
        {
            
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            _oClsFAQ.Save(model);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsFAQ.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
