import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Inventory from "../views/Inventory.vue";
import Customers from "../views/Customers.vue";
import Invoice from "../views/Invoice.vue";
import Orders from "../views/Orders.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "HomeRoute",
    component: Inventory,
  },
  {
    path: "/inventory",
    name: "inventory",
    component: Inventory,
  },
  {
    path: "/customers",
    name: "customers",
    component: Customers,
  },
  {
    path: "/invoice/new",
    name: "invoice",
    component: Invoice,
  },
  {
    path: "/orders",
    name: "orders",
    component: Orders,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
