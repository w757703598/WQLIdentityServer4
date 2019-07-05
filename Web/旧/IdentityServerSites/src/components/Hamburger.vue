<template>
  <div id="test" class="icon" @click="toggleClick">
    <svg class="hamburger" aria-hidden="true" :class="{'is-active':!sidebar.opened}">
      <use xlink:href="#icon-sidebar"></use>
    </svg>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "Hamburger",
  props: {
    isActive: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    ...mapGetters(["sidebar"])
  },
  methods: {
    toggleClick() {
      this.$store.dispatch("toggleSideBar");
    }
  }
};
</script>

<style scoped>
.hamburger {
  display: inline-block;
  vertical-align: middle;
  width: 20px;
  height: 20px;
}

.hamburger.is-active {
  transform: rotate(180deg);
}
</style>
