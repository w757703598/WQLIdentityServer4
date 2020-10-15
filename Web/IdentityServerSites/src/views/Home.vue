<template>
  <el-container id="content_box">
    <el-header>
      <Header />
    </el-header>

    <el-container class="aside_box">
      <el-scrollbar wrap-class="scrollbar-wrapper-y">
        <el-aside width="auto">
          <Sidebar />
        </el-aside>
      </el-scrollbar>
      <el-main>
        <tabview />
      </el-main>
    </el-container>
  </el-container>

  <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
</template>

<script>
// @ is an alias to /src

import Header from '../components/Header/index'
import Sidebar from '../components/Sidebar/index'
import Tabview from '../components/Tabview/index'

export default {
  name: 'Home',
  components: {
    Header,
    Sidebar,
    Tabview,
  },
}
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
  color: #333;
  height: 100%;
  padding: 0;
  position: relative;
}
.el-menu {
  overflow: hidden;
}
</style>
