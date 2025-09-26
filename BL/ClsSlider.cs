using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.BL
{
    public interface ISlider
    {
        public Slider GetSlider();
        public bool Save(Slider Slider);
    }
    public class ClsSlider : ISlider
    {
        public ChildrenContext context;
        public ClsSlider(ChildrenContext _context)
        {
            context = _context;
        }
        
        public Slider GetSlider()
        {
            try
            {
                return context.TbSilder.FirstOrDefault();
            }
            catch
            {
                return new Slider();
            }
        }
        public bool Save(Slider Slider)
        {

            try
            {
                if (Slider.SliderId > 0)
                {
                    Slider.UpdatedDate = DateTime.Now;
                    context.Entry(Slider).State = EntityState.Modified;
                }
                else
                {
                    Slider.CurrentState = 1;
                    Slider.CreatedDate = DateTime.Now;
                    context.TbSilder.Add(Slider);
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
