<template>
  <div class="hello">
    <el-button @click="callApi">callAPi</el-button>
    <el-button @click="getToken">getToken</el-button>
    <el-button @click="getTokenId">getTokenId</el-button>
    <el-button @click="getTokenSessionState">getTokenSessionState</el-button>
    <el-button @click="getAccessToken">getAccessToken</el-button>
    <el-button @click="getTokenScopes">getTokenScopes</el-button>
    <el-button @click="getTokenProfile">getTokenProfile</el-button>
    <el-button @click="getrole">getrole</el-button>
    <el-button @click="renewToken">renewToken</el-button>
    <div class="message">
      <el-alert type="success" v-if="result">{{message}}</el-alert>
      <el-alert type="error" v-if="!result">{{message}}</el-alert>
    </div>
  </div>
</template>

<script>
import Mgr from "../services/SecurityService";
export default {
  name: "HelloWorld",
  data() {
    return {
      mgr: new Mgr(),
      message: "",
      result: true
    };
  },
  methods: {
    async callApi() {
      let res = await this.$http.get("/api/TestValue/Get");
      if (res) {
        console.info(JSON.stringify(res));
        this.message = res;
      }
    },
    getToken() {
      let self = this;
      this.mgr.getUser().then(
        token => {
          self.logToken(token);
        },
        err => {
          console.log(err);
        }
      );
    },
    getrole() {
      let self = this;
      this.mgr.getRole().then(
        token => {
          self.logToken(token);
        },
        err => {
          console.log(err);
        }
      );
    },
    getTokenId() {
      let self = this;
      this.mgr.getIdToken().then(
        tokenId => {
          self.logToken(tokenId);
        },
        err => {
          console.log(err);
        }
      );
    },
    getTokenSessionState() {
      let self = this;
      this.mgr.getSessionState().then(
        sessionState => {
          self.logToken(sessionState);
        },
        err => {
          console.log(err);
        }
      );
    },
    getAccessToken() {
      let self = this;
      this.mgr.getAcessToken().then(
        acessToken => {
          self.logToken(acessToken);
        },
        err => {
          console.log(err);
        }
      );
    },
    getTokenScopes() {
      let self = this;
      this.mgr.getScopes().then(
        scopes => {
          self.logToken(scopes);
        },
        err => {
          console.log(err);
        }
      );
    },
    getTokenProfile() {
      let self = this;
      this.mgr.getProfile().then(
        tokenProfile => {
          self.logToken(tokenProfile);
        },
        err => {
          console.log(err);
        }
      );
    },
    renewToken() {
      let self = this;
      this.mgr.renewToken().then(
        newToken => {
          self.logToken(newToken);
        },
        err => {
          console.log(err);
        }
      );
    },
    logApi(msg) {
      this.message = msg;
    },
    logToken(msg) {
      this.message = msg;
    }
  }
};
</script>

<style lang="scss" scoped>
.message {
  margin-top: 30px;
}
</style>
