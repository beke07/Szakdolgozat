import axios from 'axios';
import { commonService } from './common-service';

class TaskService {

  getActivities() {
    return axios.get("/Task/GetActivities");
  }

  deleteActivity(id) {
    return axios.delete("/Task/DeleteActivity", { params: { id: id } });
  }

  createActivity(name) {
    return axios.post("/Task/CreateActivity", commonService.toFormData({ name: name }));
  }

  getTaskByProject(id) {
    return axios.get("/Task/GetTasksByProject", { params: { id: id } });
  }

  createTask(task) {
    return axios.post("/Task/CreateTask", commonService.toFormData(task));
  }

  getTask(id) {
    return axios.get("/Task/GetTask", { params: { id: id } });
  }

  deleteTask(id) {
    return axios.delete("/Task/DeleteTask", { params: { id: id } });
  }

  getUserTasks(id) {
    return axios.get("/Task/GetUserTasks", { params: { id: id } });
  }

  createTaskActivity(taskActivity) {
    return axios.post("/Task/CreateTaskActivity", commonService.toFormData(taskActivity));
  }

  getProjectLeader(id) {
    return axios.get("/Task/GetProjectLeader", { params: { id: id } });
  }
}

export const taskService = new TaskService();
