<template>
  <div id="new-task" class="mb-3">
    <b-btn v-if="taskToEdit === null" id="new-task-button" v-b-toggle.collapse1 variant="primary"><icon :icon="['fas', 'plus']" class="mr-2" />Új taszk hozzáadása</b-btn>
    <b-btn v-if="taskToEdit !== null" id="new-task-button" v-b-toggle.collapse1 variant="success"><icon :icon="['fas', 'edit']" class="mr-2" />Taszk módosítása</b-btn>

    <b-collapse id="collapse1" class="mt-2">
      <b-card>
        <form @submit.prevent="createTask">

          <div class="form-group">
            <label class="control-label">Megnevezés</label>
            <input type="text"
                   class="form-control"
                   name="title"
                   placeholder="Ide írjon!"
                   v-model="task.title"
                   v-validate="'required'"
                   :class="{ 'is-invalid': submitted && errors.has('title') }">

            <div v-if="submitted && errors.has('title')" class="invalid-feedback">{{ errors.first('title') }}</div>
          </div>

          <div class="form-group">
            <label class="control-label">Felelős</label>
            <multiselect v-model="worker"
                         :options="users"
                         placeholder="Válasszon!"
                         name="worker"
                         :deselectLabel="'Enter a kivételhez'"
                         :selectLabel="'Enter a kiválasztáshoz'"
                         :selectedLabel="'Kiválasztva'"
                         v-validate="'required'"
                         :class="{ 'is-invalid': submitted && errors.has('worker') }"
                         label="name"
                         track-by="name">
            </multiselect>

            <div v-if="submitted && errors.has('worker')" class="invalid-feedback">{{ errors.first('worker') }}</div>
          </div>


          <div class="form-group">
            <label class="control-label">Kezdet dátuma</label>
            <date-picker v-model="task.startTime"
                         placeholder="Válasszon!"
                         class="form-control"
                         v-validate="'required'"
                         autocomplete="off"
                         name="startTime"
                         :class="{ 'is-invalid': submitted && errors.has('startTime') }"
                         :config="options" />

            <div v-if="submitted && errors.has('startTime')" class="invalid-feedback">{{ errors.first('startTime') }}</div>
          </div>

          <div class="form-group">
            <label class="control-label">Becsült időigény (óra)</label>
            <input type="number"
                   class="form-control"
                   name="workHours"
                   placeholder="Ide írjon!"
                   v-model="task.workHours"
                   v-validate="'required'"
                   :class="{ 'is-invalid': submitted && errors.has('workHours') }">

            <div v-if="submitted && errors.has('workHours')" class="invalid-feedback">{{ errors.first('workHours') }}</div>
          </div>

          <div class="form-group">
            <label class="control-label">Leírás</label>
            <textarea rows="3"
                      class="form-control"
                      name="description"
                      placeholder="Ide írjon!"
                      v-model="task.description"
                      v-validate="'required'"
                      :class="{ 'is-invalid': submitted && errors.has('description') }">
            </textarea>

            <div v-if="submitted && errors.has('description')" class="invalid-feedback">{{ errors.first('description') }}</div>
          </div>

          <button class="btn btn-success float-right" type="submit">Létrehozás</button>
        </form>
      </b-card>
    </b-collapse>

  </div>
</template>
<script>
  import { taskService } from "../../../services/task-service"
  import { userService } from "../../../services/user-service"
  import { commonService } from '../../../services/common-service'

  import Multiselect from 'vue-multiselect'

  export default {
    components: { Multiselect },
    props: {
      taskToEdit: Object,
      projectId: String
    },
    data() {
      return {
        users: [],
        worker: {},
        submitted: false,
        task: {},
        options: {
          format: 'YYYY.MM.DD.',
          locale: 'hu'
        }
      }
    },
    methods: {

      loadUsers: function () {
        let app = this;

        userService.loadUsers("")
          .then(function (res) {
            if (res.data.users != null) {
              for (let i = 0; i < res.data.users.length; i++) {
                let user = {
                  name: res.data.users[i].firstName + " " + res.data.users[i].lastName,
                  id: res.data.users[i].id
                };
                app.users.push(user);

                if (i == 0) {
                  app.worker = user;
                }
              }
            }
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      createTask: function () {
        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (valid) {

            let app = this;
            $("#new-task-button").click();

            this.task.workerId = this.worker.id;
            this.task.projectId = this.projectId;

            taskService.createTask(this.task)
              .then(function (response) {
                commonService.getToast(app)({
                  type: 'success',
                  title: 'Taszk létrehozása sikeres!'
                });

                app.$emit('taskCreated');
              })
              .catch(function (error) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Taszk létrehozása sikertelen!'
                });
              });
          }
        });
      }
    },
    created() {
      this.loadUsers();

      if (this.taskToEdit != null) {
        this.task = this.taskToEdit;
      }
    }
  }
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style>

</style>
