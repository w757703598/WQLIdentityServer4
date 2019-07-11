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
      <el-form label-width="100px" label-position="left" :model="apiscope" ref="claimForm">
        <el-form-item label="名称:" prop="name">
          <el-input size="mini" v-model="apiscope.name"></el-input>
        </el-form-item>
        <el-form-item label="显示名称:" prop="displayName">
          <el-input size="mini" v-model="apiscope.displayName"></el-input>
        </el-form-item>
        <el-form-item label="描述:" prop="description">
          <el-input size="mini" v-model="apiscope.description"></el-input>
        </el-form-item>
        <el-form-item label="必须:" prop="required">
          <el-switch v-model="apiscope.required"></el-switch>
        </el-form-item>
        <el-form-item label="强调:" prop="emphasize">
          <el-switch v-model="apiscope.emphasize"></el-switch>
        </el-form-item>
        <el-form-item label="开发文档显示" prop="showInDiscoveryDocument">
          <el-switch v-model="apiscope.showInDiscoveryDocument"></el-switch>
        </el-form-item>
        <el-form-item label="用户声明:" prop="userClaims">
          <el-select
            size="mini"
            v-model="apiscope.userClaims"
            filterable
            multiple
            allow-create
            default-first-option
            placeholder="请选择"
          >
            <el-option v-for="(item,index) in claimTypes" :key="index" :label="item" :value="item"></el-option>
          </el-select>
        </el-form-item>
        <div class="dialog-footer el-message-box__btns">
          <el-button type="info" size="mini" @click="close(false)">取消</el-button>
          <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
        </div>
      </el-form>
    </el-dialog>
    <el-table
      :data="apiscopes"
      ref="multipleTable"
      tooltip-effect="dark"
      size="mini"
      border
      align="center"
      style="width:100%"
      :stripe="true"
    >
      <el-table-column width="50px" align="center" prop="id" label="序号"></el-table-column>
      <el-table-column prop="name" label="名称"></el-table-column>
      <el-table-column prop="description" label="描述"></el-table-column>
      <el-table-column label="作用域声明">
        <template slot-scope="scope">
          <el-tag v-for="(item,index) in scope.row.userClaims" :key="index">{{item}}</el-tag>
        </template>
      </el-table-column>

      <el-table-column label="操作" align="center" width="200px">
        <template slot-scope="scope">
          <el-button type="primary" size="mini" icon="el-icon-edit" @click="edit(scope.row)">编辑</el-button>
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
      claimTypes: [],
      apiresourceId: this.$route.query.apiresourceId,
      apiscope: {
        name: "",
        displayName: "",
        description: "",
        required: false,
        emphasize: false,
        showInDiscoveryDocument: false,
        userClaims: []
      },
      totalCount: 0,
      pageSize: 10,
      currentPage: 1,
      search: "",
      apiscopes: []
    };
  },
  async mounted() {
    await this.flush();
    let types = await this.$http.get("api/Claims/GetClaimTypes");
    if (types) {
      this.claimType = types[0];
      this.claimTypes = types;
    }
  },
  methods: {
    edit(row) {
      if (row) {
        this.apiscope = row;
      } else {
        this.apiscope = {};
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
          let result = await this.$http.post(
            "api/ApiResource/RemoveScope",
            row.id
          );
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
      if (this.apiscope.id) {
        this.apiscope.apiResourceId = this.apiresourceId;
        let res = await this.$http.post(
          "api/ApiResource/UpdateScope",
          this.apiscope
        );

        if (res) {
          this.$message({
            message: res,
            type: "success"
          });
        }
      } else {
        this.apiscope.apiResourceId = this.apiresourceId;
        let res = await this.$http.post(
          "api/ApiResource/AddApiScope",
          this.apiscope
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
      let result = await this.$http.get("api/ApiResource/GetApiScopes", {
        Search: this.search,
        isdesc: true,
        Page: this.currentPage,
        PageSize: this.pageSize,
        apiresourceId: this.apiresourceId
      });
      this.totalCount = result.totalCount;
      this.apiscopes = result.data;
    }
  }
};
</script>
<style scoped>
.el-tag {
  margin: 2px;
}
</style>
