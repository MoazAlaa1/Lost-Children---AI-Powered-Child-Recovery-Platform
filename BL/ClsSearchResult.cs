using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{
    public interface ISearchResult
    {
        public List<SearchResult> GetAll();
        public SearchResult GetSearchResult(int id);
        public bool Save(List<VmSearchResultChildren> ResultChildren, string userId, int lostChildId);
        public bool Delete(int id);
    }
    public class ClsSearchResult : ISearchResult
    {
        public ChildrenContext context;
        IResultChildren _oClschildren;
        public ClsSearchResult(ChildrenContext _context, IResultChildren children)
        {
            context = _context;
            _oClschildren = children;
        }
        public List<SearchResult> GetAll()
        {
            try
            {
                return context.TbSearchResult.Where(a=>a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<SearchResult>();
            }
        }
        public SearchResult GetSearchResult(int id)
        {
            try
            {
                return context.TbSearchResult.Where(a => a.ResultId == id).FirstOrDefault();
            }
            catch
            {
                return new SearchResult();
            }
        }
        public bool Save(List<VmSearchResultChildren> ResultChildren,string userId, int lostChildId)
        {

            try
            {
                SearchResult searchResult = new SearchResult();
                searchResult.LostChildId = lostChildId;
                searchResult.UserId = userId;
                searchResult.CurrentState = 1;
                searchResult.CreatedDate = DateTime.Now;
                searchResult.UpdatedDate = DateTime.Now;


                context.TbSearchResult.Add(searchResult);
                context.SaveChanges();
                _oClschildren.Save(ResultChildren, searchResult.ResultId);
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
                var SearchResult = GetSearchResult(id);
                if (SearchResult != null)
                {
                    context.TbSearchResult.Remove(SearchResult);
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
