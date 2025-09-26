using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostChildrenGP.Controllers
{
    public class HistoryController : Controller
    {
        ISearchResult _oClsSearchResult;
        IHistory _oClsHistory;
        UserManager<ApplicationUser> _UserManager;
        public HistoryController(ISearchResult ClsSearchResult, IHistory oClsHistory,
            UserManager<ApplicationUser> UserManager)
        {
            _oClsSearchResult = ClsSearchResult;
            _oClsHistory = oClsHistory;
            _UserManager = UserManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _UserManager.GetUserAsync(User);
            var History = _oClsHistory.GetAll().Where(a=>a.UserId == user.Id).ToList();
            if (History.Count == 0)
                return View(new List<ViewHistory>());
            return View(History);
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = _oClsSearchResult.Delete(id);
            if (isDeleted == false)
                return Redirect("/Error/E500?type=Admin");
            return Redirect("/History/Index");
        }
        public IActionResult HistoryDetails(int id)
        {
            var lstDetails = _oClsHistory.GetDetails(id);
            return View(lstDetails);
        }
    }
}
