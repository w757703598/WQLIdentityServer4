const webpack = require('webpack')
const path = require('path')
function resolve(dir) {
  return path.join(__dirname, dir)
}

module.exports = {
  productionSourceMap: false,
  configureWebpack: {
    // provide the app's title in webpack's name field, so that
    // it can be accessed in index.html to inject the correct title.
    name: 'IdentityServer4 Manager',
    resolve: {
      alias: {
        '@': resolve('src'),
      },
    },
    externals: {
      AMap: 'AMap',
      AMapUI: 'AMapUI',
    },
    plugins: [
      new webpack.ProvidePlugin({
        $: 'jquery',
        jQuery: 'jquery',
        'windows.jQuery': 'jquery',
      }),
    ],
  },
  devServer: {
    host: '0.0.0.0',
    port: 8082,
    proxy: {
      // '/connect/token': {
      //   target: 'http://10.53.20.175:8005/',
      // },
      // '/api': {
      //   target: 'http://10.53.20.175:8005/',
      // },

      '/connect/token': {
        target: 'http://10.53.20.226:5010/',
      },
      '/api': {
        target: 'http://10.53.20.226:5010/',
      },
    },
  },
}
