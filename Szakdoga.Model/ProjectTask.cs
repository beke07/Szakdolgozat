using System;
using System.Collections.Generic;
using System.Text;
using Szakdoga.Model.Common;

namespace Szakdoga.Model
{
    /// <summary>
    /// Egy feladatot reprezentáló entitás
    /// </summary>
    public class ProjectTask : Base<Guid>
    {
        #region Alaptulajdonságok

        /// <summary>
        /// A feladat címe
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Eddig ráfordított idő
        /// </summary>
        public long WorkHours { get; set; }

        /// <summary>
        /// Kész van a taszk
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Leírás
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Kezdés ideje
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// Tevékenységek a taszkkal kapcsolatban
        /// </summary>
        public ICollection<TaskActivity> TaskActivities { get; set; } = new List<TaskActivity>();

        #endregion

        #region Projekttel való összerendelés

        /// <summary>
        /// Projekt azonosítója
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// Projekt, amiben a feladat szerepel
        /// </summary>
        public Project Project { get; set; }

        #endregion

        #region Munkatárssal való összrendelés

        /// <summary>
        /// Munkatrás azonosítója
        /// </summary>
        public Guid? WorkerId { get; set; }

        /// <summary>
        /// A feladaton dolgozó munakatárs
        /// </summary>
        public User Worker { get; set; }

        #endregion
    }
}
