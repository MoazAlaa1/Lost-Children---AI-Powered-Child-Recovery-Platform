using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{
    public interface IResultChildren
    {
        public List<ResultChildren> GetAll();
        public ResultChildren GetResultChildren(int id);
        public bool Save(List<VmSearchResultChildren> ResultChildren, int ResultId);
        public bool Delete(int id);
    }
    public class ClsResultChildren : IResultChildren
    {
        public ChildrenContext context;
        public ClsResultChildren(ChildrenContext _context)
        {
            context = _context;
        }
        public List<ResultChildren> GetAll()
        {
            try
            {
                return context.TbResultChildren.Where(a=>a.CurrentState==1).ToList();
            }
            catch
            {
                return new List<ResultChildren>();
            }
        }
        public ResultChildren GetResultChildren(int id)
        {
            try
            {
                return context.TbResultChildren.Where(a => a.ResultChildrenId == id).FirstOrDefault();
            }
            catch
            {
                return new ResultChildren();
            }
        }
        public bool Save(List<VmSearchResultChildren> ResultChildren, int ResultId)
        {

            try
            {
                
                
                foreach (var child in ResultChildren)
                {
                    ResultChildren children = new ResultChildren()
                    {
                        CurrentState = 1,
                        FoundChildId = child.Child.FoundChildId,
                        SearchResultId = ResultId,
                        Similarity = (int)child.Similarity

                    };
                    context.TbResultChildren.Add(children);
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var ResultChildren = GetResultChildren(id);
                if (ResultChildren != null)
                {
                    context.TbResultChildren.Remove(ResultChildren);
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
