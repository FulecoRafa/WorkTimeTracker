using Domain;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace WorkTimeTracker.Models
{
    public partial class WorkEntryContext : DbContext
    {
        public DbSet<WorkEntry> WorkEntries { get; set; }

        private string dbFile;

        public WorkEntryContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            dbFile = System.IO.Path.Join(path, "worktime.db");
            Console.WriteLine($"Database file: {dbFile}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source=" + dbFile);
        }
    }

}
