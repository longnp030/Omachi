<template>
    <div id="home" data-app>
        <div id="nav"><NavigationPanel @logout="logout" /></div>

        <div id="map">
            <l-map :zoom="zoom" :center="center" @click="addMarker">
                <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
                <l-circle-marker :color="'red'"
                                 :fillColor="'red'"
                                 :fillOpacity="0.8"
                                 :radius="8"
                                 :lat-lng="[curr_loc.latitude, curr_loc.longitude]"
                                 v-if="curr_loc">
                </l-circle-marker>
                <l-control id="map-utility-controls">
                    <v-combobox id="poi-filter"
                                v-model="selected_category"
                                :items="categories"
                                label="Category"
                                @input="filterCategory"
                                clearable
                                dense
                                hide-selected
                                persistent-hint
                                small-chips
                                solo></v-combobox>
                    <v-btn @click="showActivities = !showActivities">
                        Toggle activities route
                    </v-btn>

                    <v-dialog v-model="add_act_dialog"
                              persistent>
                        <template v-slot:activator="{ on, attrs }">
                            <v-btn @click="addActivity(!add_act_dialog, 0)"
                                   v-bind="attrs"
                                   v-on="on">
                                Add activity
                            </v-btn>
                        </template>
                        <ActivityForm @addActivity="addActivity"
                                      :user_id="userId"
                                      :jwt_token="jwtToken"
                                      :formMode="formMode"
                                      :marker="new_marker"
                                      :editMarkerId="editMarkerId" />
                    </v-dialog>

                    <v-dialog v-model="find_trip_dialog"
                              persistent>
                        <template v-slot:activator="{ on, attrs }">
                            <v-btn @click="findTrip(!find_trip_dialog)"
                                   v-bind="attrs"
                                   v-on="on">
                                Find trip
                            </v-btn>
                        </template>
                        <FindTripForm @findTrip="findTrip"
                                      :user_id="userId"
                                      :jwt_token="jwtToken"
                                      :selected_category="selected_category"
                                      :categories="categories"
                                      @filterCategory="filterCategory"/>
                    </v-dialog>
                </l-control>
                <l-circle-marker v-for="marker in markers"
                                 :key="marker.Id"
                                 :color="'DarkSlateBlue'"
                                 :fillColor="'DarkSlateBlue'"
                                 :fillOpacity="0.8"
                                 :radius="8"
                                 :lat-lng="marker.LatLon"
                                 :visible="showActivities"
                                 @click="editMarker(!add_act_dialog, marker.Id)">
                    <l-tooltip>
                        Name: {{marker.Name}}
                        <br />
                        StartTime: {{marker.StartTime}}
                    </l-tooltip>
                </l-circle-marker>
                <l-polyline :lat-lngs="polyline.latlons" :color="polyline.color" :visible="showActivities"></l-polyline>

                <l-circle-marker v-for="poi in poiMarkers"
                                 :key="poi.Id"
                                 :color="'green'"
                                 :fillColor="'green'"
                                 :fillOpacity="0.8"
                                 :radius="8"
                                 :lat-lng="[poi.Lat, poi.Lon]">
                    <l-tooltip>
                        Name: {{poi.Name}}
                        <br />
                        Address: {{poi.Address}}
                        <br />
                        OpenTime: {{poi.OpenTime}}
                        <br />
                        CloseTime: {{poi.CloseTime}}
                        <br />
                        Rating: {{poi.Rating}}
                        <br />
                        Category: {{poi.Category}}
                    </l-tooltip>
                </l-circle-marker>
            </l-map>
        </div>
    </div>
</template>

