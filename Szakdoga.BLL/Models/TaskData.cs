using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdoga.BLL.Models
{
    public class TaskData
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid WorkerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BeingInChargeOfName { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public long WorkHours { get; set; }
    }
}
