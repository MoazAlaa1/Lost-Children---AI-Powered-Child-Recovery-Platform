using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{
    public interface ISections
    {
        public List<AboutSections> GetAll();
        public AboutSections GetSection(int id);
        public bool Save(AboutSections AboutSections);
        public bool Delete(int id);
    }
    public class ClsSections : ISections
    {
        public ChildrenContext context;
        public ClsSections(ChildrenContext _context)
        {
            context = _context;
        }
        public List<AboutSections> GetAll()
        {
            try
            {
                return context.TbAboutSections.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<AboutSections>();
            }
        }
        public AboutSections GetSection(int id)
        {
            try
            {
                return context.TbAboutSections.Where(a => a.SectionId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new AboutSections();
            }
        }
        public bool Save(AboutSections AboutSections)
        {

            try
            {
                if (AboutSections.SectionId > 0)
                {
                    AboutSections.UpdatedDate = DateTime.Now;
                    context.Entry(AboutSections).State = EntityState.Modified;
                }
                else
                {
                    AboutSections.CurrentState = 1;
                    AboutSections.CreatedDate = DateTime.Now;
                    context.TbAboutSections.Add(AboutSections);
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
                var section = GetSection(id);
                if (section != null)
                {
                    section.CurrentState = 0;
                    context.Entry(section).State = EntityState.Modified;
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
