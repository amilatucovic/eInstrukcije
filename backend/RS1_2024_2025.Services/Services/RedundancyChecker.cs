using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RS1_2024_2025.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS1_2024_2025.Services.Services
{
    public class RedundancyChecker
    {
        private readonly ApplicationDbContext db;

        public RedundancyChecker(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task RemoveDuplicatesAsync()
        {
            var users = await db.MyAppUsers.ToListAsync();
            var duplicateUsers = users
            .GroupBy(u => u.Username)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g.Skip(1))
            .ToList();

            db.MyAppUsers.RemoveRange(duplicateUsers);

            await db.SaveChangesAsync();

            
        }
    }
}
