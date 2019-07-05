<template>
  <el-dialog :visible.sync="config.show" width="400px" :title="config.title" style="margin:15px">
    <el-form
      :rules="rules"
      label-width="100px"
      label-position="left"
      :model="config.data"
      ref="Form"
    >
      <el-form-item label="名称:" prop="name">
        <el-input size="mini" v-model="config.data.name"></el-input>
      </el-form-item>
      <el-form-item label="显示名称:" prop="displayName">
        <el-input size="mini" v-model="config.data.displayName"></el-input>
      </el-form-item>
      <el-form-item label="描述:" prop="description">
        <el-input size="mini" v-model="config.data.description"></el-input>
      </el-form-item>
      <el-form-item label="启用:" prop="enabled">
        <el-switch v-model="config.data.enabled"></el-switch>
      </el-form-item>
      <el-form-item label="文档显示:" prop="showInDiscoveryDocument" v-if="config.type=='身份'">
        <el-switch v-model="config.data.showInDiscoveryDocument"></el-switch>
      </el-form-item>
      <el-form-item label="必须:" prop="required" v-if="config.type=='身份'">
        <el-switch v-model="config.data.required"></el-switch>
      </el-form-item>
      <el-form-item label="强调:" prop="emphasize" v-if="config.type=='身份'">
        <el-switch v-model="config.data.emphasize"></el-switch>
      </el-form-item>
      <el-form-item label="声明:" prop="claimType">
        <el-select
          v-model="config.data.userClaims"
          filterable
          multiple
          allow-create
          default-first-option
        >
          <el-option v-for="(item,index) in claimTypes" :key="index" :label="item" :value="item"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item class="dialog-footer el-message-box__btns">
        <el-button type="primary" size="mini" @click="Submit()">保存</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
export default {
  data() {
    return {
      rules: {
        name: [{ required: true }],
        description: [{ required: true }],
        displayName: [{ required: true }]
      },

      claimTypes: []
    };
  },
  async mounted() {
    let types = await this.$http.get("api/Claims/GetClaimTypes");
    if (types) {
      this.claimType = types[0];
      this.claimTypes = types;
    }
  },
  methods: {
    flush() {
      this.$emit("flush");
    },
    async Submit() {
      this.$refs.Form.validate(async valid => {
        if (valid) {
          if (this.config.type == "身份") {
            if (this.config.data.id) {
              let res = await this.$http.post(
                "api/IdentityResource/UpdateIdentityResource",
                this.config.data
              );
              if (res) {
                this.$message({
                  message: res,
                  type: "success"
                });
              }
            } else {
              let res = await this.$http.post(
                "api/IdentityResource/CreateIdentityResource",
                this.config.data
              );
              if (res) {
                this.$message({
                  message: res,
                  type: "success"
                });
              }
            }
          }

          if (this.config.type == "API") {
            if (this.config.data.id) {
              let res = await this.$http.post(
                "api/ApiResource/UpdateApiResource",
                this.config.data
              );
              if (res) {
                this.$message({
                  message: res,
                  type: "success"
                });
              }
            } else {
              let res = await this.$http.post(
                "api/ApiResource/CreateApiResource",
                this.config.data
              );
              if (res) {
                this.$message({
                  message: res,
                  type: "success"
                });
              }
            }
          }
        }

        this.flush();
      });
    }
  },
  props: {
    config: {
      type: Object,
      default: function() {
        return {
          show: false,
          title: "",
          data: {}
        };
      }
    }
  }
};
</script>
