<template>
  <div class="inventory-container">
    <h1 id="inventoryTitile">Inventory Dashboard</h1>
    <hr />
    <div class="inventory-actions">
      <solar-button @click="showInventoryItems">Inventory Items</solar-button>
      <solar-button @click="showShipment">Shipment</solar-button>
    </div>
    <table id="InventoryTable" class="table">
      <tr>
        <td>Item</td>
        <td>Quantity</td>
        <td>Unit Price</td>
        <td>Taxable</td>
        <td>Delete</td>
      </tr>
      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td :class="`${getColors(item.quantityOnHand, item.idealQuantity)}`">
          {{ item.quantityOnHand }}
        </td>
        <td>{{ parsePrice(item.product.price) }}</td>
        <td>
          <span v-if="item.product.isTaxable">Yes</span>
          <span v-else>No</span>
        </td>
        <td>
          <solar-button isFullWidth @click="archiveProduct(item.product.id)"
            >Delete</solar-button
          >
        </td>
      </tr>
    </table>

    <new-product-modal
      v-if="newProductShown"
      @close="closeModals"
      @save-product="saveNewProduct"
    ></new-product-modal>
    <shipment-modal
      v-if="shipmentShown"
      :inventory="inventory"
      @close="closeModals"
      @save-shipment="saveShipment"
    ></shipment-modal>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { IProduct, IProductInventory } from "../types/Product";
import SolarButton from "../components/SolarButton.vue";
import newProductModal from "../components/modals/NewProductModal.vue";
import shipmentModal from "../components/modals/ShipmentModal.vue";
import { IShipment } from "../types/Shipment";
import InventoryService from "../services/Inventory-service";
import ProductService from "../services/product-service";

export default defineComponent({
  name: "Inventory",
  components: {
    SolarButton,
    newProductModal,
    shipmentModal,
  },

  setup() {
    const newProductShown = ref(false);
    const shipmentShown = ref(false);
    const myInventoryService = new InventoryService();
    const myProductService = new ProductService();
    const inventory = ref<IProductInventory[]>([]);

    const fetchData = async () => {
      const response = await myInventoryService.getInventory();
      inventory.value.length = 0;
      response.forEach((invItem: IProductInventory) => {
        inventory.value.push(invItem);
      });
    };

    const parsePrice = (price: number) => {
      return price.toString() + "$";
    };
    const showInventoryItems = (): void => {
      newProductShown.value = !newProductShown.value;
    };

    const showShipment = (): void => {
      shipmentShown.value = !shipmentShown.value;
    };

    const closeModals = (): void => {
      newProductShown.value = false;
      shipmentShown.value = false;
    };

    const saveShipment = async (shipment: IShipment) => {
      await myInventoryService.updateInventoryQuantity(shipment);
      showShipment();
      await fetchData();
    };

    const archiveProduct = async (id: number) => {
      await myProductService.archiveProduct(id);
      await fetchData();
    };

    const saveNewProduct = async (product: IProduct) => {
      myProductService.saveNewProduct(product);
      newProductShown.value = false;
    };

    const getColors = (
      quantityOnHand: number,
      idealQuantity: number
    ): string => {
      if (quantityOnHand <= 0) {
        return "red";
      } else if (Math.abs(idealQuantity - quantityOnHand) < 5) {
        return "yellow";
      }
      return "green";
    };
    fetchData();

    return {
      inventory,
      parsePrice,
      showInventoryItems,
      newProductShown,
      shipmentShown,
      showShipment,
      closeModals,
      saveNewProduct,
      saveShipment,
      archiveProduct,
      getColors,
    };
  },
});
</script>

<style lang="scss" scoped>
@import "../scss/global.scss";

.green {
  font-weight: bold;
  color: $solar-green;
}
.yellow {
  font-weight: bold;
  color: $solar-yellow;
}
.red {
  font-weight: bold;
  color: $solar-red;
}
.inventory-actions {
  display: flex;
  margin-bottom: 0.8rem;
}
.product-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>