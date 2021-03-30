<template>
  <el-main>
    <el-scrollbar wrap-class="scrollbar-wrapper-y">
      <div class="tool-header">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-input
              v-model="search"
              size="mini"
              style="width:200px;margin-right:10px"
              placeholder="请输入关键字检索"
            />
            <el-button
              type="primary"
              size="mini"
              @click="flush()"
            >
              查询
            </el-button>
          </el-col>

          <el-col
            :span="6"
            :offset="6"
          >  
            <div class="tool-header-right">
              <el-button
                type="success"
                size="mini"
                icon="el-icon-circle-plus"
                @click="edit()"
              >
                创建角色
              </el-button>
              <el-button
                type="danger"
                size="mini"
                icon="el-icon-delete"
                @click="deleteRole()"
              >
                删除角色
              </el-button>
            </div>
          </el-col>
        </el-row>
      </div>
      <el-table
        ref="multipleTable"
        :data="roleData"
        tooltip-effect="dark"
        size="mini"
        border
        align="center"
        style="width:100%"
        :stripe="true"
        @selection-change="handleSelectionChange"
      >
        <el-table-column
          type="selection"
          align="center"
        />
        <el-table-column
          prop="id"
          width="80"
          align="center"
          label="序号"
        />
        <el-table-column
          prop="name"
          label="角色名称"
          align="center"
        />
        <el-table-column
          prop="createdOn"
          label="创建时间"
          align="center"
          show-overflow-tooltip
        />

        <el-table-column
          label="操作"
          align="center"
          width="180px"
        >
          <template slot-scope="scope">
            <el-button
              type="primary"
              size="mini"
              @click="edit(scope.row)"
            >
              编辑
            </el-button>
            <el-button
              type="primary"
              size="mini"
              @click="editClaim(scope.row)"
            >
              管理声明
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        class="page_footer_box"
        background
        layout="total, sizes, prev, pager, next, jumper"
        :total="totalCount"
        :current-page.sync="currentPage"
        :page-sizes="[1,10,30,50]"
        :page-size.sync="pageSize"
        @current-change="flush()"
        @size-change="flush()"
      />
      <role-edit
        v-if="config.show"
        :config="config"
        @flush="flush"
      />
      <el-dialog
        :visible.sync="claimConfig.show"
        :title="claimConfig.title"
        width="800px"
        append-to-body
      >
        <claim-edit
          v-if="claimConfig.show"
          :config="claimConfig"
        />
      </el-dialog>
    </el-scrollbar>
  </el-main>
</template>
<script>
import roleEdit from "../components/modules/RoleEdit";
import claimEdit from "../components/modules/ClaimEdit";
export default {
  components: {
    roleEdit,
    claimEdit
  },
  data() {
    return {
      roleData: [],
      totalCount: 0,
      pageSize: 10,
      currentPage: 1,
      search: "",
      config: {
        show: false,
        title: "",
        type: 0, //0 创建，1 修改名称，2 重置密码
        data: {}
      },
      claimConfig: {
        show: false,
        title: "",
        Id: "",
        type: "角色声明"
      },
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
      let result = await this.$http.get("api/RoleApp/GetRoles", {
        Search: this.search,
        isdesc: true,
        Page: this.currentPage,
        PageSize: this.pageSize
      });
      this.totalCount = result.totalCount;
      this.roleData = result.data;
    },
    async edit(row) {
      if (row) {
        this.config.title = "修改角色";
        this.config.data = row;
      } else {
        this.config.title = "创建角色";
        this.config.data = {};
      }
      this.config.show = true;
    },
    editClaim(row) {
      this.claimConfig.title = `管理角色声明：${row.name}`;
      this.claimConfig.show = true;
      this.claimConfig.Id = row.id;
      this.claimConfig.type = "角色声明";
    },
    deleteRole() {
      if (this.multipleSelection && this.multipleSelection.length) {
        const ids = this.multipleSelection.map(item => item.id);
        this.$confirm("确认删除所选角色?", "删除", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(async () => {
            for (let index = 0; index < ids.length; index++) {
              let result = await this.$http.post(
                "api/RoleApp/DeleteRole",
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
          message: "请选择要删除的角色"
        });
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.page_footer_box {
  float: right;
  margin: 3px, 10px;
}
.tool-header{
  margin-bottom: 5px;
  .tool-header-right{
      position: absolute;
      right: 5px;
  }
}
.scroll-container {
  width: 100%;
  height: 36px;
  white-space: nowrap;
::v-deep {
    .el-scrollbar__bar {
      bottom: 0px;
      &.is-vertical {
        display: none;
      }
    }
    // .el-scrollbar__wrap {
    //    height: 36px;
    // }
  }
}
.tab_box {
  background-color: #fafafa;
  &:after {
    content: "";
    display: inline-block;
    width: 100%;
    height: 1px;
    background-color: #ddd;
    position: absolute;
    bottom: 0;
    left: 0;
  }
  .el-tag {
    cursor: pointer;
    display: inline-block;
    height: 35px;
    line-height: 35px;
    border-right: solid 1px #ddd;
    border-top: solid 1px #ddd;
    border-bottom: none;
    border-left: none;
    color: #475059;
    background: #fafafa;
    padding: 0 10px;
    font-size: 12px;
    border-radius: 0;
    i {
      color: #495060;
      &:hover {
        background-color: #b4bccc;
        color: #fff;
      }
    }
  }
  .el-tag.active {
    background-color: #fff;
    color: #666;
    border-bottom-color: #fff;
    height: 36px;
    margin-bottom: -1px;
    position: relative;
    z-index: 1;
    i {
      color: #666;
    }
    // .tab_rect {
    //   content: "";
    //   background: #fff;
    //   display: inline-block;
    //   width: 8px;
    //   height: 8px;
    //   border-radius: 50%;
    //   position: relative;
    //   margin-right: 2px;
    // }
  }
}
</style>
