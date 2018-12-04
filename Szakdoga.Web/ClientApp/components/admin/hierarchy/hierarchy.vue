<template>
  <div>

    <b-tooltip v-if="role == 'admin'" target="#information" placement="bottomleft">
      A felhasználóra <strong>kettőt kattintva</strong> láthatja a részleteket!
    </b-tooltip>

    <div class="row">
      <div class="col-12 p-0">
        <h1 class="float-left">Hierarchia</h1>
        <button v-if="role == 'admin'" @click="saveHierarchy" class="btn btn-primary float-right ml-2" type="submit">Mentés</button>
        <icon v-if="role == 'admin'" id="information" class="float-right" :icon="['fas', 'info-circle']" style="width: 38px; height: 38px;" />
      </div>
    </div>
    <div id="hierarchy-body" class="row"></div>
  </div>
</template>
<script>
  import { hierarchyService } from "../../../services/hierarchy-service";
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        role: "",
        users: [],
        currentlyDragging: null
      }
    },
    methods: {
 
      handleDrag(employeeId, event) {
        this.currentlyDragging = event.srcElement;
        $(".drag").attr("hidden", "true");
        $(".drop").removeAttr("hidden");
      },

      handleDrop(employeeId, event) {
        if ($(event.srcElement).attr("id") !== $(this.currentlyDragging).attr("id")) {
          $(event.srcElement).css("border", "0");

          let app = this;
          let users = JSON.parse(window.localStorage.getItem('users'));

          let dropElement = users.find(user => user.id === employeeId);
          let dragElement = users.find(user => user.id === $(app.currentlyDragging).attr("id"));
          let dragElementBoss = null;
          let dropElementBoss = null;

          if (dragElement.employees.find(user => user.id === employeeId)) {
            return;
          }

          if (dragElement.bossId !== null) {
            dragElementBoss = users.find(user => user.id === dragElement.bossId);

            for (let i = 0; i < dragElementBoss.employees.length; i++) {
              if (dragElementBoss.employees[i].id === dragElement.id) {
                dragElementBoss.employees.splice(i, 1);
              }
            }
          }

          if (dropElement.bossId !== null) {
            dropElementBoss = users.find(user => user.id === dropElement.bossId);

            for (let i = 0; i < dropElementBoss.employees.length; i++) {
              if (dropElementBoss.employees[i].id === dropElement.id) {
                dropElementBoss.employees.splice(i, 1);
              }
            }
          }

          if (!(dragElement.employees.find(user => user.id === dropElement.bossId))) {
            if (dropElementBoss !== null) {
              dropElementBoss.employees.push(dragElement);
            }
            dragElement.bossId = dropElement.bossId;
          }

          dropElement.bossId = dragElement.id;
          dragElement.employees.unshift(dropElement);

          hierarchyService.drawTheHierarchy(users, app);
        }
      },

      details(employeeId) {
        this.$router.push({ name: 'user-details', params: { id: employeeId } })
      },

      handleDragEnd(employeeId, event) {
        this.currentlyDragging = null;
        $(".drop").attr("hidden", "true");
        $(".drag").removeAttr("hidden");
      },

      handleDragEnter(employeeId, event) {
        if ($(event.srcElement).attr("id") === $(this.currentlyDragging).attr("id")) {
          return;
        }
        else {
          $(event.srcElement).css("border", "3px solid #007bff");
        }
      },

      handleDragLeave(employeeId, event) {
        $(event.srcElement).css("border", "0");
      },

      loadUsers: function () {
        let app = this;

        hierarchyService.getHierarchyUserList()
          .then(function (res) {
            if (res.data.users != null) {
              app.users = res.data.users;
              hierarchyService.drawTheHierarchy(app.users, app)
            }
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      saveHierarchy: function () {
        let users = JSON.parse(window.localStorage.getItem('users'));
        let app = this;

        hierarchyService.saveHierarchy(users)
          .then(function (res) {
            commonService.getToast(app)({
              type: 'success',
              title: 'Hierarchia mentése sikeres!'
            });
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Hierarchia mentése sikertelen!'
            });
          });;
      },

      setRole() {
        this.role = JSON.parse(window.localStorage.getItem('lbUser')).data.role;
      }
    },
    created() {
      
      this.setRole();

      if (this.role == "admin") {
        this.$swal.mixin({
          type: 'info',
          title: 'Figyelem!',
          text: 'Húzza rá arra a munkatársat, akit alkalmazottjává szeretne tenni!'
        })({});
      }

      this.loadUsers();
    }
  }
</script>

<style>

  .drop,
  .drag {
    display: inline-flex;
    margin-right: 20px;
    width: 100px;
    height: 100px;
    border-radius: 50%;
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
  }

  .drop {
    opacity: 0.75;
  }

  .unselectable {
    -webkit-touch-callout: none;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
  }

  .draghover:hover{
    cursor: pointer;
  }

  #svg {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100vh;
  }

  .negyzet {
    display: inline-flex;
    justify-content: center;
    align-items: center;
    margin-right: 20px;
    width: 100px;
    height: 100px;
    color: white;
    background: dimgray;
    border-radius: 3px;
    opacity: 0.4;
  }

  .employee {
    width: 100%;
    margin-top: 20px;
    padding: 20px;
    font-size: 30px;
    color: dimgray;
    border-radius: 3px;
  }

  .employee-has-employees {
    box-shadow: 10px 10px 34px -2px rgba(158, 150, 158, 1);
  }

  #hierarchy-body{
    zoom: 0.8;
    padding-bottom: 25px;
  }

  #information{
    color: lightgray;
  }
</style>
