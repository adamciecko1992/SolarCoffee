<template>
  <div>
    <h1 id="ordersTitle">Sales Orders</h1>
    <hr />
    <table id="sales-orders" class="table" v-if="orders.length">
      <thead>
        <tr>
          <th>CustomerId</th>
          <th>OrderId</th>
          <th>Order Total</th>
          <th>Order Status</th>
          <th>Mark Complete</th>
        </tr>
        <tr v-for="order in orders" :key="order.id">
          <td>
            {{ order.customer.id }}
          </td>
          <td>
            {{ order.id }}
          </td>
          <td>
            {{ getTotal(order) }}
          </td>
          <td :class="{ green: order.isPaid }">
            {{ getStatus(order.isPaid) }}
          </td>
          <td>
            <solar-button v-if="!order.isPaid" @click="markComplete(order.id)"
              >Paid</solar-button
            >
          </td>
        </tr>
      </thead>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { OrderService } from "../services/order-service";
import { ISalesOrder } from "../types/SalesOrder";
import SolarButton from "../components/SolarButton.vue";
const orderService = new OrderService();

export default defineComponent({
  components: {
    SolarButton,
  },

  setup() {
    const orders = ref<ISalesOrder[]>([]);
    function getTotal(order: ISalesOrder) {
      if (order) {
        return order.lineItems.reduce(
          (a, b) => a + b["product"]["price"] * b["quantity"],
          0
        );
      }
    }

    function getStatus(isPaid: boolean) {
      return isPaid ? "Paid in Full" : "Unpaid";
    }
    async function initialize() {
      orders.value = await orderService.getOrders();
    }

    async function markComplete(orderId: number) {
      await orderService.markOrderComplete(orderId);
      await initialize();
    }

    initialize();

    return {
      getTotal,
      getStatus,
      markComplete,
      orders,
    };
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.green {
  font-weight: bold;
  color: $solar-green;
}
.order-complete {
  cursor: pointer;
  text-align: center;
}
</style>