import axios from 'axios';
import { commonService } from './common-service';

class UserService {
  createUser(user, file) {
    const formData = commonService.toFormData(user);

    if (file !== null) {
      formData.append(file.name, file);
    }

    return axios.post('/Login/Register', formData);
  }

  authenticate(user) {
    return axios.post("/Login/Authenticate", commonService.toFormData(user));
  }

  loadUsers(searchname) {
    return axios.get("/User/GetUserList", { params: { searchname: searchname } });
  }

  getUserById(id) {
    return axios.get("/User/GetUserById", { params: { id: id } });
  }

  updateUser(user) {
    return axios.post("/User/UpdateUser", commonService.toFormData(user));
  }

  deleteUser(id) {
    return axios.delete("/User/DeleteUser", { params: { id: id } });
  }

  changePassword(userid, oldpassword, newpassword) {
    let passwordchangehelper = {
      userid: userid,
      oldpassword: oldpassword,
      newpassword: newpassword
    };

    return axios.post("/User/ChangePassword", commonService.toFormData(passwordchangehelper));
  }

  getHoursToUser(id) {
    return axios.get("/User/GetHoursToUser", { params: { id: id } });
  }

}

export const userService = new UserService();
