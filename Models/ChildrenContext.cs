using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LostChildrenGP.Models
{
    public class ChildrenContext : IdentityDbContext<ApplicationUser>
    {
        public ChildrenContext()
        {
            
        }
        public ChildrenContext(DbContextOptions<ChildrenContext> options)
            : base(options)
        {
        }
        public virtual DbSet<LostChild> TbLostChild { get; set; }
        public virtual DbSet<FoundChild> TbFoundChild { get; set; }
        public virtual DbSet<SearchResult> TbSearchResult { get; set; }
        public virtual DbSet<ResultChildren> TbResultChildren { get; set; }
        public virtual DbSet<Slider> TbSilder { get; set; }
        public virtual DbSet<KPI> TbKPI { get; set; }
        public virtual DbSet<AboutSections> TbAboutSections { get; set; }
        public virtual DbSet<WorkSteps> TbWorkSteps { get; set; }
        public virtual DbSet<FAQ> TbFAQ { get; set; }
        public virtual DbSet<VwHistory> VwHistorys { get; set; }
        public virtual DbSet<ViewHistory> ViewHistorys { get; set; }
        public virtual DbSet<VwHistoryDetails> VwHistoryDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("DateTime");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SearchResult>(entity =>
            {
                entity.HasOne(a => a.TblostChild).WithMany(b => b.TbSearcResult).HasForeignKey(a => a.LostChildId);
            });
            modelBuilder.Entity<VwHistory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwHistorys");
            });
            modelBuilder.Entity<ViewHistory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewHistorys");
            });
            modelBuilder.Entity<VwHistoryDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwHistoryDetails");
            });
        }
    }
}
