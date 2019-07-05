
module.exports = {

  productionSourceMap: false,
  devServer: {
    host: '0.0.0.0',
    port: 8082,
    // proxy: {
    //   '/connect/token': {
    //     target: 'http://10.53.28.168:5010/',
    //   },
    //   '/api': {
    //     target: 'http://10.53.28.168:5000/',
    //   }
    // }
  }
};