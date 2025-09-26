using LostChildrenGP.Models;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Data;

namespace FoundChildrenGP.BL
{
    public interface IFoundChildren
    {
        public List<FoundChild> GetAll();
        public FoundChild GetFoundChild(int id);
        public List<FoundChild> GetAllSpaicific(string gender, int birthYear);
        public bool Save(FoundChild FoundChild, string userId);
        public bool Delete(int id);
        public Task<DashboardResult> GetDashboardDataAsync();
    }
    public class ClsFoundChildren : IFoundChildren
    {
        public ChildrenContext context;
        public ClsFoundChildren(ChildrenContext _context)
        {
            context = _context;
        }
        public List<FoundChild> GetAll()
        {
            try
            {
                return context.TbFoundChild.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<FoundChild>();
            }
        }
        public List<FoundChild> GetAllSpaicific(string gender, int birthYear)
        {
            try
            {
                //int minLimit = birthYear - 3;
                //int maxLimit = birthYear + 3;
                return context.TbFoundChild.Where(a => a.CurrentState == 1 && a.Gender==gender && (a.BirthYear >= (birthYear - 3)) && (a.BirthYear < (birthYear + 3))).ToList();
            }
            catch
            {
                return new List<FoundChild>();
            }
        }
        public FoundChild GetFoundChild(int id)
        {
            try
            {
                return context.TbFoundChild.Where(a => a.FoundChildId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new FoundChild();
            }
        }
        public bool Save(FoundChild FoundChild,string userId)
        {

            try
            {
                if (FoundChild.FoundChildId > 0)
                {
                    FoundChild.UpdatedBy = userId;
                    FoundChild.UpdatedDate = DateTime.Now;
                    context.Entry(FoundChild).State = EntityState.Modified;
                }
                else
                {
                    FoundChild.UserId = userId;
                    FoundChild.CurrentState = 1;
                    FoundChild.CreatedDate = DateTime.Now;
                    FoundChild.BirthYear = FoundChild.FoundDate.Year - FoundChild.EstimatedAge;
                    context.TbFoundChild.Add(FoundChild);
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
                var FoundChild = GetFoundChild(id);
                if (FoundChild != null)
                {
                    FoundChild.CurrentState = 0;
                    context.Entry(FoundChild).State = EntityState.Modified;
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
        public async Task<DashboardResult> GetDashboardDataAsync()
        {
            var result = new DashboardResult();

            // Execute the stored procedure
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SpDashboard";
            command.CommandType = CommandType.StoredProcedure;

            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            // Read first result set (statistics)
            if (await reader.ReadAsync())
            {
                result.Statistics = new DashboardStatistics
                {
                    FoundChildren = reader.GetInt32(reader.GetOrdinal("FoundChildren")),
                    LostChildren = reader.GetInt32(reader.GetOrdinal("LostChildren")),
                    Users = reader.GetInt32(reader.GetOrdinal("Users"))
                };
            }

            // Move to next result set (found children)
            await reader.NextResultAsync();
            int counter = 0;
            result.RecentFoundChildren = new List<FoundChildInfo>();
            while (await reader.ReadAsync() && counter < 4)
            {
                result.RecentFoundChildren.Add(new FoundChildInfo
                {
                    FoundChildId = reader.GetInt32(reader.GetOrdinal("FoundChildId")),
                    FoundChildImage = await reader.IsDBNullAsync(reader.GetOrdinal("FoundChildImage"))
                        ? null : reader.GetString(reader.GetOrdinal("FoundChildImage")),
                    FoundChildName = reader.GetString(reader.GetOrdinal("FoundChildName")),
                    EstimatedAge = reader.GetInt32(reader.GetOrdinal("EstimatedAge")),
                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                    CurrentCondition = reader.GetString(reader.GetOrdinal("CurrentCondition")),
                    FoundDate = reader.GetDateTime(reader.GetOrdinal("FoundDate"))
                });
                counter++;
            }

            return result;
        }
    }

}
