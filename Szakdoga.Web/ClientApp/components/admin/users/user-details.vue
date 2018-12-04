<template>
  <div v-if="user" class="container-fluid">
    <div class="row d-flex justify-content-center img-thumbnail m-0">
      <div class="background-picture" v-bind:style="{ backgroundImage: 'url(' + '/images/' + user.profilePicture + ')' }"></div>
    </div>

    <div class="row d-flex justify-content-center mt-3">
      <div class="col-md-10 text-center">
        <h1>{{user.firstName}} {{ user.lastName}}</h1>
      </div>
      <div class="col-md-1 d-flex align-items-center">
        <icon v-if="!edit" id="edit" @click="editUser" :icon="['fas', 'user-edit']" style="color: #007bff; width: 30px; height: 30px;" />
      </div>
    </div>

    <form @submit.prevent="updateUser">
      <div class="row mt-3">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="firstname">Vezetéknév</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="firstname" class="form-control" v-on:change="$emit('change', $event.target.changed)" v-model="user.firstName" readonly />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="lastName">Keresztnév</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="lastName" class="form-control" v-model="user.lastName" readonly />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">Felhasználónév</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="userName" class="form-control" v-model="user.username" readonly />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">E-mail</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <input id="email" class="form-control" v-model="user.email" readonly />
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-sm-12 col-md-5 d-flex align-items-center justify-content-end">
          <label class="m-0 display-4" for="userName">Admin</label>
        </div>
        <div class="col-sm-12 offset-md-1 col-md-6">
          <b-form-checkbox v-model="user.isAdmin" disabled></b-form-checkbox>
        </div>
      </div>

      <transition name="fade">
        <button v-if="edit" id="save-button" class="btn btn-success mt-3 ml-2 float-right" type="submit">Mentés</button>
      </transition>
      <transition name="fade">
        <button v-if="edit" id="cancel" @click="finishEdit" type="button" class="btn btn-danger mt-3 float-right">Elvetés</button>
      </transition>
      
    </form>

  </div>
</template>
<script>
  import { userService } from "../../../services/user-service";
  import { commonService } from "../../../services/common-service";

  export default {
    data() {
      return {
        edit: false,
        user: {}
      }
    },

    methods: {

      editUser: function () {
        this.edit = true;
        $("input").removeAttr("readonly");
        $("input").removeAttr("disabled");
      },

      finishEdit: function () {
        this.edit = false;
        $("input").attr("readonly", true);
        $("input").attr("disabled", true);
      },

      updateUser: function () {

        this.finishEdit();
        let app = this;

        userService.updateUser(this.user)
          .then(function (res) {
            commonService.getToast(app)({
              type: 'success',
              title: 'Munkatárs frissítése sikeres!'
            });
          })
          .catch(function (err) {
            commonService.getToast(app)({
              type: 'error',
              title: 'Munkatárs frissítése sikertelen!'
            });
          });
      }
    },

    created() {
      let id = this.$route.params.id;
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
    }
  }
</script>
<style scoped>

  .background-picture {
    height: 400px;
    width: 500px;
    background-position: center;
    background-size: contain;
    background-repeat: no-repeat;
  }

</style>
