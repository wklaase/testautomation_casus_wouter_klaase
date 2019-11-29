<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App',
  created() {
    const autoLoggedIn = this.$store.dispatch('tryAutoLogin');

    autoLoggedIn.then((loggedIn) => {
      if (loggedIn) {
        this.$router.push({ name: 'movies' });
      }
    });

    this.$store.dispatch('getMovies');
  },
};
</script>

<style lang="scss">
  @import './assets/scss/styles.scss';
</style>
