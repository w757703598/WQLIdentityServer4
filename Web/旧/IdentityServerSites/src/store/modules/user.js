import token from "../../utils/token.js";

const track = {
    state: {
        token: token.gettoken()
    },
    getters: {
        token: state => state.token
    }
};

export default track;
