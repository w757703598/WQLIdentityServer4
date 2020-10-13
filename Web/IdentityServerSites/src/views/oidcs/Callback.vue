<template>
  <div />
</template>

<script>
import { mapActions } from 'vuex'
export default {
  name: 'OidcCallback',
  created() {
    // this.user
    //   .signinRedirectCallback()
    //   .then((result) => {
    //     var returnUrl = "/";
    //     console.info(result);
    //     if (result.state) {
    //       returnUrl = result.state;
    //     }
    //     this.$router.push(returnUrl);
    //   })
    //   .catch((err) => {
    //     console.error(err);
    //     this.$router.push("/signin-oidc-error"); // Handle errors any way you want
    //   });
    console.info('callback')
    this.oidcSignInCallback()
      .then((redirectPath) => {
        console.info(redirectPath)
        console.error(redirectPath)
        this.$router.push(redirectPath)
      })
      .catch((err) => {
        console.error(err)
        this.$router.push('/signin-oidc-error') // Handle errors any way you want
      })
  },
  methods: {
    ...mapActions(['oidcSignInCallback']),
  },
}
</script>
