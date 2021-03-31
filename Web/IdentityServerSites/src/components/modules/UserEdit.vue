<template>
  <div>
    <el-dialog :visible.sync="config.show" width="50%" :title="title">
      <el-tabs v-if="config.type!=0" v-model="activeName" @tab-click="handleClick">
        <el-tab-pane label="修改名称" name="修改名称" />
        <el-tab-pane label="重置密码" name="重置密码" />
        <el-tab-pane label="分配角色" name="分配角色">
          <div class="flex">
            <el-select v-model="role" size="mini">
              <el-option v-for="(item,index) in roles" :key="index" :label="item" :value="item" />
            </el-select>
            <el-button type="primary" size="mini" @click="addRole()">添加</el-button>
          </div>
          <el-table
            ref="multipleTable"
            :data="userRoles"
            tooltip-effect="dark"
            size="mini"
            border
            align="center"
            style="width:100%"
            :stripe="true"
          >
            <el-table-column prop="name" label="角色" align="center" />
            <el-table-column label="操作" align="center">
              <template slot-scope="scope">
                <el-button type="danger" size="mini" @click="removeRoles(scope.row)">删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
        <el-tab-pane label="用户声明" name="用户声明">
          <claim-edit v-if="claimConfig.show" :config="claimConfig" />
        </el-tab-pane>
      </el-tabs>
      <el-form
        ref="Userform"
        :rules="rules"
        label-width="120px"
        label-position="left"
        :model="config.data"
      >
        <el-form-item v-if="formType==0||formType==1" label="账号:" prop="userName">
          <el-input v-model="config.data.userName" size="mini" />
        </el-form-item>
        <el-form-item v-if="formType==0||formType==1" label="姓名:" prop="name">
          <el-input v-model="config.data.name" size="mini" />
        </el-form-item>
        <el-form-item v-if="formType==0||formType==1" label="部门:" prop="department">
          <el-input v-model="config.data.department" size="mini" />
        </el-form-item>
        <el-form-item v-if="formType==0" label="邮箱:" prop="email">
          <el-input v-model="config.data.email" size="mini" />
        </el-form-item>

        <el-form-item v-if="formType==2" label="旧密码:" prop="oldPassword">
          <el-input v-model="config.data.oldPassword" size="mini" />
        </el-form-item>
        <el-form-item v-if="formType==0||formType==2" label="密码:" prop="passWord">
          <el-input v-model="config.data.passWord" size="mini" />
        </el-form-item>
        <el-form-item v-if="formType==0||formType==2" label="确认密码:" prop="confirmPassword">
          <el-input v-model="config.data.confirmPassword" size="mini" />
        </el-form-item>
        <div v-if="formType!=3" class="dialog-footer el-message-box__btns">
          <el-button type="info" size="mini" @click="close(false)">取消</el-button>
          <el-button type="primary" size="mini" @click="Submit()">确认</el-button>
        </div>
      </el-form>
    </el-dialog>
  </div>
</template>
<script>
import claimEdit from './ClaimEdit'
export default {
  components: {
    claimEdit
  },
  props: {
    config: {
      type: Object,
      default: function() {
        return {
          show: false,
          title: '创建用户',
          data: {
            // name: "admin",
            // userName: "admin",
            // email: "test@qq.com",
            // passWord: "123456",
            // confirmPassword: "123456"
          }
        }
      }
    }
  },
  data() {
    var validatePass = (rule, value, callback) => {
      if (value !== this.config.data.passWord) {
        callback(new Error('两次输入密码不一致!'))
      }
      callback()
    }
    return {
      userRoles: [],
      role: '', // 选中的角色
      roles: [],
      formType: this.config.type,
      activeName: '修改名称',
      claimConfig: {
        show: false,
        title: '',
        Id: '',
        type: '用户声明'
      },
      rules: {
        userName: [{ required: true, message: '请输入账户', trigger: 'blur' }],
        name: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
        email: [
          {
            type: 'email',
            required: true,
            message: '请输入正确的邮箱格式',
            trigger: 'blur'
          }
        ],
        department: [
          { required: true, message: '请输入部门', trigger: 'blur' }
        ],
        oldPassword: [
          { required: true, min: 6, message: '密码至少6位', trigger: 'blur' }
        ],
        passWord: [
          { required: true, min: 6, message: '密码至少6位', trigger: 'blur' }
        ],
        confirmPassword: [
          { required: true, validator: validatePass, trigger: 'blur' }
        ]
      }
    }
  },
  computed: {
    // 标题
    title: function() {
      const title =
        this.config.type === 0 ? this.config.title : this.config.data.name
      return title
    }
  },
  async mounted() {
    const res = await this.$http.get('api/RoleApp/GetRoles')
    if (res) {
      this.roles = res.data.map(r => r.name)
      this.role = this.roles[0]
    }
    await this.getUserRoles()
  },
  methods: {
    async addRole() {
      var res = await this.$http.post('api/UserApp/AddToRole', {
        userId: this.config.data.id,
        roleName: this.role
      })

      if (res) {
        this.$message({
          message: res,
          type: 'success'
        })
      }
      await this.getUserRoles()
    },
    async removeRoles(row) {
      var res = await this.$http.post('api/UserApp/RemoveUserRole', {
        userId: this.config.data.id,
        roleName: row.name
      })

      if (res) {
        this.$message({
          message: res,
          type: 'success'
        })
      }
      await this.getUserRoles()
    },
    async getUserRoles() {
      if (this.config.data.id) {
        const res = await this.$http.get('api/UserApp/GetUserRoles', {
          userId: this.config.data.id
        })
        if (res) {
          this.userRoles = res.map(r => {
            return { name: r }
          })
        }
      }
    },
    close(isFlush) {
      this.config.show = false

      if (isFlush) this.$emit('flush')
    },
    handleClick(tab, event) {
      switch (tab.name) {
        case '修改名称':
          this.formType = 1
          break
        case '重置密码':
          this.formType = 2
          break
        case '分配角色':
          this.formType = 3
          break
        case '用户声明':
          this.formType = 4
          this.claimConfig.title = `管理用户声明：${this.config.data.userName}`
          this.claimConfig.show = true
          this.claimConfig.Id = this.config.data.id
          this.claimConfig.type = '用户声明'
          break
      }
    },
    Submit() {
      this.$refs.Userform.validate(async valid => {
        if (valid) {
          switch (this.formType) {
            case 0:
              var res = await this.$http.post(
                'api/UserApp/Register',
                this.config.data
              )

              if (res) {
                this.$message({
                  message: '注册成功',
                  type: 'success'
                })

                // this.close(true);
              }
              break
            case 1:
              res = await this.$http.post(
                'api/UserApp/UpdateUser',
                this.config.data
              )

              if (res) {
                this.$message({
                  message: '修改用户成功',
                  type: 'success'
                })
                this.close(true)
              }
              break
            case 2:
              res = await this.$http.post('api/UserApp/ChangePassword', {
                userId: this.config.data.id,
                oldPassword: this.config.data.oldPassword,
                newPassword: this.config.data.passWord
              })

              if (res) {
                this.$message({
                  message: '修改密码成功',
                  type: 'success'
                })
                this.close(true)
              }
              break
          }
        }
      })
    }
  }
}
</script>
<style lang="scss" scoped>
.flex {
  justify-content: flex-end;
}
</style>
