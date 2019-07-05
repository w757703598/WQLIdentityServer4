const path = require("path");

function resolve(dir) {
  return path.join(__dirname, dir);
}
const name = "Admin Template"; // page title

module.exports = {
  assetsDir: "static",
  configureWebpack: {
    name: name,
    resolve: {
      alias: {
        "@": resolve("src")
      }
    }
  }
};
