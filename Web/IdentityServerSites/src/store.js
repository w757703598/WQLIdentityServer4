import Vue from "vue";
import Vuex from "vuex";


Vue.use(Vuex);

import app from "./stores/modules/app"
import tab from "./stores/modules/tab"
// import user from "./stores/modules/user"


export default new Vuex.Store({
  modules: {
    app,
    tab,
    //user,

  },
  state: {

  },
  mutations: {

  },
  actions: {},

});
