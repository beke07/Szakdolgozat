<template>
  <div class="col-sm-5">
    <h1 class="display-4 text-center mb-3" style="font-size: 45px;">Válasszon felületet!</h1>

    <div class="row">
      <div class="col-xs-12 col-sm-6 pr-1">
        <button @click="adminLogin" class="btn btn-success w-100">Adminisztrátor</button>
      </div>
      <div class="col-xs-12 col-sm-6 pl-1">
        <button @click="userLogin" class="btn btn-secondary w-100">Munkatárs</button>
      </div>
    </div>
  </div>
</template>

<script>
  import { mapActions } from 'vuex'
  import { userService } from '../services/user-service';

  export default {
    data() {
      return {
        user: Object
      }
    },
    methods: {
      ...mapActions(['setIsLoggedIn']),
      ...mapActions(['setloggedInAsAdmin']),
      ...mapActions(['setloggedInAsUser']),

      adminLogin: function () {
        this.setRole("admin");
        this.setloggedInAsAdmin({ loggedIn: true });
        this.$router.push('/admin');
      },

      userLogin: function () {
        this.setRole("user");
        this.setloggedInAsUser({ loggedIn: true });
        this.$router.push('/home');
      },

      setRole: function(role) {
        let authUser = JSON.parse(window.localStorage.getItem('lbUser'));
        authUser.data.role = role;
        window.localStorage.setItem('lbUser', JSON.stringify(authUser));
      }
    }
    
  }
</script>

<style lang="less" scoped>
</style>
