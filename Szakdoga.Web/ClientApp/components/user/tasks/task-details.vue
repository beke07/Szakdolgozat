<template>
  <div id="task-details">
    <new-task v-if="taskToEdit !== null && projectLeaderId === currentUserId" :projectId="''" v-bind:taskToEdit="taskToEdit"></new-task>

    <b-row>
      <b-col md="6" sm="12">
        <h1>Tevékenységek</h1>
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
             :items="activities"
             :fields="activityfields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :sort-by.sync="sortBy"
             :sort-desc.sync="sortDesc"
             :sort-direction="sortDirection"
             @filtered="onFiltered">

      <template slot="hours" slot-scope="row">
        {{row.item.hours}} óra
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
  import { commonService } from "../../../services/common-service";
  import newTask from "./new-task";

  export default {
    components: { newTask: newTask },
    data() {
      return {
        currentUserId: null,
        projectLeaderId: "",
        taskId: "",
        taskToEdit: null,
        activities: [],
        activityfields: [
          { key: 'description', label: 'Leírás', sortable: true },
          { key: 'hours', label: 'Ráfordított idő', sortable: true },
          { key: 'date', label: 'Dátum', sortable: true },
          { key: 'activityName', label: 'Tevékenység típusa', sortable: true }
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

      loadProjectLeader: function () {

        let app = this;
        taskService.getProjectLeader(this.taskId)
          .then(function (res) {
            app.projectLeaderId = res.data;
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      loadTask: function () {

        this.taskId = this.$route.params.taskId;
        this.projectId = this.$route.params.projectId;
        let app = this;

        taskService.getTask(this.taskId)
          .then(function (res) {
            app.taskToEdit = res.data;
            app.activities = res.data.activities;

            for (let i = 0; i < app.activities.length; i++) {
              app.activities[i].date = commonService.dateTimeformat(app.activities[i].date);
            }

            app.totalRows = app.activities.length;
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Taszk betöltése sikertelen!'
            });
          });

      }
    },
    created() {
      this.currentUserId = JSON.parse(window.localStorage.getItem('lbUser')).data.id;
      this.loadTask();
      this.loadProjectLeader();
    }
  }
</script>

<style lang="less" scoped>

</style>
