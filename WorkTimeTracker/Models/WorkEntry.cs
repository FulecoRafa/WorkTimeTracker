namespace Domain
{
    public class WorkEntry
    {
        public int Id { get; set; }
        public DateTime timestamp { get; set; }

        public override string ToString()
        {
            return timestamp.ToString();
        }
    }


}
