using System.Diagnostics;
using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Controllers
{
    public class HomeController : Controller
    {
        ISections _oClsSections;
        ISlider _oClsSlider;
        IKPI _oClsKPI;
        IWorkSteps _oClsWorkSteps;
        IFAQ _oClsFAQ;
        ILostChildren _oClsLostChildren;
        public HomeController(ISections sections, ISlider slider, IKPI kPI, IWorkSteps workSteps, IFAQ fAQ, ILostChildren lostChildren)
        {
            _oClsSections = sections;
            _oClsSlider = slider;
            _oClsKPI = kPI;
            _oClsWorkSteps = workSteps;
            _oClsFAQ = fAQ;
            _oClsLostChildren = lostChildren;
        }
        public IActionResult Index()
        {
            VmHomePage vm = new VmHomePage();
            vm.Slider = _oClsSlider.GetSlider();
            vm.Kpis = _oClsKPI.GetAll();
            return View(vm);
        } 
        public IActionResult LostChildren()
        {
            
            return View(_oClsLostChildren.GetAll());
        } 
        public IActionResult ChildDetails(int id)
        {
            return View(_oClsLostChildren.GetLostChild(id)??new LostChild());
        } 
        public IActionResult SuccessPage()
        {
            return View();
        } 
        public IActionResult About()
        {
            VmAboutPage vm = new VmAboutPage();
            vm.Sections = _oClsSections.GetAll();
            vm.WorkSteps =  _oClsWorkSteps.GetAll();
            vm.FAQs = _oClsFAQ.GetAll();
            return View(vm);
        }

        
    }
}
