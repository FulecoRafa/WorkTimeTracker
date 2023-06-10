using System.Data;
using System.Data.SQLite;

namespace WorkTimeTracker.Domain
{
    public class Database
    {
        private SQLiteConnection _connection;

        public Database(string filepath)
        {
            createConnection(filepath);
        }

        private void createConnection(string filepath)
        {
            if (!File.Exists(filepath))
            {
                SQLiteConnection.CreateFile(filepath);
            }
            _connection = new SQLiteConnection($"Data Source={filepath};Version=3;Compress=True;");
            _connection.Open();
        }

        public SQLiteCommand CreateQuery()
        {
            return _connection.CreateCommand();
        }
    }
}