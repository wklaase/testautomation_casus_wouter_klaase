import Vue from 'vue';
import Vuex from 'vuex';
import mutations from './mutations';
import actions from './actions';
import getters from './getters';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    token: '',
    userId: null,
    userName: null,
    proxyUrl: process.env.ROOT_PROXY_API,
    loginUri: '/v1/proxy/tokens/',
    usersUri: '/v1/proxy/users/',
    moviesUri: '/v1/proxy/movies/',
    loggedIn: false,
    loginExpires: null,
    currentMovie: {},
    movies: [],
    searchedMovies: [],
  },
  mutations,
  actions,
  getters,
});
