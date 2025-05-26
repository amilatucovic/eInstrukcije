using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class AdminData
	{
		public static void SeedData(this EntityTypeBuilder<Admin> entity)
		{
			entity.HasData(new Admin{ ID=1, MyAppUserID = 1, Role = "Admin", CanApproveRequests = true, CanViewLogs = true});
		}
	}
}
