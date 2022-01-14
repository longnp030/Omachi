<template>
    <v-app id="login" data-app>
        <v-form class="login-form"
                ref="form"
                v-model="valid"
                lazy-validation>
            <v-text-field label="Email"
                            :rules="[rules.required, rules.email]"
                            v-model="authModel.Email"></v-text-field>
            <v-text-field label="Password"
                            :rules="[rules.required]"
                            :type="'password'"
                            v-model="authModel.Password"></v-text-field>

            <v-checkbox v-model="saveCred"
                        color="red"
                        label="Remember me"></v-checkbox>

            <v-btn :disabled="!valid"
                    color="success"
                    class="mr-4"
                    @click="login">
                Login
            </v-btn>
            <v-btn color="success"
                    class="mr-4"
                    @click="register">
                Register
            </v-btn>
        </v-form>
    </v-app>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'Login',
        data() {
            return {
                authUrl: "https://localhost:5001/Users/authenticate",
                valid: true,
                saveCred: false,
                rules: {
                    required: value => !!value || 'Required.',
                    email: value => {
                        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                        return pattern.test(value) || 'Invalid email.'
                    },
                },
                authModel: {},
            };
        },
        methods: {
            /**
             * Shortcut for saving cookies
             * @param authToken JwtToken for user authentication
             * @param userId User ID
             * @param expire Cookies expire time, delete when browser is closed -> 0
             */
            saveCookie(authToken, userId, expire) {
                this.$cookies.set('omachi-auth-token', authToken, expire);
                this.$cookies.set('omachi-user-id', userId, expire);
            },

            /**
             * User authentication
             * Post by axios, then save JwtToken and UserID to Cookies, finally redirect to homepage
             */
            login() {
                this.$refs.form.validate();
                axios.post(
                    this.authUrl,
                    JSON.parse(JSON.stringify(this.authModel))
                ).then((res) => {
                    var authToken = res.data.JwtToken;
                    var userId = res.data.Id;

                    if (this.saveCred) {
                        this.saveCookie(authToken, userId, '7d');
                    } else {
                        this.saveCookie(authToken, userId, 0);
                    }
                    this.$nextTick(function () {
                        this.$router.push({
                            name: 'home',
                            params: {
                                authToken: authToken,
                                userId: userId,
                            }
                        });
                    });
                }).catch((res) => {
                    console.log(res)
                });
            },

            register() {
                this.$router.push('register');
            }
        }
    };
</script>

<style scoped>
    @import "../css/Login.css";
</style>