<template>
    <div id="navigation-panel">
        <div id="logo" class="left"></div>
        <div class="right">{{ user }}
            <v-btn color="success"
                   class="mr-4"
                   @click="logout">
                Login
            </v-btn>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'NavigationPanel',
        data() {
            return {
                user: null,
            };
        },
        created() {
        },
        methods: {
            logout() {
                this.$emit('logout');
            }
        },
    }
</script>

<style scoped>
    @import "../css/NavigationPanel.css";
</style>