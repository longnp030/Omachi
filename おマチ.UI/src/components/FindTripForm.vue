<template>
    <v-card>
        <v-form ref="form">
            <v-card-title>
                <span class="text-h5">{{findTripFormName}}</span>
            </v-card-title>
            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col cols="12">
                            <v-combobox id="poi-filter"
                                        v-model="selected_category_for_mutation"
                                        :items="categories_for_mutation"
                                        label="Category"
                                        @input="filterCategory(selected_category_for_mutation)"
                                        clearable
                                        dense
                                        hide-selected
                                        persistent-hint
                                        small-chips
                                        solo></v-combobox>
                        </v-col>
                        <v-col cols="12">
                            <v-menu ref="menu"
                                    v-model="menu2"
                                    :close-on-content-click="false"
                                    :nudge-right="40"
                                    :return-value.sync="startTime"
                                    transition="scale-transition"
                                    offset-y
                                    max-width="290px"
                                    min-width="290px">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field label="Start Time*"
                                                  v-model="startTime"
                                                  :rules="[rules.required]"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"></v-text-field>
                                </template>
                                <v-time-picker v-if="menu2"
                                               v-model="startTime"
                                               full-width
                                               format="24hr"
                                               use-seconds
                                               @click:second="$refs.menu.save(startTime)"></v-time-picker>
                            </v-menu>
                        </v-col>
                        <v-col cols="12">
                            <v-menu ref="menu_2"
                                    v-model="menu2_2"
                                    :close-on-content-click="false"
                                    :nudge-right="40"
                                    :return-value.sync="arrivalTime"
                                    transition="scale-transition"
                                    offset-y
                                    max-width="290px"
                                    min-width="290px">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field label="Arrival Time*"
                                                  v-model="arrivalTime"
                                                  :rules="[rules.required]"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"></v-text-field>
                                </template>
                                <v-time-picker v-if="menu2_2"
                                               v-model="arrivalTime"
                                               full-width
                                               format="24hr"
                                               use-seconds
                                               @click:second="$refs.menu_2.save(arrivalTime)"></v-time-picker>
                            </v-menu>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1"
                       text
                       @click="cancelFindTrip">
                    Cancel
                </v-btn>
                <v-btn color="blue darken-1"
                       text
                       @click="confirmFindTrip">
                    Find
                </v-btn>
            </v-card-actions>
        </v-form>
    </v-card>
</template>

<script>
    import axios from "axios";
    export default {
        name: 'FindTripForm',
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
            // 2 prop để đồng bộ dữ liệu từ cha -> cha chọn cate -> con cũng có cate tương tự
            selected_category: {
                type: String,
                required: false,
                default: null,
            },
            categories: {
                type: Array,
                required: false,
                default() {
                    return []
                },
            },
            startLat: Number,
            startLon: Number,
            poiId: String,
        },
        data() {
            return {
                findTripFormName: "Tìm chuyến đi chung",
                menu2: false,
                menu2_2: false,
                dialog: false,
                carRequest: {},
                rules: {
                    required: value => !!value || 'Required.',
                },
                // 2 data để mutate trong con, dùng làm v-model để hiển thị cate trong con
                selected_category_for_mutation: null,
                categories_for_mutation: [],

                poi: null,
                poi_name: '',
                startTime: '',
                arrivalTime: '', 
                get_poi_url: "https://localhost:5001/POIs/poi_id",

                matching_url: "https://localhost:5001/Users/user_id/matching",
            };
        },
        methods: {
            cancelFindTrip() {
                this.$refs.form.reset();
                this.$emit("findTrip", false);
            },
            confirmFindTrip() {
                this.$refs.form.validate();
                if (this.startTime) {
                    this.buildCarRequest();
                    console.log(this.carRequest);
                    
                    axios.post(
                        this.matching_url.replace('user_id', this.user_id),
                        JSON.parse(JSON.stringify(this.carRequest)),
                        {
                            headers: {
                                Authorization: `Bearer ${this.jwt_token}`
                            }
                        }
                    ).then((res) => {
                        console.log(res.data);
                        this.cancelFindTrip();
                    }).catch((res) => {
                        console.log(res);
                    });
                }
            },
            buildCarRequest() {
                this.carRequest.UserId = this.user_id;
                this.carRequest.StartLat = this.startLat;
                this.carRequest.StartLon = this.startLon;
                this.carRequest.StartTime = this.startTime;
                this.carRequest.ArrivalTime = this.arrivalTime;
                this.carRequest.Timestamp = Date();
            },
            filterCategory(selected_category) {
                this.$emit('filterCategory', selected_category);
            },
        },
        watch: {
            selected_category: {
                immediate: true,
                deep: true,
                handler: function () {
                    this.selected_category_for_mutation = this.selected_category;
                }
            },
            categories: {
                immediate: true,
                deep: true,
                handler: function () {
                    this.categories_for_mutation = this.categories;
                }
            },
            poiId: {
                deep: true,
                handler: function () {
                    console.log(this.poiId);
                    axios.get(
                        this.get_poi_url.replace('poi_id', this.poiId)
                    ).then((res) => {
                        this.poi = res.data;
                        this.poi_name = this.poi.Name;
                        console.log(this.poi);
                    }).catch((res) => {
                        console.log(res);
                    })
                }
            },
        }
    }
</script>

<style scoped>
</style>