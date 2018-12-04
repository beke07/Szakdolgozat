<template>
  <div id="new-rank">
    <div class="mb-3">
      <b-btn id="new-rank-button" v-b-toggle.collapse1 variant="primary"><icon :icon="['fas', 'plus']" class="mr-2" />Új tevékenység hozzáadása</b-btn>
      <b-collapse id="collapse1" class="mt-2">
        <b-card>

          <form @submit.prevent="createActivity">

            <div class="form-group">
              <label class="control-label">Elnevezés</label>
              <input type="text"
                     class="form-control"
                     name="activityName"
                     placeholder="Ide írjon!"
                     v-model="activityName"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('activityName') }">

              <div v-if="submitted && errors.has('activityName')" class="invalid-feedback">{{ errors.first('activityName') }}</div>
            </div>

            <button class="btn btn-success float-right" type="submit">Létrehozás</button>
          </form>
        </b-card>
      </b-collapse>
    </div>
  </div>
</template>
<script>
  import { taskService } from "../../../services/task-service";
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        activityName: "",
        submitted: false
      }
    },
    methods: {
      createActivity: function () {

        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (valid) {

            let app = this;
            $("#new-rank-button").click();

            taskService.createActivity(this.activityName)
              .then(function (res) {

                commonService.getToast(app)({
                  type: 'success',
                  title: 'Tevékenység létrehozása sikeres!'
                });

                app.rankName = "";
                app.$emit('activityCreated');

              })
              .catch(function (error) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Tevékenység létrehozása sikertelen!'
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
