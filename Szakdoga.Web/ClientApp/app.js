import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import VueSweetalert2 from 'vue-sweetalert2'
import BootstrapVue from 'bootstrap-vue'
import VeeValidate from 'vee-validate';
import { Validator } from 'vee-validate';
import VueDragDrop from 'vue-drag-drop';
import { Tooltip } from 'bootstrap-vue/es/components';
import datePicker from 'vue-bootstrap-datetimepicker';

axios.interceptors.request.use(config => {
  store.state.loading = true;
  return config
})

axios.interceptors.response.use(function (response) {
  store.state.loading = false;
  return response;
}, function (error) {
  store.state.loading = false;
  return Promise.reject(error);
});

// Registration of jquery
global.jQuery = require('jquery')
var $ = global.jQuery
window.$ = $

// Registration of sweetalert
Vue.use(VueSweetalert2);

//Register BootstrapVue
Vue.use(BootstrapVue);
Vue.use(Tooltip);

//Register VueDragDrop
Vue.use(VueDragDrop);

//Register VeeValidate
Vue.use(VeeValidate, { fieldsBagName: 'formFields' });

import 'pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css';
import '@fortawesome/fontawesome-free/css/fontawesome.css';
import '@fortawesome/fontawesome-free/css/solid.css';
import '@fortawesome/fontawesome-free/css/regular.css';
Vue.use(datePicker);

jQuery.extend(true, jQuery.fn.datetimepicker.defaults, {
  icons: {
    time: 'far fa-clock',
    date: 'far fa-calendar',
    up: 'fas fa-arrow-up',
    down: 'fas fa-arrow-down',
    previous: 'fas fa-chevron-left',
    next: 'fas fa-chevron-right',
    today: 'fas fa-calendar-check',
    clear: 'far fa-trash-alt',
    close: 'far fa-times-circle'
  }
});
const dictionary = {
  hu: {
    messages: {
      required: 'Mező kitöltése kitelező!',
      min: 'A mező tartalma nem haladja meg a minimális karakterszámot!',
      max: 'A mező tartalma meghaladja a maximális karakterszámot!',
      confirmed: 'Nem egyezik meg a két beírt jelszó!'
    }
  }
};

Validator.localize(dictionary);
Validator.localize('hu');

// Registration of global components
Vue.component('icon', FontAwesomeIcon)

Vue.prototype.$http = axios

sync(store, router)

const app = new Vue({
  store,
  router,
  ...App
})

export {
  app,
  router,
  store
}
