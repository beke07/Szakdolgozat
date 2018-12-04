import axios from "axios";
import { commonService } from "./common-service";

class ProjectService {

  getProjects() {
    return axios.get('/Project/GetProjects');
  }

  createProject(projectData, workers, projectLeader) {

    projectData.workers = [];
    for (let i = 0; i < workers.length; i++) {
      projectData.workers.push(workers[i].id);
    }

    projectData.projectLeaderId = projectLeader.id;

    return axios.post('/Project/CreateProject', commonService.toFormData(projectData));
  }

  deleteProject(id) {
    return axios.delete("/Project/DeleteProject", { params: { id: id } });
  }

  getProjectById(id) {
    return axios.get("/Project/GetProject", { params: { id: id } });
  }

  getProjectsByUser(id) {
    return axios.get("/Project/GetProjectsToUser", { params: { id: id } })
  }

  getProjectDetails(id) {
    return axios.get("/Project/GetProjectDetails", { params: { id: id } });
  }

  getProjectLeader(id) {
    return axios.get("/Project/GetProjectLeader", { params: { id: id } });
  }
}

export const projectService = new ProjectService();
