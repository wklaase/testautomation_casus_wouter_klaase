export default {
  setToken: (state, auth) => {
    state.token = auth.token;
    state.loggedIn = !!auth.token;
    state.userId = auth.userId;
    state.userName = auth.userName;
    state.loginExpires = auth.loginExpires;
  },
  setCurrentMovie: (state, movie) => {
    state.currentMovie = movie;
  },
  setMovies: (state, movies) => {
    state.movies = movies;
  },
  setSearchedMovies: (state, searchedMovies) => {
    state.searchedMovies = searchedMovies;
  },
};
