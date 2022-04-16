<template>
    <div id="register-page">
        <h1 id="register-title">Đăng ký</h1>
        <v-form class="register-form"
                ref="form"
                v-model="valid"
                lazy-validation>
            <v-text-field label="Email"
                          :rules="[rules.required, rules.email]"
                          v-model="registerModel.Email"></v-text-field>
            <v-text-field label="Mật khẩu"
                          :rules="[rules.required]"
                          :type="'password'"
                          v-model="confirmPwd"></v-text-field>
            <v-text-field label="Xác nhận mật khẩu"
                          :rules="[rules.required, rules.matchPwd]"
                          :type="'password'"
                          v-model="registerModel.Password"></v-text-field>

            <v-checkbox color="red"
                        :rules="[v => !!v || 'Bạn cần đồng ý để tiếp tục!']"
                        label="Tôi đồng ý với điều khoản sử dụng"></v-checkbox>
            <v-checkbox v-model="saveCred"
                        color="red"
                        label="Giữ tôi đăng nhập"></v-checkbox>

            <v-btn :disabled="!valid"
                   class="mr-4 w-100 green"
                   @click="register">
                Đăng ký
            </v-btn>
        </v-form>
    </div>
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
                    required: value => !!value || 'Trường này là bắt buộc.',
                    email: value => {
                        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                        return pattern.test(value) || 'Địa chỉ email không hợp lệ.'
                    },
                    matchPwd: value => value === this.confirmPwd || 'Mật khẩu không khớp.'
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
                                name: 'home',
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
    #register-page {
        height: 100vh;
        width: 100%;
        position: fixed;
        display: flex;
        align-items: center;
        flex-direction: column;
        background-image: url('../assets/login-bg.jpg');
        background-size: cover;
        background-position: center;
        background-repeat: no-repeat;
    }

    #register-title {
        padding-top: 50px;
        display: flex;
        justify-content: space-around;
    }

    .register-form {
        width: 50%;
        height: calc(100vh - 96px);
        border-radius: 4px;
        margin: 24px auto;
        padding: 24px;
    }
</style>