<script>
    import axios from "axios";
    import NavigationPanel from "../components/NavigationPanel.vue";
    import ActivityForm from "../components/ActivityForm.vue";
    import FindTripForm from "../components/FindTripForm.vue";

    export default {
        name: 'Home',
        components: {
            NavigationPanel,
            ActivityForm,
            FindTripForm,
        },
        data() {
            return {
                getUserUrl: "https://localhost:5001/Users/",
                add_getall_actsUrl: "https://localhost:5001/Users/user_id/activities",
                jwtToken: "",
                user: null,
                userId: null,
                activities: null,
                showActivities: true,
                curr_loc_options: {
                    enableHighAccuracy: true,
                    timeout: 5000,
                    maximumAge: 0
                },
                curr_loc: null,

                // map properties
                url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                attribution:
                    '&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',
                zoom: 14,
                center: [0, 0],
                markers: [],
                polyline: {
                    latlons: [],
                    color: 'SlateBlue'
                },
                add_act_dialog: false,
                find_trip_dialog: false,

                // for POI category combo-box filter
                selected_category: null,
                categories: [],
                poiMarkers: [],
                all_categories_url: "https://localhost:5001/POIs/Categories",
                filter_category_url: "https://localhost:5001/POIs/Filter?category=cate_str",


                // for new marker
                formMode: 2,
                new_act_idx: 0,
                new_marker: {},

                // for edit marker
                editMarkerId: '',
            };
        },
        props: {
        },
        created() {
            this.jwtToken = this.$route.params.authToken;
            if (this.jwtToken === undefined) {
                this.jwtToken = this.$cookies.get('omachi-auth-token');
                if (this.jwtToken === null) {
                    this.$router.push('login');
                }
            }
        },
        async mounted() {
            await this.getUser();
            await this.getCurrLoc();
            await this.getCategories();
            await this.drawActivitiesRoute();
        },
        methods: {
            logout() {
                this.jwtToken = this.$cookies.get('omachi-auth-token');
                if (this.jwtToken !== null) {
                    this.$cookies.remove('omachi-auth-token');
                }
                this.$nextTick(function () {
                    this.$router.push('login');
                });
            },

            async getUser() {
                this.userId = this.$cookies.get('omachi-user-id');
                if (this.userId !== null) {
                    await axios.get(
                        this.getUserUrl + this.userId,
                        {
                            headers: {
                                Authorization: `Bearer ${this.jwtToken}`
                            }
                        }
                    ).then((res) => {
                        this.user = res.data;
                        //console.log(this.user);
                    }).catch((res) => {
                        console.log(res);
                    });
                }
            },

            curr_loc_success(pos) {
                this.curr_loc = pos.coords;
                this.center = [this.curr_loc.latitude, this.curr_loc.longitude]

                console.log('Your current position is:', this.curr_loc);
                console.log(`Latitude : ${this.curr_loc.latitude}`);
                console.log(`Longitude: ${this.curr_loc.longitude}`);
                console.log(`More or less ${this.curr_loc.accuracy} meters.`);
            },
            curr_loc_error(err) {
                console.warn(`ERROR(${err.code}): ${err.message}`);
            },
            async getCurrLoc() {
                navigator.geolocation.getCurrentPosition(this.curr_loc_success, this.curr_loc_error, this.curr_loc_options);
            },

            async getActivities() {
                console.log('getting activities...');
                await axios.get(
                    this.add_getall_actsUrl.replace('user_id', this.userId),
                    {
                        headers: {
                            Authorization: `Bearer ${this.jwtToken}`
                        }
                    }
                ).then((res) => {
                    this.activities = res.data;
                    //console.log(this.activities);
                }).catch((res) => {
                    console.log(res);
                });
            },

            async getCategories() {
                console.log('getting categories...');
                await axios.get(
                    this.all_categories_url,
                ).then((res) => {
                    this.categories = res.data;
                    //console.log(this.categories);
                }).catch((res) => {
                    console.log(res);
                });
            },

            async drawActivitiesRoute() {
                console.log('drawing route...');
                this.markers = [];
                this.polyline.latlons = [];
                await this.getActivities();

                if (this.activities.length > 1) {
                    console.log('drawing...');
                    for (let idx in this.activities) {
                        let activity = this.activities[idx];
                        //console.log(this.polyline);
                        this.markers.push({
                            Index: idx,
                            Id: activity.Id,
                            LatLon: [activity.Lat, activity.Lon],
                            Name: activity.Name,
                            StartTime: activity.StartTime
                        });
                        this.polyline.latlons.push([activity.Lat, activity.Lon]);
                    }
                }
                //console.log(this.markers);
                //console.log(this.polyline);
            },

            async addActivity(dialog, formMode) {
                //console.log(this.new_act_idx);
                // Nếu [new_act_idx == 0] => hàm đang ko add
                // Nếu [!== 0]            => hàm đang add (có 1 marker mới trong list markers)
                //                           được emit để loại bỏ marker mới nhất
                //                           vì close -> xóa, save thì call drawActivitiesRoute để lấy mới toàn bộ marker
                if (this.new_act_idx) {
                    console.log('splicing...');
                    this.markers.splice(this.new_act_idx, 1);
                }
                //console.log(this.markers);
                this.add_act_dialog = dialog;
                this.formMode = formMode;
                if (this.formMode === 2) {
                    await this.drawActivitiesRoute();
                }
            },

            addMarker(event) {
                if (this.add_act_dialog && this.formMode === 0) {
                    // Nếu đang add mà thêm marker thứ 2 thì xóa cái trước nó
                    let flag = this.activities.length;
                    this.new_act_idx = this.markers.length;
                    if (this.new_act_idx - flag === 1) {
                        this.markers.splice(this.new_act_idx - 1, 1);
                        this.new_act_idx = this.new_act_idx - 1;
                    }

                    this.new_marker = {
                        Index: this.new_act_idx,
                        LatLon: [event.latlng.lat, event.latlng.lng]
                    }
                    this.markers.push(this.new_marker);
                }
            },

            editMarker(dialog, editMarkerId) {
                //console.log(this.formMode);
                if (this.formMode === 2) { // Nếu đang add mà click lại vào marker thì ko chuyển thành edit
                    this.formMode = 1;
                    this.editMarkerId = editMarkerId;

                    this.add_act_dialog = dialog;
                }
            },

            filterCategory(selected_category_from_child) {
                this.selected_category = selected_category_from_child;
                if (this.selected_category) {
                    console.log('chosen: ', this.selected_category);
                    axios.get(
                        this.filter_category_url.replace('cate_str', this.selected_category)
                    ).then((res) => {
                        this.poiMarkers = res.data;
                        console.log(this.poiMarkers);
                    }).catch((res) => {
                        console.log(res);
                    });
                } else {
                    this.poiMarkers = [];
                }
            },

            findTrip(dialog) {
                console.log('finding trip...');
                this.find_trip_dialog = dialog;
            },
        },
        watch: {
        },
    };
</script> 

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    @import "../css/Home.css";
</style>

