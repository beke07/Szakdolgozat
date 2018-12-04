using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Szakdoga.Model.Common;

namespace Szakdoga.Model
{
    public class Rank : Base<Guid>
    {
        /// <summary>
        /// Beosztás elnevezése
        /// </summary>
        public string Name { get; set; }
    }
}
