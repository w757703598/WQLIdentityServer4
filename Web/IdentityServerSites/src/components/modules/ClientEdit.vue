<template>
  <el-dialog
    :visible.sync="config.show"
    :title="this.config.data.clientName "
  >
    <el-tabs v-model="activeName">
      <el-tab-pane
        label="基本信息"
        name="基本信息"
      />
      <el-tab-pane
        label="认证/注销"
        name="认证/注销"
      />
      <el-tab-pane
        label="令牌"
        name="令牌"
      />
      <el-tab-pane
        label="同意屏幕"
        name="同意屏幕"
      />
      <el-tab-pane
        label="设备流程"
        name="设备流程"
      />
    </el-tabs>
    <el-form
      ref="Userform"
      :rules="rules"
      label-width="240px"
      label-position="left"
      :model="config.data"
      class="dialog-form"
    >
      <div v-show="activeName=='基本信息'">
        <el-form-item
          label="标识:"
          prop="clientId"
        >
          <el-input
            v-model="config.data.clientId"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="名称:"
          prop="clientName"
        >
          <el-input
            v-model="config.data.clientName"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="是否启用:"
          prop="enabled"
        >
          <el-switch
            v-model="config.data.enabled"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="协议类型:"
          prop="protocolType"
        >
          <el-select
            v-model="config.data.protocolType"
            size="mini"
          >
            <el-option
              v-for="(item,index) in protocolTypes"
              :key="index"
              :label="item.text"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="需要客户端密钥:"
          prop="requireClientSecret"
        >
          <el-switch
            v-model="config.data.requireClientSecret"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="需要Pkce:"
          prop="requirePkce"
        >
          <el-switch
            v-model="config.data.requirePkce"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="允许纯文本Pkce:"
          prop="allowPlainTextPkce"
        >
          <el-switch
            v-model="config.data.allowPlainTextPkce"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="允许刷新令牌:"
          prop="allowOfflineAccess"
        >
          <el-switch
            v-model="config.data.allowOfflineAccess"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="允许通过浏览器访问令牌:"
          prop="allowAccessTokensViaBrowser"
        >
          <el-switch
            v-model="config.data.allowAccessTokensViaBrowser"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="允许访问的范围:"
          prop="allowedScopes"
        >
          <el-select
            v-model="config.data.allowedScopes"
            filterable
            multiple
            allow-create
            default-first-option
          >
            <el-option
              v-for="(item,index) in Scopes"
              :key="index"
              :label="item"
              :value="index"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="重定向地址:"
          prop="redirectUris"
        >
          <el-select
            v-model="config.data.redirectUris"
            filterable
            multiple
            allow-create
            default-first-option
          />
        </el-form-item>
        <el-form-item
          label="允许授权类型:"
          prop="allowedGrantTypes"
        >
          <el-select
            v-model="config.data.allowedGrantTypes"
            filterable
            multiple
            default-first-option
            allow-create
          >
            <el-option
              v-for="(item,index) in allowedGrantTypes"
              :key="index"
              :label="item"
              :value="item"
            />
          </el-select>
        </el-form-item>
      </div>

      <div v-show="activeName=='认证/注销'">
        <el-form-item
          label="前端注销通道Uri:"
          prop="frontChannelLogoutUri"
        >
          <el-input
            v-model="config.data.frontChannelLogoutUri"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="需要前端通道注销会话:"
          prop="frontChannelLogoutSessionRequired"
        >
          <el-switch
            v-model="config.data.frontChannelLogoutSessionRequired"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="后端通道退出Uri:"
          prop="backChannelLogoutUri"
        >
          <el-input
            v-model="config.data.backChannelLogoutUri"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="需要后端通道注销会话:"
          prop="backChannelLogoutSessionRequired"
        >
          <el-switch
            v-model="config.data.backChannelLogoutSessionRequired"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="启用本地登录:"
          prop="enableLocalLogin"
        >
          <el-switch
            v-model="config.data.enableLocalLogin"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="注销重定向Uri:"
          prop="postLogoutRedirectUris"
        >
          <el-select
            v-model="config.data.postLogoutRedirectUris"
            filterable
            multiple
            allow-create
            default-first-option
          />
        </el-form-item>
        <el-form-item
          label="身份提供程序限制:"
          prop="identityProviderRestrictions"
        >
          <el-select
            v-model="config.data.identityProviderRestrictions"
            filterable
            multiple
            allow-create
            default-first-option
          />
        </el-form-item>
        <el-form-item
          label="用户SSO生命周期:"
          prop="userSsoLifetime "
        >
          <el-input
            v-model="config.data.userSsoLifetime "
            size="mini"
          />
        </el-form-item>
      </div>

      <div v-show="activeName=='令牌'">
        <el-form-item
          label="身份令牌生命周期:"
          prop="identityTokenLifetime "
        >
          <el-input
            v-model="config.data.identityTokenLifetime "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="访问令牌生命周期:"
          prop="accessTokenLifetime "
        >
          <el-input
            v-model="config.data.accessTokenLifetime "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="授权码生命周期:"
          prop="authorizationCodeLifetime "
        >
          <el-input
            v-model="config.data.authorizationCodeLifetime "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="绝对刷新令牌生命周期:"
          prop="absoluteRefreshTokenLifetime "
        >
          <el-input
            v-model="config.data.absoluteRefreshTokenLifetime "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="滚动刷新令牌生命周期:"
          prop="slidingRefreshTokenLifetime "
        >
          <el-input
            v-model="config.data.slidingRefreshTokenLifetime "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="访问令牌类型:"
          prop="accessTokenType "
        >
          <el-select
            v-model="config.data.accessTokenType"
            size="mini"
          >
            <el-option
              label="jwt"
              :value="0"
            />
            <el-option
              label="Refrence"
              :value="1"
            />
          </el-select>
        </el-form-item>

        <el-form-item
          label="刷新令牌使用情况:"
          prop="refreshTokenUsage "
        >
          <el-select
            v-model="config.data.refreshTokenUsage"
            size="mini"
          >
            <el-option
              label="ReUse"
              :value="0"
            />
            <el-option
              label="OneTime"
              :value="1"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="刷新令牌过期:"
          prop="refreshTokenExpiration "
        >
          <el-select
            v-model="config.data.refreshTokenExpiration"
            size="mini"
          >
            <el-option
              label="Absolute "
              :value="0"
            />
            <el-option
              label="Sliding"
              :value="1"
            />
          </el-select>
        </el-form-item>
        <el-form-item
          label="允许跨域来源:"
          prop="allowedCorsOrigins"
        >
          <el-select
            v-model="config.data.allowedCorsOrigins"
            filterable
            multiple
            allow-create
            default-first-option
          />
        </el-form-item>
        <el-form-item
          label="刷新时更新访问令牌声明 :"
          prop="updateAccessTokenClaimsOnRefresh"
        >
          <el-switch
            v-model="config.data.updateAccessTokenClaimsOnRefresh"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="包括 Jwt 标识:"
          prop="includeJwtId"
        >
          <el-switch
            v-model="config.data.includeJwtId"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="始终发送客户端声明:"
          prop="alwaysSendClientClaims"
        >
          <el-switch
            v-model="config.data.alwaysSendClientClaims"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="始终在标识令牌中包含用户声明:"
          prop="alwaysIncludeUserClaimsInIdToken"
        >
          <el-switch
            v-model="config.data.alwaysIncludeUserClaimsInIdToken"
            size="mini"
          />
        </el-form-item>

        <el-form-item
          label="客户端声明前缀:"
          prop="clientClaimsPrefix"
        >
          <el-input
            v-model="config.data.clientClaimsPrefix "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="配对主体盐 :"
          prop="pairWiseSubjectSalt"
        >
          <el-input
            v-model="config.data.pairWiseSubjectSalt "
            size="mini"
          />
        </el-form-item>
      </div>
      <div v-show="activeName=='同意屏幕'">
        <el-form-item
          label="需要同意"
          prop="requireConsent"
        >
          <el-switch
            v-model="config.data.requireConsent"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="需要记住同意"
          prop="allowRememberConsent"
        >
          <el-switch
            v-model="config.data.allowRememberConsent"
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="客户端Uri:"
          prop="clientUri"
        >
          <el-input
            v-model="config.data.clientUri "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="徽标Uri:"
          prop="logoUri"
        >
          <el-input
            v-model="config.data.logoUri "
            size="mini"
          />
        </el-form-item>
      </div>
      <div v-show="activeName=='设备流程'">
        <el-form-item
          label="用户代码类型 "
          prop="userCodeType"
        >
          <el-input
            v-model="config.data.userCodeType "
            size="mini"
          />
        </el-form-item>
        <el-form-item
          label="设备代码生命周期 "
          prop="deviceCodeLifetime"
        >
          <el-input
            v-model="config.data.deviceCodeLifetime "
            size="mini"
          />
        </el-form-item>
      </div>
    </el-form>

    <div class="dialog-footer el-message-box__btns">
      <el-button
        type="info"
        size="mini"
        @click="close(false)"
      >
        取消
      </el-button>
      <el-button
        type="primary"
        size="mini"
        @click="Submit()"
      >
        确认
      </el-button>
    </div>
  </el-dialog>
