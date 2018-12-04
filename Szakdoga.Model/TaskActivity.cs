using System;
using Szakdoga.Model.Common;

namespace Szakdoga.Model
{
    public class TaskActivity : Base<Guid>
    {
        #region Alapadatok

        /// <summary>
        /// A taszk, amelyikhez tartozik
        /// </summary>
        public Guid TaskId { get; set; }
        public ProjectTask Task { get; set; }

        /// <summary>
        /// Ráforditott idő
        /// </summary>
        public long Hours { get; set; }

        /// <summary>
        /// Időpont
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Megjegyzés
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tevékenyésg típusa
        /// </summary>
        public Activity Activity { get; set; }

        #endregion
    }
}