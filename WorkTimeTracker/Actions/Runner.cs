using ConsoleTables;
using Domain;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Actions;
class Runner
{
    public static int Show(int workHoursPerDay, DateOnly date)
    {
        TimeSpan remainingTime = TimeSpan.FromHours(workHoursPerDay);

        Console.WriteLine("Showing entries for date: " + date.ToString());

        List<WorkEntry> entries;

        using (var db = new WorkEntryContext())
        {
            var startTime = date.ToDateTime(new TimeOnly(0));
            var endTime = date.AddDays(1).ToDateTime(new TimeOnly(0));
            var query = from we in db.WorkEntries
                        where we.timestamp >= startTime && we.timestamp < endTime
                        select we;

            entries = query.ToList();
        }

        var outputTable = new ConsoleTable("Started at", "Stopped at", "Worked Time", "Remaining Working Time");

        if (entries.Count % 2 != 0)
        {
            entries.Add(new WorkEntry
            {
                Id = 0,
                timestamp = DateTime.Now
            });
        }

        var windowedEntries = entries
            .Select((entry, index) => new { entry, index })
            .GroupBy(x => x.index / 2);

        foreach (var entry in windowedEntries)
        {
            var start = entry.First().entry.timestamp;
            var end = entry.Last().entry.timestamp;
            var workedTime = end - start;
            remainingTime -= workedTime;
            outputTable.AddRow(start, end, workedTime.ToString("hh\\:mm"), remainingTime.ToString("hh\\:mm"));
        }

        outputTable.Write();


        return 0;
    }

    public static int Add()
    {
        using (var db = new WorkEntryContext())
        {
            db.WorkEntries.Add(new WorkEntry { timestamp = DateTime.Now });
            db.SaveChanges();
        }

        return 0;
    }
}
