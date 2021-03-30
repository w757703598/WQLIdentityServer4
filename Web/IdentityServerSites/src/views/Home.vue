<template>
<div class="home-content">
    <el-image
    style="width: 100px; height: 100px"
    :src="url"
    >
  </el-image>
 <el-link style="font-size:30px" type="success" :href="authUrl">欢迎来到IdentityServer4授权中心 (version 4.1.1)</el-link>
</div>
 
</template>

<script>
// @ is an alias to /src



export default {
  name: 'Home',
  computed :{
    authUrl() {
      var env=JSON.parse(process.env.VUE_APP_OIDC_CONFIG)
      return env.authority;
    }

  },
  data(){
    return {
      url:require("@/assets/imgs/wqlapi.png")
    }
  }
}
</script>

<style scoped>
.home-content{
  margin-top: 80px;
  display: flex;
  flex-direction: row;
  align-content: center;
  justify-content: center;
  min-height: 100%;
  height: 100%;
}
</style>