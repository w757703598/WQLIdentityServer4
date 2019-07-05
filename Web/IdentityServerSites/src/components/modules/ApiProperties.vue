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
      <el-form label-width="100px" label-position="left" :model="apipropertie" ref="claimForm">
        <el-form-item label="键:" prop="key">
          <el-input size="mini" v-model="apipropertie.key"></el-input>
        </el-form-item>
        <el-form-item label="值:" prop="value">
          <el-input size="mini" v-model="apipropertie.value"></el-input>
        </el-form-item>

        <div class="dialog-footer el-message-box__btns">
          <el-button type="info" size="mini" @click="close(false)">取消</el-button>
          <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
        </div>
      </el-form>
    </el-dialog>
    <el-table
      :data="apiproperties"
      ref="multipleTable"
      tooltip-effect="dark"
      size="mini"
      border
      align="center"
      style="width:100%"
      :stripe="true"
    >
      <el-table-column width="50px" align="center" prop="id" label="序号"></el-table-column>
      <el-table-column prop="key" label="键"></el-table-column>
      <el-table-column prop="value" label="值"></el-table-column>

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
      apipropertie: {
        apiResourceId: 0,
        key: "",
        value: ""
      },
      totalCount: 0,
      pageSize: 10,
      currentPage: 1,
      search: "",
      apiproperties: [],
      apiresourceId: this.$route.query.apiresourceId,
      clientId: this.$route.query.clientId,
      identityResourceId: this.$route.query.identityResourceId
    };
  },
  async mounted() {
    await this.flush();
  },
  methods: {
    edit(row) {
      if (row) {
        this.apipropertie = row;
      } else {
        this.apipropertie = {};
      }
      this.dialog = true;
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
              "api/ApiResource/RemoveApiProperties",
              row.id
            );
          }
          if (this.clientId) {
            result = await this.$http.post(
              "api/Client/RemoveApiProperties",
              row.id
            );
          }
          if (this.identityResourceId) {
            result = await this.$http.post(
              "api/IdentityResource/RemoveIdentityProperties",
              row.id
            );
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
        this.apipropertie.apiResourceId = this.apiresourceId;
        let res = await this.$http.post(
          "api/ApiResource/AddApiProperties",
          this.apipropertie
        );

        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }
      if (this.clientId) {
        this.apipropertie.clientId = this.clientId;
        let res = await this.$http.post(
          "api/Client/AddApiProperties",
          this.apipropertie
        );

        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      }
      if (this.identityResourceId) {
        this.apipropertie.identityResourceId = this.identityResourceId;
        let res = await this.$http.post(
          "api/IdentityResource/AddIdentityProperties",
          this.apipropertie
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
        let result = await this.$http.get("api/ApiResource/GetApiProperties", {
          Search: this.search,
          isdesc: true,
          Page: this.currentPage,
          PageSize: this.pageSize,
          apiresourceId: this.apiresourceId
        });
        this.totalCount = result.totalCount;
        this.apiproperties = result.data;
      }
      if (this.clientId) {
        let result = await this.$http.get("api/Client/GetApiProperties", {
          Search: this.search,
          isdesc: true,
          Page: this.currentPage,
          PageSize: this.pageSize,
          clientId: this.clientId
        });
        this.totalCount = result.totalCount;
        this.apiproperties = result.data;
      }
      if (this.identityResourceId) {
        let result = await this.$http.get(
          "api/IdentityResource/GetIdentityProperties",
          {
            Search: this.search,
            isdesc: true,
            Page: this.currentPage,
            PageSize: this.pageSize,
            identityResourceId: this.identityResourceId
          }
        );
        this.totalCount = result.totalCount;
        this.apiproperties = result.data;
      }
    }
  }
};
</script>
<style scoped>
.el-tag {
  margin: 2px;
}
</style>
