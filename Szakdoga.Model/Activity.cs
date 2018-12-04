using System;
using System.Collections.Generic;
using System.Text;
using Szakdoga.Model.Common;

namespace Szakdoga.Model
{
    public class Activity : Base<Guid>
    {
        public string Name { get; set; }
    }
}
