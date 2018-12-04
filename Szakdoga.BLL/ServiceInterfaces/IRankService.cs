using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szakdoga.Model;

namespace Szakdoga.BLL.ServiceInterfaces
{
    public interface IRankService
    {
        /// <summary>
        /// Visszaadja a beosztások listáját
        /// </summary>
        /// <returns></returns>
        Task<List<Rank>> GetRanksAsync();

        /// <summary>
        /// Létrehoz egy új beszotást
        /// </summary>
        /// <param name="name">Beosztás neve</param>
        /// <returns></returns>
        Task CreateRankAsync(string name);

        /// <summary>
        /// Törli a megfelelő besztást
        /// </summary>
        /// <param name="id">a törlendő beszotás azonosítója</param>
        /// <returns></returns>
        Task DeleteRankByIdAsync(Guid id);

        /// <summary>
        /// Kitörli a megfelelő beosztást
        /// </summary>
        /// <param name="id">Beosztás azonosítója</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
