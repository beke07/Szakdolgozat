using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdoga.BLL.Models
{
    public class UserHierarchyData
    {
        public Guid Id { get; set; }

        public Guid BossId { get; set; }

        public List<Guid> EmployeeIds { get; set; }
    }
}
