<template>
  <div class="container-fluid">
    <new-project v-on:projectCreated="loadProjects"></new-project>

    <b-row>
      <b-col md="6" sm="12">
        <h1>Projektek</h1>
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
             :items="projects"
             :fields="projectfields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :sort-by.sync="sortBy"
             :sort-desc.sync="sortDesc"
             :sort-direction="sortDirection"
             @filtered="onFiltered">

      <template slot="deadline" slot-scope="row">
        {{row.item.deadline}}
      </template>

      <template slot="details" slot-scope="row">
        <router-link class="navbar-brand" :to="{name: 'project-details', params: { id: row.item.id } }">
          <icon :icon="['fas', 'info-circle']" style="width: 30px; height: 30px;" />
        </router-link>
        <icon @click="deleteProject(row.item.id)" :icon="['fas', 'trash']" style="width: 30px; height: 30px; color: #dc3545;" />
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
  import { projectService } from "../../../services/project-service";
  import newProject from "./new-project";
  import { commonService } from "../../../services/common-service";

  export default {
    components: { newProject: newProject },
    data() {
      return {
        projects: [],
        projectfields: [
          { key: 'title', label: 'Megnevezés', sortable: true },
          { key: 'deadline', label: 'Határidő', sortable: true },
          { key: 'details', label: 'Részletek' }
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
      loadProjects: function () {

        let app = this;
        projectService.getProjects()
          .then(function (res) {
            app.projects = res.data;
            for (var i = 0; i < app.projects.length; i++) {
              app.projects[i].deadline = commonService.dateTimeformat(app.projects[i].deadline);
            }
          })
          .catch(function (err) {
            console.error(err);
          });
      },

      onFiltered: function (filteredItems) {
        this.totalRows = filteredItems.length
        this.currentPage = 1
      },

      deleteProject: function (id) {

        let app = this;

        commonService.getComfirm(this)
          .then((result) => {
            if (result.value) {
              projectService.deleteProject(id)
                .then(function (res) {
                  commonService.getToast(app)({
                    type: 'success',
                    title: 'Projekt törlése sikeres!'
                  });

                  app.loadProjects();
                })
                .catch(function (err) {
                  commonService.getToast(app)({
                    type: 'error',
                    title: 'Projekt törlése sikertelen!'
                  });
                });
            }
          });
      }
    },
    created() {
      this.loadProjects();
    }
  }
</script>
<style>
</style>
