import Vue from 'vue'
import Router from 'vue-router'
import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc'
import uilts from './services/uilts'
import store from './store'

Vue.use(Router)

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/login',
      name: 'login',
      component: () => import('./views/Login.vue'),
      meta: {
        requireAuth: false,
      },
    },
    {
      path: '/oidc-callback',
      name: 'OidcCallback',
      component: () => import('./views/oidcs/Callback.vue'),
      meta: {
        requireAuth: false,
      },
    },
    {
      meta: {
        requireAuth: true,
      },
      path: '/protected',
      name: 'protected',
      component: () => import('./views/oidcs/Protected.vue'),
    },

    {
      path: '/oidc-popup-callback', // Needs to match popupRedirectUri in you oidcSettings
      name: 'oidcPopupCallback',
      component: () => import('./views/oidcs/OidcPopupCallback.vue'),
    },
    {
      path: '/oidc-callback-error', // Needs to match redirect_uri in you oidcSettings
      name: 'oidcCallbackError',
      component: () => import('./views/oidcs/OidcCallbackError.vue'),
      meta: {
        isPublic: true,
      },
    },
    {
      meta: {
        requireAuth: false,
      },
      path: '/',
      name: 'home',
      component: () => import('./components/Layout/index.vue'),
      //一下组件为布局页显示组件
      children: [
        {
          meta: {
            title: '角色管理',
            icon: 'el-icon-s-custom',
            requireAuth: true,
            role: ['Administrator'],
          },
          path: '/role',
          component: () => import('./views/RoleManagement.vue'),
        },
        {
          meta: {
            title: '人员管理',
            icon: 'el-icon-s-custom',
            requireAuth: true,
            role: ['Administrator'],
          },
          path: '/user',
          component: () => import('./views/UserManagement.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: '声明管理',
            requireAuth: true,
            role: ['Administrator'],
          },
          path: '/claims',
          component: () => import('./views/ClaimManagement.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: 'API资源管理',
            requireAuth: true,
            role: ['Administrator'],
            breadcrumb: [
              { title: '属性配置', name: 'ApiProperties' },
              { title: '密钥配置', name: 'ApiSecret' },
              { title: '作用域配置', name: 'ApiScopes' },
            ],
          },

          path: '/Apiresource',
          name: 'Apiresource',
          component: () => import('./views/ApiResources.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: '身份资源管理',
            requireAuth: true,
            role: ['Administrator'],
            breadcrumb: [{ title: '属性配置', name: 'ApiProperties' }],
          },

          path: '/Idnetityresource',
          name: 'Idnetityresource',
          component: () => import('./views/IdentityResource.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: '客户端管理',
            requireAuth: true,
            role: ['Administrator'],
            breadcrumb: [
              { title: '属性配置', name: 'ApiProperties' },
              { title: '密钥配置', name: 'ApiSecret' },
              { title: '作用域配置', name: 'ApiScopes' },
            ],
          },

          path: '/Client',
          name: 'Client',
          component: () => import('./views/Client.vue'),
        },
        {
          meta: {
            title: '个人信息管理',
            icon: 'el-icon-s-custom',
          },
          path: '/userinfo',
          component: () => import('./views/Userinfo.vue'),
        },
        {
          meta: {
            title: '拒绝访问',
            hidden: true,
          },
          path: '/accessdenied',
          name: 'accessdenied',
          component: () => import('./views/AccessDenied.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: 'about',
          },

          path: '/about',

          name: 'about',

          // route level code-splitting
          // this generates a separate chunk (about.[hash].js) for this route
          // which is lazy-loaded when the route is visited.
          component: () =>
            import(/* webpackChunkName: "about" */ './views/About.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: 'hello',
          },
          path: '/hello',

          name: 'hello',

          // route level code-splitting
          // this generates a separate chunk (about.[hash].js) for this route
          // which is lazy-loaded when the route is visited.
          component: () => import('./components/HelloWorld.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: '面板测试',
          },
          path: '/ClaimEdit1',

          name: 'ClaimEdit1',

          // route level code-splitting
          // this generates a separate chunk (about.[hash].js) for this route
          // which is lazy-loaded when the route is visited.
          component: () =>
            import(// import("./components/modules/ClaimEdit.vue")
            './components/modules/ApiProperties.vue'),
        },
        //子组件 左边列表不显示
        {
          path: '/ApiScopes',
          meta: {
            icon: 'el-icon-s-custom',
            title: 'ApiScopes',
            hidden: true,
          },
          name: 'ApiScopes',
          props: true,
          component: () => import('./components/modules/ApiScopes.vue'),
        },
        {
          path: '/ApiSecret',
          meta: {
            icon: 'el-icon-s-custom',
            title: 'ApiSecret',
            hidden: true,
          },
          name: 'ApiSecret',

          props: true,
          component: () => import('./components/modules/ApiSecret.vue'),
        },
        {
          path: '/ApiProperties',
          meta: {
            icon: 'el-icon-s-custom',
            title: 'ApiProperties',
            hidden: true,
          },
          name: 'ApiProperties',

          props: true,
          component: () => import('./components/modules/ApiProperties.vue'),
        },
      ],
    },
  ],
})
router.beforeEach(vuexOidcCreateRouterMiddleware(store))

router.beforeEach((to, from, next) => {
  if (to.meta.requireAuth) {
    store.dispatch('getOidcUser').then((user) => {
      console.info(user)
      if (user == null) {
        // console.info('用户为空登陆', to.path)
        // store.dispatch('authenticateOidc', to.path)
      } else {
        if (to.meta.role) {
          console.info('判断角色')

          if (uilts.CheckPermiss(to.meta.role, user.profile.role)) {
            next()
          } else {
            next('/accessdenied')
          }
        } else {
          console.info('不需要判断角色')
          next()
        }
      }
    })
  } else {
    console.info('不需要授权')
    next()
  }
})

// router.beforeEach((to, from, next) => {
//   if (to.meta.requireAuth) {
//     console.info(store)
//     console.info(store.dispatch('getOidcUser'))
//     store.dispatch('getOidcUser').then((user) => {
//       console.info(user)
//       if (user == null) {
//         console.info('用户为空登陆', to.path)
//         store.dispatch('oidcSignInCallback', to.path)
//       } else {
//         if (to.meta.role) {
//           console.info('判断角色')
//           console.info(user)
//           next()
//           // console.info(user.profile.role)
//           // if (uilts.CheckPermiss(to.meta.role, user.profile.role)) {
//           //   next()
//           // } else {
//           //   next('/accessdenied')
//           // }
//         } else {
//           console.info('不需要判断角色')
//           next()
//         }
//       }
//     })
//   } else {
//     console.info('不需要授权')
//     next()
//   }
// })

export default router
