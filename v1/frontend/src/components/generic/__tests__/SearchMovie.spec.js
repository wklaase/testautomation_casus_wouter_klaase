import { shallowMount } from '@vue/test-utils';
import SearchMovie from '../SearchMovie';

describe('Item.vue', () => { // #A
  test('value of parent is bound as model', () => {
    const wrapper = shallowMount(SearchMovie, {
      propsData: {
        value: 'Pino',
      },
    });
    expect(wrapper.vm.inputVal).toBe('Pino');
  });
  test('Input value can be set from parent', () => {
    const wrapper = shallowMount(SearchMovie, {
      propsData: {
        value: 'Tommy',
      },
    });
    expect(wrapper.vm.inputVal).toBe('Tommy');
  });
  test('value of parent is bound as model', () => {
    const wrapper = shallowMount(SearchMovie, {
      propsData: {
        value: 'Pino',
      },
    });
    const input = wrapper.find('input');
    expect(input.element.value).toBe('Pino');
    input.setValue('Inimini');
    expect(input.element.value).toBe('Inimini');
  });
});
