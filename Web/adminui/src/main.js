import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import Element from "element-ui";
import Cookies from "js-cookie";

import "@/styles/index.scss"; // global css

import "./permission.js";

Vue.use(Element, {
  size: Cookies.get("size") || "medium" // set element-ui default size
});
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
