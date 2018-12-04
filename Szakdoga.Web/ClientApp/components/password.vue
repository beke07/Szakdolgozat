<template>
  <div class="col-12 col-md-4">
    <h1 class="display-4 text-center mb-3" style="font-size: 45px;">Jelszó megváltoztatása</h1>

    <form @submit.prevent="updateUserPassword">
      <div class="row">
        <div class="col-sm-12 form-group">

          <label for="oldPassword">Régi jelszó</label>
          <input name="oldPassword"
                 id="oldPassword"
                 v-model="oldPassword"
                 v-validate="'required|min:6|max:25'"
                 type="password"
                 placeholder="Ide írjon..."
                 class="form-control"
                 :class="{ 'is-invalid': submitted && errors.has('oldPassword') }"/>

          <div v-if="submitted && errors.has('oldPassword')" class="invalid-feedback">{{ errors.first('oldPassword') }}</div>

        </div>

        <div class="col-sm-12 form-group">
          <label for="password">Új jelszó</label>
          <input  name="password"
                  id="password"
                  v-model="password"
                  ref="password"
                  type="password"
                  v-validate="'required|min:6|max:25|confirmed:confirmPassword'"
                  placeholder="Ide írjon..."
                  class="form-control"
                  :class="{ 'is-invalid': submitted && errors.has('password') }"/>
          
          <div v-if="submitted && errors.has('password')" class="invalid-feedback">{{ errors.first('password') }}</div>
        </div>

        <div class="col-sm-12 form-group">
          <label for="confirmPassword">Új jelszó mégegyszer</label>
          <input  name="confirmPassword"
                  id="confirmPassword"
                  type="password"
                  ref="confirmPassword"
                  v-model="confirmPassword"
                  v-validate="'required|min:6|max:25|confirmed:password'"
                  placeholder="Ide írjon..."
                  class="form-control"
                  :class="{ 'is-invalid': submitted && errors.has('confirmPassword') }" />

          <div v-if="submitted && errors.has('confirmPassword')" class="invalid-feedback">{{ errors.first('confirmPassword') }}</div>

        </div>
      </div>
      <button class="btn btn-success float-right" type="submit">Mentés</button>
    </form>

  </div>
</template>

<script>
  import { userService } from '../services/user-service';
  import { commonService } from '../services/common-service';
  import Password from 'vue-password-strength-meter'

  export default {
    components: { Password },
    data() {
      return {
        oldPassword: "",
        password: "",
        confirmPassword: "",
        submitted: false
      }
    },
    methods: {

      updateUserPassword: function () {

        this.submitted = true;

        this.$validator.validate().then(valid => {
          if (this.oldPassword === this.password) {
            commonService.getToast(this)({
              type: 'error',
              title: 'A régi és az új jelszónak különböznie kell!'
            });

            valid = false;
          }

          if (valid) {

            let app = this;
            const status = JSON.parse(window.localStorage.getItem('lbUser'));

            userService.changePassword(status.data.id, this.oldPassword, this.password)
              .then(function (res) {
                commonService.getToast(app)({
                  type: 'success',
                  title: 'Sikeres jelszó változatás, jelentkezzen be!'
                });

                window.localStorage.removeItem('lbUser');
                app.$router.push('/login');
              })
              .catch(function (err) {
                commonService.getToast(app)({
                  type: 'error',
                  title: 'Hibás régi jelszót adott meg!'
                });
              });
          }
        });
      }
    }
  }
</script>

<style lang="less" scoped>
</style>
