using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdoga.BLL.Models
{
    public class ProjectData
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public long SumWorkHours { get; set; }

        public Guid ProjectLeaderId { get; set; }

        public List<Guid> Workers { get; set; }
    }
}
