<template>
  <div class="side-menu-container">
    <router-link to="/">
      <img
        src="../assets/images/solar_coffee_logo.png"
        alt="logo-image"
        id="logo"
      />
    </router-link>
    <h1>Menagment portal</h1>
    <solar-button
      id="menuInventory"
      isFullWidth
      @click="goToRoute('/inventory')"
    >
      Inventory
    </solar-button>
    <solar-button
      id="MenageCustomers"
      isFullWidth
      @click="goToRoute('/customers')"
      >Menage Customers</solar-button
    >
    <solar-button
      id="CreateInvoice"
      isFullWidth
      @click="goToRoute('/invoice/new')"
      >Create Invoice</solar-button
    >
    <solar-button id="Orders" isFullWidth @click="goToRoute('/orders')"
      >Orders</solar-button
    >
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";
import SolarButton from "../components/SolarButton.vue";
export default defineComponent({
  name: "SideMenu",
  components: {
    SolarButton,
  },
  setup() {
    const router = useRouter();
    const goToRoute = (route: string) => {
      router.push(route);
    };
    return { goToRoute };
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
#logo {
  width: 100%;
}

h1 {
  font-size: 1.2rem;
  margin: 1rem 0;
}
.side-menu-container {
  background-color: #fcfcfc;
  height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 0.8rem;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 1px;
}
</style>
