<template>
  <div class="login">
    <v-header>Login Failed</v-header>
    <section class="cta__container">
      <hr>
      <router-link tag="button" :to="{name: 'login'}">Retry</router-link>
      <h2>Login Failed</h2>
    </section>
  </div>
</template>

<script>

export default {
  name: 'Login-Failed',
  data() {
    return {
    };
  },
};
</script>

<style lang="scss" scoped>
  @import '../../assets/scss/_variables.scss';
</style>
