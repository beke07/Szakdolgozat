using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Szakdoga.BLL.Models;
using Szakdoga.BLL.ServiceInterfaces;

namespace Szakdoga.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            return Json(await taskService.GetActivities());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await taskService.DeleteActivity(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(string name)
        {
            await taskService.CreateActivity(name);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByProject(Guid id)
        {
            return Json(await taskService.GetTasksByProjectIdAsnyc(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskData taskData)
        {
            await taskService.CreateTaskAsync(taskData);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTask(Guid id)
        {
            return Json(await taskService.GetTaskByIdAsync(id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid id) {
            await taskService.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTasks(Guid id)
        {
            return Json(await taskService.GetUserTasks(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskActivity(ActivityData activityData)
        {
            await taskService.CreateTaskActivityAsync(activityData);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectLeader(Guid id)
        {
            return Json(await taskService.GetProjectLeaderAsync(id));
        }
    }
}
