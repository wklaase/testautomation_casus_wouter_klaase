<template>
  <div class="login">
    <v-header>Sign Up</v-header>
    <form>
      <section class="login__container">
        <hr>
        <label for="username">Username</label>
        <input type="text"
               id="username"
               class="login__username"
               v-model="userName">
        <label for="password">Password</label>
        <input type="password"
               id="password"
               autocomplete="off"
               class="login__password"
               v-model="password">
        <label for="repeat-password">Repeat Password</label>
        <input type="password"
               id="repeat-password"
               autocomplete="off"
               class="login__password"
               v-model="confirmPassword">
        <button
          :disabled="!canSubmit" class="login__submit"
          @click.prevent="signUpWithCredentials"
          :class="{'login__submit--disabled':!canSubmit}">Sign Up
        </button>
      </section>
    </form>
  </div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  name: 'SignUp',
  data() {
    return {
      userName: '',
      password: '',
      confirmPassword: '',
    };
  },
  computed: {
    canSubmit() {
      return (this.confirmPassword === this.password) &&
        !!this.password &&
        !!this.confirmPassword &&
        (this.userName.length > 3);
    },
  },
  methods: {
    ...mapActions([
      'signUp',
    ]),
    signUpWithCredentials() {
      const formData = {
        username: this.userName,
        password: this.password,
      };
      this.$store.dispatch('signUp', formData).then((signUpSuccess) => {
        if (signUpSuccess) {
          this.$router.push({ name: 'signUpSuccess', params: { userName: this.userName } });
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

  .login__submit--disabled {
    background-color: $secondary-color;
    cursor: default;
  }
</style>
