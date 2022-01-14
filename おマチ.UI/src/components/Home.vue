<template>
    <div id="home">
        <NavigationPanel @logout="logout"/>
        <FriendListPanel/>
        <StoriesPanel/>
    </div>
</template>

<script>
    import axios from "axios";
    import NavigationPanel from "../components/NavigationPanel.vue";
    import FriendListPanel from "../components/FriendListPanel.vue";
    import StoriesPanel from "../components/StoriesPanel.vue";

    export default {
        name: 'Home',
        components: {
            NavigationPanel,
            FriendListPanel,
            StoriesPanel,
        },
        data() {
            return {
                getUserUrl: "https://localhost:5001/Users/",
                jwtToken: "",
                user: null,
            };
        },
        props: {
        },
        created() {
            this.jwtToken = this.$route.params.authToken;
            if (this.jwtToken === undefined) {
                this.jwtToken = this.$cookies.get('omachi-auth-token');
                if (this.jwtToken === null) {
                    this.$router.push('login');
                }
            }
        },
        mounted() {
            let userId = this.$cookies.get('omachi-user-id');
            if (userId !== null) {
                axios.get(
                    this.getUserUrl + userId,
                    {
                        headers: {
                            Authorization: `Bearer ${this.$cookies.get('omachi-auth-token')}`
                        }
                    }
                ).then((res) => {
                    this.user = res.data;
                    console.log(this.user);
                }).catch((res) => {
                    console.log(res);
                });
            }
        },
        methods: {
            logout() {
                this.jwtToken = this.$cookies.get('omachi-auth-token');
                if (this.jwtToken !== null) {
                    this.$cookies.remove('omachi-auth-token');
                }
                this.$nextTick(function () {
                    this.$router.push('login');
                });
            },
        },
        watch: {
        },
    };
</script> 

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    #home::-webkit-scrollbar {
        display: none;
    }
</style>

