<template>
  <component v-bind="linkProps(to)">
    <slot></slot>
  </component>
</template>

<script>
export default {
  props: {
    to: {
      type: String,
      required: true
    }
  },
  methods: {
    linkProps(url) {
      return {
        is: "router-link",
        to: url
      };
    }
  }
};
</script>
