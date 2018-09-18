using System;

namespace safari_vacation.Models
{
    public class SeenAnimals
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public int CountOfTimeSeen { get; set; }
        public string LocationOfLastSeen { get; set; }
        public DateTime LastSeenTime { get; set; }
    }
}