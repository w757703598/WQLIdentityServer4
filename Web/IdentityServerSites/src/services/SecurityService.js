// import Oidc from 'oidc-client'
// import store from '../store'
// import { Message } from 'element-ui'

// var mgr = new Oidc.UserManager({
//   userStore: new Oidc.WebStorageStateStore(),
//   //authority: 'http://10.53.20.175:8005',
//   authority: 'http://10.53.20.226:5010',
//   //authority: 'http://wangqianlong.qicp.vip:5001/',

//   client_id: 'vuejsclient',
//   redirect_uri: window.location.origin + '/oidc-callback',
//   response_type: 'id_token token',
//   scope: 'openid profile offline_access IdentityServer',
//   post_logout_redirect_uri: window.location.origin,
//   silent_redirect_uri: window.location.origin + '/silent-renew.html',
//   accessTokenExpiringNotificationTime: 2500,
//   automaticSilentRenew: true,
//   filterProtocolClaims: true,
//   loadUserInfo: true,
// })

// mgr.events.addUserLoaded(function(user) {
//   console.log('New User Loaded：', arguments)
//   console.log('Acess_token: ', user.access_token)
// })

// mgr.events.addAccessTokenExpiring(function() {
//   console.log('AccessToken Expiring：', arguments)
// })

// mgr.events.addAccessTokenExpired(function() {
//   console.log('AccessToken Expired：', arguments)
//   Message.error({ showClose: true, message: '授权过期' })
//   mgr
//     .signoutRedirect()
//     .then(function(resp) {
//       console.log('signed out', resp)
//     })
//     .catch(function(err) {
//       console.log(err)
//     })
// })

// mgr.events.addSilentRenewError(function() {
//   console.error('Silent Renew Error：', arguments)
// })

// mgr.events.addUserSignedOut(function() {
//   Message.error({ showClose: true, message: '退出登陆' })
//   console.log('UserSignedOut：', arguments)
//   mgr
//     .signoutRedirect()
//     .then(function(resp) {
//       console.log('signed out', resp)
//     })
//     .catch(function(err) {
//       console.log(err)
//     })
// })

// export default class SecurityService {
//   // Get the user who is logged in
//   getUser() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           console.log('查询用户成功')
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }
//   signinRedirectCallback() {
//     return new Promise((resolve, reject) => {
//       mgr
//         .signinRedirectCallback()
//         .then(function(res) {
//           console.log('登陆回调')
//           return resolve(res)
//         })
//         .catch(function(err) {
//           return reject(err)
//         })
//     })
//   }
//   // Renew the token manually
//   renewToken() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .signinSilent()
//         .then(function(user) {
//           if (user == null) {
//             console.log('无注册用户')
//             self.signIn(null)
//           } else {
//             console.log('刷新token')
//             console.log(user)
//             return resolve(user)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Check if there is any user logged in
//   getSignedIn(returnPath) {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           console.log('检测是否登陆')
//           if (user == null) {
//             console.log(user)
//             self.signIn(returnPath)
//             return resolve(false)
//           } else {
//             return resolve(true)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Redirect of the current window to the authorization endpoint.
//   signIn(returnPath) {
//     returnPath
//       ? mgr.signinRedirect({ state: returnPath })
//       : mgr.signinRedirect()
//   }

//   // Redirect of the current window to the end session endpoint
//   signOut() {
//     mgr
//       .signoutRedirect()
//       .then(function(resp) {
//         console.log('signed out', resp)
//       })
//       .catch(function(err) {
//         console.log(err)
//       })
//   }

//   // Get the profile of the user logged in
//   getProfile() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user.profile)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Get the token id
//   getIdToken() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user.id_token)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Get the session state
//   getSessionState() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user.session_state)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Get the access token of the logged in user
//   getAcessToken() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user.access_token)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Takes the scopes of the logged in user
//   getScopes() {
//     let self = this
//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user.scopes)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }

//   // Get the user roles logged in
//   getRole() {
//     let self = this

//     return new Promise((resolve, reject) => {
//       mgr
//         .getUser()
//         .then(function(user) {
//           console.info(user)
//           if (user == null) {
//             self.signIn()
//             return resolve(null)
//           } else {
//             return resolve(user.profile.role)
//           }
//         })
//         .catch(function(err) {
//           console.log(err)
//           return reject(err)
//         })
//     })
//   }
// }
