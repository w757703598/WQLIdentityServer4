<template>
  <div id="app">
    <!-- <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link>
    </div>-->
    <router-view />
    <!-- <layout></layout> -->
  </div>
</template>
<script>
import Layout from './components/Layout/index'
export default {
  components: {
    Layout,
  },
}
</script>

<style lang="scss">
#app {
  height: 100%;
}
</style>
