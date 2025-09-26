using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{

        public interface IFAQ
        {
            public List<FAQ> GetAll();
            public FAQ GetFAQ(int id);
            public bool Save(FAQ model);
            public bool Delete(int id);
        }
        public class ClsFAQ : IFAQ
        {
            public ChildrenContext context;
            public ClsFAQ(ChildrenContext _context)
            {
                context = _context;
            }
            public List<FAQ> GetAll()
            {
                try
                {
                    return context.TbFAQ.ToList();
                }
                catch
                {
                    return new List<FAQ>();
                }
            }
            public FAQ GetFAQ(int id)
            {
                try
                {
                    return context.TbFAQ.Where(a => a.FAQId == id).FirstOrDefault();
                }
                catch
                {
                    return new FAQ();
                }
            }
            public bool Save(FAQ FAQ)
            {

                try
                {
                    if (FAQ.FAQId > 0)
                    {
                        FAQ.UpdatedDate = DateTime.Now;
                        context.Entry(FAQ).State = EntityState.Modified;
                    }
                    else
                    {
                        FAQ.CreatedDate = DateTime.Now;
                        context.TbFAQ.Add(FAQ);
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
                    var FAQ = GetFAQ(id);
                    if (FAQ != null)
                    {
                        context.TbFAQ.Remove(FAQ);
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
