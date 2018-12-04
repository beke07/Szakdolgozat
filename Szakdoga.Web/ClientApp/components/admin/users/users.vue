<template>
  <div class="container-fluid">
    <new-user v-on:userCreated="loadUsers"></new-user>

    <b-row>
      <b-col md="6" sm="12">
        <h1>Munkatársak</h1>
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
             :items="users"
             :fields="userfields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :sort-by.sync="sortBy"
             :sort-desc.sync="sortDesc"
             :sort-direction="sortDirection"
             @filtered="onFiltered">

      <template slot="isAdmin" slot-scope="row">
        <div class="p-0 d-flex justify-content-center align-items-center">
          <icon v-if="row.item.isAdmin" :icon="['fas', 'check']" style="color:green; width: 30px; height: 30px;" />
        </div>
      </template>

      <template slot="details" slot-scope="row">
        <router-link class="navbar-brand" :to="{name: 'user-details', params: { id: row.item.id } }">
          <icon :icon="['fas', 'info-circle']" style="width: 30px; height: 30px;" />
        </router-link>
        <icon @click="deleteUser(row.item.id)" :icon="['fas', 'trash']" style="width: 30px; height: 30px; color: #dc3545;" />
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
  import { userService } from "../../../services/user-service";
  import newUser from "./new-user";
  import { commonService } from "../../../services/common-service";

  export default {
    components: { newUser: newUser },
    data() {
      return {
        users: [],
        searchName: "",
        userfields: [
          { key: 'firstName', label: 'Vezetéknév', sortable: true },
          { key: 'lastName', label: 'Keresztnév', sortable: true },
          { key: 'rankName', label: 'Beosztás', sortable: true },
          { key: 'isAdmin', label: 'Admin', sortable: true },
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

      onFiltered(filteredItems) {
        this.totalRows = filteredItems.length
        this.currentPage = 1
      },

      loadUsers: function () {
        let app = this;

        userService.loadUsers(this.searchName)
          .then(function (res) {
            if (res.data.users != null) {
              app.users = res.data.users;
              app.totalRows = app.users.length;
            }
          })
          .catch(function (err) {
            console.error(err);

            commonService.getToast(app)({
              type: 'error',
              title: 'Munkatársak betöltése sikertelen!'
            });
          });
      },

      deleteUser: function (id) {

        let app = this;

        commonService.getComfirm(this)
          .then((result) => {
            if (result.value) {
              userService.deleteUser(id)
                .then(function (res) {
                  commonService.getToast(app)({
                    type: 'success',
                    title: 'Munkatárs törlése sikeres!'
                  });

                  app.loadUsers();
                })
                .catch(function (err) {

                  commonService.getToast(app)({
                    type: 'error',
                    title: 'Munkatárs törlése sikertelen!'
                  });
                });
            }
          });
      }
    },
    props: {
      user: {}
    },
    created() {
      this.loadUsers();
    }
  }
</script>
<style scoped>

</style>
