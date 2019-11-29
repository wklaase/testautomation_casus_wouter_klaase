<template>
  <div class="login">
    <v-header>Successfull Created a user</v-header>
    <section class="cta__container">
      <hr>
      <router-link tag="button" :to="{name: 'login'}">Login</router-link>
      <h2>Created user</h2>
    </section>
    <main class="movies">
      <dl>
        <dt>Name</dt>
        <dd>{{this.userName}}</dd>
      </dl>
    </main>

  </div>
</template>

<script>

export default {
  name: 'Signup-Success',
  data() {
    return {
      userName: this.$route.params.userName,
    };
  },
};
</script>

<style lang="scss" scoped>
  @import '../../assets/scss/_variables.scss';
</style>
