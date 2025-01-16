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

            var students = await db.Students.ToListAsync();
            var duplicateStudents = students
            .GroupBy(s => s.MyAppUser.Username)
            .Where(s => s.Count() > 1)
            .SelectMany(s => s.Skip(1))
            .ToList();

            db.Students.RemoveRange(duplicateStudents);

            var tutors = await db.Tutors.ToListAsync();
            var duplicateTutors = tutors
                .GroupBy(t => t.MyAppUser.Username)
                .Where(t => t.Count() > 1)
                .SelectMany(t => t.Skip(1))
                .ToList();

            db.Tutors.RemoveRange(duplicateTutors);

            var subjects = await db.Subjects.ToListAsync();
            var duplicateSubjects = subjects
                .GroupBy(s => s.Name)
                .Where(s => s.Count() > 1)
                .SelectMany(s => s.Skip(1))
                .ToList();

            db.Subjects.RemoveRange(duplicateSubjects);

            var cities = await db.Cities.ToListAsync();
            var duplicateCities = cities
                .GroupBy(c => c.Name)
                .Where(c => c.Count() > 1)
                .SelectMany(c => c.Skip(1))
                .ToList();


            db.Cities.RemoveRange(duplicateCities);

            var admins = await db.Admins.ToListAsync();
            var duplicateAdmins = admins
                .GroupBy(t => t.MyAppUser.Username)
                .Where(t => t.Count() > 1)
                .SelectMany(t => t.Skip(1))
                .ToList();

            db.Admins.RemoveRange(duplicateAdmins);

            await db.SaveChangesAsync();
        }
    }
}