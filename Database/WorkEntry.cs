using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;

namespace Domain
{
    public class WorkEntry
    {
        public int Id { get; set; }
        public DateTime timestamp { get; set; }

        //public WorkEntry[] GetEntriesByDate(Database database, DateOnly date)
        //{
        //    var query = database.CreateQuery();
        //    query.CommandText = "SELECT * FROM work_entries WHERE timestamp > @start AND timestamp < @end";
        //    query.Parameters.AddWithValue("@start", date.ToDateTime(new TimeOnly(0)));
        //    query.Parameters.AddWithValue("@end", date.AddDays(1).ToDateTime(new TimeOnly(0)));
        //    var reader = query.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        Console.WriteLine(reader["timestamp"]);
        //    }
        //}
    }

    public class WorkEntryContext : DbContext
    {
        public DbSet<WorkEntry> WorkEntries { get; set; }
    }
}
