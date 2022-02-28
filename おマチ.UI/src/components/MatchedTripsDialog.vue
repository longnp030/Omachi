<template>
    <v-container id="matched-trips-dialog-container">
        <v-row dense v-if="matchedTrips_to_mutate">
            <v-col v-for="(matchedTrip, i) in matchedTrips_to_mutate"
                   :key="matchedTrip.Id"
                   cols="12"
                   class="matched-trip-row">
                <v-card :color="'blue'" dark>
                    <!-- TODO: fix color -->
                    
                    <v-card-title class="text-h5"
                                  v-text="'Trip #' + i"></v-card-title>
                    <v-card-text>
                        <div>Start time: {{matchedTrips_to_mutate.StartTime}}</div>
                        <div>Arrival time: {{matchedTrips_to_mutate.ArrivalTime}}</div>
                        <div></div>
                    </v-card-text>

                    <v-card-actions>
                        <v-btn class="ml-2 mt-5"
                               outlined
                               rounded
                               small
                               @click="rejectTrip(i)">
                            Reject
                        </v-btn>
                        <v-spacer></v-spacer>
                        <v-btn class="ml-2 mt-5"
                               outlined
                               rounded
                               small
                               @click="acceptTrip(matchedTrips_to_mutate.Id)">
                            Accept
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-col>
        </v-row>
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
</style>