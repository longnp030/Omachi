<template>
    <v-card>
        <v-form ref="form">
            <v-card-title>
                <span class="text-h5">{{actFormName}}</span>
            </v-card-title>
            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col cols="12">
                            <v-text-field label="Name*"
                                          :rules="[rules.required]"
                                          v-model="activity.Name"
                                          required></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-menu ref="menu"
                                    v-model="menu2"
                                    :close-on-content-click="false"
                                    :nudge-right="40"
                                    :return-value.sync="activity.StartTime"
                                    transition="scale-transition"
                                    offset-y
                                    max-width="290px"
                                    min-width="290px">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field :rules="[rules.required]"
                                                  label="StartTime*"
                                                  v-model="activity.StartTime"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"></v-text-field>
                                </template>
                                <v-time-picker v-if="menu2"
                                               v-model="activity.StartTime"
                                               full-width
                                               use-seconds
                                               @click:second="$refs.menu.save(activity.StartTime)"></v-time-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-text>
            <v-card-actions>
                <v-dialog v-model="dialog"
                          persistent
                          v-if="formMode == 1"
                          max-width="290">
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn color="warning"
                               dark
                               v-bind="attrs"
                               v-on="on">
                            Delete
                        </v-btn>
                    </template>
                    <v-card>
                        <v-card-title class="text-h5">
                            Do you want to delete activity?
                        </v-card-title>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="green darken-1"
                                   text
                                   @click="dialog = false">
                                Disagree
                            </v-btn>
                            <v-btn color="green darken-1"
                                   text
                                   @click="deleteActivity">
                                Agree
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1"
                       text
                       @click="cancelActivity">
                    Close
                </v-btn>
                <v-btn color="blue darken-1"
                       text
                       @click="saveActivity">
                    Save
                </v-btn>
            </v-card-actions>
        </v-form>
    </v-card>
</template>

<script>
    import axios from 'axios';
    export default {
        name: "ActivityForm",
        components: {

        },
        data() {
            return {
                actFormName: "Thêm mới hoạt động",
                rules: {
                    required: value => !!value || 'Required.',
                },
                menu2: false,
                dialog: false,
                activity: {},
                add_act_url: "https://localhost:5001/Users/user_id/activities",
                get_act_url: "https://localhost:5001/Activities/act_id"
            };
        },
        props: {
            user_id: {
                type: String,
                required: true,
                default: ''
            },
            jwt_token: {
                type: String,
                required: true,
                default: '',
            },
            marker: {
                type: Object,
                required: true,
                default() {
                    return {}
                },
            },
            formMode: {
                type: Number,
                required: true,
                default: 2,
            },
            editMarkerId: {
                type: String,
                required: true,
                default: '',
            },
        },
        methods: {
            cancelActivity() {
                this.activity = {};
                this.$refs.form.reset();
                this.$emit("addActivity", false, 2);
            },
            saveActivity() {
                this.$refs.form.validate();
                if (this.formMode === 0) {
                    if (this.marker.LatLon) {
                        this.activity.Lat = this.marker.LatLon[0];
                        this.activity.Lon = this.marker.LatLon[1];
                        //console.log(this.activity);

                        axios.post(
                            this.add_act_url.replace('user_id', this.user_id),
                            JSON.parse(JSON.stringify(this.activity)),
                            {
                                headers: {
                                    Authorization: `Bearer ${this.jwt_token}`
                                }
                            }
                        ).then((res) => {
                            console.log(res);
                            this.cancelActivity();
                        }).catch((res) => {
                            console.log(res)
                        });
                    }
                } else {
                    axios.put(
                        this.get_act_url.replace('act_id', this.editMarkerId),
                        JSON.parse(JSON.stringify(this.activity)),
                        {
                            headers: {
                                Authorization: `Bearer ${this.jwt_token}`
                            }
                        }
                    ).then((res) => {
                        console.log(res);
                        this.cancelActivity();
                    }).catch((res) => {
                        console.log(res)
                    });
                }
            },
            deleteActivity() {
                console.log(this.editMarkerId);
                this.dialog = false;
                axios.delete(
                    this.get_act_url.replace('act_id', this.editMarkerId),
                    {
                        headers: {
                            Authorization: `Bearer ${this.jwt_token}`
                        }
                    }
                ).then((res) => {
                    console.log(res);
                    this.cancelActivity();
                }).catch((res) => {
                    console.log(res)
                });
            },
        },
        watch: {
            formMode: {
                immediate: true,
                deep: true,
                handler: function () {
                    //console.log(this.formMode);
                    if (this.formMode === 1) {
                        this.actFormName = "Sửa hoạt động";
                    } else {
                        this.actFormName = "Thêm mới hoạt động";
                    }
                }
            },
            marker: {
                immediate: true,
                deep: true,
                handler: function () {
                    console.log(this.marker);
                }
            },
            editMarkerId: {
                immediate: true,
                deep: true,
                handler: function () {
                    console.log(this.editMarkerId);
                    var self = this;
                    if (this.formMode == 1) {
                        axios.get(
                            this.get_act_url.replace('act_id', this.editMarkerId),
                            {
                                headers: {
                                    Authorization: `Bearer ${this.jwt_token}`
                                }
                            }
                        ).then((res) => {
                            console.log(res);
                            self.activity = res.data;
                        }).catch((res) => {
                            console.log(res)
                        });
                    }
                }
            },
        },
    }
</script>

<style scoped>
    @import "../css/ActivityForm.css";
</style>