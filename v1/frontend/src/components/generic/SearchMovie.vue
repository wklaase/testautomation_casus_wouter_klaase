<template>
  <div class="search-movie">
    <input class="search-movie__input" v-model="inputVal" placeholder="Search for a movie"/>
    <button class="search-movie__button">Search</button>
  </div>
</template>

<script>

export default {
  name: 'search-movie',
  props: ['value'],
  data() {
    return {
      inputVal: this.value,
    };
  },
  watch: {
    inputVal(val) {
      this.$emit('input', val);
    },
  },
};
</script>

<style lang="scss" scoped>
  .search-movie {
    display: flex;
    justify-content: space-between;
    width: 100%;
    padding: 0 15px;
    background-color: rgb(180, 180, 180);
    border-radius: 8px;

    &__input {
      width: calc(100% - 50px);
      height: 58px;
      color: #000;
      border: none;
      outline: none;
      margin-bottom: 0;
      background-color: rgb(180, 180, 180);
      font-size: 20px;
    }

    &__button {
      width: 50px;
      height: 58px;
      color: #000;
      background: url('../../assets/icons/baseline-search-24px.svg') no-repeat center;
      border: none;
      outline: none;
      text-indent: -100000px;
    }
  }
</style>
