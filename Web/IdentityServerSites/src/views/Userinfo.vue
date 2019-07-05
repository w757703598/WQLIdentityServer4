<template>
  <el-card>
    <div class="container">
      <el-form
        :rules="rules"
        label-width="120px"
        label-position="left"
        :model="userinfo"
        ref="Userform"
      >
        <el-form-item label="账号:" prop="userName">
          <el-input size="mini" v-model="userinfo.userName" :disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="姓名:" prop="name">
          <el-input size="mini" v-model="userinfo.name" :disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="旧密码:" prop="oldPassword">
          <el-input size="mini" v-model="userinfo.oldPassword"></el-input>
        </el-form-item>
        <el-form-item label="密码:" prop="passWord">
          <el-input size="mini" v-model="userinfo.passWord"></el-input>
        </el-form-item>
        <el-form-item label="确认密码:" prop="confirmPassword">
          <el-input size="mini" v-model="userinfo.confirmPassword"></el-input>
        </el-form-item>
        <div class="dialog-footer el-message-box__btns">
          <el-button type="info" size="mini" @click="close(false)">取消</el-button>
          <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
        </div>
      </el-form>
    </div>
  </el-card>
</template>
<script>
import Mgr from "../services/SecurityService";
import { async } from "q";
export default {
  data() {
    var validatePass = (rule, value, callback) => {
      if (value != this.userinfo.passWord) {
        callback(new Error("两次输入密码不一致!"));
      }
      callback();
    };

    return {
      mgr: new Mgr(),
      userinfo: {},
      rules: {
        userName: [{ required: true, message: "请输入账户", trigger: "blur" }],
        name: [{ required: true, message: "请输入用户名", trigger: "blur" }],
        oldPassword: [
          { required: true, min: 6, message: "密码至少6位", trigger: "blur" }
        ],
        passWord: [
          { required: true, min: 6, message: "密码至少6位", trigger: "blur" }
        ],
        confirmPassword: [
          { required: true, validator: validatePass, trigger: "blur" }
        ]
      }
    };
  },

  async mounted() {
    this.userinfo = await this.GetUserinfo();
  },
  methods: {
    async GetUserinfo() {
      var user = await this.mgr.getProfile();
      return {
        name: user.name,
        userName: user.username,
        id: user.sub
      };
    },
    Submit() {
      this.$refs.Userform.validate(async valid => {
        if (valid) {
          let res = await this.$http.post("api/UserApp/ChangePassword", {
            userId: this.userinfo.id,
            oldPassword: this.userinfo.oldPassword,
            newPassword: this.userinfo.passWord
          });

          if (res) {
            this.$message({
              message: "修改密码成功",
              type: "success"
            });
            this.close(true);
          }
        }
      });
    }
  }
};
</script>
<style lang="scss" scoped>
.el-card {
  margin: 20px;
}
.container {
  text-align: center;
  width: 300px;
  height: 100%;
  margin: auto;
}
</style>
