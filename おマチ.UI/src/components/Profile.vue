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
            </b-form-group>
            
            <b-form-group>
                <b-form-input id="input-email"
                              v-model="form.Email"
                              placeholder="Email"
                              readonly
                              class="email"></b-form-input>
            </b-form-group>

            <b-button type="submit" variant="primary" block>Lưu</b-button>
        </b-form>
        <!--<b-card class="mt-3" header="Form Data Result">
            <pre class="m-0">{{ form }}</pre>
        </b-card>-->
    </div>
</template>

<script>
    import axios from "axios";
    export default {
        data() {
            return {
                profileUrl: "https://localhost:5001/Users/",
                form: {},
                jwtToken: null,
                myId: null,
            }
        },
        async created() {
            this.myId = this.$route.params.userId;
            this.jwtToken = this.$route.params.jwtToken;

            await this.getUser();
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

            async onSubmit(event) {
                event.preventDefault();
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
                }).catch((res) => {
                    console.log(res.response);
                })
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