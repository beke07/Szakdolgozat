using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;
using Szakdoga.Model;

namespace Szakdoga.BLL.ServiceInterfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// Munkatárs regisztrálásáért felelős
        /// </summary>
        /// <param name="userData">felhasználó adatai</param>
        /// <param name="files">profilkép</param>
        /// <returns></returns>
        Task<User> Register(UserData userData, IFormFileCollection files);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">felhasználónév</param>
        /// <param name="password">jelszó</param>
        /// <returns></returns>
        Task<User> AuthenticateAsync(string username, string password);
    }
}
