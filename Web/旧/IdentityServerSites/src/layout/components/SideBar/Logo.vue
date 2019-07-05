<template>
  <div class="sidebar-logo-container">
    <transition name="sidebarlogofade">
      <router-link v-if="collapse" key="collapse" class="sidebar-logo-link" to="/">
        <img v-if="logo" :src="logo" class="sidebar-logo">
        <h1 v-else class="sidebar-title>">{{title}}</h1>
      </router-link>
      <router-link v-else key="expand" class="sidebar-logo-link" to="/">
        <img v-if="logo" :src="logo" class="sidebar-logo">
        <h1 class="sidebar-title">{{ title }}</h1>
      </router-link>
    </transition>
  </div>
</template>

<script>
export default {
  props: {
    collapse: {
      type: Boolean,
      required: true
    }
  },
  data() {
    return {
      title: "WQLIdentityServer",
      logo: require("../../../assets/imgs/mobo-icon.png")
    };
  }
};
</script>
<style lang="scss" scoped>
.sidebar-logo-container {
  position: relative;
  width: 100%;
  height: 50px;
  //background: #2b2f3a;
  text-align: center;
  overflow: hidden;
  .sidebar-logo-link {
    height: 100%;
    width: 100%;
    &.sidebar-logo {
      margin-right: 0px;
      vertical-align: middle;
      width: 100px;
      height: 32px;
    }
    &.sidebar-title {
      display: inline=block;
      margin: 0;
      color: #fff;
      font-weight: 600;
      line-height: 50px;
      font-size: 14px;
      font-family: Avenir, Helvetica Neue, Arial, Helvetica, sans-serif;
      //vertical-align: middle;
    }
  }

  .sidebar-logo {
    margin-right: 0px;
  }
}
</style>
