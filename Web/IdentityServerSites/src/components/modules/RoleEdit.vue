<template>
  <el-dialog :visible.sync="config.show" :title="config.title" width="500px">
    <el-form
      :rules="rules"
      label-width="120px"
      label-position="left"
      :model="config.data"
      ref="roleForm"
    >
      <el-form-item label="账号:" prop="name">
        <el-input size="mini" v-model="config.data.name"></el-input>
      </el-form-item>

      <div class="dialog-footer el-message-box__btns">
        <el-button type="info" size="mini" @click="close(false)">取消</el-button>
        <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
      </div>
    </el-form>
  </el-dialog>
</template>
<script>
import { async } from "q";
export default {
  data() {
    return {
      rules: {
        name: [{ required: true }]
      }
    };
  },
  methods: {
    close(isFlush) {
      this.config.show = false;
      if (isFlush) this.$emit("flush");
    },
    Submit() {
      this.$refs.roleForm.validate(async valid => {
        if (valid) {
          if (this.config.data.id) {
            let res = await this.$http.post(
              "api/RoleApp/UpdateRole",
              this.config.data
            );
            if (res) {
              this.$message({
                message: "修改成功",
                type: "success"
              });
              this.close(true);
            }
          } else {
            let res = await this.$http.post(
              "api/RoleApp/CreateRole",
              this.config.data
            );
            if (res) {
              this.$message({
                message: "创建成功",
                type: "success"
              });
              this.close(true);
            }
          }
        }
      });
    }
  },
  props: {
    config: {
      type: Object,
      default: () => {
        return {
          show: true,
          width: 800,
          title: "",
          data: {}
        };
      }
    }
  }
};
</script>
