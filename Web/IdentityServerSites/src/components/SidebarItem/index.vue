<template>
  <div v-if="!item.meta.hidden">
    <div v-if="item.children">
      <el-submenu :index="resolvePath(item.path)">
        <template slot="title">
          <i :class="item.meta.icon"></i>
          <span slot="title">{{item.meta.title}}</span>
        </template>
        <template v-for="(item1,index1) in item.children">
          <router-link :to="resolvePath(item1.path)" :key="index1">
            <el-menu-item>
              <i :class="item1.meta.icon"></i>
              <span slot="title">{{item1.meta.title}}</span>
            </el-menu-item>
          </router-link>
        </template>
      </el-submenu>
    </div>
    <div v-else>
      <router-link :to="item.path">
        <el-menu-item :index="resolvePath(item.path)">
          <i :class="item.meta.icon"></i>
          <span slot="title">{{item.meta.title}}</span>
        </el-menu-item>
      </router-link>
    </div>
  </div>
</template>
<script>
import path from "path";
export default {
  methods: {
    resolvePath(routePath) {
      return path.resolve(this.basePath, routePath);
    }
  },
  mounted() {},
  data() {
    return {};
  },
  props: {
    item: {
      type: Object,
      required: true
    },
    basePath: {
      type: String,
      default: ""
    }
  }
};
</script>

