<template>
  <el-dialog :visible.sync="config.show" width="400px" :title="config.title" style="margin:15px">
    <el-form
      :rules="rules"
      label-width="100px"
      label-position="left"
      :model="claimsFormdata"
      ref="claimForm"
    >
      <el-form-item label="声明类型:" prop="Type">
        <el-input size="mini" v-model="claimsFormdata.Type"></el-input>
      </el-form-item>
      <el-form-item label="声明值:" prop="Value">
        <el-input size="mini" v-model="claimsFormdata.Value"></el-input>
      </el-form-item>
      <el-form-item label="描述:" prop="description">
        <el-input size="mini" v-model="claimsFormdata.description"></el-input>
      </el-form-item>
      <el-form-item class="dialog-footer el-message-box__btns">
        <el-button type="primary" @click="AddClaims" size="mini">添加</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
export default {
  data() {
    return {
      claimsFormdata: {
        Type: "",
        Value: "",
        description: ""
      },
      rules: {
        Type: [{ required: true, message: "请输入声明类型", trigger: "blur" }],
        Value: [{ required: true, message: "请输入声明值", trigger: "blur" }],
        description: [
          { required: true, message: "请输入描述", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    close(flush) {
      this.config.show = false;
      if (flush) this.$emit("flush");
    },
    async AddClaims() {
      var res = await this.$http.post(
        "/api/Claims/CreateClaim",
        this.claimsFormdata
      );

      if (res) {
        this.$message({
          message: res,
          type: "success"
        });
      }
      this.close(true);
    }
  },
  props: {
    config: {
      type: Object,
      default: () => {
        return {
          show: false,
          title: ""
        };
      }
    }
  }
};
</script>
