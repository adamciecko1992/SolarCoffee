<template>
  <div class="invoice-step">
    <h1 id="invoiceTitle">Create Invoice</h1>
    <hr />
    <h2>Step 1: Select Customer</h2>
    <div v-if="customers.length" class="invoice-step-detail">
      <label for="customer">Customer:</label>
      <select
        v-model="selectedCustomerId"
        class="invoice-customers"
        id="customer"
      >
        <option disabled value="">Please select a Customer</option>
        <option v-for="c in customers" :value="c.id" :key="c.id">
          {{ c.firstName + " " + c.lastName }}
        </option>
      </select>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, watch } from "vue";
import { ICustomer } from "../../types/Customer";
export default defineComponent({
  props: {
    customers: {
      type: Array as () => ICustomer[],
      required: true,
    },
  },
  emits: ["customer-selected"],
  setup(_, ctx) {
    const selectedCustomerId = ref(NaN);
    watch(selectedCustomerId, () => {
      ctx.emit("customer-selected", selectedCustomerId.value);
    });
    return {
      selectedCustomerId,
    };
  },
});
</script>

<style lang="scss">
</style>