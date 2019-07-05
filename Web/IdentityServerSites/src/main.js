import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import cookie from "js-cookie";



import ElementUI from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
import "./assets/css/index.css";
import "./assets/css/common.css"

import http from "./plugin/http"



Vue.use(ElementUI);
Vue.use(http)

Vue.config.productionTip = false;



new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
