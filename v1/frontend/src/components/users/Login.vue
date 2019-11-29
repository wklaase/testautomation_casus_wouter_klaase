<template>
  <div class="login">
    <v-header>Login</v-header>
    <form>
      <section class="cta__container">
        <hr>
        <label for="username">Username</label>
        <input
          type="text"
          id="username"
          class="login__username"
          v-model="userName">
        <label for="username">Password</label>
        <input
          type="password"
          autocomplete="off"
          id="password"
          class="login__password"
          v-model="password">
        <button class="login__submit" @click.prevent="login">Submit</button>
      </section>
    </form>
  </div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  name: 'Login',
  data() {
    return {
      msg: 'Welcome to Your Vue.js App',
      loginUrl: 'http://localhost:8080/v1/proxy/tokens/',
      userName: '',
      password: '',
    };
  },
  methods: {
    ...mapActions([
      'loginWithCredentials',
    ]),
    login() {
      const formData = {
        username: this.userName,
        password: this.password,
      };
      this.$store.dispatch('loginWithCredentials', formData).then((loggedIn) => {
        if (loggedIn) {
          this.$router.push({ name: 'movies' });
        } else {
          this.$router.push({ name: 'loginFailed' });
        }
      });
    },
  },
};
</script>

<style lang="scss" scoped>
  @import '../../assets/scss/_variables.scss';
</style>
