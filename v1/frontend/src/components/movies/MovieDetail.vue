<template>
  <div>
    <v-header></v-header>
    <section class="cta__container">
      <router-link tag="button" :to="{name: 'movies'}">Show all movies</router-link>
      <h3>{{currentMovie.title}}</h3>
    </section>
    <main class="movies">
      <img :src="currentMovie.image"/>
      <dl>
        <dt>Title</dt>
        <dd>{{currentMovie.title}}</dd>
        <dt>Genre</dt>
        <dd>{{currentMovie.type}}</dd>
        <dt>Year</dt>
        <dd>{{currentMovie.year}}</dd>
      </dl>
      <div>{{currentMovie.description}}</div>
    </main>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';
export default {
  data() {
    return {
      imdbId: this.$route.params.imdbId,
    };
  },
  methods: {
    ...mapActions([
      'getMovie',
    ]),
  },
  computed: {
    ...mapGetters([
      'currentMovie',
    ]),
  },
  name: 'movie-detail',
  mounted() {
    this.getMovie(this.imdbId);
  },
};
</script>

<style lang="scss" scoped>
  @import '../../assets/scss/_variables.scss';

  dt {
    width: calc(30% - 15px);
    padding-right: 15px;
  }

  dd {
    width: 70%;
  }
</style>
