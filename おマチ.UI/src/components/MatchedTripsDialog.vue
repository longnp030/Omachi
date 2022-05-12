<template>
    <v-container id="matched-trips-dialog-container">
        <v-row dense v-if="matchedTrips_to_mutate">
            <v-col v-for="(matchedTrip, i) in matchedTrips_to_mutate"
                   :key="matchedTrip.Id"
                   cols="12"
                   class="matched-trip-row">
                <v-card :color="'blue'" dark>
                     <!--TODO: fix color--> 
                    
                    <v-card-title class="text-h5"
                                  v-text="'Chuyến #' + (i+1)"></v-card-title>
                    <v-card-text>
                        <div><b>Mã chuyến xe:</b> <i>{{matchedTrip.Id}}</i></div>
                        <div><b>Khởi hành:</b> <i>{{matchedTrip.StartTime}}</i></div>
                        <div><b>Đến lúc:</b> <i>{{matchedTrip.ArrivalTime}}</i></div>
                        <div><b>Địa điểm:</b> <i>{{matchedTrip.POI}}</i></div>
                        <!--<div> TODO: add api get user by id to show avatar and name </div>-->
                    </v-card-text>

                    <v-card-actions>
                        <v-btn class="ml-2 mt-5"
                               outlined
                               rounded
                               small
                               @click="rejectTrip(i)">
                            Từ chối
                        </v-btn>
                        <v-spacer></v-spacer>
                        <v-btn class="ml-2 mt-5"
                               outlined
                               rounded
                               small
                               @click="acceptTrip(matchedTrips_to_mutate.Id)">
                            Chấp nhận
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-col>
            <!--<v-col>Chúng tôi tìm được {{matchedTrips_to_mutate.length}} chuyến đi cho bạn!</v-col>-->
        </v-row>

        <!--<v-row v-else>
            <b-overlay show class="find-trip-overlay">
                <b-card class="finding-trip-card" title="Đang tìm chuyến đi...">
                </b-card>
            </b-overlay>
        </v-row>-->
    </v-container>
</template>

<script>
    //import axios from "axios";
    export default {
        name: "MatchedTripsDialog",
        props: {
            matchedTrips: {
                type: Array,
                required: true,
                default() {
                    return []
                },
            },
            jwt_token: {
                type: String,
                required: true,
                default: '',
            },
        },
        data() {
            return {
                matchedTrips_to_mutate: null,
                matchedTripsUrl: "https://localhost:5001/MatchedTrip/matched_trip_id",
            }
        },
        methods: {
            rejectTrip(idx) {
                this.matchedTrips_to_mutate.splice(idx, 1);
                //axios.patch(
                //    this.matchedTripsUrl.replace('matched_trip_id', matchedTripId),
                //    JSON.parse(JSON.stringify({Users: })), // TODO: make api to get matched trip by id -> del this user id in string
                //    {
                //        headers: {
                //            Authorization: `Bearer ${this.jwt_token}`
                //        }
                //    }
                //)
            },
            acceptTrip() {
                this.matchedTrips_to_mutate = null;
            }
        },
        watch: {
            matchedTrips: {
                immediate: true,
                deep: true,
                handler: function () {
                    this.matchedTrips_to_mutate = this.matchedTrips;
                }
            }
        }
    }
</script>

<style scoped>
    .finding-trip-card {
        background-color: #006688;
        color: #000;
    }
    #matched-trips-dialog-container {
        width: 480px;
        margin-left: 6px;
    }
    .v-card__title {
        padding: 0 16px;
    }
    .v-card {
        max-height: 222px;
        width: 480px;
    }
    .v-card__actions {
        margin-top: -70px;
    }
</style>