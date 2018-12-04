using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;
using Szakdoga.Model;

namespace Szakdoga.BLL.ServiceInterfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Minden felhasználót visszaad
        /// </summary>
        /// <returns></returns>
        Task<List<UserData>> GetAllAsync(string name);

        /// <summary>
        /// Azonosító szerint adja vissza a megfelelő felhasználót
        /// </summary>
        /// <param name="id">Azonosító</param>
        /// <returns></returns>
        Task<User> GetByIdAsync(Guid id);

        /// <summary>
        /// Felhasználó létrehozása
        /// </summary>
        /// <param name="user">Létrehozni kívánt entitás</param>
        /// <param name="password">Jelszó</param>
        /// <returns></returns>
        Task<User> CreateAsync(User user, string password);

        /// <summary>
        /// Felhasználó adatainak módosítása
        /// </summary>
        /// <param name="user">Felhasználó</param>
        /// <param name="password">Jelszó</param>
        /// <returns></returns>
        Task UpdateAsync(UserData user, string password = null);

        /// <summary>
        /// Megváltoztatja a felhasználó a jelszavát
        /// </summary>
        /// <param name="oldPassword">Régi jelszó</param>
        /// <param name="newPassword">Új jelszó</param>
        /// <returns></returns>
        Task<bool> UpdatePasswordAsync(Guid userId, string oldPassword, string newPassword);

        /// <summary>
        /// Felhasználó törlése azonosító szerint
        /// </summary>
        /// <param name="id">Azonosító</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Hierarchiához szükséges adatok
        /// </summary>
        /// <returns></returns>
        Task<List<UserHierarchyDetailsData>> GetUserHierarchyUserList();

        /// <summary>
        /// A munkahelyi hierarchia elmentésére szolgál
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        Task<bool> SetUserHierarchyAsync(List<UserHierarchyData> userData);

        /// <summary>
        /// Generál egy random jelszót regisztráció után
        /// </summary>
        /// <returns></returns>
        string GeneratePassword();

        /// <summary>
        /// Havi ledolgozott óraszámot ad vissza
        /// </summary>
        /// <param name="id">felhasználó azononítója</param>
        /// <returns></returns>
        Task<long> GetHoursToUserAsync(Guid id);
    }
}
