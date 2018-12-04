<template>
  <div class="col-sm-5">
    <h1 class="display-4 text-center mb-3" style="font-size: 45px;">Bejelentkezés</h1>
    <form @submit.prevent="loginUser">
      <div class="form-row">

        <div class="col-md-12 mb-3">
          <label class="display-4" for="username">Felhasználónév</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><icon :icon="['fas', 'user']" /></span>
            </div>
            <input required class="form-control" id="username" placeholder="Felhasználónév" v-model="user.username" />
          </div>
        </div>

        <div class="col-md-12 mb-3">
          <label class="display-4" for="password">Jelszó</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <span class="input-group-text"><icon :icon="['fas', 'unlock-alt']" /></span>
            </div>
            <input required class="form-control" id="password" placeholder="Jelszó" type="password" v-model="user.password" />
          </div>
        </div>
      </div>

      <button class="btn btn-secondary w-50 mt-3 float-right" type="submit">Bejelentkezés</button>

    </form>
  </div>
</template>

<script>
  import { mapActions } from 'vuex'
  import { userService } from '../services/user-service';
  import { commonService } from "../services/common-service";

  export default {
    data() {
      return {
        user: {}
      }
    },
    methods: {
      ...mapActions(['setIsLoggedIn']),
      ...mapActions(['setloggedInAsAdmin']),
      ...mapActions(['setloggedInAsUser']),

      loginUser: function () {
        const authUser = {}
        let app = this;

        window.localStorage.removeItem('lbUser');
        this.setIsLoggedIn({ loggedIn: false });
        this.setloggedInAsAdmin({ loggedIn: false });
        this.setloggedInAsUser({ loggedIn: false });

        userService.authenticate(this.user)
          .then(function (res) {

            app.user.userName = "";
            app.user.password = "";

            let role = res.data.role;
            authUser.data = res.data.auth_user;
            authUser.data.role = role;

            if (res.data.auth_user.firstSignIn) {
              window.localStorage.setItem('lbUser', JSON.stringify(authUser));
              app.$router.push('/password');
            }
            else {

              app.setIsLoggedIn({ loggedIn: true });
              window.localStorage.setItem('lbUser', JSON.stringify(authUser));

              if (role === 'admin') {
                app.$router.push('/admin/admin-user-login');
              }
              else if (role === 'user') {
                app.setloggedInAsUser({ loggedIn: true });
                app.$router.push('/home');
              }
            }
          })
          .catch(function (err) {
            app.setIsLoggedIn({ loggedIn: false });
            commonService.getToast(app)({
              type: 'error',
              title: 'Hibás felhasználónév vagy jelszó!'
            });
          });
      }
    }
  }
</script>

<style lang="less" scoped>

</style>
