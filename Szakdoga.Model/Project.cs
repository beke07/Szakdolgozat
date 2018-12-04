using System;
using System.Collections.Generic;
using Szakdoga.Model.Common;
using Szakdoga.Model.JoiningClasses;

namespace Szakdoga.Model
{
    /// <summary>
    /// Egy projektet reprezentáló entitás
    /// </summary>
    public class Project : Base<Guid>
    {
        #region Alaptulajdonságok

        /// <summary>
        /// Projekt neve
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Kezdeti dátum
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// A projekt vezetője
        /// </summary>
        public Guid ProjectLeaderId { get; set; }

        /// <summary>
        /// Kész van- e a projekt
        /// </summary>
        public bool isDone { get; set; } = false;

        /// <summary>
        /// Projekt határideje
        /// </summary>
        public DateTimeOffset Deadline { get; set; }

        /// <summary>
        /// Szükséges órák száma
        /// </summary>
        public long SumWorkHours { get; set; }

        #endregion

        #region Munkatársakkal való összerendelés

        /// <summary>
        /// Munkatársak, akik ezen a projekten dolgoznak
        /// </summary>
        public ICollection<ProjectWorker> Workers { get; } = new List<ProjectWorker>();

        #endregion

        #region Feladatokkal való összerendelés

        /// <summary>
        /// Projekt feladatai
        /// </summary>
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        #endregion
    }
}
