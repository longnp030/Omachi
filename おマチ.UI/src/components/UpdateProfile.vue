<template>
    <v-app id="user-profile" data-app>
        <v-stepper v-model="step" class="profile-form">
            <v-stepper-header>
                <v-stepper-step :complete="step > 1" step="1">Basic information</v-stepper-step>
                <v-divider></v-divider>

                <v-stepper-step :complete="step > 2" step="2">Topics of interest</v-stepper-step>
                <v-divider></v-divider>

                <v-stepper-step :complete="step > 3" step="3">Languages</v-stepper-step>
                <v-divider></v-divider>

                <v-stepper-step :complete="complete" step="4">Done</v-stepper-step>
            </v-stepper-header>

            <v-stepper-items>
                <v-stepper-content step="1">
                    <v-text-field label="Username" v-model="user.UserName"></v-text-field>

                    <v-menu v-model="showPicker"
                            :close-on-content-click="false"
                            transition="scale-transition"
                            max-width="290px"
                            min-height="290px"
                            max=""
                            offset-y>
                        <template v-slot:activator="{ on }">
                            <v-text-field v-model="user.DateOfBirth"
                                          label="Date Of Birth"
                                          persistent-hint
                                          readonly
                                          v-on="on"></v-text-field>
                        </template>
                        <v-date-picker v-model="user.DateOfBirth"
                                       no-title
                                       @input="showPicker = false"></v-date-picker>
                    </v-menu>

                    <v-select :items="genders"
                              label="Gender"></v-select>

                    <v-text-field label="About me" v-model="user.AboutMe"></v-text-field>

                    <v-btn color="primary"
                           @click="step = 2">
                        Continue
                    </v-btn>
                </v-stepper-content>

                <v-stepper-content step="2">
                    <v-btn-toggle v-model="interests"
                                  group
                                  multiple>
                        <v-btn v-for="interest in interests"
                                :key="interest">{{ interest }}</v-btn>
                    </v-btn-toggle>

                    <v-btn color="primary"
                           @click="step = 3">
                        Continue
                    </v-btn>
                </v-stepper-content>

                <v-stepper-content step="3">
                    <v-btn-toggle v-model="interests"
                                  group
                                  multiple>
                        <v-btn v-for="interest in interests"
                               :key="interest">{{ interest }}</v-btn>
                    </v-btn-toggle>

                    <v-btn color="primary"
                           @click="step = 4">
                        Continue
                    </v-btn>
                </v-stepper-content>

                <v-stepper-content step="4">
                    <v-alert :value="true"
                             type="success">
                        Cihazınız Başarılı Bir Şekilde Yüklendi.
                    </v-alert>
                    <v-btn color="primary"
                           @click="complete = true">
                        Anasayfa
                    </v-btn>
                </v-stepper-content>
            </v-stepper-items>
        </v-stepper>
    </v-app>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'UpdateProfile',
        data() {
            return {
                step: 1,
                complete: false,
                user: {},
                genders: ["Male", "Female", "Others"],
                showPicker: false,
                interestsUrl: "https://localhost:5001/Users/interests",
                interests: [],
                userInterests: [],
            };
        },
        created() {
            axios.get(this.interestsUrl).then((res) => {
                res.data.forEach(interest => {
                    this.interests.push(interest.Name);
                });
            }).catch((res) => { console.log(res); });
        },
        methods: {

        }
    }
</script>

<style scoped>
    @import '../css/UpdateProfile.css';
</style>