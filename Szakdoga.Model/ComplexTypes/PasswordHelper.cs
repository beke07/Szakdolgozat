using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Szakdoga.Model.ComplexTypes
{
    [Owned]
    public class PasswordHelper
    {
        /// <summary>
        /// Jelszó, titkosítva
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Jelszó titkosításához szükséges "salt"
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}
