using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{
    public interface IWorkSteps
    {
        public List<WorkSteps> GetAll();
        public WorkSteps GetWorkStep(int id);
        public bool Save(WorkSteps WorkSteps);
        public bool Delete(int id);
    }
    public class ClsWorkSteps : IWorkSteps
    {
        public ChildrenContext context;
        public ClsWorkSteps(ChildrenContext _context)
        {
            context = _context;
        }
        public List<WorkSteps> GetAll()
        {
            try
            {
                return context.TbWorkSteps.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<WorkSteps>();
            }
        }
        public WorkSteps GetWorkStep(int id)
        {
            try
            {
                return context.TbWorkSteps.Where(a => a.WorkStepsId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new WorkSteps();
            }
        }
        public bool Save(WorkSteps WorkSteps)
        {

            try
            {
                if (WorkSteps.WorkStepsId > 0)
                {
                    WorkSteps.UpdatedDate = DateTime.Now;
                    context.Entry(WorkSteps).State = EntityState.Modified;
                }
                else
                {
                    WorkSteps.CurrentState = 1;
                    WorkSteps.CreatedDate = DateTime.Now;
                    context.TbWorkSteps.Add(WorkSteps);
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
                var step = GetWorkStep(id);
                if (step != null)
                {
                    step.CurrentState = 0;
                    context.Entry(step).State = EntityState.Modified;
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
