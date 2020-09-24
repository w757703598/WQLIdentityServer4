import Vue from "vue";
import Vuex from "vuex";
import getters from "./getters.js";

Vue.use(Vuex);

import app from "../store/modules/app.js";
import settings from "../store/modules/settings";

export default new Vuex.Store({
  modules: {
    app,
    settings
  },
  state: {},
  mutations: {},
  actions: {},
  getters
});
