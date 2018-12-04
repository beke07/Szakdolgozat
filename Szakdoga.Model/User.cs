using System;
using System.Collections.Generic;
using Szakdoga.Model.Common;
using Szakdoga.Model.ComplexTypes;
using Szakdoga.Model.JoiningClasses;

namespace Szakdoga.Model
{
    /// <summary>
    /// Egy dolgozót reprezentáló entitás
    /// </summary>
    public class User : Base<Guid>
    {
        #region Alaptulajdonságok
        
        /// <summary>
        /// Vezetéknév
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Keresztnév
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// A munkatárs e-mail címe
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Felhasználónév
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Profilkép elérési útja
        /// </summary>
        public string ProfilePicture { get; set; }

        /// <summary>
        /// A munkatárs admin-e
        /// </summary>
        public bool IsAdmin{ get; set; }

        /// <summary>
        /// A munkatárs beosztása
        /// </summary>
        public Rank Rank { get; set; }

        /// <summary>
        /// Jelszó titkosítás
        /// </summary>
        public PasswordHelper PasswordHelper { get; set; }

        /// <summary>
        /// Első bejelentkezés
        /// </summary>
        public bool FirstSignIn { get; set; }

        #endregion

        #region Munkatársak összerendelése

        /// <summary>
        /// A munkatárs főnöke
        /// </summary>
        public virtual User Boss { get; set; }

        /// <summary>
        /// Főnök azonosítója
        /// </summary>
        public Guid? BossId { get; set; }

        /// <summary>
        /// Az adott munkatárs alkalmazottai
        /// </summary>
        public virtual List<User> Employees { get; set; } = new List<User>();

        #endregion

        #region Projektekkel való összrendelés

        /// <summary>
        /// A hozzárandelt projektek
        /// </summary>
        public ICollection<ProjectWorker> Projects { get; } = new List<ProjectWorker>();

        #endregion

        #region Feladatokkal való összerendelés

        /// <summary>
        /// A hozzárendelt feladatok
        /// </summary>
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        #endregion
    }
}
