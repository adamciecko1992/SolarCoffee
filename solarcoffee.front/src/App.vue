<template>
  <div class="app">
    <div class="app-menu">
      <side-menu></side-menu>
    </div>
    <div class="app-content">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import sideMenu from "./components/SideMenu.vue";
export default defineComponent({
  name: "app",
  components: {
    sideMenu: sideMenu,
  },
});
</script>

<style lang="scss">
@import "@/scss/global.scss";
*,
*::before,
*::after {
  box-sizing: border-box;
}
.app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  display: flex;
}
.app-menu {
  position: fixed;
}
.app-content {
  padding: 1.2rem;
  width: 90%;
  margin: 1rem auto 1rem $menu-width;
  text-align: left;
}

* {
  margin: 0;
}
a {
  text-decoration: none;
}
hr {
  border: 1px solid #eee;
  margin: 1rem 0;
}
.table {
  width: 100%;
  max-width: 100%;
  margin-bottom: 2rem;
  border-collapse: collapse;

  tr {
    border-top: 1px solid #eee;
    border-bottom: 1px solid #eee;
    transition: background-color 0.3s;

    &:hover {
      background-color: #f5f5f5;
      transition: background-color 0.3s;
    }
  }

  td {
    padding: 1.2rem;
  }

  th {
    background-color: #fafafa;
    padding: 1.2rem;
    border-bottom: 2px solid $solar-blue;
  }
}
</style>
