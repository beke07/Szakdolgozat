<template>
  <div class="container-fluid">
    <new-rank v-on:rankCreated="loadRanks"></new-rank>

    <b-row>
      <b-col md="6" sm="12">
        <h1>Beosztások</h1>
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
             :items="ranks"
             :fields="rankfields"
             :current-page="currentPage"
             :per-page="perPage"
             :filter="filter"
             :sort-by.sync="sortBy"
             :sort-desc.sync="sortDesc"
             :sort-direction="sortDirection"
             @filtered="onFiltered">

      <template slot="name" slot-scope="row">
        <div class="text-center">
          {{row.item.name}}
        </div>
      </template>

      <template slot="details" slot-scope="row">
        <div class="text-center">
          <icon @click="deleteRank(row.item.id)" :icon="['fas', 'trash']" style="width: 30px; height: 30px; color: #dc3545;" />
        </div>
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
  import { rankService } from "../../../services/rank-service";
  import newRank from "./new-rank";
  import { commonService } from "../../../services/common-service";

  export default {
    components: { newRank: newRank },
    data() {
      return {
        ranks: [],
        rankfields: [
          { key: 'name', label: 'Elnevezés', sortable: true },
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

      loadRanks: function () {
        let app = this;

        rankService.getRanks()
          .then(function (res) {
            if (res.data != null) {
              app.ranks = res.data;
              app.totalRows = app.ranks.length;
            }
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Beosztások betöltése sikertelen!'
            });
          });
      },

      deleteRank: function (id) {

        let app = this;

        commonService.getComfirm(this)
          .then((result) => {
            if (result.value) {
              rankService.deleteRank(id)
                .then(function (res) {
                  commonService.getToast(app)({
                    type: 'success',
                    title: 'Beosztás törlése sikeres!'
                  });

                  app.loadRanks();
                })
                .catch(function (err) {
                  commonService.getToast(app)({
                    type: 'error',
                    title: 'Beosztás törlése sikertelen!'
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
      this.loadRanks();
    }
  }
</script>
<style scoped>
</style>
