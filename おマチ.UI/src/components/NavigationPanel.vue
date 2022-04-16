<template>
    <b-navbar toggleable="lg" type="dark" id="navbar" v-if="user">
        <b-navbar-brand href="/home"><b-icon icon="map-fill"></b-icon></b-navbar-brand>
            <b-navbar-nav class="ml-auto">
                <b-navbar-brand @click="openUserProfile" class="info">
                    <b-avatar src="https://picsum.photos/300"
                              size="2rem"
                              button
                              class="mr-2"
                              @click="openUserProfile"></b-avatar>
                    <span class="text-truncate mt-2">{{user.Name}}</span>
                </b-navbar-brand>

                <b-nav-item-dropdown right no-caret class="ml-2">
                    <template #button-content>
                        <em><b-icon icon="grid3x3-gap-fill" font-scale="1.5"></b-icon></em>
                    </template>
                    <b-button @click="logout">Đăng xuất</b-button>
                </b-nav-item-dropdown>
            </b-navbar-nav>
    </b-navbar>
</template>

<script>
    export default {
        name: 'NavigationPanel',
        data() {
            return {
            };
        },
        props: ["user", "jwtToken"],
        created() {
        },
        methods: {
            openUserProfile(e) {
                this.$router.push({
                    name: 'profile',
                    params: {
                        userId: this.user.Id,
                        jwtToken: this.jwtToken
                    }
                }).catch(err => {
                    console.log(err);
                    this.$router.go();
                });
                e.stopPropagation();
            },

            logout() {
                this.$emit('logout');
            }
        },
    }
</script>

<style>
    #navbar {
        background-color: #006688;
        padding: 0 20px;
    }

    .navbar-brand {
        padding-top: 0 !important;
    }
</style>