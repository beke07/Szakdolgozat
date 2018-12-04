using System;
using System.Collections.Generic;
using System.Text;
using Szakdoga.Model.Common;

namespace Szakdoga.Model.JoiningClasses
{
    public class ProjectWorker : Base<Guid>
    {
        public Guid WorkerId { get; set; }
        public User Worker { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
