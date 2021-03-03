using System;
using Assessment.Models;

namespace Assessment.Domains.Timesheets
{
    public class Activity : IAuditable
    {
        public long Id { get; set; }
        public long ActivityGroupId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public DurationType DurationType { get; set; }
        public string Info { get; set; }
        public long ActivityTypeId { get; set; }
        public int Frequency { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdatedBy { get; set; }
    }
}
