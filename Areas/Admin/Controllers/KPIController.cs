using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KPIController : Controller
    {
        IKPI _oClsKPI;
        public KPIController(IKPI ClsLPI)
        {
            _oClsKPI = ClsLPI;
        }
        public IActionResult List()
        {

            return View(_oClsKPI.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(new KPI());
            }
            else
            {
                var kpi = _oClsKPI.GetKPI(Convert.ToInt32(id));
                if (kpi == null)
                    // To Error 404 or not found
                    return RedirectToAction("List");
                return View(kpi);
            }
        }
        public IActionResult Save(KPI model)
        {
            
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            _oClsKPI.Save(model);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsKPI.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
