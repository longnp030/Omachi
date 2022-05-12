<template>
    <div id="login-page">
        <h1 id="login-title">Đăng nhập</h1>

        <b-form @submit="login" @reset="reset" v-if="show" class="w-50 mt-5 form">
            <b-form-group id="input-group-1"
                          label="Địa chỉ email:"
                          label-for="input-1">
                <b-form-input id="input-1"
                              v-model="form.Email"
                              type="email"
                              placeholder="Nhập email của bạn.."
                              required></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-2"
                          label="Mật khẩu:"
                          label-for="input-2">
                <b-form-input id="input-2"
                              v-model="form.Password"
                              type="password"
                              placeholder="Nhập mật khẩu.."
                              required></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-4">
                <b-form-checkbox v-model="saveCred">Giữ tôi đăng nhập</b-form-checkbox>
            </b-form-group>

            <div id="btns">
                <b-button block type="submit" variant="primary">Đăng nhập</b-button>
                <b-button block type="reset" variant="danger">Nhập lại</b-button>
                <b-button block @click="register" variant="warning">Đăng ký</b-button>
            </div>
        </b-form>        
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'Login',
        data() {
            return {
                authUrl: "https://localhost:5001/Users/authenticate",
                saveCred: false,
                form: {},
                show: true,
            };
        },
        mounted() {
            console.log(this.saveCred);
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
            login(event) {
                event.preventDefault();
                axios.post(
                    this.authUrl,
                    JSON.parse(JSON.stringify(this.form))
                ).then((res) => {
                    var authToken = res.data.JwtToken;
                    var userId = res.data.Id;

                    console.log(this.saveCred);
                    if (this.saveCred) {
                        this.saveCookie(authToken, userId, -1);
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
                    console.log(res);
                    this.$toast.error("Sai email hoặc mật khẩu, vui lòng kiểm tra lại.");
                });
            },

            register() {
                this.$router.push('register');
            },

            reset(event) {
                event.preventDefault()
                // Reset our form values
                this.form = {};
                // Trick to reset/clear native browser form validation state
                this.show = false;
                this.$nextTick(() => {
                    this.show = true
                })
            },
        },
        watch: {
            saveCred: function () {
                console.log('saveCred is set to: ', this.saveCred);
            },
        }
    };
</script>

<style scoped>
    #login-page {
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

    #login-title {
        padding-top: 50px;
        display: flex;
        justify-content: space-around;
    }

    #btns {
        display: flex;
        flex-direction: column;
        gap: 20px;
    }
</style>