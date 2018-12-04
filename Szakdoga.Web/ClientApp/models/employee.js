export default class Employee {
  constructor(id, profilePicture, username, firstname, lastname, bossId, employeeIds) {
    this.id = id;
    this.profilePicture = profilePicture;
    this.username = username;
    this.firstName = firstname;
    this.lastName = lastname;
    this.iWasHere = false;
    this.writeIt = false;
    this.bossId = bossId;
    this.employeeIds = employeeIds;
    this.employees = [];
    this.boss = null;
  }
}
