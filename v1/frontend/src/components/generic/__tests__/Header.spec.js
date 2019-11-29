import { createLocalVue, shallowMount } from '@vue/test-utils';
import Vuex from 'vuex';
import VueRouter from 'vue-router';
import Header from '../Header';

const localVue = createLocalVue();
localVue.use(Vuex);
localVue.use(VueRouter);

const router = new VueRouter();

describe('Header.vue', () => {
  let storeOptions;
  let store;

  beforeEach(() => {
    storeOptions = {
      getters: {
        loggedIn: jest.fn(),
      },
    };
    store = new Vuex.Store(storeOptions);
  });

  test('Show link options when not logged in', () => {
    storeOptions.getters.loggedIn.mockReturnValue(false);
    const $loggedIn = false;
    const wrapper = shallowMount(Header, {
      mocks: { $loggedIn },
      localVue,
      store,
    });
    const linkList = wrapper.find('.header-menu');
    expect(linkList.text()).toContain('Sign Up');
    expect(linkList.text()).toContain('Login');
    expect(linkList.text()).not.toContain('Profile');
    expect(linkList.text()).not.toContain('Logout');
  });
  test('Show link options when logged in', () => {
    storeOptions.getters.loggedIn.mockReturnValue(true);
    const $loggedIn = true;
    const wrapper = shallowMount(Header, {
      mocks: { $loggedIn },
      localVue,
      store,
    });
    const linkList = wrapper.find('.header-menu');
    expect(linkList.text()).toContain('Profile');
    expect(linkList.text()).toContain('Logout');
    expect(linkList.text()).not.toContain('Sign Up');
    expect(linkList.text()).not.toContain('Login');
  });
  test('Navigate home when logo is clicked', () => {
    const wrapper = shallowMount(Header, {
      localVue,
      store,
      router,
    });
    wrapper.vm.$router.push({ path: '/blabla' });
    expect(wrapper.vm.$route.path).toBe('/blabla');
    wrapper.find('img.logo').trigger('click');
    expect(wrapper.vm.$route.path).toBe('/');
  });
});
