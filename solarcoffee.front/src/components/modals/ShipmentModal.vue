<template>
  <solar-modal>
    <template #header> Receive Shipment </template>
    <template #body>
      <label for="product">Product Received:</label>
      <select v-model="selectedProduct" class="shipmentItems" id="product">
        <option disabled value="">Please select one</option>
        <option v-for="item in inventory" :value="item" :key="item.product.id">
          {{ item.product.name }}
        </option>
      </select>
      <label for="qtyRecived">Quantity Received:</label>
      <input type="number" id="qtyRecived" v-model.number="qtyRecived" />
    </template>
    <template #footer>
      <solar-button type="button" @click="save" aria-label="save new shipment">
        Save Received Shipment
      </solar-button>
      <solar-button type="button" @btn-clicked="close" aria-label="Close modal">
        Close
      </solar-button>
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import solarModal from "./SolarModal.vue";
import solarButton from "../SolarButton.vue";
import { IProductInventory, IProduct } from "@/types/Product";
// import { IShipment } from "@/types/Shipment";

export default defineComponent({
  props: {
    inventory: Array as () => IProductInventory[],
  },
  components: {
    solarModal,
    solarButton,
  },
  emits: ["save-shipment", "close"],
  setup(props, ctx) {
    const qtyRecived = ref(0);
    const selectedProduct = ref<IProduct>({
      createdOn: new Date(),
      updatedOn: new Date(),
      id: 0,
      description: "",
      isTaxable: false,
      name: "",
      price: 0,
      isArchived: false,
    });

    const close = () => {
      ctx.emit("close");
    };
    const save = () => {
      const shipment = {
        idProduct: selectedProduct.value.id,
        productAdjustment: qtyRecived.value,
      };
      console.log(shipment);
      ctx.emit("save-shipment", shipment);
    };
    return { selectedProduct, qtyRecived: qtyRecived, close, save };
  },
});
</script>

<style lang="scss">
</style>