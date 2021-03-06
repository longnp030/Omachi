import Vue from 'vue';
import App from './App.vue';

Vue.config.productionTip = false;

/** Import components **/
import router from './plugins/router';
import vuetify from './plugins/vuetify';
import 'material-design-icons-iconfont/dist/material-design-icons.css'

import { BootstrapVue, BootstrapVueIcons } from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
Vue.use(BootstrapVue);
Vue.use(BootstrapVueIcons);
/** End Import components **/

/** Vue cookies **/
import VueCookies from 'vue-cookies';
Vue.use(VueCookies);
/** End Vue cookies **/

/** Vue router **/
import VueRouter from 'vue-router';
Vue.use(VueRouter);
/** End Vue router **/

/** Vue Toastification **/
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

const options = {
    position: "bottom-left",
    closeOnClick: false,
    pauseOnFocusLoss: true,
    pauseOnHover: true,
    draggable: true,
    draggablePercent: 2,
    showCloseButtonOnHover: true,
    closeButton: "button",
    transition: "Vue-Toastification__fade",
}
Vue.use(Toast, options);
/** End Vue Toastification**/

/** Vue Leaflet **/
import {
    LMap,
    LTileLayer,
    LMarker,
    LTooltip,
    LPolyline,
    LControl,
    LCircleMarker,
} from 'vue2-leaflet';
import 'leaflet/dist/leaflet.css';
Vue.component('l-map', LMap);
Vue.component('l-tile-layer', LTileLayer);
Vue.component('l-marker', LMarker);
Vue.component('l-tooltip', LTooltip);
Vue.component('l-polyline', LPolyline);
Vue.component('l-control', LControl);
Vue.component('l-circle-marker', LCircleMarker);

import { Icon } from 'leaflet';

delete Icon.Default.prototype._getIconUrl;
Icon.Default.mergeOptions({
    iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
    iconUrl: require('leaflet/dist/images/marker-icon.png'),
    shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
});
/** End Vue Leaflet **/

/** Chat Socket **/
import ChatSocket from './plugins/chat';
Vue.use(ChatSocket);
/** End Chat Socket **/

new Vue({
    router,
    vuetify,
    render: h => h(App)
}).$mount('#app');
