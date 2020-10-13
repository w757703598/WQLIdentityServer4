<template>
  <div />
</template>

<script>
import { mapActions } from 'vuex'

export default {
  name: 'OidcPopupCallback',
  methods: {
    ...mapActions(['oidcSignInPopupCallback']),
  },
  created() {
    this.oidcSignInPopupCallback().catch((err) => {
      console.error(err) // Handle errors any way you want
    })
  },
}
</script>
