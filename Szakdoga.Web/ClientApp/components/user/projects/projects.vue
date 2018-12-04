<template>
  <div>
    <b-row>
      <b-col md="6" sm="12">
        <h1>Projektjeim</h1>
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

      <template slot="details" slot-scope="row">
        <router-link class="navbar-brand" :to="{name: 'user-project-details', params: { id: row.item.id } }">
          <icon :icon="['fas', 'info-circle']" style="width: 30px; height: 30px;" />
        </router-link>
        <router-link class="navbar-brand" :to="{name: 'project-tasks', params: { id: row.item.id } }">
          <icon :icon="['fas', 'tasks']" style="width: 30px; height: 30px; color: #ffc107" />
        </router-link>
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
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        user: JSON.parse(window.localStorage.getItem('lbUser')).data,
        projects: [],
        projectfields: [
          { key: 'title', label: 'Megnevezés', sortable: true },
          { key: 'startTime', label: 'Kezdet', sortable: true },
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

      onFiltered: function (filteredItems) {
        this.totalRows = filteredItems.length
        this.currentPage = 1
      },

      loadProjects: function () {

        let app = this;
        projectService.getProjectsByUser(this.user.id)
          .then(function (res) {
            app.projects = res.data;

            for (var i = 0; i < app.projects.length; i++) {
              app.projects[i].deadline = commonService.dateTimeformat(app.projects[i].deadline);
            }

            for (var i = 0; i < app.projects.length; i++) {
              app.projects[i].startTime = commonService.dateTimeformat(app.projects[i].startTime);
            }

            app.totalRows = app.projects.length;
          })
          .catch(function (err) {
            console.error(err);
          });
      }
    },
    created() {
      this.loadProjects();
    }
  }
</script>

<style lang="less" scoped>

</style>
