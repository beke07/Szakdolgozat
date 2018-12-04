using System;

namespace Szakdoga.BLL.Models
{
    public class ActivityData
    {
        public Guid TaskId { get; set; }

        public Guid ActivityId { get; set; }

        public long Hours { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Description { get; set; }

        public string ActivityName { get; set; }
    }
}