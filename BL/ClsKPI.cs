using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace LostChildrenGP.BL
{
    public interface IKPI
    {
        public List<KPI> GetAll();
        public KPI GetKPI(int id);
        public bool Save(KPI model);
        public bool Delete(int id);
    }
    public class ClsKPI : IKPI
    {
        public ChildrenContext context;
        public ClsKPI(ChildrenContext _context)
        {
            context = _context;
        }
        public List<KPI> GetAll()
        {
            try
            {
                return context.TbKPI.Where(a=>a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<KPI>();
            }
        }
        public KPI GetKPI(int id)
        {
            try
            {
                return context.TbKPI.Where(a => a.KpiId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new KPI();
            }
        }
        public bool Save(KPI KPI)
        {

            try
            {
                if (KPI.KpiId > 0)
                {
                    KPI.UpdatedDate = DateTime.Now;
                    context.Entry(KPI).State = EntityState.Modified;
                }
                else
                {
                    KPI.CreatedDate = DateTime.Now;
                    context.TbKPI.Add(KPI);
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
                var KPI = GetKPI(id);
                if (KPI != null)
                {
                    KPI.CurrentState = 0;
                    context.Entry(KPI).State = EntityState.Modified;
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
