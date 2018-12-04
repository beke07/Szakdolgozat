<template>
  <div id="tasks">
    <new-task v-if="projectLeaderId === currentUserId" :projectId="projectId" :taskToEdit="null" v-on:taskCreated="loadTasks"></new-task>

    <b-row>
      <b-col md="6" sm="12">
        <h1>Projekt taszkjai</h1>
      </b-col>
      <b-col class="d-flex align-items-center" md="6" sm="12">
        <b-input-group>
          <b-form-input v-model="filter" placeholder="Ide írjon..." />
          <b-input-group-append>
            <b-btn :disabled="!filter" @click="filter = ''">Törlés</b-btn>
          </b-input-group-append>
        </b-input-group>
      </b-col>
    </b-row>

    <b-table hover
             show-empty
             stacked="md"
             :items="tasks"
             :fields="taskfields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :sort-by.sync="sortBy"
             :sort-desc.sync="sortDesc"
             :sort-direction="sortDirection"
             @filtered="onFiltered">

      <template slot="workHours" slot-scope="row">
        {{row.item.workHours}} óra
      </template>

      <template slot="details" slot-scope="row">
        <router-link class="navbar-brand" :to="{name: 'task-details', params: { taskId: row.item.id } }">
          <icon :icon="['fas', 'info-circle']" style="width: 30px; height: 30px;" />
        </router-link>
        <icon v-if="projectLeaderId === currentUserId" @click="deleteTask(row.item.id)" :icon="['fas', 'trash']" style="width: 30px; height: 30px; color: #dc3545;" />
      </template>

    </b-table>

    <b-row>
      <b-col md="6" class="my-1">
        <b-pagination :total-rows="totalRows" :per-page="perPage" v-model="currentPage" class="my-0" />
      </b-col>
    </b-row>

  </div>
</template>
<script>
  import { taskService } from "../../../services/task-service"
  import { projectService } from "../../../services/project-service"
  import { commonService } from "../../../services/common-service";
  import newTask from "./new-task";

  export default {
    components: { newTask: newTask },
    data() {
      return {
        projectLeaderId: "",
        currentUserId: null,
        projectId: "",
        tasks: [],
        taskfields: [
          { key: 'title', label: 'Megnevezés', sortable: true },
          { key: 'startTime', label: 'Kezdés időpontja', sortable: true },
          { key: 'beingInChargeOfName', label: 'Felelős', sortable: true },
          { key: 'workHours', label: 'Becsült idő', sortable: true },
          { key: 'details', label: 'Részletek', sortable: true }
        ],
        currentPage: 1,
        perPage: 5,
        totalRows: 0,
        sortBy: null,
        sortDesc: false,
        sortDirection: 'asc',
        filter: null
      }
    },
    methods: {

      onFiltered: function (filteredItems) {
        this.totalRows = filteredItems.length
        this.currentPage = 1
      },

      deleteTask: function (id) {

        let app = this;

        commonService.getComfirm(this)
          .then((result) => {
            if (result.value) {
              taskService.deleteTask(id)
                .then(function () {

                  commonService.getToast(app)({
                    type: 'success',
                    title: 'Taszk törlése sikeres!'
                  });

                  app.loadTasks();
                })
                .catch(function () {
                  commonService.getToast(app)({
                    type: 'error',
                    title: 'Taszk törlése sikertelen!'
                  });
                });
            }
          });
      },

      loadProjectLeader: function () {

        let app = this;
        projectService.getProjectLeader(this.projectId)
          .then(function (res) {
            app.projectLeaderId = res.data;
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      loadTasks: function () {

        this.projectId = this.$route.params.id;
        let app = this;

        taskService.getTaskByProject(this.projectId)
          .then(function (res) {
            if (res.data != null) {
              app.tasks = res.data;

              for (let i = 0; i < app.tasks.length; i++) {
                app.tasks[i].startTime = commonService.dateTimeformat(app.tasks[i].startTime);
              }

              app.totalRows = app.tasks.length;
            }
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Taszkok betöltése sikertelen!'
            });
          });
      }
    },
    created() {
      this.currentUserId = JSON.parse(window.localStorage.getItem('lbUser')).data.id;
      this.loadTasks();
      this.loadProjectLeader();
    }
  }
</script>

<style lang="less" scoped>
</style>
