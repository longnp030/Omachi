<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>

<script>
    export default {
        name: 'app',
        components: {
            
        },
        created() {
            this.$router.push('update-profile', () => { });
        }
    };
</script>

<style>
    @import "css/Main.css";
</style>

