<template>
  <div id="home">
    <b-row>
      <b-col md="6" sm="12">
        <h1>Taszkjaim</h1>
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
             @row-clicked="taskClicked"
             @filtered="onFiltered">

      <template slot="workHours" slot-scope="row">
        {{row.item.workHours}} óra
      </template>

      <template slot="details" slot-scope="row">
        <router-link class="navbar-brand" :to="{name: 'task-details', params: { taskId: row.item.id } }">
          <icon :icon="['fas', 'info-circle']" style="width: 30px; height: 30px;" />
        </router-link>
        <icon v-b-modal.modalPrevent :icon="['fas', 'plus']" style="width: 30px; height: 30px; color: #28a745;" />

      </template>

    </b-table>

    <b-row>
      <b-col md="6" class="my-1">
        <b-pagination :total-rows="totalRows" :per-page="perPage" v-model="currentPage" class="my-0" />
      </b-col>
    </b-row>

    <b-modal id="modalPrevent"
             ref="modal"
             title="Tevékenység hozzáadása"
             @ok="handleOk"
             @shown="clear">
      <form @submit.stop.prevent="createActivity">

        <div class="form-group">
          <label class="control-label">Dátum</label>
          <date-picker v-model="taskActivity.date"
                       placeholder="Válasszon!"
                       class="form-control"
                       v-validate="'required'"
                       autocomplete="off"
                       name="date"
                       :class="{ 'is-invalid': submitted && errors.has('date') }"
                       :config="options" />

          <div v-if="submitted && errors.has('date')" class="invalid-feedback">{{ errors.first('date') }}</div>
        </div>

        <div class="form-group">
          <label class="control-label">Leírás</label>
          <input type="text"
                 class="form-control"
                 name="description"
                 placeholder="Ide írjon!"
                 v-model="taskActivity.description"
                 v-validate="'required'"
                 :class="{ 'is-invalid': submitted && errors.has('description') }">

          <div v-if="submitted && errors.has('description')" class="invalid-feedback">{{ errors.first('description') }}</div>
        </div>

        <div class="form-group">
          <label class="control-label">Ráfordított idő (óra)</label>
          <input type="number"
                 class="form-control"
                 name="hours"
                 placeholder="Ide írjon!"
                 v-model="taskActivity.hours"
                 v-validate="'required'"
                 :class="{ 'is-invalid': submitted && errors.has('hours') }">

          <div v-if="submitted && errors.has('hours')" class="invalid-feedback">{{ errors.first('hours') }}</div>
        </div>

        <div class="form-group">
          <label class="control-label">Tevékenység típusa</label>
          <multiselect v-model="activity"
                       :options="activities"
                       placeholder="Válasszon!"
                       :deselectLabel="'Enter a kivételhez'"
                       :selectLabel="'Enter a kiválasztáshoz'"
                       :selectedLabel="'Kiválasztva'"
                       label="name"
                       track-by="name">
          </multiselect>
        </div>

      </form>
    </b-modal>

  </div>
</template>
<script>
  import { taskService } from "../../../services/task-service"
  import { commonService } from "../../../services/common-service";
  import Multiselect from 'vue-multiselect'

  export default {
    components: { Multiselect },
    data() {
      return {
        submitted: false,
        taskId: null,
        taskActivity: {},
        activities: [],
        activity: {},
        userId: JSON.parse(window.localStorage.getItem('lbUser')).data.id,
        tasks: [],
        options: {
          format: 'YYYY.MM.DD.',
          locale: 'hu'
        },
        taskfields: [
          { key: 'title', label: 'Megnevezés', sortable: true },
          { key: 'startTime', label: 'Kezdés időpontja', sortable: true },
          { key: 'workHours', label: 'Becsült idő', sortable: true },
          { key: 'details', label: 'Részletek', sortable: true }
        ],
        currentUserId: null,
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

      taskClicked(item) {
        this.taskId = item.id;
      },

      clear() {
        this.activity = {}
      },

      handleOk() {
        this.createActivity();
      },

      createActivity: function () {

        this.taskActivity.activityId = this.activity.id;
        this.taskActivity.taskId = this.taskId;

        console.log(this.taskActivity);

        let app = this;
        taskService.createTaskActivity(this.taskActivity)
          .then(function (res) {
            commonService.getToast(app)({
              type: 'success',
              title: 'Tevékenység létrehozása sikeres!'
            });

            app.clear();
            app.$refs.modal.hide();
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Tevékenység létrehozása sikertelen!'
            });
          });
      },

      loadTasks: function () {

        let app = this;

        taskService.getUserTasks(this.userId)
          .then(function (res) {
            app.tasks = res.data;

            for (let i = 0; i < app.tasks.length; i++) {
              app.tasks[i].startTime = commonService.dateTimeformat(app.tasks[i].startTime);
            }

            app.totalRows = app.tasks.length;
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Taszkok betöltése sikertelen!'
            });
          })
      },

      loadActivities: function () {

        let app = this;
        taskService.getActivities()
          .then(function (res) {


            for (let i = 0; i < res.data.length; i++) {
              app.activities.push({
                name: res.data[i].name,
                id: res.data[i].id
              });
            }
          })
          .catch(function (err) {
            console.log(err);
          });
      }
    },
    created() {
      this.loadTasks();
      this.loadActivities();
    }
  }
</script>

<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style lang="less" scoped>

</style>
