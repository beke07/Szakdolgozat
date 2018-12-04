import { Drop, Drag } from "vue-drag-drop";
import Vue from 'vue';
import Employee from '../models/employee';
import axios from 'axios';
import { commonService } from './common-service';

class HierarchyService {

  resetTheUsers(users) {

    let newUsers = [];

    for (let i = 0; i < users.length; i++) {

      let employeeIds = [];
      for (let j = 0; j < users[i].employees.length; j++) {
        employeeIds.push(users[i].employees[j].id);
      }

      newUsers.push(
        new Employee(
          users[i].id,
          users[i].profilePicture,
          users[i].username,
          users[i].firstName,
          users[i].lastName,
          users[i].bossId,
          employeeIds
        )
      );
    }

    this.createTheHierarchy(newUsers);
  }

  createTheHierarchy(newUsers) {
    for (let i = 0; i < newUsers.length; i++) {
      newUsers[i].employees = [];
      for (let j = 0; j < newUsers[i].employeeIds.length; j++) {
        newUsers[i].employees.push(newUsers.find(user => user.id === newUsers[i].employeeIds[j]));
      }
    }

    window.localStorage.setItem('users', JSON.stringify(newUsers));
  }

  drawTheHierarchy(users, app) {

    $("#hierarchy-body").empty();
    hierarchyService.resetTheUsers(users);

    let newusers = JSON.parse(window.localStorage.getItem('users'));

    for (let i = 0; i < newusers.length; i++) {
      if (newusers[i].bossId === null) {
        this.recursiveDepthFirstSearch(newusers[i], app)
      }
    }
  }

  setEmployeeInUsers(employee) {
    let users = JSON.parse(window.localStorage.getItem('users'));

    for (let i = 0; i < users.length; i++) {
      if (users[i].id == employee.id) {
        users[i] = employee;
      }
    }

    this.createTheHierarchy(users);
  }

  recursiveDepthFirstSearch(employee, app) {

    if (employee.bossId === null && !(employee.writeIt)) {
      employee.writeIt = true;
      this.setEmployeeInUsers(employee);

      this.createElement(employee, app);
    }

    if (employee.employees.length !== 0 && !this.allChecked(employee)) {
      for (let i = 0; i < employee.employees.length; i++) {
        if (!(employee.employees[i].writeIt)) {

          employee.employees[i].writeIt = true;
          this.setEmployeeInUsers(employee.employees[i]);

          this.createElement(employee.employees[i], app);
        }
      }
      for (let i = 0; i < employee.employees.length; i++) {
        if (!(employee.employees[i].iWasHere)) {
          return this.recursiveDepthFirstSearch(employee.employees[i], app);
        }
      }
    }
    else {
      employee.iWasHere = true;
      this.setEmployeeInUsers(employee);

      if (employee.bossId !== null) {

        let users = JSON.parse(window.localStorage.getItem('users'));
        let boss = users.find(user => user.id === employee.bossId);

        return this.recursiveDepthFirstSearch(boss, app);
      }
    }
  }

  allChecked(employee) {
    for (let i = 0; i < employee.employees.length; i++) {
      if (!(employee.employees[i].iWasHere)) {
        return false;
      }
    }
    return true;
  }

  createDrop(employee, app) {
    let DropClass = Vue.extend(Drop);
    let drop = new DropClass();
    drop.$mount();

    let dropelement = $(drop.$el);
    $(dropelement).addClass("drop");
    $(dropelement).css("background-image", 'url(/images/' + employee.profilePicture + ')');
    $(dropelement).attr("hidden", "true");
    $(dropelement).attr("id", employee.id);

    $(dropelement).on("drop", function () {
      app.handleDrop(employee.id, event)
    });

    $(dropelement).on("dragenter", function () {
      app.handleDragEnter(employee.id, event)
    });

    $(dropelement).on("dragleave", function () {
      app.handleDragLeave(employee.id, event)
    });

    return drop.$el;
  }

  createDrag(employee, app) {
    let DragClass = Vue.extend(Drag);
    let drag = new DragClass();
    drag.$mount();

    let dragelement = $(drag.$el);
    $(dragelement).addClass("drag");
    $(dragelement).css("background-image", 'url(/images/' + employee.profilePicture +')');
    $(dragelement).attr("id", employee.id);

    let name = document.createElement("h6");
    $(name).html(employee.firstName + " " + employee.lastName);
    $(name).css("width", "100%");
    $(name).css("text-align", "center");
    $(name).css("font-weight", "bold");
    $(name).css("padding-top", "95px");
    $(name).appendTo(dragelement);

    $(dragelement).on("drag", function () {
      app.handleDrag(employee.id, event)
    });

    $(dragelement).on("dragend", function () {
      app.handleDragEnd(employee.id, event)
    });

    if (app.role != "admin") {
      $(dragelement).attr("draggable", false);
      $(name).addClass("unselectable");
    }
    else {
      $(dragelement).addClass("draghover");

      $(dragelement).on("dblclick", function () {
        app.details(employee.id);
      });
    }

    return drag.$el;
  }

  createElement(employee, app) {

    let dragnegyzet = this.createDrag(employee, app);
    let dropnegyzet = this.createDrop(employee, app);

    if (employee.bossId !== null && $("#" + employee.bossId).children().length > 2 && employee.employees.length === 0) {
      $("#" + employee.bossId).find(".employee").first().append(dragnegyzet);
      $("#" + employee.bossId).find(".employee").first().append(dropnegyzet);
    }
    else {
      let element = document.createElement("div");
      $(element).addClass("employee");
      $(element).attr("id", employee.id);
      $(element).addClass("employee-has-employees");

      $(element).append(dragnegyzet);
      $(element).append(dropnegyzet);

      if (employee.bossId !== null) {
        $("#" + employee.bossId).append(element);
      }
      else {
        $("#hierarchy-body").append(element);
      }
    }
  }

  saveHierarchy(users) {

    let userDataList = [];

    for (var i = 0; i < users.length; i++) {
      let user = new Object();
      user.Id = users[i].id;
      user.BossId = users[i].bossId;
      user.EmployeeIds = users[i].employeeIds;
      userDataList.push(user);
    }

    return axios.post("/User/SaveHierarchy", commonService.toListFormData({ userData: userDataList }));
  }

  getHierarchyUserList() {
    return axios.get("/User/GetHierarchyUserList");
  }
}

export const hierarchyService = new HierarchyService();
