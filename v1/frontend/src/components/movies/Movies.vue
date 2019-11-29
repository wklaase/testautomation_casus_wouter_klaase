<template>
  <div class="movies">
    <v-header>All Movies</v-header>
    <section class="cta__container">
      <hr v-if="loggedIn">
      <search-movie class="search-movie" v-model="searchValue" v-if="loggedIn"></search-movie>
    </section>
    <main class="movies">
      <ul>
        <movie-item v-if="!searchValue"
                    v-for="(movie, index) in getMovies"
                    :key="index" :movie="movie"></movie-item>
        <movie-item v-if="searchValue && getSearchedMovies.length > 0"
                    v-for="(searchedMovie, index) in getSearchedMovies"
                    :key="index" :movie="searchedMovie"></movie-item>
      </ul>
      <div v-if="searchValue && getSearchedMovies.length === 0">No search results!</div>
    </main>
  </div>
</template>

<script>
/* eslint-disable func-names */
/* eslint-disable object-shorthand */
import { mapGetters, mapActions } from 'vuex';
import SearchMovie from '../generic/SearchMovie';
import MovieItem from './MovieItem';
export default {
  name: 'Movies',
  data() {
    return {
      movies: [],
      searchValue: '',
      searchResults: [],
    };
  },
  watch: {
    searchValue: function (newValue) {
      this.checkSearch(newValue);
    },
  },
  methods: {
    ...mapActions([
      'searchMovie',
    ]),
    checkSearch(value) {
      this.searchResults = [];
      if (value.length > 1) {
        this.searchMovie(value);
      }
    },
  },
  components: {
    'search-movie': SearchMovie,
    'movie-item': MovieItem,
  },
  computed: {
    ...mapGetters([
      'loggedIn',
      'getMovies',
      'getSearchedMovies',
    ]),
  },
};
</script>

<style lang="scss" scoped>
  @import '../../assets/scss/_variables.scss';

  ul {
    min-width: 100%;
    padding: 0;
    margin-top: 0;
  }

</style>

