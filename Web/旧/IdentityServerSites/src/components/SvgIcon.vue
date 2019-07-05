<template>
  <svg :class="svgClass" aria-hidden="true" v-on="$listeners">
    <use :xlink:href="iconName"></use>
  </svg>
</template>
<script>
export default {
  name: "svgIcon",
  props: {
    iconClass: {
      tyoe: String,
      default: ""
    },
    className: {
      tyoe: String,
      default: ""
    }
  },
  computed: {
    iconName() {
      return `#icon-${this.iconClass}`;
    },
    svgClass() {
      if (this.className) {
        return "svg-icon" + this.className;
      } else {
        return "svg-icon";
      }
    }
  }
};
</script>
<style scoped>
.svg-icon {
  width: 1em;
  height: 1em;
  vertical-align: -0.15em;
  fill: currentColor;
  overflow: hidden;
  margin-left: inherit;
}
</style>

