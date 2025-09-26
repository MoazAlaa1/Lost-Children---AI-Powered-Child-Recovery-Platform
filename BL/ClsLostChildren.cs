using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{
    public interface ILostChildren
    {
        public List<LostChild> GetAll();
        public LostChild GetLostChild(int id);
        public bool Save(LostChild LostChild, string userId);
        public bool Save(LostChild LostChild, string userId, List<VmSearchResultChildren> lstSearchResultChildren);
        public bool Delete(int id);
    }
    public class ClsLostChildren : ILostChildren
    {
        public ChildrenContext context;
        ISearchResult _oClsSearchResult;
        public ClsLostChildren(ChildrenContext _context, ISearchResult oClsSearchResult)
        {
            context = _context;
            _oClsSearchResult = oClsSearchResult;
        }
        public List<LostChild> GetAll()
        {
            try
            {
                return context.TbLostChild.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<LostChild>();
            }
        }
        public LostChild GetLostChild(int id)
        {
            try
            {
                return context.TbLostChild.Where(a => a.LostChildrenId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new LostChild();
            }
        }
        public bool Save(LostChild LostChild , string userId)
        {

            try
            {
                if (LostChild.LostChildrenId > 0)
                {
                    LostChild.UpdatedBy = userId;
                    LostChild.UpdatedDate = DateTime.Now;
                    context.Entry(LostChild).State = EntityState.Modified;
                }
                else
                {
                    LostChild.UserId = userId;
                    LostChild.CurrentState = 1;
                    LostChild.CreatedDate = DateTime.Now;
                    context.TbLostChild.Add(LostChild);
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Save(LostChild LostChild , string userId,List<VmSearchResultChildren> resultChildren)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (LostChild.LostChildrenId > 0)
                {
                    LostChild.UpdatedBy = userId;
                    LostChild.UpdatedDate = DateTime.Now;
                    context.Entry(LostChild).State = EntityState.Modified;
                }
                else
                {
                    LostChild.UserId = userId;
                    LostChild.CurrentState = 1;
                    LostChild.CreatedDate = DateTime.Now;
                    context.TbLostChild.Add(LostChild);
                }
                context.SaveChanges();
                _oClsSearchResult.Save(resultChildren,userId, LostChild.LostChildrenId);
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var lostChild = GetLostChild(id);
                if (lostChild != null)
                {
                    lostChild.CurrentState = 0;
                    context.Entry(lostChild).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
    }

}
