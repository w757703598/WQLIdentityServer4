<template>
  <div></div>
</template>

<script>
import Mgr from "../services/SecurityService";
// var mgr = new Mgr();
export default {
  name: "OidcCallback",
  data() {
    return {
      user: new Mgr({ response_mode: "query" })
    };
  },
  methods: {},
  mounted() {
    this.user
      .signinRedirectCallback()
      .then(result => {
        var returnUrl="/"
        console.info(result);
        if(result.state){returnUrl=result.state}
        this.$router.push(returnUrl);
      })
      .catch(err => {
        console.error(err);
        this.$router.push("/signin-oidc-error"); // Handle errors any way you want
      });
  }
};
</script>
