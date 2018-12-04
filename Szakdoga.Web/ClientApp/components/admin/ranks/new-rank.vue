<template>
  <div id="new-rank">
    <div class="mb-3">
      <b-btn id="new-rank-button" v-b-toggle.collapse1 variant="primary"><icon :icon="['fas', 'plus']" class="mr-2" />Új beosztás hozzáadása</b-btn>
      <b-collapse id="collapse1" class="mt-2">
        <b-card>

          <form @submit.prevent="createRank">

            <div class="form-group">
              <label class="control-label">Elnevezés</label>
              <input type="text"
                     class="form-control"
                     name="rankName"
                     placeholder="Ide írjon!"
                     v-model="rankName"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('rankName') }">

              <div v-if="submitted && errors.has('rankName')" class="invalid-feedback">{{ errors.first('rankName') }}</div>
            </div>

            <button class="btn btn-success float-right" type="submit">Létrehozás</button>
          </form>
        </b-card>
      </b-collapse>
    </div>
  </div>
</template>
<script>
  import { rankService } from "../../../services/rank-service";
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        rankName: "",
        submitted : false
      }
    },
    methods: {
      createRank: function () {

        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (valid) {

            let app = this;
            $("#new-rank-button").click();

            rankService.createRank(this.rankName)
              .then(function (res) {
                commonService.getToast(app)({
                  type: 'success',
                  title: 'Beosztás létrehozása sikeres!'
                });

                app.rankName = "";
                app.$emit('rankCreated');

              })
              .catch(function (error) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Beosztás létrehozása sikertelen!'
                });
              });
          }
        });
      }
    }
  }
</script>
<style scoped>

</style>
