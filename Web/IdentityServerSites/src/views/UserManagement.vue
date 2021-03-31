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
                @click="create()"
              >
                创建用户
              </el-button>
              <el-button
                type="danger"
                size="mini"
                icon="el-icon-delete"
                @click="deleteUser()"
              >
                删除用户
              </el-button>
            </div>
          </el-col>
        </el-row>
      </div>

      <el-table
        ref="multipleTable"
        :data="userData"
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
          prop="userName"
          label="账号"
          align="center"
        />
        <el-table-column
          prop="name"
          label="用户名"
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
          width="100px"
        >
          <template slot-scope="scope">
            <el-button
              type="primary"
              size="mini"
              icon="el-icon-edit"
              @click="edit(scope.row)"
            >
              编辑
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
      <user-edit
        v-if="config.show"
        :config="config"
        @flush="flush"
      />
    </el-scrollbar>
  </el-main>
</template>
<script>
import userEdit from '../components/modules/UserEdit'
export default {
  components: {
    userEdit
  },
  data() {
    return {
      userData: [],
      totalCount: 0,
      pageSize: 10,
      currentPage: 1,
      search: '',
      config: {
        show: false,
        title: '',
        type: 0, // 0 创建，
        data: {}
      },
      multipleSelection: []
    }
  },
  mounted() {
    this.flush()
  },
  methods: {
    handleSelectionChange(val) {
      this.multipleSelection = val
    },
    async flush() {
      const result = await this.$http.get('api/UserApp/GetUsers', {
        Search: this.search,
        isdesc: true,
        Page: this.currentPage,
        PageSize: this.pageSize
      })
      this.totalCount = result.totalCount
      this.userData = result.data
    },
    create() {
      this.config.title = '创建用户'
      this.config.data = {
        confirmPassword: '123456',
        department: '瞎折腾部',
        email: 'test@qq.com',
        name: 'wql',
        passWord: '123456',
        userName: 'test'
      }
      this.config.show = true
      this.config.type = 0
    },
    async edit(row) {
      this.config.type = 1
      this.config.show = true
      this.config.data = row
    },
    delay() {},
    deleteUser() {
      if (this.multipleSelection && this.multipleSelection.length) {
        const ids = this.multipleSelection.map(item => item.id)
        this.$confirm('确认删除所选用户?', '删除', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        })
          .then(async() => {
            for (let index = 0; index < ids.length; index++) {
              const result = await this.$http.post(
                'api/UserApp/DleteUser',
                ids[index]
              )
              if (result) {
                this.$message({
                  showClose: true,
                  type: 'success',
                  message: '删除成功!'
                })
              }
            }
            this.flush()
          })
          .catch(() => {
            this.$message({
              showClose: true,
              type: 'info',
              message: '已取消删除!'
            })
          })
      } else {
        this.$message({
          showClose: true,
          type: 'warning',
          message: '请选择要删除的用户'
        })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.page_footer_box {
  float: right;
  margin: 3px, 10px;
}
.content {
  flex: 1;
  width: 100%;
  padding: 0;
  height: 100%;

  display: flex;

  flex-direction: column;

  flex-wrap: wrap;
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
</style>
