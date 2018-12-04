<template>
  <div class="main-nav">
    <nav class="navbar navbar-expand-md navbar-dark bg-dark">
      <button class="navbar-toggler" type="button" @click="toggleCollapsed">
        <span class="navbar-toggler-icon"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>

      <router-link v-if="isLoggedInAsAdmin" class="navbar-brand" to="/admin"><icon :icon="['fas', 'home']" /> Kezdőlap</router-link>
      <router-link v-if="isLoggedInAsUser" class="navbar-brand" to="/home"><icon :icon="['fas', 'home']" /> Kezdőlap</router-link>

      <transition name="slide">
        <div :class="'collapse navbar-collapse' + (!collapsed ? ' show':'')" v-show="!collapsed">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item"
                v-for="(route, index) in routes" :key="index"
                v-if="(isLoggedInAsAdmin && route.meta.adminAuth && route.meta.navRender) ||
                      (isLoggedInAsUser && route.meta.userAuth && route.meta.navRender) ||
                      (isLoggedIn && route.meta.requiresAuth && route.meta.navRender && !route.meta.userAuth && !route.meta.adminAuth)">
              <router-link :to="route.path" exact-active-class="active">
                <icon :icon="route.icon" style="width: 20px" class="mr-2" /><span>{{ route.display }}</span>
              </router-link>
            </li>
          </ul>
        </div>
      </transition>

      <a v-if="isLoggedIn" class="navbar-brand text-white" @click="logOut"><icon :icon="['fas', 'sign-out-alt']" /> Kijelentkezés</a>
    </nav>
  </div>
</template>
<script>
  import { routes } from '../router/routes'
  import { commonService } from '../services/common-service'
  import { mapActions, mapState } from 'vuex'

  export default {
    data() {
      return {
        routes,
        collapsed: true
      }
    },
    computed: {
      ...mapState({
        isLoggedIn: state => state.isLoggedIn,
        isLoggedInAsAdmin: state => state.isLoggedInAsAdmin,
        isLoggedInAsUser: state => state.isLoggedInAsUser
      })
    },
    methods: {
      ...mapActions(['setIsLoggedIn']),
      ...mapActions(['setloggedInAsAdmin']),
      ...mapActions(['setloggedInAsUser']),

      toggleCollapsed: function (event) {
        this.collapsed = !this.collapsed
      },
      logOut: function () {
        const status = JSON.parse(window.localStorage.getItem('lbUser'));
        if (status !== null) {

          this.setIsLoggedIn({ loggedIn: false });
          this.setloggedInAsAdmin({ loggedIn: false });
          this.setloggedInAsUser({ loggedIn: false });

          commonService.getToast(this)({
            type: 'success',
            title: 'Sikeres kijelentkezés!'
          });

          window.localStorage.removeItem('lbUser');
          this.$router.push('/login');
        }
      }
    }
  }
</script>
<style scoped>

  a:hover,
  a:focus {
    cursor: pointer;
  }

  .slide-enter-active, .slide-leave-active {
    transition: max-height .35s
  }

  .slide-enter, .slide-leave-to {
    max-height: 0px;
  }

  .slide-enter-to, .slide-leave {
    max-height: 20em;
  }
</style>
