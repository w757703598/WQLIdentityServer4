<template>
  <el-menu
    class="el-menu-vertical-demo"
    :collapse="isCollapse"
    :collapse-transition="false"
    @select="OpenTab"
  >
    <sidebar-item
      v-for="(route,index) in routes"
      :base-path="route.path"
      :key="index"
      v-bind:item="route"
    ></sidebar-item>
  </el-menu>
</template>
<script>
import { mapGetters, mapMutations } from "vuex";
import SidebarItem from "../SidebarItem/index";
export default {
  data() {
    return {};
  },
  components: {
    SidebarItem
  },
  computed: {
    ...mapGetters(["isCollapse"]),
    routes() {
      return this.$router.options.routes.filter(d => d.name == "home")[0]
        .children;
    }
  },
  methods: {
    ...mapMutations(["OpenTab"])
  }
};
</script>
<style lang="scss" scoped>
.el-menu-vertical-demo:not(.el-menu--collapse) {
  min-width: 180px; /* 左边导航宽度 */
  min-height: 100%;
}
</style>

