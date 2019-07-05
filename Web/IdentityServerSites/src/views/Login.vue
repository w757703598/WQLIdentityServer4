<template>
  <div class="login">
    <div class="login_box">
      <div class="login_title">
        <span>天线知识库</span>
      </div>
      <div class="login_content">
        <el-form :model="loginForm" ref="loginForm" :rules="rules">
          <el-form-item label="账号:" prop="username">
            <el-input
              v-model="loginForm.username"
              size="mini"
              placeholder="UserName"
              prefix-icon="el-icon-user"
            ></el-input>
          </el-form-item>
          <el-form-item label="密码:" prop="password">
            <el-input
              v-model="loginForm.password"
              size="mini"
              placeholder="Password"
              prefix-icon="el-icon-key"
            ></el-input>
          </el-form-item>
          <el-form-item label prop="remeber" class="login_check">
            <el-checkbox>是否记住登陆状态</el-checkbox>
          </el-form-item>
          <el-form-item class="btn_item">
            <el-button type="primary" class="login_btn" size="mini" @click="submit()">登&emsp;录</el-button>
            <el-button type="primary" class="login_btn" size="mini" @click="cancle()">取&emsp;消</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
    <p class="copyright">© 2019 摩比天线技术(深圳)有限公司. All rigthts reserved.</p>
  </div>
</template>
<script>
import { async } from "q";
import qs from "qs";
import { mapActions } from "vuex";
export default {
  data() {
    return {
      rules: {
        username: [{ required: true, message: "请输入账户" }],
        password: [{ required: true, min: 6, message: "密码至少6位" }]
      },
      loginForm: {
        username: "030704",
        password: "123456",
        grant_type: "password",
        scope: "api1",
        client_Id: "AntennaKnowbaseApi",
        client_secret: "secret"
      }
    };
  },
  methods: {
    ...mapActions(["set_token"]),
    submit() {
      this.$refs["loginForm"].validate(async valid => {
        if (valid) {
          const loading = this.$loading({
            lock: true,
            text: "Loading",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });
          let res = await this.$http.post(
            "connect/token",
            qs.stringify(this.loginForm),
            {
              headers: { "Content-Type": "application/x-www-form-urlencoded" }
            }
          );
          if (res) {
            loading.close();
            this.set_token(res.access_token);
            this.$router.push(this.$route.query.redirect);
          }
        } else {
          return false;
        }
      });
    }
  }
};
</script>

<style lang="scss" scoped>
.login {
  background-image: url("../assets/imgs/back.jpg");
  background-size: cover;
  background-position: center;
  height: 100%;
  width: 100%;
  text-align: center;
  .copyright {
    position: absolute;
    bottom: 20%;
    left: 40%;
    color: #fff;
  }
  .login_box {
    top: 30%;
    left: 40%;
    width: 300px;
    height: 310px;
    text-align: center;
    border-radius: 5px;
    position: absolute;
    background-color: #fff;
    .login_title {
      position: absolute;
      text-align: center;
      width: 100%;
      height: 30px;
      margin-top: 20px;
      font-size: 20px;
      font-weight: bold;
    }
    .login_content {
      width: 100%;
      height: 100%;
      border-radius: 5px 5px 5px 5px;
      background-repeat: no-repeat;
      position: absolute;
      .el-form {
        border-radius: 5px 5px 5px 5px;
        padding: 80px 20px 0;
      }
      .login_check {
        margin-bottom: 10px;
      }
      .el-checkbox {
        float: left;
      }
      .btn_item {
        float: right;
      }
    }
  }
}
</style>
