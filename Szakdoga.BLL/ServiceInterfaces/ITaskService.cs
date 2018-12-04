using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;
using Szakdoga.Model;

namespace Szakdoga.BLL.ServiceInterfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Visszaadja a tevékenységeket
        /// </summary>
        /// <returns></returns>
        Task<List<Activity>> GetActivities();

        /// <summary>
        /// Tevékenség létrehozás
        /// </summary>
        /// <param name="name">tevékenység neve</param>
        /// <returns></returns>
        Task<int> CreateActivity(string name);

        /// <summary>
        /// Tevékenység törlése
        /// </summary>
        /// <param name="id">tevékenység azonosítója</param>
        /// <returns></returns>
        Task<int> DeleteActivity(Guid id);

        /// <summary>
        /// Visszadja egy adott projekthez tartozó taszkokat
        /// </summary>
        /// <returns></returns>
        Task<List<TaskData>> GetTasksByProjectIdAsnyc(Guid id);

        /// <summary>
        /// Taszk létrehozása
        /// </summary>
        /// <param name="taskData">taszk adatai</param>
        /// <returns></returns>
        Task<int> CreateTaskAsync(TaskData taskData);

        /// <summary>
        /// Taszkot ad vissza
        /// </summary>
        /// <param name="id">taszk azonosítója</param>
        /// <returns></returns>
        Task<TaskDetailsData> GetTaskByIdAsync(Guid id);

        /// <summary>
        /// Taszk törlése
        /// </summary>
        /// <param name="id">taszk azonosítója</param>
        /// <returns></returns>
        Task<int> DeleteTaskAsync(Guid id);

        /// <summary>
        /// Munkatárshoz tartozó taszkok listája
        /// </summary>
        /// <param name="id">munkatárs azonosítója</param>
        /// <returns></returns>
        Task<List<TaskDetailsData>> GetUserTasks(Guid id);

        /// <summary>
        /// Tevékenység létrehozása
        /// </summary>
        /// <param name="activityData">tevékenység adatai</param>
        /// <returns></returns>
        Task<int> CreateTaskActivityAsync(ActivityData activityData);

        /// <summary>
        /// Visszaadja a projekt vezetőjének azonosítóját
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> GetProjectLeaderAsync(Guid id);
    }
}
