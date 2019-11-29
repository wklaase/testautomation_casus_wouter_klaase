import getters from '../getters';

describe('getters', () => {
  test('token returns the same value form state', () => {
    const token = 'eenhelelangestring';
    const state = { token };
    const result = getters.token(state);
    expect(result).toEqual(token);
  });
});
