using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;

namespace Szakdoga.BLL.ServiceInterfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Visszaadja az összes projektet
        /// </summary>
        /// <returns></returns>
        Task<List<ProjectData>> GetProjectsAsync();

        /// <summary>
        /// Új projekt létrehozása
        /// </summary>
        /// <param name="projectData">projekt adatai</param>
        /// <returns></returns>
        Task<int> CreateProjectAsync(ProjectData projectData);

        /// <summary>
        /// Projekt törlése
        /// </summary>
        /// <param name="id">projekt azonostó</param>
        /// <returns></returns>
        Task<int> DeleteProjectAsync(Guid id);

        /// <summary>
        /// Egy projektet ad vissza
        /// </summary>
        /// <param name="id">egyedi azonositó</param>
        /// <returns></returns>
        Task<ProjectData> GetProjectByIdAsync(Guid id);

        /// <summary>
        /// Munkatárshoz tartozó projektek
        /// </summary>
        /// <param name="id">munkatárs azonositoja</param>
        /// <returns></returns>
        Task<List<ProjectData>> GetProjectByUserAsync(Guid id);

        /// <summary>
        /// Projekt részletei
        /// </summary>
        /// <param name="id">projekt azonosítója</param>
        /// <returns></returns>
        Task<ProjectDetailsData> GetProjectDetailsByIdAsync(Guid id);

        /// <summary>
        /// Visszaadja a projekt vezetőjének azonosítóját
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> GetProjectLeaderAsync(Guid id);
    }
}
