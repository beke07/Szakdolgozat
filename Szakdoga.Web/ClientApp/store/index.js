import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'
const MAIN_SET_IS_LOGGED_IN = 'MAIN_SET_IS_LOGGED_IN'
const MAIN_SET_IS_LOGGED_IN_AS_ADMIN = 'MAIN_SET_IS_LOGGED_IN_AS_ADMIN'
const MAIN_SET_IS_LOGGED_IN_AS_USER = 'MAIN_SET_IS_LOGGED_IN_AS_USER'
const MAIN_SET_IS_LOADING = 'MAIN_SET_IS_LOADING'

// STATE
const state = {
  counter: 1,
  isLoggedIn: !!localStorage.getItem('lbUser'),
  isLoggedInAsAdmin: false,
  isLoggedInAsUser: false,
  loading: false
}

// MUTATIONS
const mutations = {
  [MAIN_SET_COUNTER] (state, obj) {
    state.counter = obj.counter
  },
  [MAIN_SET_IS_LOGGED_IN](state, obj) {
    state.isLoggedIn = obj.loggedIn
  },
  [MAIN_SET_IS_LOGGED_IN_AS_ADMIN](state, obj) {
    state.isLoggedInAsAdmin = obj.loggedIn
  },
  [MAIN_SET_IS_LOGGED_IN_AS_USER](state, obj) {
    state.isLoggedInAsUser = obj.loggedIn
  },
  [MAIN_SET_IS_LOADING](state, obj) {
    state.loading = obj.loading
  }
}

// ACTIONS
const actions = ({
  setCounter ({ commit }, obj) {
    commit(MAIN_SET_COUNTER, obj)
  },
  setIsLoggedIn({ commit }, obj) {
    commit(MAIN_SET_IS_LOGGED_IN, obj)
  },
  setloggedInAsAdmin({ commit }, obj) {
    commit(MAIN_SET_IS_LOGGED_IN_AS_ADMIN, obj)
  },
  setloggedInAsUser({ commit }, obj) {
    commit(MAIN_SET_IS_LOGGED_IN_AS_USER, obj)
  },
  setLoading({ commit }, obj) {
    commit(MAIN_SET_IS_LOADING, obj)
  }
})

export default new Vuex.Store({
  state,
  mutations,
  actions
})
