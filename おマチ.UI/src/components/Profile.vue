<template>
    <div id="profile-page">
        <h1 id="profile-title">Thông tin cá nhân</h1>
        <b-form @submit="onSubmit" class="mt-5 form">
            <b-avatar src="https://picsum.photos/300" size="10rem" class="avatar"></b-avatar>

            <b-form-group>
                <b-form-input id="input-name"
                              v-model="form.Name"
                              placeholder="Tên"
                              required></b-form-input>
                <b-form-input id="input-email"
                              v-model="form.Email"
                              placeholder="Email"
                              readonly
                              class="email"></b-form-input>
            </b-form-group>
            
            <b-form-group>
                <v-checkbox v-model="isDriver"
                            label="Tôi là tài xế"></v-checkbox>
            </b-form-group>
            <b-form-group v-if="isDriver">
                <b-form-input id="input-car"
                              v-model="car.Name"
                              placeholder="Tên xe"
                              required></b-form-input>
                <b-form-input id="input-car"
                              v-model="car.Number"
                              placeholder="Biển số"
                              required></b-form-input>
                <b-form-input id="input-car"
                              v-model="car.Color"
                              placeholder="Màu sắc"
                              required></b-form-input>
            </b-form-group>

            <b-button type="submit" variant="primary" block>Lưu</b-button>
        </b-form>
        <!--<b-card class="mt-3" header="Form Data Result">
            <pre class="m-0">{{ car }}</pre>
        </b-card>-->
    </div>
</template>

<script>
    import axios from "axios";
    export default {
        data() {
            return {
                profileUrl: "https://localhost:5001/Users/",
                getCarUrl: "https://localhost:5001/Users/userId/car",
                form: {},
                jwtToken: null,
                myId: null,
                isDriver: false,
                car: {},
            }
        },
        async created() {
            this.myId = this.$route.params.userId;
            this.jwtToken = this.$route.params.jwtToken;

            await this.getUser();
            await this.checkCar();
        },
        methods: {
            async getUser() {
                if (this.userId !== null) {
                    await axios.get(
                        this.profileUrl + this.myId,
                        {
                            headers: {
                                Authorization: `Bearer ${this.jwtToken}`
                            }
                        }
                    ).then((res) => {
                        this.form = res.data;
                    }).catch((res) => {
                        console.log(res);
                    });
                }
            },

            async checkCar() {
                await axios.get(
                    this.getCarUrl.replace("userId", this.myId) + "/own",
                    {
                        headers: {
                            Authorization: `Bearer ${this.jwtToken}`
                        }
                    }
                ).then(res => {
                    console.log(res);
                    if (res.data === false) {
                        this.isDriver = false;
                    } else {
                        this.isDriver = true;
                        this.car = res.data;
                    }
                }).catch(err => {
                    console.log(err);
                });
            },

            async onSubmit(event) {
                event.preventDefault();

                if (this.isDriver) {
                    console.log(this.car);
                    await axios.patch(
                        this.getCarUrl.replace("userId", this.myId),
                        JSON.parse(JSON.stringify(this.car)),
                        {
                            headers: {
                                Authorization: `Bearer ${this.jwtToken}`
                            }
                        }
                    ).then(res => {
                        console.log(res);
                    }).catch(err => {
                        console.log(err);
                    });
                }

                this.form.Timestamp = new Date();
                await axios.patch(
                    this.profileUrl + this.myId,
                    JSON.parse(JSON.stringify(this.form)),
                    {
                        headers: {
                            Authorization: `Bearer ${this.jwtToken}`
                        }
                    }
                ).then(() => {
                    this.$toast.success("Cập nhật thành công.");
                    this.$router.push({
                        name: 'home',
                        params: {
                            authToken: this.jwtToken,
                            userId: this.myId,
                        }
                    });
                }).catch((res) => {
                    console.log(res.response);
                });
            },
        }
    }
</script>

<style scoped>
    .email {
        cursor: not-allowed;
    }

    .avatar {
        margin: 20px auto;
        display: flex;
        justify-content: space-around;
    }

    #profile-page {
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
        overflow-y: auto;
    }

    #profile-title {
        padding-top: 50px;
        display: flex;
        justify-content: space-around;
    }

    .form {
        width: 32%;
    }
</style>