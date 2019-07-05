import axios from "axios"
import { Loading } from 'element-ui';
import { Message } from 'element-ui';

import Mgr from '../services/SecurityService';

let mgr = new Mgr();





//请求拦截器
axios.interceptors.request.use(async config => {
  await mgr.getAcessToken().then(authToken => {
    console.info(authToken)
    config.headers.Authorization = `Bearer ${authToken}`;
  })

  return config;
}, error => {

  return Promise.reject(error);// 错误提示
})

//响应拦截器
axios.interceptors.response.use(response => {
  return response
}, error => {
  Message.error({ showClose: true, message: "网络异常" });
  return Promise.reject(error.response)
})
//处理结果
const handleResult = (res) => {

  switch (res.status) {
    case 200:
      return res.data;
    case 500:
      Message.error({ showClose: true, message: res.data.message });
      break;
    case 401:
      Message.error({ showClose: true, message: "未授权" });
      break;
    case 403:
      Message.error({ showClose: true, message: "无权限访问" });
      break;
    case 404:
      Message.error({ showClose: true, message: "请求地址不存在" });
      break;
    case 400:
      var errorMsg = res.data.error_description || res.data.message || res.data[0].description;

      if (errorMsg) {
        Message.error({ showClose: true, message: errorMsg });
      }
      break;
    default:
      Message.error({ showClose: true, message: "未知异常联系管理员" });
      break;
  }
}

// var axiosInstance=axios.create({
//   baseURL: 'https://localhost:5000/',
//   timeout: 1000,
// })

axios.defaults.baseURL = 'http://10.53.28.168:5010/';
axios.defaults.headers = {
  "Content-Type": "application/json;charset=UTF-8",
  // Authorization: "bearer " + cookie.get("access_token")
};

var http = {

  count: 0,
  loading: null,
  /** block ui */
  block() {
    this.count = this.count + 1;
    if (this.count != 1) return;
    this.loading = Loading.service({
      lock: true,
      text: 'Loading',
      spinner: 'el-icon-loading',
      background: 'rgba(0, 0, 0, 0.0)'
    });
  },
  /** unblock */
  unblock() {
    this.count = this.count - 1;
    if (this.count < 0) this.count = 0;
    if (this.count) return;
    this.loading.close();
  },

  // async defineHeaderAxios() {

  //   await user.getAcessToken().then(
  //     acessToken => {
  //       console.info(acessToken)
  //       axios.defaults.headers.common['Authorization'] = 'Bearer ' + acessToken
  //     }, err => {
  //       console.log(err)
  //     })
  // },

  async get(url, params) {
    this.block();

    const res = await axios.get(url, { params: params }).catch(err => err);

    this.unblock();

    return handleResult(res)
  },
  async post(url, data, config) {
    this.block();
    const res = await axios.post(url, data, config).catch(err => err);
    this.unblock();
    return handleResult(res)
  }


}


export default {
  http: http,
  install(Vue, options) {
    Vue.prototype.$http = http;
  }
}