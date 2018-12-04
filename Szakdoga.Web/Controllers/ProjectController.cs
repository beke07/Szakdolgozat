using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Szakdoga.BLL.Models;
using Szakdoga.BLL.ServiceInterfaces;

namespace Szakdoga.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Json(await projectService.GetProjectsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectData projectData)
        {
            await projectService.CreateProjectAsync(projectData);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            await projectService.DeleteProjectAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProject(Guid id)
        {
            return Json(await projectService.GetProjectByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectsToUser(Guid id)
        {
            return Json(await projectService.GetProjectByUserAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectDetails(Guid id)
        {
            return Json(await projectService.GetProjectDetailsByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectLeader(Guid id)
        {
            return Json(await projectService.GetProjectLeaderAsync(id));
        }
    }
}
