<template>
  <div class="hello">
    <el-button @click="callApi">
      callAPi
    </el-button>
    <el-button @click="weixin">
      企业微信
    </el-button>
    <el-button @click="getUser">
      getUser
    </el-button>
    <el-button @click="getTokenId">
      getTokenId
    </el-button>

    <el-button @click="getAccessToken">
      getAccessToken
    </el-button>
    <el-button @click="getTokenScopes">
      getTokenScopes
    </el-button>
    <el-button @click="getTokenProfile">
      getTokenProfile
    </el-button>
    <el-button @click="getrole">
      getrole
    </el-button>
    <el-button @click="renewToken">
      renewToken
    </el-button>
    <div class="message">
      <el-alert v-if="result" type="success">
        {{ message }}
      </el-alert>
      <el-alert v-if="!result" type="error">
        {{ message }}
      </el-alert>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import { mapActions, mapGetters } from 'vuex'
export default {
  name: 'HelloWorld',
  data() {
    return {
      message: '',
      result: true,
    }
  },

  computed: {
    ...mapGetters([
      'oidcIsAuthenticated',
      'oidcUser',
      'oidcAccessToken',
      'oidcAccessTokenExp',
      'oidcIdToken',
      'oidcIdTokenExp',
      'oidcRefreshToken',
      'oidcRefreshTokenExp',
      'oidcAuthenticationIsChecked',
      'oidcError',
    ]),
    hasAccess: function() {
      return this.oidcIsAuthenticated || this.$route.meta.isPublic
    },
  },
  mounted() {
    console.info(this)
  },
  methods: {
    ...mapActions([]),

    async weixin() {
      var axiosInstance = axios.create({
        timeout: 1000,
      })
      let message = {
        msgtype: 'markdown',
        markdown: {
          content:
            '<font color="warning"># 重要通知</font>，请相关同事注意: \n ><font color="comment">今天下午五点喝咖啡</font> ',
        },
      }

      let res = await axiosInstance
        .post(
          'https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=5b7cb0c8-68d8-4b06-ba6c-9f9cee87cfdd',
          message,
          {
            headers: { 'Content-Type': 'text/plain' },
          }
        )
        .catch((err) => {
          self.message = err
        })
      if (res) {
        console.info(JSON.stringify(res))
        self.message = res
      }
    },
    async callApi() {
      let res = await this.$http.get('/api/TestValue/Get')
      if (res) {
        console.info(JSON.stringify(res))
        this.message = res
      }
    },
    getUser() {
      this.message = this.oidcUser
    },
    getrole() {
      this.message = this.oidcUser.role
    },
    getTokenId() {
      this.message = this.oidcIdToken
    },

    getAccessToken() {
      this.message = this.oidcAccessToken
    },
    getTokenScopes() {
      this.message = this.user.scopes
    },
    getTokenProfile() {
      this.message = this.oidcUser.profile
    },
    renewToken() {
      this.authenticateOidcSilent()
    },
  },
}
</script>

<style lang="scss" scoped>
.message {
  margin-top: 30px;
}
</style>
