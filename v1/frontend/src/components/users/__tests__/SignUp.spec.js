import { createLocalVue, shallowMount } from '@vue/test-utils';
import Vuex from 'vuex';
import VueRouter from 'vue-router';
import SignUp from '../SignUp';
import Header from '../../generic/Header';

const localVue = createLocalVue();
localVue.use(Vuex);
localVue.use(VueRouter);
localVue.component('v-header', Header);

const router = new VueRouter();

describe('SignUp.vue', () => {
  let storeOptions;
  let store;

  beforeEach(() => {
    storeOptions = {
      actions: {
        signUp: jest.fn(),
      },
    };
    store = new Vuex.Store(storeOptions);
  });

  test('when sign up button pressed, a sign up call should be done', async () => {
    // todo
    // expect.assertions(1);
    const $signUp = true;
    const wrapper = shallowMount(SignUp, {
      mocks: { $signUp },
      localVue,
      store,
      router,
    });
    wrapper.find('button').trigger('click');
    // todo expect
  });
  test('a valid username and password should be entered before sign up button is enabled', () => {
  });
  test('when not logged in a user can only be signed up with role user', () => {
  });
  test('When logged in with role user this page is not visible', () => {
  });
});

