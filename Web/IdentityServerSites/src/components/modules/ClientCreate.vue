<template>
  <div>
    <el-dialog :visible.sync="config.show" width="50%" :title="this.config.title ">
      <el-form
        ref="Userform"
        :rules="rules"
        label-width="120px"
        label-position="left"
        :model="config.data"
      >
        <el-form-item label="标识:" prop="clientId">
          <el-input v-model="config.data.clientId" size="mini" />
        </el-form-item>
        <el-form-item label="名称:" prop="clientName">
          <el-input v-model="config.data.clientName" size="mini" />
        </el-form-item>
        <el-form-item label="类型:" prop="clientType">
          <el-select v-model="config.data.clientType" size="mini">
            <el-option
              v-for="item in clientTypes"
              :key="item.id"
              :label="item.text"
              :value="item.id"
            />
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
  props: {
    config: {
      type: Object,
      default: function() {
        return {
          show: false,
          title: '创建用户',
          data: {}
        }
      }
    }
  },
  data() {
    return {
      clientTypes: [],
      formType: this.config.type,
      activeName: '修改名称',
      claimConfig: {
        show: false,
        title: '',
        Id: '',
        type: '用户声明'
      },
      rules: {
        clientId: [
          { required: true, message: '请输入客户端标识', trigger: 'blur' }
        ],
        clientName: [
          { required: true, message: '请输入客户端名称', trigger: 'blur' }
        ],
        clientType: [{ required: true, message: '客户端类型', trigger: 'blur' }]
      }
    }
  },
  async mounted() {
    const res = await this.$http.get('api/Infrastructure/GetClientTypes')
    if (res) {
      console.info()
      this.clientTypes = res.map(d => { return { 'id': parseInt(d.id), 'text': d.text } })
    }
    await this.getUserRoles()
  },
  methods: {
    close(isFlush) {
      this.config.show = false

      if (isFlush) this.$emit('flush')
    },
    Submit() {
      this.$refs.Userform.validate(async valid => {
        if (valid) {
          var res = await this.$http.post(
            'api/Client/AddClient',
            this.config.data
          )

          if (res) {
            this.$message({
              message: '注册成功',
              type: 'success'
            })
            this.close(true)
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
