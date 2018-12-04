<template>
  <div id="profil" v-if="user">

    <div class="row pr-3 justify-content-center">
      <div class="col-sm-4">
        <h1>Profil</h1>
        <h6>Havi ledolgozott órák : <strong>{{hours}}</strong></h6>
      </div>
      <div class="col-sm-8 d-flex justify-content-center flex-wrap img-thumbnail m-0">
        <div class="background-picture" v-bind:style="{ backgroundImage: 'url(' + '/images/' + user.profilePicture + ')' }"></div>
        <h1>{{user.firstName}} {{user.lastName}}</h1>
      </div>
    </div>

    <form @submit.prevent="updateUser">

      <div class="row mt-5">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="image">Profilkép</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <b-form-file v-model="image"
                       name="image"
                       placeholder="Válasszon egy fájlt...">
          </b-form-file>
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="firstname">Vezetéknév</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="firstname" class="form-control" v-on:change="$emit('change', $event.target.changed)" v-model="user.firstName" />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="lastName">Keresztnév</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="lastName" class="form-control" v-model="user.lastName" />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">Felhasználónév</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="userName" class="form-control" v-model="user.username" />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">E-mail</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="email" class="form-control" v-model="user.email" />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">Régi jelszó</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input name="oldPassword"
                 id="oldPassword"
                 v-model="oldPassword"
                 v-validate="'min:4|max:25'"
                 type="password"
                 placeholder="Ide írjon..."
                 class="form-control"
                 :class="{ 'is-invalid': submitted && errors.has('oldPassword') }" />
          <div v-if="submitted && errors.has('oldPassword')" class="invalid-feedback">{{ errors.first('oldPassword') }}</div>
        </div>

      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">Új jelszó</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input name="password"
                 id="password"
                 v-model="password"
                 ref="password"
                 type="password"
                 v-validate="'min:4|max:25|confirmed:confirmPassword'"
                 placeholder="Ide írjon..."
                 class="form-control"
                 :class="{ 'is-invalid': submitted && errors.has('password') }" />

          <div v-if="submitted && errors.has('password')" class="invalid-feedback">{{ errors.first('password') }}</div>
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">Új jelszó mégegyszer</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input name="confirmPassword"
                 id="confirmPassword"
                 type="password"
                 ref="confirmPassword"
                 v-model="confirmPassword"
                 v-validate="'min:4|max:25|confirmed:password'"
                 placeholder="Ide írjon..."
                 class="form-control"
                 :class="{ 'is-invalid': submitted && errors.has('confirmPassword') }" />

          <div v-if="submitted && errors.has('confirmPassword')" class="invalid-feedback">{{ errors.first('confirmPassword') }}</div>
        </div>
      </div>

      <button id="save-button" class="btn btn-success mt-3 ml-2 float-right" type="submit">Mentés</button>
    </form>

  </div>
</template>
<script>
  import { userService } from "../../services/user-service";
  import { commonService } from "../../services/user-service";

  export default {
    data() {
      return {
        hours: 0,
        user: {},
        oldPassword: "",
        password: "",
        confirmPassword: "",
        submitted: false,
        image: null,
      }
    },
    methods: {

      updateUser: function () {

        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (valid) {

            let app = this;

            userService.createUser(this.user, this.image)
              .then(function (response) {

                app.user = response.data.user;

                if (app.password !== "") {
                  userService.changePassword(app.user.id, app.oldPassword, app.password)
                    .then(function (res) {
                      commonService.getToast(app)({
                        type: 'success',
                        title: 'Munkatárs frissítése sikeres!'
                      });
                    })
                    .catch(function (error) {
                      commonService.getToast(app)({
                        type: 'error',
                        title: 'Hibás régi jelszót adott meg!'
                      });
                    });
                }

              })
              .catch(function (error) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Munkatárs frissítése sikertelen!'
                });
              });
          }
        });
      },

      getHoursToUser: function (id) {

        let app = this;
        userService.getHoursToUser(id)
          .then(function (res) {
            app.hours = res.data;
          })
          .catch(function (err) {
            console.error(err);
          });
      }

    },
    created() {
      let id = JSON.parse(window.localStorage.getItem('lbUser')).data.id;
      let app = this;

      if (id !== "") {
        userService.getUserById(id)
          .then(function (res) {
            if (res.data.user != null) {
              app.user = res.data.user;
            }
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Munkatárs betöltése sikertelen!'
            });
          });
      }

      this.getHoursToUser(id);
    }
  }
</script>

<style lang="less" scoped>
  .background-picture {
    height: 300px;
    width: 100%;
    background-position: center;
    background-size: contain;
    background-repeat: no-repeat;
  }
</style>
