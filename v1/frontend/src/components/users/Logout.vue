<template>
  <div class="logout">
    <v-header></v-header>
    <section class="cta__container">
      <p v-if="loggedIn">Hello, still logged in..</p>
      <p v-if="!loggedIn">You are successfully logged out.</p>
    </section>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex';

export default {
  name: 'Logout',
  computed: {
    ...mapGetters([
      'loggedIn',
    ]),
  },
  methods: {
    ...mapActions([
      'logOut',
    ]),
  },
  mounted() {
    if (this.logOut()) {
      this.$router.push('/');
    }
  },
};
</script>

