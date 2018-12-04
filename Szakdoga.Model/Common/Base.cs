using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Szakdoga.Model.Common
{
    public class Base<T>
    {
        /// <summary>
        /// Egyedi azonosító
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public T Id { get; set; }

        /// <summary>
        /// Létrehozás dátuma
        /// </summary>
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;
    }
}
