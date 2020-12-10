<template>
  <div class="invoice-step">
    <h2>Step 2: Create Order</h2>
    <div v-if="inventory.length" class="invoice-step-detail">
      <label for="product">Product:</label>
      <select v-model="newItem.product" class="invoiceLineItem" id="product">
        <option disabled value="">Please select a Product</option>
        <option v-for="i in inventory" :value="i.product" :key="i.product.id">
          {{ i.product.name }}
        </option>
      </select>
      <label for="quantity">Quantity:</label>
      <input v-model="newItem.quantity" id="quantity" type="number" min="0" />
    </div>

    <div class="invoice-item-actions">
      <solar-button
        :disabled="!newItem.product || !newItem.quantity"
        @click="addLineItem"
      >
        Add Line Item
      </solar-button>

      <solar-button :disabled="!lineItems.length" @click="addOrder">
        Finalize Order
      </solar-button>
    </div>

    <div class="invoice-order-list" v-if="lineItems.length">
      <div class="runningTotal">
        <h3>Running Total:</h3>
        {{ runningTotal }}
      </div>
      <hr />
      <table class="table">
        <thead>
          <tr>
            <th>Product</th>
            <th>Description</th>
            <th>Qty.</th>
            <th>Price</th>
            <th>Subtotal</th>
          </tr>
        </thead>
        <tr v-for="lineItem in lineItems" :key="`index_${lineItem.product.id}`">
          <td>{{ lineItem.product.name }}</td>
          <td>{{ lineItem.product.description }}</td>
          <td>{{ lineItem.quantity }}</td>
          <td>{{ lineItem.product.price }}</td>
          <td>{{ lineItem.product.price * lineItem.quantity }}</td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, PropType, ref, watch } from "vue";
import InventoryService from "../../services/Inventory-service";
import { ILineItem } from "../../types/Invoice";
import { IProductInventory } from "../../types/Product";
import isValidResponse from "../../helpers/axiosTypeGuard";
import SolarButton from "../../components/SolarButton.vue";

export default defineComponent({
  components: {
    SolarButton,
  },
  props: {
    inventory: {
      required: true,
      type: Array as () => IProductInventory[],
    },
  },
  emits: ["add-order"],
  setup(_, ctx) {
    const lineItems = ref<ILineItem[]>([]);
    const newItem = ref<ILineItem>({
      product: {
        id: 0,
        name: "",
        createdOn: new Date(),
        updatedOn: new Date(),
        description: "",
        price: 0,
        isTaxable: false,
        isArchived: false,
      },
      quantity: 0,
    });

    function addLineItem() {
      const lineItem: ILineItem = {
        product: newItem.value.product,
        quantity: newItem.value.quantity,
      };
      lineItems.value.push(lineItem);
    }

    const runningTotal = computed(() => {
      return lineItems.value.reduce(
        (a, b) => a + b.product.price * b.quantity,
        0
      );
    });
    function addOrder() {
      ctx.emit("add-order", { items: lineItems.value, total: runningTotal });
    }

    return {
      addLineItem,
      newItem,
      lineItems,
      addOrder,
      runningTotal,
    };
  },
});
</script>

<style>
</style>