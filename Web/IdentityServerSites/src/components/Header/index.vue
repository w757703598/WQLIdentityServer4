<template>
  <div class="top">
    <div class="logo">
      <img src="../../assets/imgs/mobo-icon.png">
      <span></span>
    </div>
    <div class="open-aside">
      <i class="el-icon-s-unfold" @click="toggleSideBar" :class="{'is-active':isActive}"></i>
    </div>

    <el-dropdown class="header-user-menu" @command="handleCommand">
      <span>
        <img :src="src" class="user-image">
        <i class="user-name">{{username}}</i>
      </span>
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item command="userinfo">个人信息</el-dropdown-item>
        <el-dropdown-item command="logOut">安全退出</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </div>
</template>
<script>
import { mapActions } from "vuex";
import Mgr from "../../services/SecurityService";
export default {
  data() {
    return {
      mgr: new Mgr(),
      src:
        "https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg",
      username: "030704",
      isActive: false
    };
  },

  methods: {
    ...mapActions(["changeCollapse"]),
    handleCommand(command) {
      if (command == "logOut") {
        this.logOut();
      }
      if (command == "userinfo") {
        this.$router.push("/userinfo");
      }
    },
    logOut() {
      this.mgr.signOut();
    },
    toggleSideBar() {
      this.changeCollapse();
    }
  }
};
</script>

<style lang="scss" scoped>
.top {
  background-image: url("../../assets/imgs/back.jpg");
  width: 100%;
  // background-color: #257fed;
  height: 50px;
  .logo {
    height: 50px;
    width: 180px;
    text-align: center;
    float: left;
    line-height: 50px;
  }
  img {
    vertical-align: middle;
    margin-top: -6px;
  }
}
.open-aside {
  margin-left: 2px;
  float: left;
  line-height: 50px;
  width: 36px;
  font-size: 30px;
  cursor: pointer;
  color: rgb(22, 15, 15);
  &:hover {
    color: #fff;
  }
  i {
    &.is-active {
      transform: rotate(180deg);
    }
  }
}
.header-user-menu {
  float: right;
  cursor: pointer;
  margin-right: 10px;
  line-height: 50px;

  .user-image {
    width: 25px;
    height: 25px;
    float: left;
    margin-right: 10px;
    margin-top: 12px;
    border-radius: 20%;
  }
  .user-name {
    color: antiquewhite;
    float: right;
    margin-bottom: 1px;
  }
}
</style>
