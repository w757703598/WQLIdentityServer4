import axios from "axios";
import { Message } from "element-ui";
//import store from "./../store/index";
import token from "./token";

// let baseURL = "http://10.53.28.168:8010/";
let baseURL = "http://localhost:5000/";

const service = axios.create({
    baseURL: baseURL, // url = base url + request url
    withCredentials: true, // send cookies when cross-domain requests
    timeout: 5000 // request timeout
});

//请求拦截器
service.interceptors.request.use(
    config => {
        let tokenStr = token.gettoken();
        if (tokenStr) {
            config.headers = {
                "Content-Type": "application/json;charset=UTF-8",
                Authorization: "bearer " + tokenStr
            };
        }
    },
    error => {
        return Promise.reject(error);
    }
);

//响应拦截器
service.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        return Promise.reject(error);
    }
);

const handleResult = res => {
    if (res.status == 200) {
        return res.data;
    } else if (res.status == 500) {
        Message.error({ showClose: true, message: "服务器错误" });
    }

    if (res.status === 401) {
        Message.error({ showClose: true, message: "未授权" });
    } else if (res.status === 403) {
        Message.error({ showClose: true, message: "您无权限访问！" });
    } else if (res.status === 404) {
        Message.error({
            showClose: true,
            message: "请求接口地址不存在，请检查！"
        });
    } else if (res.status === 400) {
        Message.error({ showClose: true, message: "请求有误" + res.data });
    } else {
        Message.error({ showClose: true, message: "网络异常！" });
    }
    throw res;
};

var http = {
    baseUrl() {
        return baseURL;
    },
    async get(url) {
        const res = await axios.get(url).catch(res => res);
        return handleResult(res);
    },
    async post(url, data) {
        const res = await axios.post(url, data).catch(res => res);
        this.unblock();
        return handleResult(res);
    }
};

export default http;
