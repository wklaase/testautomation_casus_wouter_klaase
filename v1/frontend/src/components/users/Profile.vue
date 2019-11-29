<template>
  <div class="user-profile">
    <v-header>User Profile</v-header>
    <section class="cta__container">
      <router-link tag="button" :to="{name: 'movies'}">Show all movies</router-link>
      <h2>Profile settings of: {{currentUser.name}}</h2>
    </section>
    <main class="movies">
      <dl>
        <dt>Name</dt>
        <dd>{{currentUser.name}}</dd>
        <dt>ID</dt>
        <dd>{{currentUser.id}}</dd>
        <dt>Is Logged in</dt>
        <dd>{{currentUser.isLoggedIn}}</dd>
        <dt>Session Expires</dt>
        <dd>{{currentUser.sessionExpires}}</dd>
      </dl>
    </main>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name: 'UserProfile',
  computed: {
    ...mapGetters([
      'currentUser',
    ]),
  },
};
</script>

<style lang="scss" scoped>
  @import '../../assets/scss/_variables.scss';
</style>
