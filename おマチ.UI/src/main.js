import Vue from 'vue';
import App from './App.vue';

Vue.config.productionTip = false;

/** Import components **/
import router from './plugins/router';
import vuetify from './plugins/vuetify';
/** End Import components **/

/** Vue cookies **/
import VueCookies from 'vue-cookies';
Vue.use(VueCookies);
/** End Vue cookies **/

/** Vue router **/
import VueRouter from 'vue-router';
Vue.use(VueRouter);
/** End Vue router **/

new Vue({
    router,
    vuetify,
    render: h => h(App)
}).$mount('#app');
