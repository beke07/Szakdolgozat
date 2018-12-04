using System;
using System.Collections.Generic;
using System.Text;
using Szakdoga.Model;

namespace Szakdoga.BLL.Models
{
    public class UserHierarchyDetailsData : UserData
    {
        public Guid? BossId { get; set; }

        public string ProfilePicture { get; set; }

        public List<UserHierarchyDetailsData> Employees { get; set; }
    }
}
