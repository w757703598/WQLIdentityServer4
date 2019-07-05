<template>
  <el-main>
    <div class="flex">
      <el-input v-model="search" size="mini" placeholder="请输入关键字检索"></el-input>
      <el-button type="primary" size="mini" @click="flush()">查询</el-button>
      <div class="flex1"></div>
      <el-button type="success" size="mini" icon="el-icon-circle-plus" @click="edit()">创建</el-button>
    </div>
    <el-dialog
      :visible.sync="dialog"
      width="400px"
      title="创建ApiResource"
      style="margin:15px"
      @close="close"
    >
      <el-form label-width="100px" label-position="left" :model="apisecret" ref="claimForm">
        <el-form-item label="值:" prop="value">
          <el-input size="mini" v-model="apisecret.value"></el-input>
        </el-form-item>
        <el-form-item label="类型:" prop="type">
          <el-select size="mini" v-model="apisecret.type" placeholder="请选择">
            <el-option v-for="(item,index) in secretTypes" :key="index" :label="item" :value="item"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="描述:" prop="description">
          <el-input size="mini" v-model="apisecret.description"></el-input>
        </el-form-item>
        <el-form-item label="哈希类型:" prop="hash">
          <el-select size="mini" v-model="apisecret.hash" placeholder="请选择">
            <el-option
              v-for="(item,index) in hashTypes"
              :key="index"
              :label="item.type"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <div class="dialog-footer el-message-box__btns">
          <el-button type="info" size="mini" @click="close(false)">取消</el-button>
          <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
        </div>
      </el-form>
    </el-dialog>
    <el-table
      :data="apisecrets"
      ref="multipleTable"
      tooltip-effect="dark"
      size="mini"
      border
      align="center"
      style="width:100%"
      :stripe="true"
    >
      <el-table-column width="50px" align="center" prop="id" label="序号"></el-table-column>
      <el-table-column prop="type" label="密钥类型"></el-table-column>
      <el-table-column prop="value" label="值"></el-table-column>
      <el-table-column prop="description" label="描述"></el-table-column>

      <el-table-column label="操作" align="center" width="100px">
        <template slot-scope="scope">
          <el-button
            type="danger"
            size="mini"
            icon="el-icon-delete"
            @click="removeScope(scope.row)"
          >删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      class="page_footer_box"
      @current-change="flush()"
      @size-change="flush()"
      background
      layout="total, sizes, prev, pager, next, jumper"
      :total="totalCount"
      :current-page.sync="currentPage"
      :page-sizes="[1,10,30,50]"
      :page-size.sync="pageSize"
    ></el-pagination>
  </el-main>
</template>
<script>
export default {
  data() {
    return {
      dialog: false, //创建模态窗口
      hashTypes: [{ type: "sha256", value: 0 }, { type: "sha512", value: 1 }],
      apisecret: {
        description: "string",
        value: "string",
        expiration: "2019-05-21T07:39:11.945Z",
        hash: 0,
        type: "string",
        resourceName: "string"
      },
      totalCount: 0,
      pageSize: 10,
      currentPage: 1,
      search: "",
      apisecrets: [],
      secretTypes: [],
      apiresourceId: this.$route.query.apiresourceId,
      clientId: this.$route.query.clientId
    };
  },
  async mounted() {
    await this.flush();
  },
  methods: {
    async edit(row) {
      if (row) {
        this.apisecret = row;
      } else {
        this.apisecret = {};
      }
      await this.getSecretTypes();
      this.dialog = true;
    },
    async getSecretTypes() {
      let res = await this.$http.get("api/Infrastructure/GetSecretTypes");
      if (res) {
        this.secretTypes = res;
        this.apisecret.type = res[0];
      }
    },
    removeScope(row) {
      this.$confirm("确认删除所选用户?", "删除", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(async () => {
          let result = "";
          if (this.apiresourceId) {
            result = await this.$http.post(
              "api/ApiResource/RemoveSecret",
              row.id
            );
          }
          if (this.clientId) {
            result = await this.$http.post("api/Client/RemoveSecret", row.id);
          }
          if (result) {
            this.$message({
              showClose: true,
              type: "success",
              message: "删除成功!"
            });
          }

          this.flush();
        })
        .catch(() => {
          this.$message({
            showClose: true,
            type: "info",
            message: "已取消删除!"
          });
        });
    },
    async close() {
      this.dialog = false;

      await this.flush();
    },
    async Submit() {
      if (this.apiresourceId) {
        this.apisecret.apiResourceId = this.apiresourceId;
        let res = await this.$http.post(
          "api/ApiResource/AddApiSecret",
          this.apisecret
        );

        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }
      if (this.clientId) {
        this.apisecret.clientId = this.clientId;
        let res = await this.$http.post(
          "api/Client/AddApiSecret",
          this.apisecret
        );

        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }
      this.close();
    },
    async flush() {
      if (this.apiresourceId) {
        let result = await this.$http.get("api/ApiResource/GetApiSecrets", {
          Search: this.search,
          isdesc: true,
          Page: this.currentPage,
          PageSize: this.pageSize,
          apiresourceId: this.apiresourceId
        });
        this.totalCount = result.totalCount;
        this.apisecrets = result.data;
      }
      if (this.clientId) {
        let result = await this.$http.get("api/Client/GetApiSecrets", {
          Search: this.search,
          isdesc: true,
          Page: this.currentPage,
          PageSize: this.pageSize,
          clientId: this.clientId
        });
        this.totalCount = result.totalCount;
        this.apisecrets = result.data;
      }
    }
  }
};
</script>
<style lang="scss" scoped>
.el-tag {
  margin: 2px;
}
.el-form {
  .el-form-item {
    margin-bottom: 4px;
    .el-input {
      width: 90%;
    }
    .el-select {
      width: 90%;
    }
  }
}
</style>
