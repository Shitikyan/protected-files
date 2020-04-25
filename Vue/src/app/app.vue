<template>
  <div id="app">
    <b-navbar type="dark" variant="dark" class="pl-0 pr-0">
      <b-navbar-nav class="container justify-content-start">
        <router-link :to="{ name: 'Home' }" class="nav-link pl-0">Home</router-link>
        <router-link :to="{ name: 'Upload', query: { itemId: 1 }}" class="nav-link">Upload</router-link>
      </b-navbar-nav>
    </b-navbar>
    <router-view class="container p-0"/>
  </div>
</template>

<style lang="scss" src="./app.scss">
</style>

<script src="./app.js">
</script>
