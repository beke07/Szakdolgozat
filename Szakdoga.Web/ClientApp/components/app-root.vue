<template>
  <div id="app" class="container-fluid h-100 p-5">

    <div v-if="loading" style="z-index: 2;" class="row h-75 w-100 position-absolute d-flex justify-content-center align-items-center">
      <div class="lds-dual-ring"></div>
    </div>

    <div class="row" v-bind:class="{ 'opacity-50': loading }" v-if="this.$route.name != 'login' && this.$route.name != 'password' && this.$route.name != 'admin-user-login'">
      <div class="col-sm-3">
        <nav-menu></nav-menu>
      </div>
      <div class="col-sm-7">
        <router-view></router-view>
      </div>
      <div class="col-sm-2">

      </div>
    </div>

    <div class="row h-75 d-flex justify-content-center align-items-center" v-bind:class="{ 'opacity-50': loading }" v-if="this.$route.name == 'login' || this.$route.name == 'password' || this.$route.name == 'admin-user-login'">
      <router-view></router-view>
    </div>
  </div>

</template>
<script>
  import NavMenu from './nav-menu'
  import { mapActions, mapState } from 'vuex'
  import { commonService } from "../services/common-service";

  export default {
    components: {
      'nav-menu': NavMenu
    },

    data() {
      return {
      }
    },

    computed: {
      ...mapState({
        loading: state => state.loading
      })
    },

    methods: {
      ...mapActions(['setloggedInAsAdmin']),
      ...mapActions(['setloggedInAsUser']),

      loginAuth: function () {
        let app = this;
        const status = JSON.parse(window.localStorage.getItem('lbUser'));

        if (status === null || status === undefined) {
          app.$router.push('/login');
        } else if (status.data.role === 'admin') {
          this.setloggedInAsAdmin({ loggedIn: true });
          if (app.$route.name == 'login' || app.$route.path == '/') {
            app.$router.push('/admin');
          }
        } else if (status.data.role === 'user') {
          this.setloggedInAsUser({ loggedIn: true });
          if (app.$route.name == 'login' || app.$route.path == '/') {
            app.$router.push('/home');
          }
        }
      }
    },
    created() {
      this.loginAuth();
    }
  }
</script>
<style>
  @import url('https://fonts.googleapis.com/css?family=Raleway:200');

  @keyframes lds-dual-ring {
    0% {
      transform: rotate(0deg);
    }

    100% {
      transform: rotate(360deg);
    }
  }

  html, body {
    width: 100%;
    height: 100%;
    overflow-x: hidden;
  }

  body {
    font-family: 'Raleway', sans-serif;
  }

  .lds-dual-ring {
    display: inline-block;
    width: 96px;
    height: 96px;
  }

  .lds-dual-ring:after {
    content: " ";
    display: block;
    width: 64px;
    height: 64px;
    margin: 1px;
    border-radius: 50%;
    border: 5px solid #0069d9;
    border-color: #0069d9 transparent #0069d9 transparent;
    animation: lds-dual-ring 1.2s linear infinite;
  }

  .opacity-50 {
    opacity: 0.5;
  }

  label.display-4 {
    font-size: 20px;
  }

  svg:hover {
    cursor: pointer;
  }

  .multiselect__tags {
    border: 1px solid #ced4da;
  }

  .multiselect__placeholder {
    color: #495057;
    font-size: 1rem;
  }
</style>
