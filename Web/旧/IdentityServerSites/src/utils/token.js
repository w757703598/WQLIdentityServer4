import cookie from "js-cookie";

const tokenKey = "access_token";
export default {
    gettoken() {
        return cookie.get(tokenKey);
    },
    settoken(token) {
        return cookie.set(tokenKey, token);
    },
    removetoken() {
        return cookie.remove(tokenKey);
    }
};
