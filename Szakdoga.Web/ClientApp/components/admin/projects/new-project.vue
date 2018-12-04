<template>
  <div class="new-project">
    <div class="mb-3">
      <b-btn id="new-project-button" v-b-toggle.collapse1 variant="primary"><icon :icon="['fas', 'plus']" class="mr-2" />Új projekt hozzáadása</b-btn>
      <b-collapse id="collapse1" class="mt-2">
        <b-card>
          <form @submit.prevent="createProject">

            <div class="form-group">
              <label class="control-label">Megnevezés</label>
              <input type="text"
                     class="form-control"
                     name="title"
                     placeholder="Ide írjon!"
                     v-model="project.title"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('title') }">

              <div v-if="submitted && errors.has('title')" class="invalid-feedback">{{ errors.first('title') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Becsült időigény (óra)</label>
              <input type="number"
                     class="form-control"
                     name="sumWorkHours"
                     placeholder="Ide írjon!"
                     v-model="project.sumWorkHours"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('sumWorkHours') }">

              <div v-if="submitted && errors.has('sumWorkHours')" class="invalid-feedback">{{ errors.first('sumWorkHours') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Kezdet</label>
              <date-picker v-model="project.startTime"
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
              <label class="control-label">Határidő</label>
              <date-picker v-model="project.deadline"
                           placeholder="Válasszon!"
                           class="form-control"
                           v-validate="'required'"
                           autocomplete="off"
                           name="deadline"
                           :class="{ 'is-invalid': submitted && errors.has('deadline') }"
                           :config="options" />

              <div v-if="submitted && errors.has('deadline')" class="invalid-feedback">{{ errors.first('deadline') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Projekt vezetője</label>
              <multiselect v-model="projectLeader"
                           :options="users"
                           placeholder="Válasszon!"
                           name="projectLeader"
                           :deselectLabel="'Enter a kivételhez'"
                           :selectLabel="'Enter a kiválasztáshoz'"
                           :selectedLabel="'Kiválasztva'"
                           v-validate="'required'"
                           :class="{ 'is-invalid': submitted && errors.has('projectLeader') }"
                           label="name"
                           @input="input"
                           @remove="removeLeader"
                           track-by="name">
              </multiselect>

              <div v-if="submitted && errors.has('projectLeader')" class="invalid-feedback">{{ errors.first('projectLeader') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Projekten dolgozók</label>
              <multiselect v-model="workers" :options="users" :multiple="true" :placeholder="'Válasszon!'" :deselectLabel="'Enter a kivételhez'" :selectLabel="'Enter a kiválasztáshoz'" :selectedLabel="'Kiválasztva'" :close-on-select="false" :clear-on-select="false" :preserve-search="true" label="name" track-by="id">
                <template slot="selection" slot-scope="{ values, search, isOpen }">
                  <span class="multiselect__single" v-if="values.length &amp;&amp; !isOpen">{{ values.length }} dolgozó kiválasztva</span>
                </template>
              </multiselect>
            </div>

            <button class="btn btn-success float-right" type="submit">Létrehozás</button>
          </form>
        </b-card>
      </b-collapse>
    </div>
  </div>
</template>

<script>
  import { projectService } from "../../../services/project-service";
  import { commonService } from "../../../services/common-service";
  import { userService } from "../../../services/user-service";
  import Multiselect from 'vue-multiselect'

  export default {
    components: { Multiselect },
    data() {
      return {
        projectLeader: {},
        submitted: false,
        options: {
          format: 'YYYY.MM.DD.',
          locale: 'hu'
        },
        users: [],
        workers: [],
        project: {
          workers: []
        }
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
                  app.projectLeader = user;
                  app.workers.push(user);
                  app.users[0].$isDisabled = true;
                }
              }

            }
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      createProject: function () {

        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (valid) {

            let app = this;
            $("#new-project-button").click();

            projectService.createProject(this.project, this.workers, this.projectLeader)
              .then(function (res) {
                commonService.getToast(app)({
                  type: 'success',
                  title: 'Projekt létrehozása sikeres!'
                });

                app.$emit('projectCreated');
              })
              .catch(function (err) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Projekt már létezik!'
                });
              });
          }
        });
      }
    },
    created() {
      this.loadUsers();
    }
  }
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style>

</style>
