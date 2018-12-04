using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdoga.BLL.Models
{
    public class PasswordChangeHelper
    {
        /// <summary>
        /// Munkatárs azonosítója
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Régi jelszó
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Új jelszó
        /// </summary>
        public string NewPassword { get; set; }
    }
}
