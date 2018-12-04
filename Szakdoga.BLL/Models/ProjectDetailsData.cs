using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdoga.BLL.Models
{
    public class ProjectDetailsData : ProjectData
    {
        public List<UserData> WorkersData { get; set; }

        public long SumTaskHours { get; set; }
    }
}
