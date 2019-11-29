import { createLocalVue } from '@vue/test-utils';
import Vuex from 'vuex';
import VueRouter from 'vue-router';
// import Movies from '../Movies';
import Header from '../../generic/Header';

const localVue = createLocalVue();
localVue.use(Vuex);
localVue.use(VueRouter);
localVue.component('v-header', Header);

describe('Movies.vue', () => {
  // let storeOptions;
  // let store;
  //
  // beforeEach(() => {
  //   storeOptions = {
  //     getters: {
  //       getMovies: jest.fn(),
  //       loggedIn: jest.fn(),
  //     },
  //   };
  //   store = new Vuex.Store(storeOptions);
  // });
  test('shows listed movie-items', () => {
  });
  test('shows search bar when logged in', () => {
  });
});

