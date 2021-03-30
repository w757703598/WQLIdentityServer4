import Vue from 'vue'
import Vuex from 'vuex'
import getters from './getters'
import app from './modules/app'
import settings from './modules/settings'
import user from './modules/user'

import { vuexOidcCreateStoreModule } from 'vuex-oidc'
import { oidcSettings } from '../config/oidc'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    app,
    settings,
    user,
    oidcStore: vuexOidcCreateStoreModule(
      oidcSettings,
      // NOTE: If you do not want to use localStorage for tokens, in stead of just passing oidcSettings, you can
      // spread your oidcSettings and define a userStore of your choice
      // {
      //   ...oidcSettings,
      //   userStore: new WebStorageStateStore({ store: window.sessionStorage })
      // },
      // Optional OIDC store settings
      {
        namespaced: false,
        dispatchEventsOnWindow: true,
      },
      // Optional OIDC event listeners
      {
        userLoaded: (user) => console.log('OIDC user is loaded:', user),
        userUnloaded: () => console.log('OIDC user is unloaded'),
        accessTokenExpiring: () => console.log('Access token will expire'),
        accessTokenExpired: () => console.log('Access token did expire'),
        silentRenewError: () => console.log('OIDC user is unloaded'),
        userSignedOut: () => console.log('OIDC user is signed out'),
        oidcError: (payload) => console.log('OIDC error', payload),
        automaticSilentRenewError: (payload) =>
          console.log('OIDC automaticSilentRenewError', payload),
      }
    )
  },

  getters
})

export default store
