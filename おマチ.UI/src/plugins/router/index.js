import Vue from 'vue'
import VueRouter from 'vue-router';
Vue.use(VueRouter);

import Login from '../../components/Login.vue';
import Register from '../../components/Register.vue';
import Home from '../../components/Home.vue';
import Profile from '../../components/Profile.vue';

const routes = [
    {
        path: '/login',
        name: 'login',
        component: Login,
    },
    {
        path: '/register',
        name: 'register',
        component: Register,
    },
    {
        path: '/home',
        name: 'home',
        component: Home,
    },
    {
        path: '/user/:userId',
        name: 'profile',
        component: Profile,
    },
]
const router = new VueRouter({
    mode: 'history',
    routes
});
export default router