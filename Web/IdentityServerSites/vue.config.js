module.exports = {
  productionSourceMap: false,

  devServer: {
    host: '0.0.0.0',
    port: 8082,
    proxy: {
      '/connect/token': {
        target: 'http://10.53.20.175:8005/',
      },
      '/api': {
        target: 'http://10.53.20.175:8005/',
      },

      // '/connect/token': {
      //   target: 'http://10.53.20.226:5010/',
      // },
      // '/api': {
      //   target: 'http://10.53.20.226:5010/',
      // },
    },
  },
}
