<template>
    <v-app id="register" data-app>
        <v-form class="register-form"
                ref="form"
                v-model="valid"
                lazy-validation>
            <v-text-field label="Email"
                            :rules="[rules.required, rules.email]"
                            v-model="registerModel.Email"></v-text-field>
            <v-text-field label="Password"
                            :rules="[rules.required]"
                            :type="'password'"
                            v-model="confirmPwd"></v-text-field>
            <v-text-field label="Confirm Password"
                            :rules="[rules.required, rules.matchPwd]"
                            :type="'password'"
                            v-model="registerModel.Password"></v-text-field>

            <v-checkbox color="red"
                        :rules="[v => !!v || 'You must agree to continue!']"
                        label="I agree to the terms of services"></v-checkbox>
            <v-checkbox v-model="saveCred"
                        color="red"
                        label="Save credentials for next login."></v-checkbox>

            <v-btn :disabled="!valid"
                    color="success"
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
        name: 'Register',
        data() {
            return {
                registerUrl: "https://localhost:5001/Users/register",
                authUrl: "https://localhost:5001/Users/authenticate",
                valid: true,
                saveCred: false,
                registerModel: {},
                confirmPwd: '',
                rules: {
                    required: value => !!value || 'Required.',
                    email: value => {
                        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                        return pattern.test(value) || 'Invalid email.'
                    },
                    matchPwd: value => value === this.confirmPwd || 'Passwords do not match.'
                }, 
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

            register() {
                this.$refs.form.validate();
                axios.post(
                    this.registerUrl,
                    JSON.parse(JSON.stringify(this.registerModel))
                ).then(() => {
                    let authModel = {
                        Email: this.registerModel.Email,
                        Password: this.registerModel.Password
                    };
                    /** Perform Login **/
                    axios.post(
                        this.authUrl,
                        authModel
                    ).then((res) => {
                        var authToken = res.data.JwtToken;
                        var userId = res.data.Id;

                        if (this.saveCred) {
                            this.saveCookie(authToken, userId, '7d');
                        } else {
                            this.saveCookie(authToken, userId, 0);
                        }
                        /** Redirect to complete user profile **/
                        this.$nextTick(function () {
                            this.$router.push({
                                name: 'update-profile',
                                params: {
                                    authToken: authToken,
                                    userId: userId,
                                    userEmail: res.data.Email,
                                }
                            });
                        });
                    }).catch((res) => {
                        console.log(res)
                    });
                }).catch((res) => {
                    console.log(res)
                });
            },
        }
    }
</script>

<style scoped>
    @import '../css/Register.css';
</style>