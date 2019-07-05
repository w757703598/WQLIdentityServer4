<template>
  <div>
    <el-form
      :rules="rules"
      label-width="100px"
      label-position="left"
      :model="claimsFormdata"
      ref="claimForm"
      :inline="true"
    >
      <el-form-item label="声明类型:" prop="claimsType">
        <el-select size="mini" v-model="claimsFormdata.claimsType" @change="handleclaimTypeChange">
          <el-option v-for="(item,index) in claimTypes" :key="index" :label="item" :value="item"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="声明值:" prop="claimsValue" label-width="100px">
        <el-select size="mini" v-model="claimsFormdata.claimsValue">
          <el-option v-for="(item,index) in claimsValues" :key="index" :label="item" :value="item"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="AddClaims" size="mini">添加</el-button>
      </el-form-item>
    </el-form>
    <el-table
      :data="CurrentClaims"
      ref="multipleTable"
      tooltip-effect="dark"
      size="mini"
      border
      align="center"
      style="width:100%"
      :stripe="true"
    >
      <el-table-column prop="type" label="类型" align="center"></el-table-column>
      <el-table-column prop="value" label="值" align="center"></el-table-column>
      <el-table-column label="操作" align="center" width="300px">
        <template slot-scope="scope">
          <el-button type="danger" size="mini" @click="removeClaims(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
<script>
import uilts from "../../services/uilts";
export default {
  data() {
    return {
      claimsFormdata: {
        claimsType: "",
        claimsValue: ""
      },
      CurrentClaims: [], //查询的声明结果
      claims: [], //声明列表
      claimTypes: [],
      claimsValues: [],
      rules: {
        claimsType: [
          {
            required: true
          }
        ],
        claimsValue: [
          {
            required: true
          }
        ]
      }
    };
  },
  async mounted() {
    let res = await this.$http.get("api/Claims/GetClaims");
    if (res.data) {
      this.claims = res.data;
      this.claimTypes = uilts.distinct(res.data.map(r => r.type));
      console.info(this.claimTypes);
      this.claimsValues = res.data
        .filter(c => c.type == this.claimTypes[0])
        .map(v => v.value);
      //默认值
      this.claimsFormdata.claimsType = this.claimTypes[0];
      this.claimsFormdata.claimsValue = this.claimsValues[0];
    }
    await this.flush();
  },
  methods: {
    async flush() {
      if (this.config.type == "角色声明") {
        let tempClaims = await this.$http.get("api/RoleApp/GetRoleClaims", {
          roleId: this.config.Id
        });
        if (tempClaims) {
          this.CurrentClaims = tempClaims;
        }
      }
      if (this.config.type == "用户声明") {
        let tempClaims = await this.$http.get("api/UserApp/GetUserClaims", {
          userId: this.config.Id
        });
        if (tempClaims) {
          this.CurrentClaims = tempClaims;
        }
      }
    },
    async AddClaims() {
      if (this.config.type == "角色声明") {
        let res = await this.$http.post("api/RoleApp/CreateRoleClaim", {
          id: this.config.Id,
          value: this.claimsFormdata.claimsValue,
          type: this.claimsFormdata.claimsType
        });
        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }
      if (this.config.type == "用户声明") {
        let res = await this.$http.post("api/UserApp/CreateUserClaim", {
          id: this.config.Id,
          value: this.claimsFormdata.claimsValue,
          type: this.claimsFormdata.claimsType
        });
        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }

      await this.flush();
    },
    async removeClaims(row) {
      if (this.config.type == "角色声明") {
        if (row) {
          let res = await this.$http.post("api/RoleApp/RemoveRoleClaim", {
            id: this.config.Id,
            value: row.value,
            type: row.type
          });
          if (res) {
            this.$message({
              message: res,
              type: "success"
            });
          }
        }
      }
      if (this.config.type == "用户声明") {
        let res = await this.$http.post("api/UserApp/RemoveUserClaim", {
          id: this.config.Id,
          value: row.value,
          type: row.type
        });
        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }
      await this.flush();
    },
    handleclaimTypeChange(item) {
      if (item) {
        this.claimsValues = this.claims
          .filter(c => c.type == item)
          .map(v => v.value);
        this.claimsFormdata.claimsValue = this.claimsValue[0];
      }
    }
  },
  props: {
    config: {
      type: Object,
      default: () => {
        return {
          show: false,
          title: "",
          Id: "",
          type: "" //判断为角色声明还是用户声明
        };
      }
    }
  }
};
</script>
