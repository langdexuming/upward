<template>
  <div id="app">
    <div class="text-right">
      <button type="button" class="btn btn-lg text-right" v-on:click="goToHomePage">
        <span class="glyphicon glyphicon-home" aria-hidden="true"></span>
      </button>
    </div>
    <router-view />
  </div>
</template>

<script>
export default {
  name: "app",
  methods: {
    goToHomePage() {
      this.$router.go(-1);
    }
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 0px;
}

body {
  background: #fff;
}
</style>
