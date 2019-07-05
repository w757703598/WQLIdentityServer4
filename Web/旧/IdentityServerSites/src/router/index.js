import Vue from "vue";
import Router from "vue-router";

Vue.use(Router);

import Layout from "@/layout";
export const constantRoutes = [
  {
    path: "/",
    name: "root"
  },
  {
    path: "/example",
    component: Layout,
    redirect: "/example/table",
    name: "Example",
    meta: { title: "Example", icon: "sidebar" },
    children: [
      {
        path: "table",
        name: "Table",
        component: () => import("@/views/login/index"),
        meta: { title: "Table", icon: "sidebar" }
      },
      {
        path: "tree",
        name: "Tree",
        component: () => import("@/views/login/index"),
        meta: { title: "Tree", icon: "sidebar" }
      }
    ]
  },

  {
    path: "/form",
    component: Layout,
    children: [
      {
        path: "index",
        name: "Form",
        component: () => import("@/views/login/index"),
        meta: { title: "Form", icon: "sidebar" }
      }
    ]
  }
];

const createRouter = () =>
  new Router({
    // mode: 'history', // require service support
    scrollBehavior: () => ({ y: 0 }),
    routes: constantRoutes
  });

const router = createRouter();

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter();
  router.matcher = newRouter.matcher; // reset router
}

export default router;
