import cookie from "js-cookie";


const SET_TOKEN = "set_token"
const SET_USERNAME = "set_username"

export default {
  state: {
    access_token: "",
    username: "",
    userId: "",
    name: ""
  },

  mutations: {
    [SET_TOKEN](state, access_token) {
      state.access_token = access_token;
    },
    [SET_USERNAME](state, username) {
      state.username = username
    },

  },
  actions: {
    set_token({ commit }, access_token) {
      commit(SET_TOKEN, access_token);
      cookie.set("access_token", access_token, { expires: 1 });
    },
    del_token({ commit }) {
      commit(SET_TOKEN, "")
      cookie.remove("access_token");
    }
  }
}