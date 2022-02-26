(function(t){function e(e){for(var i,n,s=e[0],c=e[1],l=e[2],d=0,f=[];d<s.length;d++)n=s[d],Object.prototype.hasOwnProperty.call(a,n)&&a[n]&&f.push(a[n][0]),a[n]=0;for(i in c)Object.prototype.hasOwnProperty.call(c,i)&&(t[i]=c[i]);u&&u(e);while(f.length)f.shift()();return o.push.apply(o,l||[]),r()}function r(){for(var t,e=0;e<o.length;e++){for(var r=o[e],i=!0,s=1;s<r.length;s++){var c=r[s];0!==a[c]&&(i=!1)}i&&(o.splice(e--,1),t=n(n.s=r[0]))}return t}var i={},a={app:0},o=[];function n(e){if(i[e])return i[e].exports;var r=i[e]={i:e,l:!1,exports:{}};return t[e].call(r.exports,r,r.exports,n),r.l=!0,r.exports}n.m=t,n.c=i,n.d=function(t,e,r){n.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:r})},n.r=function(t){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},n.t=function(t,e){if(1&e&&(t=n(t)),8&e)return t;if(4&e&&"object"===typeof t&&t&&t.__esModule)return t;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var i in t)n.d(r,i,function(e){return t[e]}.bind(null,i));return r},n.n=function(t){var e=t&&t.__esModule?function(){return t["default"]}:function(){return t};return n.d(e,"a",e),e},n.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},n.p="/";var s=window["webpackJsonp"]=window["webpackJsonp"]||[],c=s.push.bind(s);s.push=e,s=s.slice();for(var l=0;l<s.length;l++)e(s[l]);var u=c;o.push([0,"chunk-vendors"]),r()})({0:function(t,e,r){t.exports=r("56d7")},"034f":function(t,e,r){"use strict";r("85ec")},"202c":function(t,e,r){"use strict";r("6016")},4613:function(t,e,r){"use strict";r("4a3c")},"4a3c":function(t,e,r){},"56d7":function(t,e,r){"use strict";r.r(e);r("e260"),r("e6cf"),r("cca6"),r("a79d");var i=r("2b0e"),a=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",{attrs:{id:"app"}},[r("router-view")],1)},o=[],n={name:"app",components:{},created:function(){this.$router.push("home",(function(){}))}},s=n,c=(r("034f"),r("2877")),l=Object(c["a"])(s,a,o,!1,null,null,null),u=l.exports,d=r("8c4f"),f=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("v-app",{attrs:{id:"login","data-app":""}},[r("v-form",{ref:"form",staticClass:"login-form",attrs:{"lazy-validation":""},model:{value:t.valid,callback:function(e){t.valid=e},expression:"valid"}},[r("v-text-field",{attrs:{label:"Email",rules:[t.rules.required,t.rules.email]},model:{value:t.authModel.Email,callback:function(e){t.$set(t.authModel,"Email",e)},expression:"authModel.Email"}}),r("v-text-field",{attrs:{label:"Password",rules:[t.rules.required],type:"password"},model:{value:t.authModel.Password,callback:function(e){t.$set(t.authModel,"Password",e)},expression:"authModel.Password"}}),r("v-checkbox",{attrs:{color:"red",label:"Remember me"},model:{value:t.saveCred,callback:function(e){t.saveCred=e},expression:"saveCred"}}),r("v-btn",{staticClass:"mr-4",attrs:{disabled:!t.valid,color:"success"},on:{click:t.login}},[t._v(" Login ")]),r("v-btn",{staticClass:"mr-4",attrs:{color:"success"},on:{click:t.register}},[t._v(" Register ")])],1)],1)},h=[],m=r("bc3a"),v=r.n(m),p={name:"Login",data:function(){return{authUrl:"https://localhost:5001/Users/authenticate",valid:!0,saveCred:!1,rules:{required:function(t){return!!t||"Required."},email:function(t){var e=/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;return e.test(t)||"Invalid email."}},authModel:{}}},mounted:function(){console.log(this.saveCred)},methods:{saveCookie:function(t,e,r){this.$cookies.set("omachi-auth-token",t,r),this.$cookies.set("omachi-user-id",e,r)},login:function(){var t=this;this.$refs.form.validate(),v.a.post(this.authUrl,JSON.parse(JSON.stringify(this.authModel))).then((function(e){var r=e.data.JwtToken,i=e.data.Id;console.log(t.saveCred),t.saveCred?t.saveCookie(r,i,-1):t.saveCookie(r,i,0),t.$nextTick((function(){this.$router.push({name:"home",params:{authToken:r,userId:i}})}))})).catch((function(t){console.log(t)}))},register:function(){this.$router.push("register")}},watch:{saveCred:function(){console.log("saveCred is set to: ",this.saveCred)}}},g=p,_=(r("7930"),Object(c["a"])(g,f,h,!1,null,"06cca53c",null)),k=_.exports,y=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("v-app",{attrs:{id:"register","data-app":""}},[r("v-form",{ref:"form",staticClass:"register-form",attrs:{"lazy-validation":""},model:{value:t.valid,callback:function(e){t.valid=e},expression:"valid"}},[r("v-text-field",{attrs:{label:"Email",rules:[t.rules.required,t.rules.email]},model:{value:t.registerModel.Email,callback:function(e){t.$set(t.registerModel,"Email",e)},expression:"registerModel.Email"}}),r("v-text-field",{attrs:{label:"Password",rules:[t.rules.required],type:"password"},model:{value:t.confirmPwd,callback:function(e){t.confirmPwd=e},expression:"confirmPwd"}}),r("v-text-field",{attrs:{label:"Confirm Password",rules:[t.rules.required,t.rules.matchPwd],type:"password"},model:{value:t.registerModel.Password,callback:function(e){t.$set(t.registerModel,"Password",e)},expression:"registerModel.Password"}}),r("v-checkbox",{attrs:{color:"red",rules:[function(t){return!!t||"You must agree to continue!"}],label:"I agree to the terms of services"}}),r("v-checkbox",{attrs:{color:"red",label:"Save credentials for next login."},model:{value:t.saveCred,callback:function(e){t.saveCred=e},expression:"saveCred"}}),r("v-btn",{staticClass:"mr-4",attrs:{disabled:!t.valid,color:"success"},on:{click:t.register}},[t._v(" Register ")])],1)],1)},b=[],w={name:"Register",data:function(){var t=this;return{registerUrl:"https://localhost:5001/Users/register",authUrl:"https://localhost:5001/Users/authenticate",valid:!0,saveCred:!1,registerModel:{},confirmPwd:"",rules:{required:function(t){return!!t||"Required."},email:function(t){var e=/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;return e.test(t)||"Invalid email."},matchPwd:function(e){return e===t.confirmPwd||"Passwords do not match."}}}},methods:{saveCookie:function(t,e,r){this.$cookies.set("omachi-auth-token",t,r),this.$cookies.set("omachi-user-id",e,r)},register:function(){var t=this;this.$refs.form.validate(),v.a.post(this.registerUrl,JSON.parse(JSON.stringify(this.registerModel))).then((function(){var e={Email:t.registerModel.Email,Password:t.registerModel.Password};v.a.post(t.authUrl,e).then((function(e){var r=e.data.JwtToken,i=e.data.Id;t.saveCred?t.saveCookie(r,i,"7d"):t.saveCookie(r,i,0),t.$nextTick((function(){this.$router.push({name:"update-profile",params:{authToken:r,userId:i,userEmail:e.data.Email}})}))})).catch((function(t){console.log(t)}))})).catch((function(t){console.log(t)}))}}},x=w,T=(r("4613"),Object(c["a"])(x,y,b,!1,null,"6eaef396",null)),S=T.exports,C=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",{attrs:{id:"home","data-app":""}},[r("div",{attrs:{id:"nav"}},[r("NavigationPanel",{on:{logout:t.logout}})],1),r("div",{attrs:{id:"map"}},[r("l-map",{attrs:{zoom:t.zoom,center:t.center},on:{click:t.addMarker}},[r("l-tile-layer",{attrs:{url:t.url,attribution:t.attribution}}),t.curr_loc?r("l-circle-marker",{attrs:{color:"red",fillColor:"red",fillOpacity:.8,radius:8,"lat-lng":[t.curr_loc.latitude,t.curr_loc.longitude]}}):t._e(),r("l-control",{attrs:{id:"map-utility-controls"}},[r("v-combobox",{attrs:{id:"poi-filter",items:t.categories,label:"Category",clearable:"",dense:"","hide-selected":"","persistent-hint":"","small-chips":"",solo:""},on:{input:t.filterCategory},model:{value:t.selected_category,callback:function(e){t.selected_category=e},expression:"selected_category"}}),r("v-btn",{on:{click:function(e){t.showActivities=!t.showActivities}}},[t._v(" Toggle activities route ")]),r("v-dialog",{attrs:{persistent:""},scopedSlots:t._u([{key:"activator",fn:function(e){var i=e.on,a=e.attrs;return[r("v-btn",t._g(t._b({on:{click:function(e){return t.addActivity(!t.add_act_dialog,0)}}},"v-btn",a,!1),i),[t._v(" Add activity ")])]}}]),model:{value:t.add_act_dialog,callback:function(e){t.add_act_dialog=e},expression:"add_act_dialog"}},[r("ActivityForm",{attrs:{user_id:t.userId,jwt_token:t.jwtToken,formMode:t.formMode,marker:t.new_marker,editMarkerId:t.editMarkerId},on:{addActivity:t.addActivity}})],1),r("v-dialog",{attrs:{persistent:""},scopedSlots:t._u([{key:"activator",fn:function(e){var i=e.on,a=e.attrs;return[r("v-btn",t._g(t._b({on:{click:function(e){return t.findTrip(!t.find_trip_dialog)}}},"v-btn",a,!1),i),[t._v(" Find trip ")])]}}]),model:{value:t.find_trip_dialog,callback:function(e){t.find_trip_dialog=e},expression:"find_trip_dialog"}},[r("FindTripForm",{attrs:{user_id:t.userId,jwt_token:t.jwtToken,selected_category:t.selected_category,categories:t.categories,startLat:t.center[0],startLon:t.center[1],poiId:t.destPoiId},on:{findTrip:t.findTrip,filterCategory:t.filterCategory}})],1)],1),t._l(t.markers,(function(e){return r("l-circle-marker",{key:e.Id,attrs:{color:"DarkSlateBlue",fillColor:"DarkSlateBlue",fillOpacity:.8,radius:8,"lat-lng":e.LatLon,visible:t.showActivities},on:{click:function(r){return t.editMarker(!t.add_act_dialog,e.Id)}}},[r("l-tooltip",[t._v(" Name: "+t._s(e.Name)+" "),r("br"),t._v(" StartTime: "+t._s(e.StartTime)+" ")])],1)})),r("l-polyline",{attrs:{"lat-lngs":t.polyline.latlons,color:t.polyline.color,visible:t.showActivities}}),t._l(t.poiMarkers,(function(e){return r("l-circle-marker",{key:e.Id,attrs:{color:"green",fillColor:"green",fillOpacity:.8,radius:8,"lat-lng":[e.Lat,e.Lon]},on:{click:function(r){return t.chooseTripDest(r,e.Id)}}},[r("l-tooltip",[t._v(" Name: "+t._s(e.Name)+" "),r("br"),t._v(" Address: "+t._s(e.Address)+" "),r("br"),t._v(" OpenTime: "+t._s(e.OpenTime)+" "),r("br"),t._v(" CloseTime: "+t._s(e.CloseTime)+" "),r("br"),t._v(" Rating: "+t._s(e.Rating)+" "),r("br"),t._v(" Category: "+t._s(e.Category)+" ")])],1)})),t.chosenPoi?r("l-circle-marker",{attrs:{color:"yellow",fillColor:"yellow",fillOpacity:.8,radius:8,"lat-lng":[t.chosenPoi.Lat,t.chosenPoi.Lon]}}):t._e()],2)],1)])},M=[],R=r("1da1"),$=(r("99af"),r("ac1f"),r("5319"),r("a434"),r("96cf"),function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",{attrs:{id:"navigation-panel"}},[r("div",{staticClass:"left",attrs:{id:"logo"}}),r("div",{staticClass:"right"},[t._v(t._s(t.user)+" "),r("v-btn",{staticClass:"mr-4",attrs:{color:"success"},on:{click:t.logout}},[t._v(" Login ")])],1)])}),I=[],O={name:"NavigationPanel",data:function(){return{user:null}},created:function(){},methods:{logout:function(){this.$emit("logout")}}},q=O,A=(r("e065"),Object(c["a"])(q,$,I,!1,null,"74c8850a",null)),j=A.exports,P=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("v-card",[r("v-form",{ref:"form"},[r("v-card-title",[r("span",{staticClass:"text-h5"},[t._v(t._s(t.actFormName))])]),r("v-card-text",[r("v-container",[r("v-row",[r("v-col",{attrs:{cols:"12"}},[r("v-text-field",{attrs:{label:"Name*",rules:[t.rules.required],required:""},model:{value:t.activity.Name,callback:function(e){t.$set(t.activity,"Name",e)},expression:"activity.Name"}})],1),r("v-col",{attrs:{cols:"12"}},[r("v-menu",{ref:"menu",attrs:{"close-on-content-click":!1,"nudge-right":40,"return-value":t.activity.StartTime,transition:"scale-transition","offset-y":"","max-width":"290px","min-width":"290px"},on:{"update:returnValue":function(e){return t.$set(t.activity,"StartTime",e)},"update:return-value":function(e){return t.$set(t.activity,"StartTime",e)}},scopedSlots:t._u([{key:"activator",fn:function(e){var i=e.on,a=e.attrs;return[r("v-text-field",t._g(t._b({attrs:{rules:[t.rules.required],label:"StartTime*",readonly:""},model:{value:t.activity.StartTime,callback:function(e){t.$set(t.activity,"StartTime",e)},expression:"activity.StartTime"}},"v-text-field",a,!1),i))]}}]),model:{value:t.menu2,callback:function(e){t.menu2=e},expression:"menu2"}},[t.menu2?r("v-time-picker",{attrs:{"full-width":"","use-seconds":""},on:{"click:second":function(e){return t.$refs.menu.save(t.activity.StartTime)}},model:{value:t.activity.StartTime,callback:function(e){t.$set(t.activity,"StartTime",e)},expression:"activity.StartTime"}}):t._e()],1)],1)],1)],1)],1),r("v-card-actions",[1==t.formMode?r("v-dialog",{attrs:{persistent:"","max-width":"290"},scopedSlots:t._u([{key:"activator",fn:function(e){var i=e.on,a=e.attrs;return[r("v-btn",t._g(t._b({attrs:{color:"warning",dark:""}},"v-btn",a,!1),i),[t._v(" Delete ")])]}}],null,!1,4099921117),model:{value:t.dialog,callback:function(e){t.dialog=e},expression:"dialog"}},[r("v-card",[r("v-card-title",{staticClass:"text-h5"},[t._v(" Do you want to delete activity? ")]),r("v-card-actions",[r("v-spacer"),r("v-btn",{attrs:{color:"green darken-1",text:""},on:{click:function(e){t.dialog=!1}}},[t._v(" Disagree ")]),r("v-btn",{attrs:{color:"green darken-1",text:""},on:{click:t.deleteActivity}},[t._v(" Agree ")])],1)],1)],1):t._e(),r("v-spacer"),r("v-btn",{attrs:{color:"blue darken-1",text:""},on:{click:t.cancelActivity}},[t._v(" Close ")]),r("v-btn",{attrs:{color:"blue darken-1",text:""},on:{click:t.saveActivity}},[t._v(" Save ")])],1)],1)],1)},L=[],N=(r("a9e3"),{name:"ActivityForm",components:{},data:function(){return{actFormName:"Thêm mới hoạt động",rules:{required:function(t){return!!t||"Required."}},menu2:!1,dialog:!1,activity:{},add_act_url:"https://localhost:5001/Users/user_id/activities",get_act_url:"https://localhost:5001/Activities/act_id"}},props:{user_id:{type:String,required:!0,default:""},jwt_token:{type:String,required:!0,default:""},marker:{type:Object,required:!0,default:function(){return{}}},formMode:{type:Number,required:!0,default:2},editMarkerId:{type:String,required:!0,default:""}},methods:{cancelActivity:function(){this.activity={},this.$refs.form.reset(),this.$emit("addActivity",!1,2)},saveActivity:function(){var t=this;this.$refs.form.validate(),0===this.formMode?this.marker.LatLon&&(this.activity.Lat=this.marker.LatLon[0],this.activity.Lon=this.marker.LatLon[1],v.a.post(this.add_act_url.replace("user_id",this.user_id),JSON.parse(JSON.stringify(this.activity)),{headers:{Authorization:"Bearer ".concat(this.jwt_token)}}).then((function(e){console.log(e),t.cancelActivity()})).catch((function(t){console.log(t)}))):v.a.put(this.get_act_url.replace("act_id",this.editMarkerId),JSON.parse(JSON.stringify(this.activity)),{headers:{Authorization:"Bearer ".concat(this.jwt_token)}}).then((function(e){console.log(e),t.cancelActivity()})).catch((function(t){console.log(t)}))},deleteActivity:function(){var t=this;console.log(this.editMarkerId),this.dialog=!1,v.a.delete(this.get_act_url.replace("act_id",this.editMarkerId),{headers:{Authorization:"Bearer ".concat(this.jwt_token)}}).then((function(e){console.log(e),t.cancelActivity()})).catch((function(t){console.log(t)}))}},watch:{formMode:{immediate:!0,deep:!0,handler:function(){1===this.formMode?this.actFormName="Sửa hoạt động":this.actFormName="Thêm mới hoạt động"}},marker:{immediate:!0,deep:!0,handler:function(){console.log(this.marker)}},editMarkerId:{immediate:!0,deep:!0,handler:function(){console.log(this.editMarkerId);var t=this;1==this.formMode&&v.a.get(this.get_act_url.replace("act_id",this.editMarkerId),{headers:{Authorization:"Bearer ".concat(this.jwt_token)}}).then((function(e){console.log(e),t.activity=e.data})).catch((function(t){console.log(t)}))}}}}),F=N,U=(r("c8dd"),Object(c["a"])(F,P,L,!1,null,"570f7572",null)),E=U.exports,z=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("v-card",[r("v-form",{ref:"form"},[r("v-card-title",[r("span",{staticClass:"text-h5"},[t._v(t._s(t.findTripFormName))])]),r("v-card-text",[r("v-container",[r("v-row",[r("v-col",{attrs:{cols:"12"}},[r("v-combobox",{attrs:{id:"poi-filter",items:t.categories_for_mutation,label:"Category",clearable:"",dense:"","hide-selected":"","persistent-hint":"","small-chips":"",solo:""},on:{input:function(e){return t.filterCategory(t.selected_category_for_mutation)}},model:{value:t.selected_category_for_mutation,callback:function(e){t.selected_category_for_mutation=e},expression:"selected_category_for_mutation"}})],1),r("v-col",{attrs:{cols:"12"}},[r("v-text-field",{attrs:{label:"Place",value:t.poi_name,readonly:""}})],1),r("v-col",{attrs:{cols:"12"}},[r("v-menu",{ref:"menu",attrs:{"close-on-content-click":!1,"nudge-right":40,"return-value":t.carRequest.StartTime,transition:"scale-transition","offset-y":"","max-width":"290px","min-width":"290px"},on:{"update:returnValue":function(e){return t.$set(t.carRequest,"StartTime",e)},"update:return-value":function(e){return t.$set(t.carRequest,"StartTime",e)}},scopedSlots:t._u([{key:"activator",fn:function(e){var i=e.on,a=e.attrs;return[r("v-text-field",t._g(t._b({attrs:{label:"StartTime*",rules:[t.rules.required],readonly:""},model:{value:t.carRequest.StartTime,callback:function(e){t.$set(t.carRequest,"StartTime",e)},expression:"carRequest.StartTime"}},"v-text-field",a,!1),i))]}}]),model:{value:t.menu2,callback:function(e){t.menu2=e},expression:"menu2"}},[t.menu2?r("v-time-picker",{attrs:{"full-width":"","use-seconds":""},on:{"click:second":function(e){return t.$refs.menu.save(t.carRequest.StartTime)}},model:{value:t.carRequest.StartTime,callback:function(e){t.$set(t.carRequest,"StartTime",e)},expression:"carRequest.StartTime"}}):t._e()],1)],1)],1)],1)],1),r("v-card-actions",[r("v-spacer"),r("v-btn",{attrs:{color:"blue darken-1",text:""},on:{click:t.cancelFindTrip}},[t._v(" Cancel ")]),r("v-btn",{attrs:{color:"blue darken-1",text:""},on:{click:t.confirmFindTrip}},[t._v(" Find ")])],1)],1)],1)},J=[],B={name:"FindTripForm",props:{user_id:{type:String,required:!0,default:""},jwt_token:{type:String,required:!0,default:""},selected_category:{type:String,required:!1,default:null},categories:{type:Array,required:!1,default:function(){return[]}},startLat:Number,startLon:Number,poiId:String},data:function(){return{findTripFormName:"Tìm chuyến đi chung",menu2:!1,dialog:!1,carRequest:{},rules:{required:function(t){return!!t||"Required."}},selected_category_for_mutation:null,categories_for_mutation:[],poi:null,poi_name:"",get_poi_url:"https://localhost:5001/POIs/poi_id",matching_url:"https://localhost:5001/Users/user_id/matching"}},methods:{cancelFindTrip:function(){this.$refs.form.reset(),this.$emit("findTrip",!1)},confirmFindTrip:function(){var t=this;this.$refs.form.validate(),this.carRequest.StartTime&&(this.buildCarRequest(),v.a.put(this.matching_url,JSON.parse(JSON.stringify(this.carRequest)),{headers:{Authorization:"Bearer ".concat(this.jwt_token)}}).then((function(e){console.log(e),t.cancelActivity()})).catch((function(t){console.log(t)})))},buildCarRequest:function(){this.carRequest.UserId=this.user_id,this.carRequest.StartLat=this.startLat,this.carRequest.StartLon=this.startLon,this.carRequest.PointOfInterest=this.poi,this.carRequest.Timestamp=Date(),console.log(this.carRequest.StartTime),console.log(this.carRequest)},filterCategory:function(t){this.$emit("filterCategory",t)}},watch:{selected_category:{immediate:!0,deep:!0,handler:function(){this.selected_category_for_mutation=this.selected_category}},categories:{immediate:!0,deep:!0,handler:function(){this.categories_for_mutation=this.categories}},poiId:{deep:!0,handler:function(){var t=this;console.log(this.poiId),v.a.get(this.get_poi_url.replace("poi_id",this.poiId)).then((function(e){t.poi=e.data,t.poi_name=t.poi.Name,console.log(t.poi)})).catch((function(t){console.log(t)}))}}}},D=B,Z=Object(c["a"])(D,z,J,!1,null,"a6404c60",null),H=Z.exports,V={name:"Home",components:{NavigationPanel:j,ActivityForm:E,FindTripForm:H},data:function(){return{getUserUrl:"https://localhost:5001/Users/",add_getall_actsUrl:"https://localhost:5001/Users/user_id/activities",jwtToken:"",user:null,userId:null,activities:null,showActivities:!0,curr_loc_options:{enableHighAccuracy:!0,timeout:5e3,maximumAge:0},curr_loc:null,url:"https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",attribution:'&copy; <a target="_blank" href="http://osm.org/copyright">OpenStreetMap</a> contributors',zoom:14,center:[0,0],markers:[],polyline:{latlons:[],color:"SlateBlue"},add_act_dialog:!1,find_trip_dialog:!1,selected_category:null,categories:[],poiMarkers:[],all_categories_url:"https://localhost:5001/POIs/Categories",filter_category_url:"https://localhost:5001/POIs/Filter?category=cate_str",formMode:2,new_act_idx:0,new_marker:{},editMarkerId:"",destPoiId:"",chosenPoi:null}},props:{},created:function(){this.jwtToken=this.$route.params.authToken,void 0===this.jwtToken&&(this.jwtToken=this.$cookies.get("omachi-auth-token"),null===this.jwtToken&&this.$router.push("login"))},mounted:function(){var t=this;return Object(R["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return e.next=2,t.getUser();case 2:return e.next=4,t.getCurrLoc();case 4:return e.next=6,t.getCategories();case 6:return e.next=8,t.drawActivitiesRoute();case 8:case"end":return e.stop()}}),e)})))()},methods:{logout:function(){this.jwtToken=this.$cookies.get("omachi-auth-token"),null!==this.jwtToken&&this.$cookies.remove("omachi-auth-token"),this.$nextTick((function(){this.$router.push("login")}))},getUser:function(){var t=this;return Object(R["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:if(t.userId=t.$cookies.get("omachi-user-id"),null===t.userId){e.next=4;break}return e.next=4,v.a.get(t.getUserUrl+t.userId,{headers:{Authorization:"Bearer ".concat(t.jwtToken)}}).then((function(e){t.user=e.data})).catch((function(t){console.log(t)}));case 4:case"end":return e.stop()}}),e)})))()},curr_loc_success:function(t){this.curr_loc=t.coords,this.center=[this.curr_loc.latitude,this.curr_loc.longitude],console.log("Your current position is:",this.curr_loc),console.log("Latitude : ".concat(this.curr_loc.latitude)),console.log("Longitude: ".concat(this.curr_loc.longitude)),console.log("More or less ".concat(this.curr_loc.accuracy," meters."))},curr_loc_error:function(t){console.warn("ERROR(".concat(t.code,"): ").concat(t.message))},getCurrLoc:function(){var t=this;return Object(R["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:navigator.geolocation.getCurrentPosition(t.curr_loc_success,t.curr_loc_error,t.curr_loc_options);case 1:case"end":return e.stop()}}),e)})))()},getActivities:function(){var t=this;return Object(R["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return console.log("getting activities..."),e.next=3,v.a.get(t.add_getall_actsUrl.replace("user_id",t.userId),{headers:{Authorization:"Bearer ".concat(t.jwtToken)}}).then((function(e){t.activities=e.data})).catch((function(t){console.log(t)}));case 3:case"end":return e.stop()}}),e)})))()},getCategories:function(){var t=this;return Object(R["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return console.log("getting categories..."),e.next=3,v.a.get(t.all_categories_url).then((function(e){t.categories=e.data})).catch((function(t){console.log(t)}));case 3:case"end":return e.stop()}}),e)})))()},drawActivitiesRoute:function(){var t=this;return Object(R["a"])(regeneratorRuntime.mark((function e(){var r,i;return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return console.log("drawing route..."),t.markers=[],t.polyline.latlons=[],e.next=5,t.getActivities();case 5:if(t.activities.length>1)for(r in console.log("drawing..."),t.activities)i=t.activities[r],t.markers.push({Index:r,Id:i.Id,LatLon:[i.Lat,i.Lon],Name:i.Name,StartTime:i.StartTime}),t.polyline.latlons.push([i.Lat,i.Lon]);case 6:case"end":return e.stop()}}),e)})))()},addActivity:function(t,e){var r=this;return Object(R["a"])(regeneratorRuntime.mark((function i(){return regeneratorRuntime.wrap((function(i){while(1)switch(i.prev=i.next){case 0:if(r.new_act_idx&&(console.log("splicing..."),r.markers.splice(r.new_act_idx,1)),r.add_act_dialog=t,r.formMode=e,2!==r.formMode){i.next=6;break}return i.next=6,r.drawActivitiesRoute();case 6:case"end":return i.stop()}}),i)})))()},addMarker:function(t){if(this.add_act_dialog&&0===this.formMode){var e=this.activities.length;this.new_act_idx=this.markers.length,this.new_act_idx-e===1&&(this.markers.splice(this.new_act_idx-1,1),this.new_act_idx=this.new_act_idx-1),this.new_marker={Index:this.new_act_idx,LatLon:[t.latlng.lat,t.latlng.lng]},this.markers.push(this.new_marker)}},editMarker:function(t,e){2===this.formMode&&(this.formMode=1,this.editMarkerId=e,this.add_act_dialog=t)},filterCategory:function(t){var e=this;this.selected_category=t,this.selected_category?(console.log("chosen: ",this.selected_category),v.a.get(this.filter_category_url.replace("cate_str",this.selected_category)).then((function(t){e.poiMarkers=t.data,console.log(e.poiMarkers)})).catch((function(t){console.log(t)}))):this.poiMarkers=[]},findTrip:function(t){this.find_trip_dialog=t,t||(this.chosenPoi=null)},chooseTripDest:function(t,e){this.find_trip_dialog&&(this.destPoiId=e,this.chosenPoi={Lat:t.target._latlng.lat,Lon:t.target._latlng.lng})}},watch:{}},Y=V,G=(r("202c"),Object(c["a"])(Y,C,M,!1,null,"c8b57e72",null)),K=G.exports;i["default"].use(d["a"]);var Q=[{path:"/login",name:"login",component:k},{path:"/register",name:"register",component:S},{path:"/home",name:"home",component:K}],W=new d["a"]({mode:"history",routes:Q}),X=W,tt=r("ce5b"),et=r.n(tt);r("bf40"),r("5363");i["default"].use(et.a);var rt={theme:{dark:!0,themes:{primary:"#1976D2",secondary:"#424242",accent:"#82B1FF",error:"#FF5252",info:"#2196F3",success:"#4CAF50",warning:"#FB8C00"}}},it=new et.a(rt),at=(r("d1e78"),r("2b27")),ot=r.n(at),nt=r("2699"),st=r("a40a"),ct=r("4e2b"),lt=r("31dc"),ut=r("635f"),dt=r("2253"),ft=r("ea97"),ht=(r("6cc5"),r("e11e"));i["default"].config.productionTip=!1,i["default"].use(ot.a),i["default"].use(d["a"]),i["default"].component("l-map",nt["a"]),i["default"].component("l-tile-layer",st["a"]),i["default"].component("l-marker",ct["a"]),i["default"].component("l-tooltip",lt["a"]),i["default"].component("l-polyline",ut["a"]),i["default"].component("l-control",dt["a"]),i["default"].component("l-circle-marker",ft["a"]),delete ht["Icon"].Default.prototype._getIconUrl,ht["Icon"].Default.mergeOptions({iconRetinaUrl:r("584d"),iconUrl:r("6397"),shadowUrl:r("e2b9")}),new i["default"]({router:X,vuetify:it,render:function(t){return t(u)}}).$mount("#app")},6016:function(t,e,r){},6488:function(t,e,r){},7930:function(t,e,r){"use strict";r("6488")},"85ec":function(t,e,r){},aa92:function(t,e,r){},c8dd:function(t,e,r){"use strict";r("aa92")},d557:function(t,e,r){},e065:function(t,e,r){"use strict";r("d557")}});
//# sourceMappingURL=app.ad86586a.js.map