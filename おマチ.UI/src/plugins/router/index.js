import Vue from 'vue'
import VueRouter from 'vue-router';
Vue.use(VueRouter);

import Login from '../../components/Login.vue'
import Home from '../../components/Home.vue'
import UpdateProfile from '../../components/UpdateProfile.vue';

const routes = [
    {
        path: '/login',
        name: 'login',
        component: Login,
    },
    {
        path: '/home',
        name: 'home',
        component: Home,
    },
    {
        path: '/update-new-profile',
        name: 'update-new-profile',
        component: UpdateProfile,
    },
]
const router = new VueRouter({
    mode: 'history',
    routes
});
export default router