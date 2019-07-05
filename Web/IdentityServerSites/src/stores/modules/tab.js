export default {
  state: {
    Tabs: []
  },
  mutations: {
    OpenTab(state, tab) {
      state.Tabs.push(tab);
    }
  }
}