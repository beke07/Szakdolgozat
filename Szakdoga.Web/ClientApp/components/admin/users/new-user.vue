<template>
  <div id="new-user">
    <div class="mb-3">
      <b-btn id="new-user-button" v-b-toggle.collapse1 variant="primary"><icon :icon="['fas', 'plus']" class="mr-2" />Új munkatárs hozzáadása</b-btn>
      <b-collapse id="collapse1" class="mt-2">
        <b-card>
          <form @submit.prevent="createUser">

            <div class="form-group">
              <label class="control-label">Vezetéknév</label>
              <input type="text"
                     class="form-control"
                     name="firstname"
                     placeholder="Ide írjon!"
                     v-model="user.firstname"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('firstname') }">

              <div v-if="submitted && errors.has('firstname')" class="invalid-feedback">{{ errors.first('firstname') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Keresztnév</label>
              <input type="text"
                     class="form-control"
                     name="lastname"
                     placeholder="Ide írjon!"
                     v-model="user.lastname"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('lastname') }">

              <div v-if="submitted && errors.has('lastname')" class="invalid-feedback">{{ errors.first('lastname') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Felhasználónév</label>
              <input type="text"
                     class="form-control"
                     name="username"
                     placeholder="Ide írjon!"
                     v-model="user.username"
                     v-validate="'required'"
                     :class="{ 'is-invalid': submitted && errors.has('username') }">

              <div v-if="submitted && errors.has('username')" class="invalid-feedback">{{ errors.first('username') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">E-mail</label>
              <input type="text"
                     class="form-control"
                     name="email"
                     placeholder="Ide írjon!"
                     v-model="user.email"
                     v-validate="'required|email'"
                     :class="{ 'is-invalid': submitted && errors.has('email') }">

              <div v-if="submitted && errors.has('email')" class="invalid-feedback">{{ errors.first('email') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Beosztás</label>
              <b-form-select v-model="user.rankId"
                             name="rankId"
                             :options="options"
                             v-validate="'required'"
                             :class="{ 'is-invalid': submitted && errors.has('rankId') }" />

              <div v-if="submitted && errors.has('rankId')" class="invalid-feedback">{{ errors.first('rankId') }}</div>
            </div>

            <div class="form-group">
              <label class="control-label">Profilkép</label>
              <b-form-file v-model="image"
                           name="image"
                           placeholder="Válasszon egy fájlt...">
              </b-form-file>
            </div>

            <div class="form-group">
              <b-form-checkbox v-model="user.isAdmin">
                Adminisztrátorként regisztrálom
              </b-form-checkbox>
            </div>

            <button class="btn btn-success float-right" type="submit">Létrehozás</button>
          </form>
        </b-card>
      </b-collapse>
    </div>
  </div>
</template>

<script>
  import { userService } from "../../../services/user-service";
  import { rankService } from "../../../services/rank-service";
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        submitted: false,
        user: {
          rankId: null
        },
        options: [],
        image: null
      }
    },
    methods: {

      createUser: function () {

        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (valid) {

            let app = this;
            $("#new-user-button").click();

            userService.createUser(this.user, this.image)
              .then(function (response) {
                commonService.getToast(app)({
                  type: 'success',
                  title: 'Munkatárs létrehozása sikeres!'
                });

                app.user = {
                  rankId: null
                };
                app.$emit('userCreated');
              })
              .catch(function (error) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Munkatárs már létezik!'
                });
              });
          }
        });
      },

      loadOptions: function () {

        let app = this;

        rankService.getRanks()
          .then(function (res) {

            app.options.push({ value: null, text: 'Kérem, válasszon!' });

            for (let i = 0; i < res.data.length; i++) {
              app.options.push(
                {
                  value: res.data[i].id,
                  text: res.data[i].name
                });
            }
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Nem sikerült betölteni a beosztásokat!'
            });
          });
      }
    },
    created() {
      this.loadOptions();
    }
  }
</script>
<style scoped>
</style>
