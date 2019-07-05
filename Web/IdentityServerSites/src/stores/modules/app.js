

export default {
  state: {
    isCollapse: false
  },
  mutations: {
    IS_COLLAPSE(state) {
      state.isCollapse = !state.isCollapse
    }
  },
  actions: {
    changeCollapse({ commit }) {
      commit('IS_COLLAPSE')
    }
  }
  ,
  getters: {
    isCollapse: state => state.isCollapse
  }
}