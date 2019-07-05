import Vue from "vue";

import App from "./App.vue";
import router from "./router";
import store from "./store";

//import "normalize.css/normalize.css"; // A modern alternative to CSS resets
import "./assets/common/common.css";
import "./assets/iconfont/iconfont.js";

import ElementUI from "element-ui";
import "element-ui/lib/theme-chalk/index.css";

import "./styles/index.scss";

Vue.use(ElementUI);
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
