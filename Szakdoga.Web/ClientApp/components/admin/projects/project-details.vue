<template>
  <div class="container-fluid">

    <div class="row d-flex justify-content-center mt-3">
      <div class="col-md-10 text-center">
        <h1>{{ project.title }}</h1>
      </div>
      <div class="col-md-1 d-flex align-items-center">
        <transition name="fade">
          <icon v-if="!edit" id="edit" @click="editProject" :icon="['fas', 'user-edit']" style="color: #007bff; width: 30px; height: 30px;" />
        </transition>
        
      </div>
    </div>
    <form @submit.prevent="updateProject">

      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="title">Megnevezés</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="title" class="form-control" v-model="project.title" readonly />
        </div>
      </div>

      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="title">Becsült időigény (óra)</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input type="number"
                 class="form-control"
                 name="sumWorkHours"
                 placeholder="Ide írjon!"
                 v-model="project.sumWorkHours"
                 readonly>
        </div>
      </div>
      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="title">Kezdet</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <date-picker v-model="project.startTime"
                       placeholder="Válasszon!"
                       class="form-control"
                       autocomplete="off"
                       name="startTime"
                       readonly
                       :config="options" />
        </div>
      </div>

      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="title">Határidő</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <date-picker v-model="project.deadline"
                       placeholder="Válasszon!"
                       class="form-control"
                       autocomplete="off"
                       name="deadline"
                       readonly
                       :config="options" />
        </div>
      </div>

      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="title">Projekt vezetője</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <multiselect v-model="projectLeader"
                       :options="users"
                       placeholder="Válasszon!"
                       name="projectLeader"
                       :deselectLabel="'Enter a kivételhez'"
                       :selectLabel="'Enter a kiválasztáshoz'"
                       :selectedLabel="'Kiválasztva'"
                       :disabled="isDisabled"
                       label="name"
                       @input="input"
                       @remove="removeLeader"
                       track-by="name">
          </multiselect>
        </div>
      </div>

      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="title">Projekten dolgozók</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <multiselect v-model="workers" :options="users" :disabled="isDisabled" :multiple="true" :placeholder="'Válasszon!'" :deselectLabel="'Enter a kivételhez'" :selectLabel="'Enter a kiválasztáshoz'" :selectedLabel="'Kiválasztva'" :close-on-select="false" :clear-on-select="false" :preserve-search="true" label="name" track-by="id">
            <template slot="selection" slot-scope="{ values, search, isOpen }">
              <span class="multiselect__single" v-if="values.length &amp;&amp; !isOpen">{{ values.length }} dolgozó kiválasztva</span>
            </template>
          </multiselect>
        </div>
      </div>

      <transition name="fade">
        <button v-if="edit" id="save-button" class="btn btn-success mt-3 ml-2 float-right" type="submit">Mentés</button>
      </transition>
      <transition name="fade">
        <button v-if="edit" id="cancel" @click="finishEdit" type="button" class="btn btn-danger mt-3 float-right">Elvetés</button>
      </transition>

    </form>
  </div>
</template>
<script>
  import { projectService } from "../../../services/project-service";
  import { userService } from "../../../services/user-service";
  import { commonService } from "../../../services/common-service";
  import newProject from "./new-project";
  import Multiselect from 'vue-multiselect'

  export default {
    components: { Multiselect },
    data() {
      return {
        edit: false,
        projectLeader: {},
        project: {},
        options: {
          format: 'YYYY.MM.DD.',
          locale: 'hu'
        },
        isDisabled: true,
        users: [],
        workers: []
      }
    },
    methods: {

      input: function (item) {
        this.workers = [];
        this.users.forEach(function (user) { user.$isDisabled = false; });

        if (item != null || item != undefined) {
          this.workers.push(item);

          let user = this.users.find(user => user.id === item.id);
          user.$isDisabled = true;
        }
      },

      removeLeader: function (item) {
        let index = this.workers.indexOf(item);
        this.workers.splice(index, 1);
      },

      editProject: function () {
        this.edit = true;
        $("input").removeAttr("readonly");
        this.isDisabled = false;
      },

      finishEdit: function () {
        this.edit = false;
        $("input").attr("readonly", true);
        this.isDisabled = true;
      },

      loadUsers: function () {
        let app = this;

        userService.loadUsers("")
          .then(function (res) {
            if (res.data.users != null) {
              for (let i = 0; i < res.data.users.length; i++) {
                app.users.push({
                  name: res.data.users[i].firstName + " " + res.data.users[i].lastName,
                  id: res.data.users[i].id
                });
              }
            }
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      updateProject: function () {
        this.finishEdit();
        let app = this;

        projectService.createProject(this.project, this.workers, this.projectLeader)
          .then(function (res) {
            commonService.getToast(app)({
              type: 'success',
              title: 'Projekt frissítése sikeres!'
            });
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Projekt frissítése sikertelen!'
            });
          });

      }
    },
    created() {
      this.loadUsers();

      let id = this.$route.params.id;
      let app = this;

      if (id !== "") {
        projectService.getProjectById(id)
          .then(function (res) {
            if (res.data != null) {
              app.project = res.data;

              app.projectLeader = app.users.find(user => user.id === app.project.projectLeaderId);

              for (let i = 0; i < app.project.workers.length; i++) {
                app.workers.push(app.users.find(user => user.id === app.project.workers[i]));
              }
            }
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Projekt betöltése sikertelen!'
            });
          });
      }

    }
  }
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style>

</style>
