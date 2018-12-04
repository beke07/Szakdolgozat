import Vue from 'vue'
import VueRouter from 'vue-router'
import { routes } from './routes'

Vue.use(VueRouter)

let router = new VueRouter({
  mode: 'history',
  routes
})

export default router

router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth) {
    const authUser = JSON.parse(window.localStorage.getItem('lbUser'))

    if (!authUser) {
      next({ name: 'login' })
    }
    else if (authUser.data.firstSignIn) {
      next({ name: 'password' })
    }
    else if (to.meta.adminAuth) {
      const authUser = JSON.parse(window.localStorage.getItem('lbUser'))
      if (authUser.data.role === 'admin') {
        next()
      } else {
        next({ name: 'login' })
      }
    } else if (to.meta.userAuth) {
      const authUser = JSON.parse(window.localStorage.getItem('lbUser'))
      if (authUser.data.role === 'user') {
        next()
      } else {
        next({ name: 'admin' })
      }
    }
    else {
      next()
    }
  } else {
    next()
  }
})

