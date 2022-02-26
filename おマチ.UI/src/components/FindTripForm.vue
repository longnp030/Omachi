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
                            <v-text-field label="Place"
                                          required></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-menu ref="menu"
                                    v-model="menu2"
                                    :close-on-content-click="false"
                                    :nudge-right="40"
                                    :return-value.sync="carRequest.StartTime"
                                    transition="scale-transition"
                                    offset-y
                                    max-width="290px"
                                    min-width="290px">
                                <template v-slot:activator="{ on, attrs }">
                                    <v-text-field label="StartTime*"
                                                  v-model="carRequest.StartTime"
                                                  readonly
                                                  v-bind="attrs"
                                                  v-on="on"></v-text-field>
                                </template>
                                <v-time-picker v-if="menu2"
                                               v-model="carRequest.StartTime"
                                               full-width
                                               use-seconds
                                               @click:second="$refs.menu.save(carRequest.StartTime)"></v-time-picker>
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
        },
        data() {
            return {
                findTripFormName: "Tìm chuyến đi chung",
                menu2: false,
                dialog: false,
                carRequest: {},
                // 2 data để mutate trong con, dùng làm v-model để hiển thị cate trong con
                selected_category_for_mutation: null,
                categories_for_mutation: [],
            };
        },
        methods: {
            cancelFindTrip() {
                this.$refs.form.reset();
                this.$emit("findTrip", false);
            },
            confirmFindTrip() {

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
        }
    }
</script>

<style scoped>
</style>