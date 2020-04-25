import Vue from 'vue';
import app from './app/app.vue';
import router from './router';
import store from './store';
import Axios from 'axios';
import config from '../config';
import BootstrapVue from 'bootstrap-vue/dist/bootstrap-vue.esm';

Vue.use(BootstrapVue);

Axios.defaults.baseURL = config.base_url;
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(app)
}).$mount('#app');
