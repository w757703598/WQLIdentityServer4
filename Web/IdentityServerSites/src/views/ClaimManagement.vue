<template>
  <el-main>
    <div class="flex">
      <el-input v-model="search" size="mini" placeholder="请输入关键字检索"></el-input>
      <el-button type="primary" size="mini" @click="flush()">查询</el-button>
      <div class="flex1"></div>
      <el-button type="success" size="mini" icon="el-icon-circle-plus" @click="AddClaim()">创建</el-button>
      <el-button type="danger" size="mini" icon="el-icon-delete" @click="DeleteClaims()">删除</el-button>
    </div>
    <el-table
      :data="claimsDta"
      ref="multipleTable"
      tooltip-effect="dark"
      size="mini"
      border
      align="center"
      style="width:100%"
      :stripe="true"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" prop="id" label="序号"></el-table-column>
      <el-table-column prop="type" label="类型"></el-table-column>
      <el-table-column prop="value" label="值"></el-table-column>
      <el-table-column prop="description" label="描述"></el-table-column>
      <el-table-column prop="createdOn" label="创建时间"></el-table-column>
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
    <claim-create :config="config" @flush="flush"></claim-create>
  </el-main>
</template>


<script>
import claimCreate from "../components/modules/ClaimCreate";
export default {
  components: {
    claimCreate
  },
  data() {
    return {
      config: {
        show: false,
        title: ""
      },
      claimsDta: [],
      totalCount: 0,
      pageSize: 10,
      currentPage: 1,
      search: "",
      multipleSelection: []
    };
  },
  mounted() {
    this.flush();
  },
  methods: {
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    async flush() {
      let result = await this.$http.get("api/Claims/GetClaims", {
        Search: this.search,
        isdesc: true,
        Page: this.currentPage,
        PageSize: this.pageSize
      });
      this.totalCount = result.totalCount;
      this.claimsDta = result.data;
    },
    AddClaim() {
      this.config.show = true;
      this.config.title = "创建声明";
    },
    DeleteClaims() {
      if (this.multipleSelection && this.multipleSelection.length) {
        const ids = this.multipleSelection.map(item => item.id);
        this.$confirm("确认删除所选用户?", "删除", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(async () => {
            for (let index = 0; index < ids.length; index++) {
              let result = await this.$http.post(
                "api/Claims/RemoveClaim",
                ids[index]
              );
              if (result) {
                this.$message({
                  showClose: true,
                  type: "success",
                  message: "删除成功!"
                });
              }
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
      } else {
        this.$message({
          showClose: true,
          type: "warning",
          message: "请选择要删除的声明"
        });
      }
    }
  }
};
</script>


