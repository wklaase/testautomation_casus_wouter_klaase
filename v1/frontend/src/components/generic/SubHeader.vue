<template>
  <div class="sub-header">
    <section class="cta__container jumbotron text-center">
      <hr>
      <router-link tag="button" :to="'movies'">List all movies</router-link>
    </section>
  </div>
</template>

<script>
export default {
  computed: {
  },
  methods: {
  },
  name: 'search-movie',
};
</script>

<style lang="scss" scoped>
  section {
    width: 100%;
  }
</style>
