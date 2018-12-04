using System;

namespace Szakdoga.BLL.Models
{
    public class UserData
    {
        /// <summary>
        /// Egyedi azonosító
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Vezetéknév
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Keresztnév
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Felhasználónév
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Jelszó
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Adminisztrátor jogokkal rendelkezik-e
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Beosztás
        /// </summary>
        public Guid RankId { get; set; }

        /// <summary>
        /// Beosztás neve
        /// </summary>
        public string RankName{ get; set; }
    }
}
