<template>
  <div class="tabbody">
    {{Tabs}}
    <el-scrollbar wrap-class="scrollbar-wrapper-y">
      <span draggable="true" @dragstart="drag($event)" v-for="tab in Tabs" :key="tab">{{tab.title}}</span>
    </el-scrollbar>
  </div>
</template>
<script>
import { mapState } from "vuex";
export default {
  computed: {
    ...mapState(["Tabs"])
  }
};
</script>

<style lang="scss" scoped>
.tabbody {
  height: 100%;
  overflow: hidden;
  background-color: azure;
}
</style>
