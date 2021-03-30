import Vue from 'vue'
import 'normalize.css/normalize.css' // A modern alternative to CSS resets


import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'

import '@/styles/index.scss' // global css

import App from './App.vue'
import store from './store'
import router from './router'

import  './icons'



import http from './plugin/http'

Vue.use(ElementUI)
Vue.use(http)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app')
