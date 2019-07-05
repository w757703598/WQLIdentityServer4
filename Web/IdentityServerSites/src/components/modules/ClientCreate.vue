<template>
  <div>
    <el-dialog :visible.sync="config.show" width="50%" :title="this.config.title ">
      <el-form
        :rules="rules"
        label-width="120px"
        label-position="left"
        :model="config.data"
        ref="Userform"
      >
        <el-form-item label="标识:" prop="clientId">
          <el-input size="mini" v-model="config.data.clientId"></el-input>
        </el-form-item>
        <el-form-item label="名称:" prop="clientName">
          <el-input size="mini" v-model="config.data.clientName"></el-input>
        </el-form-item>
        <el-form-item label="类型:" prop="clientType">
          <el-select size="mini" v-model="config.data.clientType">
            <el-option
              v-for="item in clientTypes"
              :key="item.id"
              :label="item.text"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <div class="dialog-footer el-message-box__btns">
          <el-button type="info" size="mini" @click="close(false)">取消</el-button>
          <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
        </div>
      </el-form>
    </el-dialog>
  </div>
</template>
<script>
export default {
  data() {
    return {
      clientTypes: [],
      formType: this.config.type,
      activeName: "修改名称",
      claimConfig: {
        show: false,
        title: "",
        Id: "",
        type: "用户声明"
      },
      rules: {
        clientId: [
          { required: true, message: "请输入客户端标识", trigger: "blur" }
        ],
        clientName: [
          { required: true, message: "请输入客户端名称", trigger: "blur" }
        ],
        clientType: [{ required: true, message: "客户端类型", trigger: "blur" }]
      }
    };
  },
  async mounted() {
    let res = await this.$http.get("api/Infrastructure/GetClientTypes");
    if (res) {
      this.clientTypes = res;
    }
    await this.getUserRoles();
  },
  props: {
    config: {
      type: Object,
      default: function() {
        return {
          show: false,
          title: "创建用户",
          data: {}
        };
      }
    }
  },
  methods: {
    close(isFlush) {
      this.config.show = false;

      if (isFlush) this.$emit("flush");
    },
    Submit() {
      this.$refs.Userform.validate(async valid => {
        if (valid) {
          var res = await this.$http.post(
            "api/Client/AddClient",
            this.config.data
          );

          if (res) {
            this.$message({
              message: "注册成功",
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
.flex {
  justify-content: flex-end;
}
</style>
