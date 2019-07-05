<template>
  <div id="app">
    <img src="./assets/imgs/mobo-icon.png">
    <!-- <router-view></router-view> -->
    <layout></layout>
  </div>
</template>

<script>
import layout from "./layout/index";

export default {
  data() {
    return {
      isCollapse: true
    };
  },
  name: "app",
  components: {
    layout
  }
};
</script>

