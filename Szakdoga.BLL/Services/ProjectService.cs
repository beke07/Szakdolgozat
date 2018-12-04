using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdoga.BLL.Models;
using Szakdoga.BLL.ServiceInterfaces;
using Szakdoga.DAL;
using Szakdoga.Model;
using Szakdoga.Model.JoiningClasses;

namespace Szakdoga.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public ProjectService(
            IMapper mapper,
            ApplicationDbContext DbContext)
        {
            this.mapper = mapper;
            this.DbContext = DbContext;
        }

        private async Task<int> UpdateProject(ProjectData projectData)
        {
            Project projectToUpdate = await DbContext.Projects.FirstOrDefaultAsync(e => e.Id == projectData.Id);
            projectToUpdate.Title = projectData.Title;
            projectToUpdate.SumWorkHours = projectData.SumWorkHours;
            projectToUpdate.Deadline = projectData.Deadline;
            projectToUpdate.StartTime = projectData.StartTime;

            List<ProjectWorker> projectWorkers = DbContext.ProjectWorkers.Where(e => e.ProjectId == projectData.Id).ToList();
            DbContext.ProjectWorkers.RemoveRange(projectWorkers);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> CreateProjectAsync(ProjectData projectData)
        {
            Guid projectId;
            if (projectData.Id != null && projectData.Id != Guid.Empty)
            {
                await UpdateProject(projectData);
                projectId = projectData.Id;
            }
            else
            {
                Project project = new Project();
                project = mapper.Map<Project>(projectData);
                
                DbContext.Projects.Add(project);
                await DbContext.SaveChangesAsync();

                projectId = project.Id;
            }

            projectData.Workers.ForEach((w) =>
            {
                DbContext.ProjectWorkers.Add(new ProjectWorker()
                {
                    ProjectId = projectId,
                    WorkerId = w
                });
            });

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteProjectAsync(Guid id)
        {
            Project projectToDelete = await DbContext.Projects.Include(e => e.Tasks).Include(e => e.Workers).FirstOrDefaultAsync(e => e.Id == id);
            if (projectToDelete != null)
            {
                DbContext.Projects.Remove(projectToDelete);
            }

            return await DbContext.SaveChangesAsync();
        }

        public async Task<ProjectData> GetProjectByIdAsync(Guid id)
        {
            return mapper.Map<ProjectData>(await DbContext.Projects.Include(e => e.Workers).FirstOrDefaultAsync(e => e.Id == id));
        }

        public async Task<List<ProjectData>> GetProjectsAsync()
        {
            return mapper.Map<List<ProjectData>>(await DbContext.Projects.Include(e => e.Workers).ToListAsync());
        }

        public async Task<List<ProjectData>> GetProjectByUserAsync(Guid id)
        {
            return mapper.Map<List<ProjectData>>(await DbContext.ProjectWorkers.Where(e => e.WorkerId == id).Select(e => e.Project).ToListAsync());
        }

        public async Task<ProjectDetailsData> GetProjectDetailsByIdAsync(Guid id)
        {
            ProjectDetailsData projectDetailsData =  mapper.Map<ProjectDetailsData>(await DbContext.Projects
                                                                                            .Include(e => e.Tasks)
                                                                                            .ThenInclude(t => t.TaskActivities)
                                                                                            .FirstOrDefaultAsync(e => e.Id == id));

            projectDetailsData.WorkersData = mapper.Map<List<UserData>>(await DbContext.ProjectWorkers
                                                                                .Where(e => e.ProjectId == id)
                                                                                .Select(e => e.Worker)
                                                                                .ToListAsync());

            return projectDetailsData;
        }

        public async Task<Guid> GetProjectLeaderAsync(Guid id)
        {
            return (await DbContext.Projects.FirstOrDefaultAsync(e => e.Id == id)).ProjectLeaderId;
        }
    }
}
