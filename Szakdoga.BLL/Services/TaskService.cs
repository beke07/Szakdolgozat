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

namespace Szakdoga.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public TaskService(
            IMapper mapper,
            ApplicationDbContext DbContext)
        {
            this.mapper = mapper;
            this.DbContext = DbContext;
        }

        public async Task<List<Activity>> GetActivities()
        {
            return await DbContext.Activities.ToListAsync();
        }

        public async Task<int> DeleteActivity(Guid id)
        {
            Activity activityToDelete = await DbContext.Activities.FirstOrDefaultAsync(e => e.Id == id);
            if (activityToDelete != null)
            {
                DbContext.Activities.Remove(activityToDelete);
            }

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> CreateActivity(string name)
        {
            Activity activity = new Activity() { Name = name };
            DbContext.Activities.Add(activity);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<List<TaskData>> GetTasksByProjectIdAsnyc(Guid id)
        {
            return mapper.Map<List<TaskData>>((await DbContext.Projects.Include(e => e.Tasks).ThenInclude(t => t.Worker).FirstOrDefaultAsync(e => e.Id == id)).Tasks);
        }

        public async Task<int> CreateTaskAsync(TaskData taskData)
        {
            if(taskData.Id != null && taskData.Id != Guid.Empty)
            {
                ProjectTask task = await DbContext.Tasks.FirstOrDefaultAsync(e => e.Id == taskData.Id);
                task.Title = taskData.Title;
                task.Description = taskData.Description;
                task.StartTime = taskData.StartTime;
                task.WorkHours = taskData.WorkHours;
                task.WorkerId = taskData.WorkerId;
            }
            else
            {
                ProjectTask task = new ProjectTask();
                task = mapper.Map<ProjectTask>(taskData);
                DbContext.Tasks.Add(task);
            }

            return await DbContext.SaveChangesAsync();
        }

        public async Task<TaskDetailsData> GetTaskByIdAsync(Guid id)
        {
            return mapper.Map<TaskDetailsData>(await DbContext.Tasks.Include(e => e.TaskActivities).ThenInclude(t => t.Activity).FirstOrDefaultAsync(e => e.Id == id));
        }

        public async Task<int> DeleteTaskAsync(Guid id)
        {
            ProjectTask taskToDelete = await DbContext.Tasks.FirstOrDefaultAsync(e => e.Id == id);
            DbContext.Tasks.Remove(taskToDelete);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<List<TaskDetailsData>> GetUserTasks(Guid id)
        {
            return mapper.Map<List<TaskDetailsData>>(DbContext.Tasks.Include(e => e.Worker).Include(e => e.TaskActivities).ThenInclude(t => t.Activity).Where(e => e.WorkerId == id));
        }

        public async Task<int> CreateTaskActivityAsync(ActivityData activityData)
        {
            TaskActivity taskActivity = new TaskActivity();
            taskActivity = mapper.Map<TaskActivity>(activityData);
            taskActivity.Activity = await DbContext.Activities.FirstOrDefaultAsync(e => e.Id == activityData.ActivityId);

            DbContext.TaskActivities.Add(taskActivity);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<Guid> GetProjectLeaderAsync(Guid id)
        {
            return (await DbContext.Tasks.Include(e => e.Project).FirstOrDefaultAsync(e => e.Id == id)).Project.ProjectLeaderId;
        }
    }
}
