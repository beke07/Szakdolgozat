using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.DAL;
using Szakdoga.Model;

namespace Szakdoga.BLL.Services
{
    public class RankService : IRankService
    {
        private readonly ApplicationDbContext DbContext;

        public RankService(ApplicationDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public async Task CreateRankAsync(string name)
        {
            DbContext.Ranks.Add(new Rank() { Name = name });
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Rank rankTodelete = DbContext.Ranks.Find(id);
            if (rankTodelete != null)
            {
                DbContext.Ranks.Remove(rankTodelete);
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteRankByIdAsync(Guid id)
        {
            DbContext.Ranks.Remove(await DbContext.Ranks.FirstAsync(e => e.Id == id));
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Rank>> GetRanksAsync()
        {
            return await DbContext.Ranks.ToListAsync();
        }
    }
}
