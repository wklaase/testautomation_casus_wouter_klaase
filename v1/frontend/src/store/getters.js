export default {
  token: state => state.token,
  loggedIn: state => state.loggedIn,
  currentMovie: state => state.currentMovie,
  /* eslint-disable */
  currentUser: (state) => {
    return {
      id: state.userId,
      name: state.userName,
      isLoggedIn: state.loggedIn,
      sessionExpires: state.loginExpires,
    };
  },
  getMovies: (state) => {
    return state.movies;
  },
  getSearchedMovies: (state) => {
    return state.searchedMovies;
  },
  /* eslint-enable */
};
