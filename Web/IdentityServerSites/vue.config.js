module.exports = {
  productionSourceMap: false,

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
        target: 'https://10.53.20.226:5011/',
      },
      '/api': {
        target: 'https://10.53.20.226:5011/',
      },
    },
  },
}
