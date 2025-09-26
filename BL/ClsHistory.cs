using LostChildrenGP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LostChildrenGP.BL
{
    public interface IHistory
    {
        public List<ViewHistory> GetAll();
        public List<VwHistoryDetails> GetDetails(int id);
        //public bool Save(FAQ model);
        //public bool Delete(int id);
    }
    public class ClsHistory : IHistory
    {
        public ChildrenContext context;
        UserManager<ApplicationUser> userManager;
        public ClsHistory(ChildrenContext _context)
        {
            context = _context;
        }
        public List<ViewHistory> GetAll()
        {
            try
            {
                return context.ViewHistorys.Where(a=>a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<ViewHistory>();
            }
        }
        public List<VwHistoryDetails> GetDetails(int id)
        {
            try
            {
                return context.VwHistoryDetails.Where(a => a.SearchResultId == id).ToList();
            }
            catch 
            {
                return new List<VwHistoryDetails>();
            }
        }



    }
}
