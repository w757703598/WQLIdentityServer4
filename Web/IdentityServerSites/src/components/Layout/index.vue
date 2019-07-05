<template>
  <el-container id="content_box">
    <el-header>
      <Header></Header>
    </el-header>
    <el-container class="aside_box">
      <el-scrollbar wrap-class="scrollbar-wrapper-y">
        <el-aside width="auto">
          <Sidebar></Sidebar>
        </el-aside>
      </el-scrollbar>
      <el-main>
        <el-breadcrumb separator="/" style="margin :10px">
          <el-breadcrumb-item
            v-for="(item,index) in breadcrumbItem "
            :key="index"
            :to="item.path"
          >{{item.title}}</el-breadcrumb-item>
        </el-breadcrumb>
        <el-divider></el-divider>
        <div class="home">
          <h1 v-if="routeName=='/'">欢迎来到统一认证管理系统</h1>
        </div>
        <router-view></router-view>

        <!-- <tabview></tabview> -->
      </el-main>
    </el-container>
  </el-container>

  <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
</template>

<script>
// @ is an alias to /src

import Header from "../Header/index";
import Sidebar from "../Sidebar/index";
import Tabview from "../Tabview/index";

export default {
  data() {
    return {
      parentMenu: [],
      breadcrumbItem: [],
      showBreadcrumb: false
    };
  },
  computed: {
    // breadcrumbItem: function() {
    //   let menu = [];
    //   menu.push();
    //   let breadcrumb = this.$route.meta.breadcrumb;
    //   if (breadcrumb) {
    //     menu.push({
    //       path: breadcrumb.path,
    //       title: breadcrumb.title
    //     });
    //   }
    //   return menu;
    // }
  },
  watch: {
    $route(to, from) {
      this.breadcrumbItem = [];
      let breadcrumb = from.meta.breadcrumb;
      if (breadcrumb && to.meta.hidden == true) {
        this.breadcrumbItem.push({
          path: from.path,
          title: from.meta.title
        });
        this.breadcrumbItem.push(breadcrumb.filter(b => b.name == to.name)[0]);
      } else {
        this.breadcrumbItem.push({
          path: to.path,
          title: to.meta.title
        });
      }
    }
  },
  mounted() {},
  computed: {
    routeName: function() {
      return this.$route.path;
    }
  },
  name: "home",
  components: {
    Header,
    Sidebar,
    Tabview
  }
};
</script>
<style lang="scss" scoped>
#content_box {
  height: 100%;
}
.el-header {
  line-height: 50px;
  height: 50px !important;
  padding: 0;
  flex-grow: 0;
  flex-shrink: 0;
}

.aside_box > .el-scrollbar {
  flex-grow: 0; /* 不索取父容器空间 默认0 */
  flex-shrink: 0; /* 父容器空间不够，不压缩 默认1 */
}
.el-menu-vertical-demo:not(.el-menu--collapse) {
  min-width: 180px; /* 左边导航宽度 */
  min-height: 100%;
}
.el-main {
  background-color: #fff;
  height: 100%;
  padding: 0;

  position: relative;
  .home {
    float: top;
    left: 40%;
    position: relative;
    font-size: 20px;
  }
}

.el-menu {
  overflow: hidden;
}
</style>
