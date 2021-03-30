import Vue from 'vue'
import Router from 'vue-router'
import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc'
import uilts from './services/uilts'
import store from './store'

/* Layout */
import Layout from '@/layout'


/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'/'el-icon-x' the icon show in the sidebar
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */

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
      hidden: true
    },
    {
      path: '/oidc-callback',
      name: 'OidcCallback',
      component: () => import('./views/oidcs/Callback.vue'),
      meta: {
        requireAuth: false,
      },
      hidden: true
    },
    {
      meta: {
        requireAuth: true,
      },
      path: '/protected',
      name: 'protected',
      component: () => import('./views/oidcs/Protected.vue'),
      hidden: true
    },

    {
      path: '/oidc-popup-callback', // Needs to match popupRedirectUri in you oidcSettings
      name: 'oidcPopupCallback',
      component: () => import('./views/oidcs/OidcPopupCallback.vue'),
      hidden: true
    },
    {
      path: '/oidc-callback-error', // Needs to match redirect_uri in you oidcSettings
      name: 'oidcCallbackError',
      component: () => import('./views/oidcs/OidcCallbackError.vue'),
      meta: {
        isPublic: true,
      },
      hidden: true
    },
    {
      path: '/',
      component: Layout,
      redirect: '/home',
      hidden:true,
      children: [
        {
          path: '/home',
          component: () => import('@/views/Home.vue'),
          meta: { title: 'Monitor', icon: 'dashboard' }
        }
      ]
    },
    {
      path:"/admin",
      meta: {
        requireAuth: false,
        title: 'IdentityServer', icon: 'wqlapi'
      },
      redirect: '/user',
   
      component: Layout,
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
          },
          hidden: true,
          path: '/accessdenied',
          name: 'accessdenied',
          component: () => import('./views/AccessDenied.vue'),
        },
        {
          meta: {
            icon: 'el-icon-s-custom',
            title: 'about',
          },
          hidden: true,
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
          hidden: true,
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
          hidden: true,
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
          
          },
          hidden: true,
          name: 'ApiScopes',
          props: true,
          component: () => import('./components/modules/ApiScopes.vue'),
        },
        {
          path: '/ApiSecret',
          meta: {
            icon: 'el-icon-s-custom',
            title: 'ApiSecret',
         
          },
          hidden: true,
          name: 'ApiSecret',

          props: true,
          component: () => import('./components/modules/ApiSecret.vue'),
        },
        {
          path: '/ApiProperties',
          meta: {
            icon: 'el-icon-s-custom',
            title: 'ApiProperties',
           
          },
          hidden: true,
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
    console.info(to.meta.requireAuth)
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
