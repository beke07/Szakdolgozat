using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdoga.BLL.Models
{
    public class TaskDetailsData : TaskData
    {
        public List<ActivityData> Activities { get; set; } = new List<ActivityData>();
    }
}