</template>
<script>
export default {
  props: {
    config: {
      type: Object,
      default: function() {
        return {
          show: false,
          title: "编辑客户端",
          data: {}
        };
      }
    }
  },
  data() {
    return {
      allowedGrantTypes: [],
      refreshTokenExpiration: [],
      Scopes: [],
      protocolTypes: [],
      activeName: "基本信息",
      rules: {}
    };
  },
  async mounted() {
    let res = await this.$http.get("api/Client/GetClient", {
      id: this.config.data.id
    });
    if (res) {
      this.config.data = res;
    }
    await this.SelectInit();
  },
  methods: {
    async SelectInit() {
      let res = await this.$http.get("api/Infrastructure/GetProtocolTypes");
      if (res) {
        this.protocolTypes = res;
        this.config.data.protocolType = res[0].id;
      }
      res = await this.$http.get("api/Infrastructure/GetGrantTypes");
      if (res) {
        this.allowedGrantTypes = res;
      }
    },
    async Submit() {
      let res = await this.$http.post(
        "api/Client/UpdateClient",
        this.config.data
      );
      if (res) {
        this.$message({
          message: res,
          type: "success"
        });
      }
    }
  }
};
</script>
<style lang="scss" scoped>
.dialog-form{
  .el-select{
  width: 100%;
  }
}
</style>
